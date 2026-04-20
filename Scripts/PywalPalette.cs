using System;

using UnityEngine;

namespace UnityPywal.Editor
{
    internal sealed class PywalPalette
    {
        private readonly Color[] colors;

        public PywalPalette(
            string sourcePath,
            string contentHash,
            Color background,
            Color foreground,
            Color cursor,
            Color[] colors)
        {
            SourcePath = sourcePath;
            ContentHash = contentHash;
            Background = background;
            Foreground = foreground;
            Cursor = cursor;
            this.colors = colors ?? throw new ArgumentNullException(nameof(colors));
        }

        public string SourcePath { get; }
        public string ContentHash { get; }
        public Color Background { get; }
        public Color Foreground { get; }
        public Color Cursor { get; }
        public int ColorCount => colors.Length;

        public Color GetColor(int index)
        {
            if ((uint)index >= colors.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return colors[index];
        }
    }
}
