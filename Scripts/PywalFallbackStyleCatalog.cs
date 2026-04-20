using System.Collections.Generic;
using System.Text;

namespace UnityPywal.Editor
{
    internal static class PywalFallbackStyleCatalog
    {
        private static readonly string[] WindowChromeSelectors =
        {
            "DockArea",
            "dockarea"
        };

        private static readonly string[] ShellSelectors =
        {
            "HostView",
            "InspectorWindow",
            "ProjectBrowser",
            "SceneHierarchyWindow",
            "ConsoleWindow",
            "ProjectBrowserView",
            "Window",
            "ScrollViewAlt",
            "TabWindowBackground",
            "PreBackground",
            "ProjectBrowserIconAreaBg",
            "RL-Background",
            "RL-Empty-Header",
            "FrameBox",
            "GroupBox",
            "Box",
            "WhiteBackground",
            "ColorPickerBackground",
            "ColorPickerBox",
            "AvatarMappingBox",
            "ChannelStripBg",
            "ProfilerLeftPane",
            "ShurikenEffectBg",
            "ShurikenModuleBg",
            "OL-box",
            "OL-box-NoExpand",
            "OL-box-flat"
        };

        private static readonly string[] ToolbarSelectors =
        {
            "AppToolbar",
            "Toolbar",
            "ToolbarBottom",
            "ContentToolbar",
            "PreToolbar",
            "PreToolbar2",
            "SceneTopBarBg",
            "ProjectBrowserTopBarBg",
            "ProjectBrowserBottomBarBg",
            "AnimPlayToolbar",
            "AnimClipToolbar",
            "TE-Toolbar",
            "MultiColumnTopBar",
            "dockHeader"
        };

        private static readonly string[] UiToolKitShellSelectors =
        {
            "unity-host-view",
            "unity-box",
            "unity-scroll-view",
            "unity-collection-view",
            "unity-list-view",
            "unity-tree-view",
            "unity-inspector-element",
            "unity-tab-view__content-container",
            "unity-pane-content"
        };

        private static readonly string[] UiToolKitWindowChromeSelectors =
        {
            "unity-dock-area"
        };

        private static readonly string[] UiToolKitToolbarSelectors =
        {
            "unity-toolbar",
            "unity-overlay",
            "unity-tab-view__header-container"
        };

        private static readonly string[] UiToolKitHierarchyContainerSelectors =
        {
            "hierarchy__container",
            "unity-tree-view__list-view",
            "unity-collection-view__scroll-view",
            "unity-collection-view__background-fill",
            "unity-multi-column-view__row-container"
        };

        private static readonly string[] UiToolKitHierarchyHeaderSelectors =
        {
            "unity-multi-column-view__header-container",
            "unity-multi-column-header",
            "unity-multi-column-header__column-container",
            "unity-multi-column-header__column"
        };

        private static readonly string[] UiToolKitHierarchyTransparentSurfaceSelectors =
        {
            "unity-multi-column-view__cell",
            "unity-tree-view__item-content",
            "unity-tree-view__item-indent",
            "unity-tree-view__item-indent-even",
            "unity-tree-view__item-indent-odd",
            "unity-tree-view__item-toggle",
            "#unity-tree-view__item-content",
            "#unity-tree-view__item-indent",
            "#unity-tree-view__item-toggle",
            "cell-prop-field",
            "toggle-icon",
            "toggle-scene-visibility",
            "toggle-scene-picking"
        };

        private static readonly string[] ButtonSelectors =
        {
            "AC-Button",
            "AppCommand",
            "AppCommandLeft",
            "AppCommandMid",
            "AppCommandRight",
            "Button",
            "ButtonLeft",
            "ButtonMid",
            "ButtonRight",
            "Command",
            "CommandLeft",
            "CommandMid",
            "CommandRight",
            "DropDownButton",
            "DropDownToggleButton",
            "EditModeSingleButton",
            "HeaderButton",
            "IconButton",
            "InvisibleButton",
            "LargeButton",
            "LargeButtonLeft",
            "LargeButtonMid",
            "LargeButtonRight",
            "MiniToolbarButton",
            "PaneOptions",
            "PreButton",
            "StatusBarIcon",
            "ToolbarButtonFlat",
            "ToolbarCreateAddNewDropDown",
            "ToolbarDropDown",
            "ToolbarDropDownLeft",
            "ToolbarDropDownRight",
            "ToolbarDropDownToggle",
            "ToolbarDropDownToggleButton",
            "ToolbarDropDownToggleLeft",
            "ToolbarDropDownToggleRight",
            "ToolbarPopup",
            "ToolbarPopupLeft",
            "ToolbarPopupRight",
            "WindowMenuButton",
            "AnimClipToolbarButton",
            "AnimClipToolbarPopup",
            "TimeScrubberButton",
            "TE-ToolbarDropDown",
            "dragtab",
            "dragtab-first",
            "dragtab-scroller-next",
            "dragtab-scroller-prev"
        };

