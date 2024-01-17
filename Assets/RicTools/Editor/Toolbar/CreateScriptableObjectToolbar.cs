using RicTools.Editor.Windows;
using UnityEditor;
using UnityEngine;

namespace RicTools.Editor.ToolbarMenuItems
{
    internal static class CreateScriptableObjectToolbar
    {
        [MenuItem("RicTools/Create New Scriptable Object", priority = 1)]
        public static void CreateAvailableScripts()
        {
            var window = ScriptableObject.CreateInstance<CreateScriptableObjectEditorWindow>();
            window.useCurrentProjectLocation = true;
            window.ShowUtility();
        }
    }
}
