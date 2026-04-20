using System;
using System.IO;
using System.Reflection;

using UnityEditor;
using UnityEditor.Experimental;
using UnityEditorInternal;
using UnityEngine;

namespace UnityPywal.Editor
{
    [InitializeOnLoad]
    internal static class PywalSyncService
    {
        private static readonly object watcherLock = new();
        private static readonly PywalReloadScheduler scheduler = new(TimeSpan.FromMilliseconds(400));
        private static readonly TimeSpan palettePollInterval = TimeSpan.FromSeconds(1);

        private static FileSystemWatcher watcher;
        private static bool initialized;
        private static DateTime nextPalettePollUtc;
        private static string observedPalettePath = string.Empty;
        private static DateTime observedPaletteWriteUtc = DateTime.MinValue;
        private static long observedPaletteLength = -1L;
        private static bool observedPaletteExists;

        static PywalSyncService()
        {
            EditorApplication.update += OnEditorUpdate;
            EditorApplication.delayCall += Initialize;
            EditorApplication.focusChanged += OnEditorFocusChanged;
            AssemblyReloadEvents.beforeAssemblyReload += DisposeWatcher;
            EditorApplication.quitting += DisposeWatcher;
        }

        public static void Initialize()
        {
            if (initialized)
            {
                return;
            }

            initialized = true;
            ReconfigureWatcher();

            if (!PywalSettings.Enabled)
            {
                ApplyDisabledTheme("Unity Pywal is disabled.");
                return;
            }

            if (PywalSettings.ApplyOnStartup)
            {
                ApplyConfiguredTheme("startup", false, true);
            }
            else
            {
                PywalSettings.SetStatus(PywalStatusKind.Info, "Unity Pywal is enabled and waiting for a manual apply.");
            }
        }

        public static void HandleSettingsChanged()
        {
            Initialize();
            ReconfigureWatcher();

            if (!PywalSettings.Enabled)
            {
                ApplyDisabledTheme("Unity Pywal is disabled.");
                return;
            }

            ApplyConfiguredTheme("settings changed", false, false);
        }

        public static bool ApplyNow()
        {
            Initialize();
            return ApplyConfiguredTheme("manual apply", true, false);
        }

        private static void OnEditorUpdate()
        {
            if (!initialized)
            {
                return;
            }

            PollPaletteChanges();

            string reason = string.Empty;
            lock (watcherLock)
            {
                if (!scheduler.TryConsume(DateTime.UtcNow, out reason))
                {
                    return;
                }
            }

            ApplyConfiguredTheme(reason, false, false);
        }

        private static void QueueReload(string reason)
        {
            lock (watcherLock)
            {
                scheduler.RequestReload(DateTime.UtcNow, reason);
            }
        }

        private static void OnEditorFocusChanged(bool focused)
        {
            if (!focused || !initialized || !PywalSettings.Enabled || !PywalSettings.WatchForChanges)
            {
                return;
            }

            nextPalettePollUtc = DateTime.MinValue;
            QueueReload("editor focus regained");
        }

