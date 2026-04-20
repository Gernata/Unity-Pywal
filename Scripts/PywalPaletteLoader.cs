using System;
using System.IO;

using UnityEngine;

namespace UnityPywal.Editor
{
    internal static class PywalPaletteLoader
    {
#pragma warning disable CS0649
        [Serializable]
        private sealed class PywalJsonRoot
        {
            public string checksum;
            public PywalSpecial special;
            public PywalColors colors;
        }

        [Serializable]
        private sealed class PywalSpecial
        {
            public string background;
            public string foreground;
            public string cursor;
        }

        [Serializable]
        private sealed class PywalColors
        {
            public string color0;
            public string color1;
            public string color2;
            public string color3;
            public string color4;
            public string color5;
            public string color6;
            public string color7;
            public string color8;
            public string color9;
            public string color10;
            public string color11;
            public string color12;
            public string color13;
            public string color14;
            public string color15;

            public string Get(int index)
            {
                return index switch
                {
                    0 => color0,
                    1 => color1,
                    2 => color2,
                    3 => color3,
                    4 => color4,
                    5 => color5,
                    6 => color6,
                    7 => color7,
                    8 => color8,
                    9 => color9,
                    10 => color10,
                    11 => color11,
                    12 => color12,
                    13 => color13,
                    14 => color14,
                    15 => color15,
                    _ => null
                };
            }
        }
#pragma warning restore CS0649

        public static bool TryLoad(string palettePath, out PywalPalette palette, out string error)
        {
            palette = null;
            error = string.Empty;

            string resolvedPath = PywalSettings.ResolvePalettePath(palettePath);
            if (string.IsNullOrWhiteSpace(resolvedPath))
            {
                error = "Palette path is empty.";
                return false;
            }

            if (!File.Exists(resolvedPath))
            {
                error = $"Pywal palette file not found: {resolvedPath}";
                return false;
            }

            string json;
            try
            {
                json = File.ReadAllText(resolvedPath);
            }
            catch (Exception exception)
            {
                error = $"Unable to read the pywal palette: {exception.Message}";
                return false;
            }

            if (string.IsNullOrWhiteSpace(json))
            {
                error = $"Pywal palette file is empty: {resolvedPath}";
                return false;
            }

            PywalJsonRoot root;
            try
            {
                root = JsonUtility.FromJson<PywalJsonRoot>(json);
            }
            catch (Exception exception)
            {
                error = $"Pywal palette file is not valid JSON: {exception.Message}";
                return false;
            }

            if (root?.special == null || root.colors == null)
            {
                error = "Pywal palette JSON is missing required 'special' or 'colors' sections.";
                return false;
            }

            if (!PywalColorUtility.TryParseHex(root.special.background, out Color background))
            {
                error = "Pywal palette JSON is missing a valid special.background color.";
                return false;
            }

            if (!PywalColorUtility.TryParseHex(root.special.foreground, out Color foreground))
            {
                error = "Pywal palette JSON is missing a valid special.foreground color.";
                return false;
            }

            if (!PywalColorUtility.TryParseHex(root.special.cursor, out Color cursor))
            {
                error = "Pywal palette JSON is missing a valid special.cursor color.";
                return false;
            }

            Color[] colors = new Color[16];
            for (int index = 0; index < colors.Length; index++)
            {
                string value = root.colors.Get(index);
                if (!PywalColorUtility.TryParseHex(value, out colors[index]))
                {
                    error = $"Pywal palette JSON is missing a valid colors.color{index} value.";
                    return false;
                }
            }

            palette = new PywalPalette(
                resolvedPath,
                PywalColorUtility.ComputeSha256(json),
                background,
                foreground,
                cursor,
                colors);

            return true;
        }
    }
}