        private static readonly string[] UiToolKitButtonSelectors =
        {
            "unity-button",
            "unity-toolbar-button",
            "unity-button-strip-field__button"
        };

        private static readonly string[] FieldSelectors =
        {
            "AnimationSelectionTextField",
            "AxisLabelNumberField",
            "BoldTextField",
            "ColorField",
            "FloatFieldLinkButton",
            "IN-TextField",
            "MiniTextField",
            "ObjectField",
            "ObjectFieldButton",
            "PR-TextField",
            "SearchTextField",
            "StaticDropdown",
            "TextField",
            "TextFieldDropDown",
            "TextFieldDropDownText",
            "ToolbarSearchTextField",
            "ToolbarSearchTextFieldPopup",
            "ToolbarSearchTextFieldWithJump",
            "ToolbarSearchTextFieldWithJumpPopup",
            "ToolbarSearchTextFieldWithJumpPopupSynced",
            "ToolbarSearchTextFieldWithJumpSynced",
            "ToolbarSliderTextField",
            "ToolbarTextField",
            "Popup",
            "MiniPopup",
            "MiniPullDown",
            "DropDown"
        };

        private static readonly string[] UiToolKitFieldSelectors =
        {
            "unity-base-field",
            "unity-base-field__input",
            "unity-base-text-field",
            "unity-base-text-field__input",
            "unity-composite-field__input",
            "unity-text-field",
            "unity-search-field",
            "unity-search-field-base",
            "unity-search-field-base__text-field",
            "unity-toolbar-search-field",
            "unity-popup-field",
            "unity-enum-field",
            "unity-object-field",
            "unity-color-field",
            "unity-numeric-field"
        };

        private static readonly string[] UiToolKitToggleSelectors =
        {
            "unity-toggle__input",
            "unity-toggle__checkmark",
            "unity-radio-button__input",
            "unity-checkbox",
            "unity-checkbox__input"
        };

        private static readonly string[] UiToolKitToggleTextSelectors =
        {
            "unity-toggle__text",
            "unity-radio-button__label"
        };

        private static readonly string[] UiToolKitScrollerSelectors =
        {
            "unity-scroller--horizontal",
            "unity-scroller--vertical",
            "unity-scroller__slider",
            "unity-slider"
        };

        private static readonly string[] UiToolKitTabSelectors =
        {
            "unity-tab",
            "unity-tab__header",
            "unity-tab-view"
        };

        private static readonly string[] LabelSelectors =
        {
            "BoldLabel",
            "ControlLabel",
            "Label",
            "LargeButton",
            "ProjectBrowserGridLabel",
            "ScriptText",
            "OL-ResultLabel",
            "OL-Title",
            "OL-Title-TextRight",
            "PR-Label",
            "PR-PrefabLabel",
            "SettingsTreeItem",
            "SearchModeFilter",
            "ShurikenLabel",
            "ShurikenValue",
            "TE-DefaultTime",
            "CurveEditorLabelTickmarks",
            "IN-TitleText"
        };

        private static readonly string[] UiToolKitLabelSelectors =
        {
            "unity-label",
            "unity-text-element",
            "unity-foldout__text"
        };

        private static readonly string[] HelpBoxSelectors =
        {
            "HelpBox",
            "CN-Box",
            "CN-Message",
            "DD-Background",
            "DD-HeaderStyle",
            "PopupCurveSwatchBackground"
        };

        private static readonly string[] UiToolKitHelpBoxSelectors =
        {
            "unity-help-box"
        };

        private static readonly string[] InspectorTitleSelectors =
        {
            "IN-BigTitle",
            "IN-BigTitle-Post",
            "IN-Title",
            "IN-Footer",
            "FoldoutHeader",
            "Titlebar-Foldout",
            "SettingsHeader",
            "ShurikenEmitterTitle",
            "ShurikenModuleTitle"
        };