        private static bool ApplyConfiguredTheme(string reason, bool forceApply, bool allowChecksumSkip)
        {
            if (!PywalSettings.Enabled)
            {
                ApplyDisabledTheme("Unity Pywal is disabled.");
                return false;
            }

            if (!PywalPaletteLoader.TryLoad(PywalSettings.PalettePath, out PywalPalette palette, out string error))
            {
                return ApplyFailure(error);
            }

            PywalThemeTokens darkTokens = PywalThemeTokens.Create(palette, UnitySkinVariant.Dark);
            PywalThemeTokens lightTokens = PywalThemeTokens.Create(palette, UnitySkinVariant.Light);
            PywalThemeTokens activeTokens = EditorGUIUtility.isProSkin ? darkTokens : lightTokens;

            PywalStatusSnapshot snapshot = PywalSettings.GetStatus();
            bool checksumMatches = string.Equals(snapshot.LastAppliedChecksum, palette.ContentHash, StringComparison.Ordinal);
            bool filesExist = PywalThemeWriter.GeneratedFilesExist();

            if (!forceApply && allowChecksumSkip && checksumMatches && filesExist)
            {
                PywalSelectionColorApplier.Queue(activeTokens.SelectionColor);
                InternalEditorUtility.RepaintAllViews();
                PywalSettings.SetStatus(PywalStatusKind.Info, $"Unity Pywal is up to date ({reason}).");
                return true;
            }

            bool wroteFiles;
            try
            {
                wroteFiles = PywalThemeWriter.WriteThemes(palette, darkTokens, lightTokens);
            }
            catch (Exception exception)
            {
                return ApplyFailure($"Failed to write Unity Pywal theme files: {exception.Message}");
            }

            if (wroteFiles)
            {
                ImportGeneratedThemes();
            }

            CapturePaletteObservation();
            PywalEditorStyleRefresher.Refresh();
            PywalSelectionColorApplier.Queue(activeTokens.SelectionColor);

            PywalSettings.SetAppliedState(palette.ContentHash, DateTime.UtcNow);
            PywalSettings.SetStatus(PywalStatusKind.Info, $"Applied pywal palette ({reason}).");
            return true;
        }

        private static bool ApplyFailure(string error)
        {
            bool wroteFiles = PywalThemeWriter.WriteNoOpThemes(error);
            if (wroteFiles)
            {
                ImportGeneratedThemes();
            }

            CapturePaletteObservation();
            PywalEditorStyleRefresher.Refresh();
            PywalSettings.ClearAppliedState();
            PywalSettings.SetStatus(PywalStatusKind.Error, error);
            Debug.LogWarning($"[Unity Pywal] {error}");
            return false;
        }

        private static void ApplyDisabledTheme(string reason)
        {
            bool wroteFiles = PywalThemeWriter.WriteNoOpThemes(reason);
            if (wroteFiles)
            {
                ImportGeneratedThemes();
            }

            CapturePaletteObservation();
            PywalEditorStyleRefresher.Refresh();
            PywalSettings.ClearAppliedState();
            PywalSettings.SetStatus(PywalStatusKind.Info, reason);
        }

        private static void ReconfigureWatcher()
        {
            DisposeWatcher();
            CapturePaletteObservation();
            nextPalettePollUtc = DateTime.MinValue;

            if (!PywalSettings.Enabled || !PywalSettings.WatchForChanges)
            {
                return;
            }

            string palettePath = PywalSettings.ResolvedPalettePath;
            string directory = Path.GetDirectoryName(palettePath);
            string fileName = Path.GetFileName(palettePath);

            if (string.IsNullOrWhiteSpace(directory) || string.IsNullOrWhiteSpace(fileName) || !Directory.Exists(directory))
            {
                PywalSettings.SetStatus(PywalStatusKind.Warning, $"Watching is enabled, but the palette directory does not exist yet: {directory}");
                return;
            }

            watcher = new FileSystemWatcher(directory, fileName)
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.CreationTime,
                IncludeSubdirectories = false,
                EnableRaisingEvents = true
            };

            watcher.Changed += OnPaletteChanged;
            watcher.Created += OnPaletteChanged;
            watcher.Deleted += OnPaletteChanged;
            watcher.Renamed += OnPaletteRenamed;
        }

        private static void DisposeWatcher()
        {
            lock (watcherLock)
            {
                scheduler.Reset();

                if (watcher == null)
                {
                    return;
                }

                watcher.EnableRaisingEvents = false;
                watcher.Changed -= OnPaletteChanged;
                watcher.Created -= OnPaletteChanged;
                watcher.Deleted -= OnPaletteChanged;
                watcher.Renamed -= OnPaletteRenamed;
                watcher.Dispose();
                watcher = null;
            }
        }

