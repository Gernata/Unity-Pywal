using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

using UnityEngine;

namespace UnityPywal.Editor
{
    internal static class PywalColorUtility
    {
        private const float OpaqueThreshold = 0.999f;

        public static bool TryParseHex(string value, out Color color)
        {
            color = default;
            return !string.IsNullOrWhiteSpace(value) && ColorUtility.TryParseHtmlString(value.Trim(), out color);
        }

        public static Color Mix(Color source, Color destination, float destinationAmount)
        {
            float t = Mathf.Clamp01(destinationAmount);
            return new Color(
                Mathf.Lerp(source.r, destination.r, t),
                Mathf.Lerp(source.g, destination.g, t),
                Mathf.Lerp(source.b, destination.b, t),
                Mathf.Lerp(source.a, destination.a, t));
        }

        public static Color WithAlpha(Color color, float alpha)
        {
            color.a = Mathf.Clamp01(alpha);
            return color;
        }

        public static Color ClampHsv(Color color, float maxSaturation, float minValue, float maxValue)
        {
            Color.RGBToHSV(color, out float hue, out float saturation, out float value);
            Color adjusted = Color.HSVToRGB(
                hue,
                Mathf.Min(saturation, Mathf.Clamp01(maxSaturation)),
                Mathf.Clamp(value, Mathf.Clamp01(minValue), Mathf.Clamp01(maxValue)));
            adjusted.a = color.a;
            return adjusted;
        }

        public static Color EnsureContrast(Color candidate, Color background, float minimumContrast, Color? fallback = null)
        {
            if (ContrastRatio(candidate, background) >= minimumContrast)
            {
                return candidate;
            }

            Color target = RelativeLuminance(background) > 0.5f ? Color.black : Color.white;
            const int steps = 32;

            for (int step = 1; step <= steps; step++)
            {
                Color adjusted = Mix(candidate, target, step / (float)steps);
                if (ContrastRatio(adjusted, background) >= minimumContrast)
                {
                    return adjusted;
                }
            }

            if (fallback.HasValue)
            {
                Color fallbackValue = fallback.Value;
                if (ContrastRatio(fallbackValue, background) >= minimumContrast)
                {
                    return fallbackValue;
                }
            }

            return ContrastRatio(target, background) >= ContrastRatio(candidate, background) ? target : candidate;
        }

        public static float ContrastRatio(Color a, Color b)
        {
            float luminanceA = RelativeLuminance(a);
            float luminanceB = RelativeLuminance(b);
            float light = Mathf.Max(luminanceA, luminanceB);
            float dark = Mathf.Min(luminanceA, luminanceB);
            return (light + 0.05f) / (dark + 0.05f);
        }

        public static float RelativeLuminance(Color color)
        {
            static float Convert(float value)
            {
                return value <= 0.03928f
                    ? value / 12.92f
                    : Mathf.Pow((value + 0.055f) / 1.055f, 2.4f);
            }

            float r = Convert(color.r);
            float g = Convert(color.g);
            float b = Convert(color.b);
            return 0.2126f * r + 0.7152f * g + 0.0722f * b;
        }

        public static string ToCss(Color color)
        {
            int r = Mathf.RoundToInt(color.r * 255f);
            int g = Mathf.RoundToInt(color.g * 255f);
            int b = Mathf.RoundToInt(color.b * 255f);
            float alpha = Mathf.Clamp01(color.a);

            if (alpha >= OpaqueThreshold)
            {
                return $"rgb({r}, {g}, {b})";
            }

            return string.Format(
                CultureInfo.InvariantCulture,
                "rgba({0}, {1}, {2}, {3:0.###})",
                r,
                g,
                b,
                alpha);
        }

        public static string ComputeSha256(string text)
        {
            byte[] input = Encoding.UTF8.GetBytes(text ?? string.Empty);
            using SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(input);

            StringBuilder builder = new(hash.Length * 2);
            foreach (byte value in hash)
            {
                builder.Append(value.ToString("x2", CultureInfo.InvariantCulture));
            }

            return builder.ToString();
        }
    }
}
