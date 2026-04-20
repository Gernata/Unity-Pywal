using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using UnityEditor;

namespace UnityPywal.Editor
{
    internal static class PywalThemeWriter
    {
        public const string ExtensionsDirectory = "Assets/Editor/StyleSheets/Extensions";
        public const string DarkFileName = "Dark.uss";
        public const string LightFileName = "Light.uss";

        public static string DarkFilePath => $"{ExtensionsDirectory}/{DarkFileName}";
        public static string LightFilePath => $"{ExtensionsDirectory}/{LightFileName}";

        public static bool GeneratedFilesExist()
        {
            return File.Exists(DarkFilePath) && File.Exists(LightFilePath);
        }

        public static bool WriteThemes(PywalPalette palette, PywalThemeTokens darkTokens, PywalThemeTokens lightTokens)
        {
            Directory.CreateDirectory(ExtensionsDirectory);

            string darkStyleSheet = BuildStyleSheet(palette, darkTokens);
            string lightStyleSheet = BuildStyleSheet(palette, lightTokens);

            bool darkChanged = WriteIfDifferent(DarkFilePath, darkStyleSheet);
            bool lightChanged = WriteIfDifferent(LightFilePath, lightStyleSheet);
            return darkChanged || lightChanged;
        }

        public static bool WriteNoOpThemes(string reason)
        {
            Directory.CreateDirectory(ExtensionsDirectory);

            string content =
                "/* Unity Pywal inactive. */\n" +
                $"/* {reason} */\n";

            bool darkChanged = WriteIfDifferent(DarkFilePath, content);
            bool lightChanged = WriteIfDifferent(LightFilePath, content);
            return darkChanged || lightChanged;
        }

        public static string BuildStyleSheet(PywalPalette palette, PywalThemeTokens tokens)
        {
            StringBuilder builder = new();
            builder.AppendLine("/* Unity Pywal - generated file. */");
            builder.AppendLine($"/* Skin: {tokens.SkinVariant} */");
            builder.AppendLine($"/* Palette: {palette.SourcePath} */");
            builder.AppendLine($"/* Checksum: {palette.ContentHash} */");
            builder.AppendLine();
            builder.AppendLine(":root");
            builder.AppendLine("{");
            foreach (KeyValuePair<string, UnityEngine.Color> variable in tokens.RootVariables.OrderBy(entry => entry.Key, StringComparer.Ordinal))
            {
                builder.Append("    ");
                builder.Append(variable.Key);
                builder.Append(": ");
                builder.Append(PywalColorUtility.ToCss(variable.Value));
                builder.AppendLine(";");
            }
            builder.Append("    --unity-selection-color: ");
            builder.Append(PywalColorUtility.ToCss(tokens.SelectionColor));
            builder.AppendLine(";");
            builder.AppendLine("}");
            builder.AppendLine();

            PywalFallbackStyleCatalog.AppendFallbackRules(builder, tokens);
            return builder.ToString();
        }

        private static bool WriteIfDifferent(string path, string content)
        {
            if (File.Exists(path))
            {
                string existing = File.ReadAllText(path);
                if (string.Equals(existing, content, StringComparison.Ordinal))
                {
                    return false;
                }
            }

            File.WriteAllText(path, content, Encoding.UTF8);
            return true;
        }
    }
}