        private static void PollPaletteChanges()
        {
            if (!PywalSettings.Enabled || !PywalSettings.WatchForChanges)
            {
                return;
            }

            DateTime utcNow = DateTime.UtcNow;
            if (utcNow < nextPalettePollUtc)
            {
                return;
            }

            nextPalettePollUtc = utcNow + palettePollInterval;

            string palettePath = PywalSettings.ResolvedPalettePath;
            bool exists = File.Exists(palettePath);
            DateTime writeUtc = exists ? File.GetLastWriteTimeUtc(palettePath) : DateTime.MinValue;
            long length = exists ? new FileInfo(palettePath).Length : -1L;

            bool pathChanged = !string.Equals(observedPalettePath, palettePath, StringComparison.Ordinal);
            bool existenceChanged = observedPaletteExists != exists;
            bool writeChanged = observedPaletteWriteUtc != writeUtc;
            bool lengthChanged = observedPaletteLength != length;

            if (pathChanged || existenceChanged || writeChanged || lengthChanged)
            {
                observedPalettePath = palettePath;
                observedPaletteExists = exists;
                observedPaletteWriteUtc = writeUtc;
                observedPaletteLength = length;
                QueueReload(exists ? "palette polled change" : "palette removed");
            }
        }

        private static void OnPaletteChanged(object sender, FileSystemEventArgs args)
        {
            if (MatchesConfiguredPalette(args.FullPath))
            {
                QueueReload($"palette {args.ChangeType.ToString().ToLowerInvariant()}");
            }
        }

        private static void OnPaletteRenamed(object sender, RenamedEventArgs args)
        {
            if (MatchesConfiguredPalette(args.FullPath) || MatchesConfiguredPalette(args.OldFullPath))
            {
                QueueReload("palette renamed");
            }
        }

        private static bool MatchesConfiguredPalette(string fullPath)
        {
            if (string.IsNullOrWhiteSpace(fullPath))
            {
                return false;
            }

            return string.Equals(
                Path.GetFullPath(fullPath),
                PywalSettings.ResolvedPalettePath,
                StringComparison.Ordinal);
        }

        private static void CapturePaletteObservation()
        {
            string palettePath = PywalSettings.ResolvedPalettePath;
            observedPalettePath = palettePath;
            observedPaletteExists = File.Exists(palettePath);
            observedPaletteWriteUtc = observedPaletteExists ? File.GetLastWriteTimeUtc(palettePath) : DateTime.MinValue;
            observedPaletteLength = observedPaletteExists ? new FileInfo(palettePath).Length : -1L;
        }

        private static void ImportGeneratedThemes()
        {
            AssetDatabase.ImportAsset(PywalThemeWriter.DarkFilePath, ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);
            AssetDatabase.ImportAsset(PywalThemeWriter.LightFilePath, ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);
        }
    }

    internal static class PywalEditorStyleRefresher
    {
        private static readonly MethodInfo DeleteStyleCatalogCacheMethod =
            typeof(EditorResources).GetMethod("DeleteStyleCatalogCache", BindingFlags.Static | BindingFlags.NonPublic);

        private static readonly MethodInfo RefreshSkinMethod =
            typeof(EditorResources).GetMethod("RefreshSkin", BindingFlags.Static | BindingFlags.NonPublic);

        private static readonly MethodInfo SkinChangedMethod =
            typeof(EditorGUIUtility).GetMethod("SkinChanged", BindingFlags.Static | BindingFlags.NonPublic);

        public static void Refresh()
        {
            Invoke(DeleteStyleCatalogCacheMethod, "DeleteStyleCatalogCache");
            Invoke(RefreshSkinMethod, "RefreshSkin");
            Invoke(SkinChangedMethod, "SkinChanged");
            InternalEditorUtility.RepaintAllViews();
        }

        private static void Invoke(MethodInfo method, string methodName)
        {
            if (method == null)
            {
                return;
            }

            try
            {
                method.Invoke(null, null);
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"[Unity Pywal] Failed to invoke {methodName}: {exception.Message}");
            }
        }
    }
}