        private static readonly string[] UiToolKitInspectorTitleSelectors =
        {
            "unity-foldout__toggle",
            "unity-foldout__input",
            "unity-inspector-element__header",
            "header-foldout"
        };

        private static readonly string[] RowEvenSelectors =
        {
            "AnimationRowEven",
            "CN-EntryBackEven",
            "OL-EntryBackEven",
            "ObjectPickerResultsEven"
        };

        private static readonly string[] RowOddSelectors =
        {
            "AnimationRowOdd",
            "CN-EntryBackOdd",
            "OL-EntryBackOdd",
            "ObjectPickerResultsOdd"
        };

        private static readonly string[] RowSelectedSelectors =
        {
            "CN-EntryBackEven:checked",
            "CN-EntryBackOdd:checked",
            "TV-Selection",
            "SelectionRect",
            "MeTransitionSelect",
            "MeTransitionSelectHead"
        };

        private static readonly string[] UiToolKitRowSelectors =
        {
            "unity-list-view__item",
            "unity-tree-view__item",
            "unity-multi-column-view__row"
        };

        private static readonly string[] UiToolKitRowSelectedSelectors =
        {
            "unity-list-view__item--selected",
            "unity-tree-view__item--selected",
            "unity-list-view__item:checked",
            "unity-tree-view__item:checked",
            ".unity-imgui-container.unity-list-view__item--selected"
        };

        private static readonly string[] IconTintSelectors =
        {
            "Foldout",
            "IN-Foldout",
            "Toggle",
            "ToggleMixed",
            "Radio",
            "MenuItem",
            "ObjectFieldThumb",
            "ObjectFieldThumbOverlay2",
            "TV-Ping",
            "OL-Ping",
            "CN-EntryInfoIcon",
            "CN-EntryWarnIcon",
            "CN-EntryErrorIcon"
        };

        private static readonly string[] UiToolKitIconTintSelectors =
        {
            "unity-foldout__checkmark",
            "unity-checkmark",
            "unity-object-field__selector",
            "unity-toggle__checkmark",
            "unity-radio-button__input > .unity-image",
            "toggle-icon"
        };

        private static readonly string[] SeparatorSelectors =
        {
            "#splitter",
            "split-view",
            "split-view-report",
            "Splitter",
            "unity-two-pane-split-view__dragline",
            "unity-two-pane-split-view__dragline--horizontal",
            "unity-two-pane-split-view__dragline--vertical",
            "unity-two-pane-split-view__dragline-anchor",
            "unity-two-pane-split-view__dragline-anchor--horizontal",
            "unity-two-pane-split-view__dragline-anchor--vertical",
            "#unity-dragline-anchor",
            "#windowResizer",
            "#windowResizerIcon",
            "#Resizer",
            "resizer"
        };

        private static readonly string[] SeparatorHoverSelectors =
        {
            "unity-two-pane-split-view__dragline-anchor",
            "unity-two-pane-split-view__dragline-anchor--horizontal",
            "unity-two-pane-split-view__dragline-anchor--vertical",
            "#unity-dragline-anchor"
        };

        private static readonly string[] PaneBorderSelectors =
        {
            "DockArea",
            "dockarea",
            "HostView",
            "unity-dock-area",
            "unity-host-view",
            "dockHeader",
            "unity-two-pane-split-view",
            "unity-two-pane-split-view__content-container",
            "unity-two-pane-split-view__handle-container",
            "unity-pane-content",
            "unity-inspector-element",
            "unity-tab-view__content-container"
        };

