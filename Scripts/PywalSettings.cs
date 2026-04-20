using System;
using System.IO;

using UnityEditor;

namespace UnityPywal.Editor
{
    internal enum PywalStatusKind
    {
        Info,
        Warning,
        Error
    }

    internal readonly struct PywalStatusSnapshot
    {
        public PywalStatusSnapshot(PywalStatusKind kind, string message, string checksum, DateTime? appliedAtUtc)
        {
            Kind = kind;
            Message = message;
            LastAppliedChecksum = checksum;
            LastAppliedAtUtc = appliedAtUtc;
        }

        public PywalStatusKind Kind { get; }
        public string Message { get; }
        public string LastAppliedChecksum { get; }
        public DateTime? LastAppliedAtUtc { get; }

        public MessageType MessageType => Kind switch
        {
            PywalStatusKind.Warning => MessageType.Warning,
            PywalStatusKind.Error => MessageType.Error,
            _ => MessageType.Info
        };
    }

    internal static class PywalSettings
    {
        private const string Prefix = "UnityPywal.";
        private const string EnabledKey = Prefix + "Enabled";
        private const string PalettePathKey = Prefix + "PalettePath";
        private const string WatchKey = Prefix + "WatchForChanges";
        private const string StartupKey = Prefix + "ApplyOnStartup";
        private const string StatusKindKey = Prefix + "StatusKind";
        private const string StatusMessageKey = Prefix + "StatusMessage";
        private const string LastChecksumKey = Prefix + "LastAppliedChecksum";
        private const string LastAppliedTicksKey = Prefix + "LastAppliedTicksUtc";

        private const string DefaultPalettePath = "~/.cache/wal/colors.json";
        private const string DefaultStatusMessage = "Waiting for the pywal palette.";

        public static bool Enabled
        {
            get => EditorPrefs.GetBool(EnabledKey, true);
            set => EditorPrefs.SetBool(EnabledKey, value);
        }

        public static string PalettePath
        {
            get => EditorPrefs.GetString(PalettePathKey, DefaultPalettePath);
            set => EditorPrefs.SetString(PalettePathKey, string.IsNullOrWhiteSpace(value) ? DefaultPalettePath : value.Trim());
        }

        public static bool WatchForChanges
        {
            get => EditorPrefs.GetBool(WatchKey, true);
            set => EditorPrefs.SetBool(WatchKey, value);
        }

        public static bool ApplyOnStartup
        {
            get => EditorPrefs.GetBool(StartupKey, true);
            set => EditorPrefs.SetBool(StartupKey, value);
        }

        public static string ResolvedPalettePath => ResolvePalettePath(PalettePath);
        public static string DefaultPalettePathValue => DefaultPalettePath;

        public static string ResolvePalettePath(string rawPath)
        {
            string path = string.IsNullOrWhiteSpace(rawPath) ? DefaultPalettePath : rawPath.Trim();
            if (path.StartsWith("~", StringComparison.Ordinal))
            {
                string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                path = Path.Combine(home, path.Substring(1).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            }

            return Path.GetFullPath(path);
        }

        public static void SetStatus(PywalStatusKind kind, string message)
        {
            EditorPrefs.SetInt(StatusKindKey, (int)kind);
            EditorPrefs.SetString(StatusMessageKey, string.IsNullOrWhiteSpace(message) ? DefaultStatusMessage : message);
        }

        public static PywalStatusSnapshot GetStatus()
        {
            PywalStatusKind kind = (PywalStatusKind)EditorPrefs.GetInt(StatusKindKey, (int)PywalStatusKind.Info);
            string message = EditorPrefs.GetString(StatusMessageKey, DefaultStatusMessage);
            string checksum = EditorPrefs.GetString(LastChecksumKey, string.Empty);
            string ticksText = EditorPrefs.GetString(LastAppliedTicksKey, string.Empty);
            long ticks = 0L;
            _ = long.TryParse(ticksText, out ticks);
            DateTime? appliedAt = ticks > 0 ? new DateTime(ticks, DateTimeKind.Utc) : null;
            return new PywalStatusSnapshot(kind, message, checksum, appliedAt);
        }

        public static void ClearAppliedState()
        {
            EditorPrefs.DeleteKey(LastChecksumKey);
            EditorPrefs.DeleteKey(LastAppliedTicksKey);
        }

        public static void SetAppliedState(string checksum, DateTime appliedAtUtc)
        {
            EditorPrefs.SetString(LastChecksumKey, checksum ?? string.Empty);
            EditorPrefs.SetString(LastAppliedTicksKey, appliedAtUtc.Ticks.ToString());
        }
    }
}
