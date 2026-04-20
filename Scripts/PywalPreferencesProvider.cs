using System;
using System.Collections.Generic;
using System.IO;

using UnityEditor;
using UnityEngine;

namespace UnityPywal.Editor
{
    internal static class PywalPreferencesProvider
    {
        [SettingsProvider]
        private static SettingsProvider CreateProvider()
        {
            return new SettingsProvider("Preferences/Unity Pywal", SettingsScope.User)
            {
                guiHandler = _ => DrawPreferences(),
                keywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "pywal",
                    "wal",
                    "theme",
                    "editor",
                    "palette"
                }
            };
        }

        private static void DrawPreferences()
        {
            EditorGUILayout.LabelField("Unity Pywal", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "Unity Pywal preserves Unity's stock visual structure and only remaps editor colors from a pywal palette.",
                MessageType.Info);

            EditorGUI.BeginChangeCheck();

            bool enabled = EditorGUILayout.ToggleLeft("Enable pywal syncing", PywalSettings.Enabled);
            string palettePath = EditorGUILayout.TextField("Palette Path", PywalSettings.PalettePath);
            bool watchForChanges = EditorGUILayout.ToggleLeft("Watch for palette changes", PywalSettings.WatchForChanges);
            bool applyOnStartup = EditorGUILayout.ToggleLeft("Apply on startup", PywalSettings.ApplyOnStartup);

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(EditorGUIUtility.labelWidth);
                if (GUILayout.Button("Reset to default", GUILayout.Width(120f)))
                {
                    palettePath = PywalSettings.DefaultPalettePathValue;
                }

                if (GUILayout.Button("Browse...", GUILayout.Width(90f)))
                {
                    string startingDirectory = Path.GetDirectoryName(PywalSettings.ResolvedPalettePath) ?? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string selected = EditorUtility.OpenFilePanel("Choose pywal palette", startingDirectory, "json");
                    if (!string.IsNullOrWhiteSpace(selected))
                    {
                        palettePath = selected;
                    }
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                PywalSettings.Enabled = enabled;
                PywalSettings.PalettePath = palettePath;
                PywalSettings.WatchForChanges = watchForChanges;
                PywalSettings.ApplyOnStartup = applyOnStartup;
                PywalSyncService.HandleSettingsChanged();
            }

            EditorGUILayout.Space(8f);
            EditorGUILayout.LabelField("Resolved Palette", PywalSettings.ResolvedPalettePath);

            PywalStatusSnapshot status = PywalSettings.GetStatus();
            EditorGUILayout.HelpBox(status.Message, status.MessageType);

            if (!string.IsNullOrWhiteSpace(status.LastAppliedChecksum))
            {
                EditorGUILayout.LabelField("Last Checksum", status.LastAppliedChecksum);
            }

            if (status.LastAppliedAtUtc.HasValue)
            {
                EditorGUILayout.LabelField("Last Applied", status.LastAppliedAtUtc.Value.ToLocalTime().ToString("u"));
            }

            using (new EditorGUI.DisabledScope(!PywalSettings.Enabled))
            {
                if (GUILayout.Button("Apply Now", GUILayout.Height(28f)))
                {
                    PywalSyncService.ApplyNow();
                }
            }
        }
    }
}