        public static void AppendFallbackRules(StringBuilder builder, PywalThemeTokens tokens)
        {
            bool isDark = tokens.SkinVariant == UnitySkinVariant.Dark;
            UnityEngine.Color separatorColor = PywalColorUtility.Mix(tokens.BorderColor, tokens.TextColor, 0.34f);
            UnityEngine.Color paneBorderColor = PywalColorUtility.Mix(tokens.BorderColor, separatorColor, 0.58f);
            UnityEngine.Color separatorHoverColor = PywalColorUtility.Mix(separatorColor, tokens.TextColor, 0.18f);
            UnityEngine.Color controlBackground = isDark
                ? PywalColorUtility.Mix(tokens.SurfaceBackground, tokens.RaisedBackground, 0.40f)
                : tokens.SurfaceBackground;
            UnityEngine.Color toolbarColor = isDark
                ? PywalColorUtility.Mix(controlBackground, tokens.RaisedBackground, 0.30f)
                : PywalColorUtility.Mix(tokens.WindowBackground, tokens.SurfaceBackground, 0.42f);
            UnityEngine.Color controlBorderColor = tokens.BorderColor;
            UnityEngine.Color toolbarBorderColor = isDark ? controlBorderColor : paneBorderColor;
            UnityEngine.Color headerColor = PywalColorUtility.Mix(tokens.SurfaceBackground, tokens.RaisedBackground, 0.72f);

            AppendRule(
                builder,
                "Window Chrome",
                Combine(WindowChromeSelectors, UiToolKitWindowChromeSelectors),
                ColorProperties(tokens.WindowBackground, paneBorderColor, tokens.TextColor));

            AppendRule(
                builder,
                "Shell",
                Combine(ShellSelectors, UiToolKitShellSelectors),
                ColorProperties(controlBackground, controlBorderColor, tokens.TextColor));

            AppendRule(
                builder,
                "Toolbar",
                Combine(ToolbarSelectors, UiToolKitToolbarSelectors, UiToolKitHierarchyHeaderSelectors),
                ColorProperties(toolbarColor, toolbarBorderColor, tokens.TextColor));

            AppendRule(
                builder,
                "Buttons",
                Combine(ButtonSelectors, UiToolKitButtonSelectors),
                ColorProperties(tokens.RaisedBackground, controlBorderColor, tokens.TextColor));

            AppendRule(
                builder,
                "Buttons Hover",
                AppendState(Combine(ButtonSelectors, UiToolKitButtonSelectors), ":hover"),
                ColorProperties(
                    PywalColorUtility.Mix(tokens.RaisedBackground, tokens.PrimaryAccent, 0.22f),
                    PywalColorUtility.Mix(controlBorderColor, tokens.PrimaryAccent, 0.14f),
                    tokens.TextColor));

            AppendRule(
                builder,
                "Buttons Pressed",
                AppendStates(Combine(ButtonSelectors, UiToolKitButtonSelectors), ":active", ":checked", ":active:checked", ":hover:checked"),
                ColorProperties(
                    PywalColorUtility.Mix(tokens.RaisedBackground, tokens.PrimaryAccent, 0.34f),
                    PywalColorUtility.Mix(controlBorderColor, tokens.PrimaryAccent, 0.20f),
                    tokens.TextColor));

            AppendRule(
                builder,
                "Fields",
                Combine(FieldSelectors, UiToolKitFieldSelectors),
                ColorProperties(controlBackground, controlBorderColor, tokens.TextColor));

            AppendRule(
                builder,
                "Fields Focus",
                AppendState(Combine(FieldSelectors, UiToolKitFieldSelectors), ":focus"),
                ColorProperties(
                    controlBackground,
                    PywalColorUtility.Mix(controlBorderColor, tokens.PrimaryAccent, 0.24f),
                    tokens.TextColor));

            AppendRule(
                builder,
                "Toggle Inputs",
                UiToolKitToggleSelectors,
                ColorProperties(tokens.RaisedBackground, controlBorderColor, tokens.TextColor));

            AppendRule(
                builder,
                "Toggle Inputs Hover",
                AppendState(UiToolKitToggleSelectors, ":hover"),
                ColorProperties(
                    PywalColorUtility.Mix(tokens.RaisedBackground, tokens.PrimaryAccent, 0.18f),
                    PywalColorUtility.Mix(controlBorderColor, tokens.PrimaryAccent, 0.14f),
                    tokens.TextColor));

            AppendRule(
                builder,
                "Toggle Inputs Checked",
                AppendStates(UiToolKitToggleSelectors, ":checked", ":hover:checked", ":active:checked"),
                ColorProperties(
                    PywalColorUtility.Mix(tokens.RaisedBackground, tokens.PrimaryAccent, 0.30f),
                    PywalColorUtility.Mix(controlBorderColor, tokens.PrimaryAccent, 0.18f),
                    tokens.TextColor));

            AppendRule(
                builder,
                "Labels",
                Combine(LabelSelectors, UiToolKitLabelSelectors),
                TextProperties(tokens.TextColor));

            AppendRule(
                builder,
                "Toggle Labels",
                UiToolKitToggleTextSelectors,
                TextProperties(tokens.TextColor));

            AppendRule(
                builder,
                "Subtle Borders",
                new[] { "grey_border", "RL-Header", "RL-Footer" },
                BorderProperties(tokens.MutedTextColor));

            AppendRule(
                builder,
                "Helpboxes",
                Combine(HelpBoxSelectors, UiToolKitHelpBoxSelectors),
                ColorProperties(
                    controlBackground,
                    PywalColorUtility.Mix(controlBorderColor, tokens.WarningAccent, 0.18f),
                    tokens.TextColor));

            AppendRule(
                builder,
                "Inspector Titles",
                Combine(InspectorTitleSelectors, UiToolKitInspectorTitleSelectors),
                ColorProperties(
                    headerColor,
                    isDark
                        ? PywalColorUtility.Mix(controlBorderColor, tokens.SecondaryAccent, 0.08f)
                        : PywalColorUtility.Mix(paneBorderColor, tokens.SecondaryAccent, 0.08f),
                    tokens.TextColor));

            AppendRule(
                builder,
                "Rows Even",
                RowEvenSelectors,
                BackgroundProperties(tokens.EvenRowBackground));

            AppendRule(
                builder,
                "Rows Odd",
                RowOddSelectors,
                BackgroundProperties(tokens.OddRowBackground));

            AppendRule(
                builder,
                "Collection Rows",
                UiToolKitRowSelectors,
                ColorProperties(controlBackground, controlBorderColor, tokens.TextColor));

            AppendRule(
                builder,
                "Collection Background Fill",
                UiToolKitHierarchyContainerSelectors,
                ColorProperties(controlBackground, controlBorderColor, tokens.TextColor));

            AppendRule(
                builder,
                "Collection Row Subparts",
                UiToolKitHierarchyTransparentSurfaceSelectors,
                BackgroundProperties(UnityEngine.Color.clear));

            AppendRule(
                builder,
                "Collection Rows Hover",
                AppendState(UiToolKitRowSelectors, ":hover"),
                ColorProperties(
                    PywalColorUtility.Mix(controlBackground, tokens.PrimaryAccent, 0.12f),
                    PywalColorUtility.Mix(controlBorderColor, tokens.PrimaryAccent, 0.10f),
                    tokens.TextColor));

            AppendRule(
                builder,
                "Selected Rows",
                Combine(RowSelectedSelectors, UiToolKitRowSelectedSelectors),
                new Dictionary<string, string>
                {
                    ["background-color"] = PywalColorUtility.ToCss(tokens.SelectedRowBackground),
                    ["border-color"] = PywalColorUtility.ToCss(PywalColorUtility.Mix(controlBorderColor, tokens.PrimaryAccent, 0.12f)),
                    ["color"] = PywalColorUtility.ToCss(tokens.TextColor)
                });

            AppendRule(
                builder,
                "Tabs",
                UiToolKitTabSelectors,
                ColorProperties(tokens.RaisedBackground, controlBorderColor, tokens.TextColor));

            AppendRule(
                builder,
                "Tabs Active",
                AppendStates(UiToolKitTabSelectors, ":checked", ":hover:checked", ":active", ":active:checked"),
                ColorProperties(
                    PywalColorUtility.Mix(tokens.RaisedBackground, tokens.PrimaryAccent, 0.22f),
                    PywalColorUtility.Mix(controlBorderColor, tokens.PrimaryAccent, 0.16f),
                    tokens.TextColor));

            AppendRule(
                builder,
                "Scrollers",
                UiToolKitScrollerSelectors,
                ColorProperties(controlBackground, controlBorderColor, tokens.TextColor));

            AppendRule(
                builder,
                "Scrollers Hover",
                AppendState(UiToolKitScrollerSelectors, ":hover"),
                ColorProperties(
                    PywalColorUtility.Mix(controlBackground, tokens.PrimaryAccent, 0.08f),
                    PywalColorUtility.Mix(controlBorderColor, tokens.PrimaryAccent, 0.10f),
                    tokens.TextColor));

            AppendRule(
                builder,
                "Icons",
                Combine(IconTintSelectors, UiToolKitIconTintSelectors),
                new Dictionary<string, string>
                {
                    ["-unity-background-image-tint-color"] = PywalColorUtility.ToCss(tokens.IconTint)
                });

            AppendRule(
                builder,
                "Console Icons",
                new[] { "CN-EntryInfoIcon" },
                new Dictionary<string, string>
                {
                    ["-unity-background-image-tint-color"] = PywalColorUtility.ToCss(tokens.PrimaryAccent)
                });

            AppendRule(
                builder,
                "Console Warnings",
                new[] { "CN-EntryWarnIcon" },
                new Dictionary<string, string>
                {
                    ["-unity-background-image-tint-color"] = PywalColorUtility.ToCss(tokens.WarningAccent)
                });

            AppendRule(
                builder,
                "Console Errors",
                new[] { "CN-EntryErrorIcon" },
                new Dictionary<string, string>
                {
                    ["-unity-background-image-tint-color"] = PywalColorUtility.ToCss(tokens.DangerAccent)
                });

            AppendRule(
                builder,
                "Pane Borders",
                PaneBorderSelectors,
                BorderProperties(paneBorderColor));

            AppendRule(
                builder,
                "Separators",
                SeparatorSelectors,
                SeparatorProperties(separatorColor));

            AppendRule(
                builder,
                "Separators Hover",
                AppendState(SeparatorHoverSelectors, ":hover"),
                SeparatorProperties(separatorHoverColor));
        }

