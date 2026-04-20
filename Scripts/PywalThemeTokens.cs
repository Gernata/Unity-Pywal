using System.Collections.Generic;

using UnityEngine;

namespace UnityPywal.Editor
{
    internal enum UnitySkinVariant
    {
        Dark,
        Light
    }

    internal readonly struct PywalThemeTokens
    {
        private static readonly int[] PrimaryAccentCandidates = { 12, 4, 13, 6, 9 };
        private static readonly int[] SecondaryAccentCandidates = { 6, 14, 5, 13 };

        public PywalThemeTokens(
            UnitySkinVariant skinVariant,
            Color windowBackground,
            Color surfaceBackground,
            Color raisedBackground,
            Color textColor,
            Color mutedTextColor,
            Color borderColor,
            Color primaryAccent,
            Color secondaryAccent,
            Color warningAccent,
            Color dangerAccent,
            Color iconTint,
            Color evenRowBackground,
            Color oddRowBackground,
            Color selectedRowBackground,
            Color selectionColor,
            Dictionary<string, Color> rootVariables)
        {
            SkinVariant = skinVariant;
            WindowBackground = windowBackground;
            SurfaceBackground = surfaceBackground;
            RaisedBackground = raisedBackground;
            TextColor = textColor;
            MutedTextColor = mutedTextColor;
            BorderColor = borderColor;
            PrimaryAccent = primaryAccent;
            SecondaryAccent = secondaryAccent;
            WarningAccent = warningAccent;
            DangerAccent = dangerAccent;
            IconTint = iconTint;
            EvenRowBackground = evenRowBackground;
            OddRowBackground = oddRowBackground;
            SelectedRowBackground = selectedRowBackground;
            SelectionColor = selectionColor;
            RootVariables = rootVariables;
        }

        public UnitySkinVariant SkinVariant { get; }
        public Color WindowBackground { get; }
        public Color SurfaceBackground { get; }
        public Color RaisedBackground { get; }
        public Color TextColor { get; }
        public Color MutedTextColor { get; }
        public Color BorderColor { get; }
        public Color PrimaryAccent { get; }
        public Color SecondaryAccent { get; }
        public Color WarningAccent { get; }
        public Color DangerAccent { get; }
        public Color IconTint { get; }
        public Color EvenRowBackground { get; }
        public Color OddRowBackground { get; }
        public Color SelectedRowBackground { get; }
        public Color SelectionColor { get; }
        public IReadOnlyDictionary<string, Color> RootVariables { get; }

        public static PywalThemeTokens Create(PywalPalette palette, UnitySkinVariant variant)
        {
            Color paletteBackground = palette.Background;
            Color paletteForeground = palette.Foreground;
            Color rawPrimaryAccent = PickAccent(palette, PrimaryAccentCandidates, paletteBackground, 3f, palette.GetColor(12));
            Color rawSecondaryAccent = PickSecondaryAccent(
                palette,
                PickAccent(palette, SecondaryAccentCandidates, paletteBackground, 2f, palette.GetColor(14)),
                rawPrimaryAccent);

            return variant == UnitySkinVariant.Dark
                ? CreateDarkTokens(palette, paletteBackground, paletteForeground, rawPrimaryAccent, rawSecondaryAccent)
                : CreateLightTokens(palette, paletteBackground, rawPrimaryAccent, rawSecondaryAccent);
        }

        private static PywalThemeTokens CreateDarkTokens(
            PywalPalette palette,
            Color paletteBackground,
            Color paletteForeground,
            Color rawPrimaryAccent,
            Color rawSecondaryAccent)
        {
            Color unityWindowBase = new(0.16f, 0.165f, 0.18f, 1f);
            Color unitySurfaceBase = new(0.19f, 0.195f, 0.215f, 1f);
            Color unityToolbarBase = new(0.215f, 0.22f, 0.24f, 1f);
            Color unityRaisedBase = new(0.235f, 0.24f, 0.26f, 1f);
            Color unityBorderBase = new(0.125f, 0.13f, 0.15f, 1f);
            Color chromeTint = PywalColorUtility.ClampHsv(
                PywalColorUtility.Mix(paletteBackground, rawPrimaryAccent, 0.24f),
                0.24f,
                0.20f,
                0.36f);
            Color window = PywalColorUtility.Mix(unityWindowBase, chromeTint, 0.32f);
            Color text = PywalColorUtility.EnsureContrast(paletteForeground, window, 7f);
            Color primaryAccent = NormalizeAccent(rawPrimaryAccent, window, text, UnitySkinVariant.Dark, 2.2f);
            Color secondaryAccent = NormalizeAccent(rawSecondaryAccent, window, text, UnitySkinVariant.Dark, 1.8f);
            Color warningAccent = NormalizeAccent(palette.GetColor(11), window, text, UnitySkinVariant.Dark, 2f);
            Color dangerAccent = NormalizeAccent(palette.GetColor(9), window, text, UnitySkinVariant.Dark, 2f);
            Color surface = PywalColorUtility.Mix(unitySurfaceBase, chromeTint, 0.38f);
            Color toolbar = PywalColorUtility.Mix(unityToolbarBase, chromeTint, 0.35f);
            Color raised = PywalColorUtility.Mix(unityRaisedBase, chromeTint, 0.31f);
            raised = PywalColorUtility.Mix(raised, primaryAccent, 0.016f);
            Color button = PywalColorUtility.Mix(raised, surface, 0.38f);
            Color buttonHover = PywalColorUtility.Mix(button, primaryAccent, 0.08f);
            Color buttonPressed = PywalColorUtility.Mix(button, primaryAccent, 0.14f);
            Color border = PywalColorUtility.Mix(unityBorderBase, chromeTint, 0.20f);
            Color mutedText = PywalColorUtility.Mix(text, surface, 0.42f);
            Color selectionBase = PywalColorUtility.Mix(primaryAccent, text, 0.10f);
            Color selection = PywalColorUtility.WithAlpha(selectionBase, 0.18f);
            Color selectedRow = PywalColorUtility.Mix(surface, selectionBase, 0.14f);
            Color helpboxBackground = PywalColorUtility.Mix(surface, warningAccent, 0.05f);
            Color helpboxBorder = PywalColorUtility.Mix(border, warningAccent, 0.18f);
            Color highlight = PywalColorUtility.Mix(surface, primaryAccent, 0.08f);
            Color highlightHover = PywalColorUtility.Mix(surface, primaryAccent, 0.11f);
            Color highlightHoverLighter = PywalColorUtility.Mix(surface, primaryAccent, 0.14f);
            Color alternatedRows = PywalColorUtility.Mix(surface, text, 0.03f);
            Color labelFocus = PywalColorUtility.Mix(text, primaryAccent, 0.35f);
            Color focusBorder = PywalColorUtility.Mix(border, primaryAccent, 0.24f);
            Color hoverBorder = PywalColorUtility.Mix(border, primaryAccent, 0.16f);
            Color inspectorBackground = PywalColorUtility.Mix(raised, primaryAccent, 0.02f);
            Color inspectorBackgroundHover = PywalColorUtility.Mix(raised, primaryAccent, 0.05f);
            Color inspectorBorderAccent = PywalColorUtility.Mix(border, secondaryAccent, 0.10f);

            return new PywalThemeTokens(
                UnitySkinVariant.Dark,
                window,
                surface,
                raised,
                text,
                mutedText,
                border,
                primaryAccent,
                secondaryAccent,
                warningAccent,
                dangerAccent,
                PywalColorUtility.Mix(text, primaryAccent, 0.05f),
                alternatedRows,
                PywalColorUtility.Mix(alternatedRows, text, 0.025f),
                selectedRow,
                selection,
                BuildRootVariables(
                    window,
                    surface,
                    border,
                    text,
                    labelFocus,
                    mutedText,
                    toolbar,
                    border,
                    buttonHover,
                    buttonPressed,
                    border,
                    buttonHover,
                    button,
                    buttonHover,
                    PywalColorUtility.Mix(buttonPressed, primaryAccent, 0.08f),
                    buttonPressed,
                    PywalColorUtility.Mix(button, primaryAccent, 0.10f),
                    PywalColorUtility.Mix(border, primaryAccent, 0.12f),
                    inspectorBackground,
                    inspectorBackgroundHover,
                    border,
                    inspectorBorderAccent,
                    toolbar,
                    button,
                    border,
                    text,
                    helpboxBackground,
                    helpboxBorder,
                    text,
                    raised,
                    highlight,
                    highlightHover,
                    highlightHoverLighter,
                    mutedText,
                    focusBorder,
                    hoverBorder,
                    PywalColorUtility.Mix(button, primaryAccent, 0.06f),
                    PywalColorUtility.Mix(surface, text, 0.10f),
                    alternatedRows,
                    PywalColorUtility.Mix(raised, primaryAccent, 0.10f)));
        }

        private static PywalThemeTokens CreateLightTokens(
            PywalPalette palette,
            Color paletteBackground,
            Color rawPrimaryAccent,
            Color rawSecondaryAccent)
        {
            Color fallbackBlack = new(0.102f, 0.102f, 0.102f, 1f);
            Color window = PywalColorUtility.Mix(Color.white, paletteBackground, 0.015f);
            Color text = PywalColorUtility.EnsureContrast(palette.GetColor(0), window, 7f, fallbackBlack);
            Color primaryAccent = NormalizeAccent(rawPrimaryAccent, window, text, UnitySkinVariant.Light, 1.8f);
            Color secondaryAccent = NormalizeAccent(rawSecondaryAccent, window, text, UnitySkinVariant.Light, 1.6f);
            Color warningAccent = NormalizeAccent(palette.GetColor(11), window, text, UnitySkinVariant.Light, 1.8f);
            Color dangerAccent = NormalizeAccent(palette.GetColor(9), window, text, UnitySkinVariant.Light, 1.8f);
            Color surface = PywalColorUtility.Mix(window, text, 0.045f);
            Color toolbar = PywalColorUtility.Mix(window, text, 0.025f);
            Color raised = PywalColorUtility.Mix(surface, text, 0.035f);
            raised = PywalColorUtility.Mix(raised, primaryAccent, 0.01f);
            Color button = PywalColorUtility.Mix(surface, text, 0.03f);
            Color buttonHover = PywalColorUtility.Mix(button, primaryAccent, 0.06f);
            Color buttonPressed = PywalColorUtility.Mix(button, primaryAccent, 0.11f);
            Color border = PywalColorUtility.Mix(window, text, 0.20f);
            Color mutedText = PywalColorUtility.Mix(text, window, 0.50f);
            Color selectionBase = PywalColorUtility.Mix(primaryAccent, text, 0.06f);
            Color selection = PywalColorUtility.WithAlpha(selectionBase, 0.14f);
            Color selectedRow = PywalColorUtility.Mix(surface, selectionBase, 0.10f);
            Color helpboxBackground = PywalColorUtility.Mix(surface, warningAccent, 0.04f);
            Color helpboxBorder = PywalColorUtility.Mix(border, warningAccent, 0.15f);
            Color highlight = PywalColorUtility.Mix(surface, primaryAccent, 0.06f);
            Color highlightHover = PywalColorUtility.Mix(surface, primaryAccent, 0.09f);
            Color highlightHoverLighter = PywalColorUtility.Mix(surface, primaryAccent, 0.12f);
            Color alternatedRows = PywalColorUtility.Mix(surface, text, 0.02f);
            Color labelFocus = PywalColorUtility.Mix(text, primaryAccent, 0.28f);
            Color focusBorder = PywalColorUtility.Mix(border, primaryAccent, 0.18f);
            Color hoverBorder = PywalColorUtility.Mix(border, primaryAccent, 0.12f);
            Color inspectorBackground = PywalColorUtility.Mix(raised, primaryAccent, 0.015f);
            Color inspectorBackgroundHover = PywalColorUtility.Mix(raised, primaryAccent, 0.035f);
            Color inspectorBorderAccent = PywalColorUtility.Mix(border, secondaryAccent, 0.08f);

            return new PywalThemeTokens(
                UnitySkinVariant.Light,
                window,
                surface,
                raised,
                text,
                mutedText,
                border,
                primaryAccent,
                secondaryAccent,
                warningAccent,
                dangerAccent,
                PywalColorUtility.Mix(text, primaryAccent, 0.03f),
                alternatedRows,
                PywalColorUtility.Mix(alternatedRows, text, 0.02f),
                selectedRow,
                selection,
                BuildRootVariables(
                    window,
                    surface,
                    border,
                    text,
                    labelFocus,
                    mutedText,
                    toolbar,
                    border,
                    buttonHover,
                    buttonPressed,
                    border,
                    buttonHover,
                    button,
                    buttonHover,
                    PywalColorUtility.Mix(buttonPressed, primaryAccent, 0.08f),
                    buttonPressed,
                    PywalColorUtility.Mix(button, primaryAccent, 0.08f),
                    PywalColorUtility.Mix(border, primaryAccent, 0.08f),
                    inspectorBackground,
                    inspectorBackgroundHover,
                    border,
                    inspectorBorderAccent,
                    toolbar,
                    button,
                    border,
                    text,
                    helpboxBackground,
                    helpboxBorder,
                    text,
                    raised,
                    highlight,
                    highlightHover,
                    highlightHoverLighter,
                    mutedText,
                    focusBorder,
                    hoverBorder,
                    PywalColorUtility.Mix(button, primaryAccent, 0.05f),
                    PywalColorUtility.Mix(surface, text, 0.08f),
                    alternatedRows,
                    PywalColorUtility.Mix(raised, primaryAccent, 0.08f)));
        }

        private static Dictionary<string, Color> BuildRootVariables(
            Color windowBackground,
            Color defaultBackground,
            Color defaultBorder,
            Color defaultText,
            Color labelTextFocus,
            Color labelTextDisabled,
            Color toolbarBackground,
            Color toolbarBorder,
            Color toolbarButtonHover,
            Color toolbarButtonChecked,
            Color toolbarButtonBorder,
            Color appToolbarButtonHover,
            Color buttonBackground,
            Color buttonBackgroundHover,
            Color buttonBackgroundHoverPressed,
            Color buttonBackgroundPressed,
            Color buttonBackgroundFocus,
            Color buttonBorderPressed,
            Color inspectorTitlebarBackground,
            Color inspectorTitlebarBackgroundHover,
            Color inspectorTitlebarBorder,
            Color inspectorTitlebarBorderAccent,
            Color headerbarBackground,
            Color dropdownBackground,
            Color dropdownBorder,
            Color dropdownText,
            Color helpboxBackground,
            Color helpboxBorder,
            Color helpboxText,
            Color popupBackground,
            Color highlightBackground,
            Color highlightBackgroundHover,
            Color highlightBackgroundHoverLighter,
            Color highlightTextInactive,
            Color inputFieldBorderFocus,
            Color objectFieldBorderHover,
            Color scrollbarButtonHover,
            Color scrollbarThumbBackground,
            Color alternatedRowsBackground,
            Color tabBackgroundChecked)
        {
            return new Dictionary<string, Color>
            {
                ["--unity-colors-window-background"] = windowBackground,
                ["--unity-colors-default-background"] = defaultBackground,
                ["--unity-colors-default-border"] = defaultBorder,
                ["--unity-colors-default-text"] = defaultText,
                ["--unity-colors-default-text-hover"] = labelTextFocus,
                ["--unity-colors-label-text"] = defaultText,
                ["--unity-colors-label-text-focus"] = labelTextFocus,
                ["--unity-colors-label-text-disabled"] = labelTextDisabled,
                ["--unity-colors-toolbar-background"] = toolbarBackground,
                ["--unity-colors-toolbar-border"] = toolbarBorder,
                ["--unity-colors-toolbar_button-background-hover"] = toolbarButtonHover,
                ["--unity-colors-toolbar_button-background-checked"] = toolbarButtonChecked,
                ["--unity-colors-toolbar_button-border"] = toolbarButtonBorder,
                ["--unity-colors-app_toolbar_button-background-hover"] = appToolbarButtonHover,
                ["--unity-colors-button-background"] = buttonBackground,
                ["--unity-colors-button-background-hover"] = buttonBackgroundHover,
                ["--unity-colors-button-background-hover_pressed"] = buttonBackgroundHoverPressed,
                ["--unity-colors-button-background-pressed"] = buttonBackgroundPressed,
                ["--unity-colors-button-background-focus"] = buttonBackgroundFocus,
                ["--unity-colors-button-border-pressed"] = buttonBorderPressed,
                ["--unity-colors-inspector_titlebar-background"] = inspectorTitlebarBackground,
                ["--unity-colors-inspector_titlebar-background-hover"] = inspectorTitlebarBackgroundHover,
                ["--unity-colors-inspector_titlebar-border"] = inspectorTitlebarBorder,
                ["--unity-colors-inspector_titlebar-border_accent"] = inspectorTitlebarBorderAccent,
                ["--unity-colors-headerbar-background"] = headerbarBackground,
                ["--unity-colors-dropdown-background"] = dropdownBackground,
                ["--unity-colors-dropdown-border"] = dropdownBorder,
                ["--unity-colors-dropdown-text"] = dropdownText,
                ["--unity-colors-helpbox-background"] = helpboxBackground,
                ["--unity-colors-helpbox-border"] = helpboxBorder,
                ["--unity-colors-helpbox-text"] = helpboxText,
                ["--unity-colors-popup-background"] = popupBackground,
                ["--unity-colors-highlight-background"] = highlightBackground,
                ["--unity-colors-highlight-background-hover"] = highlightBackgroundHover,
                ["--unity-colors-highlight-background-hover-lighter"] = highlightBackgroundHoverLighter,
                ["--unity-colors-highlight-text-inactive"] = highlightTextInactive,
                ["--unity-colors-input_field-border-focus"] = inputFieldBorderFocus,
                ["--unity-colors-object_field-border-hover"] = objectFieldBorderHover,
                ["--unity-colors-scrollbar_button-background-hover"] = scrollbarButtonHover,
                ["--unity-colors-scrollbar_thumb-background"] = scrollbarThumbBackground,
                ["--unity-colors-alternated_rows-background"] = alternatedRowsBackground,
                ["--unity-colors-tab-background-checked"] = tabBackgroundChecked
            };
        }

        private static Color PickAccent(PywalPalette palette, IReadOnlyList<int> candidates, Color background, float minimumContrast, Color fallback)
        {
            Color best = fallback;
            float bestContrast = PywalColorUtility.ContrastRatio(best, background);

            for (int index = 0; index < candidates.Count; index++)
            {
                Color candidate = palette.GetColor(candidates[index]);
                float contrast = PywalColorUtility.ContrastRatio(candidate, background);
                if (contrast >= minimumContrast)
                {
                    return candidate;
                }

                if (contrast > bestContrast)
                {
                    best = candidate;
                    bestContrast = contrast;
                }
            }

            return best;
        }

        private static Color PickSecondaryAccent(PywalPalette palette, Color secondaryAccent, Color primaryAccent)
        {
            if (secondaryAccent != primaryAccent)
            {
                return secondaryAccent;
            }

            for (int index = 0; index < palette.ColorCount; index++)
            {
                Color candidate = palette.GetColor(index);
                if (candidate != primaryAccent)
                {
                    return candidate;
                }
            }

            return secondaryAccent;
        }

        private static Color NormalizeAccent(Color accent, Color background, Color foreground, UnitySkinVariant variant, float minimumContrast)
        {
            Color toned = variant == UnitySkinVariant.Dark
                ? PywalColorUtility.ClampHsv(accent, 0.45f, 0.36f, 0.78f)
                : PywalColorUtility.ClampHsv(accent, 0.40f, 0.22f, 0.72f);

            toned = variant == UnitySkinVariant.Dark
                ? PywalColorUtility.Mix(toned, foreground, 0.12f)
                : PywalColorUtility.Mix(toned, background, 0.24f);

            toned = variant == UnitySkinVariant.Dark
                ? PywalColorUtility.Mix(toned, background, 0.06f)
                : PywalColorUtility.Mix(toned, foreground, 0.05f);

            return PywalColorUtility.EnsureContrast(toned, background, minimumContrast, toned);
        }
    }
}
