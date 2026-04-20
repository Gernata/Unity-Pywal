using UnityEditor;
using UnityEngine;

namespace UnityPywal.Editor
{
    [InitializeOnLoad]
    internal static class PywalSelectionColorApplier
    {
        private static bool pending;
        private static bool hooked;
        private static Color selectionColor;

        static PywalSelectionColorApplier()
        {
            EnsureHooks();
        }

        public static void Queue(Color color)
        {
            selectionColor = color;
            pending = true;
            EnsureHooks();
        }

        private static void EnsureHooks()
        {
            if (hooked)
            {
                return;
            }

            hooked = true;
            SceneView.duringSceneGui += OnSceneGui;
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGui;
#pragma warning disable CS0618
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemGui;
#pragma warning restore CS0618
        }

        private static void OnSceneGui(SceneView sceneView)
        {
            ApplyPendingSelectionColor();
        }

        private static void OnProjectWindowItemGui(string guid, Rect selectionRect)
        {
            ApplyPendingSelectionColor();
        }

        private static void OnHierarchyWindowItemGui(int instanceId, Rect selectionRect)
        {
            ApplyPendingSelectionColor();
        }

        private static void ApplyPendingSelectionColor()
        {
            if (!pending || GUI.skin?.settings == null)
            {
                return;
            }

            GUI.skin.settings.selectionColor = selectionColor;
            pending = false;
        }
    }
}