        private static Dictionary<string, string> BackgroundProperties(UnityEngine.Color background)
        {
            return new Dictionary<string, string>
            {
                ["background-color"] = PywalColorUtility.ToCss(background)
            };
        }

        private static Dictionary<string, string> BorderProperties(UnityEngine.Color border)
        {
            string borderCss = PywalColorUtility.ToCss(border);
            return new Dictionary<string, string>
            {
                ["border-color"] = borderCss,
                ["border-left-color"] = borderCss,
                ["border-right-color"] = borderCss,
                ["border-top-color"] = borderCss,
                ["border-bottom-color"] = borderCss
            };
        }

        private static Dictionary<string, string> TextProperties(UnityEngine.Color text)
        {
            return new Dictionary<string, string>
            {
                ["color"] = PywalColorUtility.ToCss(text)
            };
        }

        private static Dictionary<string, string> ColorProperties(UnityEngine.Color background, UnityEngine.Color border, UnityEngine.Color text)
        {
            Dictionary<string, string> properties = BorderProperties(border);
            properties["background-color"] = PywalColorUtility.ToCss(background);
            properties["color"] = PywalColorUtility.ToCss(text);
            return properties;
        }

        private static Dictionary<string, string> SeparatorProperties(UnityEngine.Color separator)
        {
            Dictionary<string, string> properties = BorderProperties(separator);
            properties["background-color"] = PywalColorUtility.ToCss(separator);
            return properties;
        }

        private static IEnumerable<string> AppendState(IEnumerable<string> selectors, string state)
        {
            foreach (string selector in selectors)
            {
                yield return selector + state;
            }
        }

        private static IEnumerable<string> AppendStates(IEnumerable<string> selectors, params string[] states)
        {
            foreach (string selector in selectors)
            {
                for (int index = 0; index < states.Length; index++)
                {
                    yield return selector + states[index];
                }
            }
        }

        private static void AppendRule(StringBuilder builder, string title, IEnumerable<string> selectors, IReadOnlyDictionary<string, string> properties)
        {
            List<string> selectorList = new();
            foreach (string selector in selectors)
            {
                if (!string.IsNullOrWhiteSpace(selector))
                {
                    selectorList.Add(IsRawSelector(selector) ? selector : "." + selector);
                }
            }

            if (selectorList.Count == 0 || properties.Count == 0)
            {
                return;
            }

            builder.AppendLine($"/* {title} */");
            builder.AppendLine(string.Join(",\n", selectorList));
            builder.AppendLine("{");
            foreach (KeyValuePair<string, string> property in properties)
            {
                builder.Append("    ");
                builder.Append(property.Key);
                builder.Append(": ");
                builder.Append(property.Value);
                builder.AppendLine(";");
            }
            builder.AppendLine("}");
            builder.AppendLine();
        }

        private static IEnumerable<string> Combine(params IEnumerable<string>[] selectorSets)
        {
            foreach (IEnumerable<string> selectorSet in selectorSets)
            {
                foreach (string selector in selectorSet)
                {
                    yield return selector;
                }
            }
        }

        private static bool IsRawSelector(string selector)
        {
            return selector.StartsWith(".")
                || selector.StartsWith("#")
                || selector.StartsWith(":")
                || selector.StartsWith("*")
                || selector.Contains(" ")
                || selector.Contains(">")
                || selector.Contains("+")
                || selector.Contains("~");
        }
    }
}
