using RicTools.Editor.Windows;
using RicTools.ScriptableObjects;
using RicTools.Utilities;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace RicTools.Editor.Utilities
{
    internal class GenericScriptableObjectProcessing : UnityEditor.AssetModificationProcessor
    {
        public static AssetDeleteResult OnWillDeleteAsset(string AssetPath, RemoveAssetOptions rao)
        {
            var temp = AssetDatabase.LoadMainAssetAtPath(AssetPath);
            if (temp == null) return AssetDeleteResult.DidNotDelete;
            if (temp is GenericScriptableObject asset)
            {
                var openedWindows = Resources.FindObjectsOfTypeAll<EditorWindow>();

                foreach (var window in openedWindows)
                {
                    var type = window.GetType();
                    if (!RicUtilities.IsSubclassOfRawGeneric(typeof(GenericEditorWindow<>), type)) continue;
                    var genericTypeArguments = type.BaseType.GetTypeInfo().GenericTypeArguments;
                    if (genericTypeArguments.Length < 1) continue;
                    if (!RicUtilities.IsSubclassOfRawGeneric(genericTypeArguments[0], asset.GetType())) continue;
                    var instanceField = type.GetFieldRecursive("instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                    if (instanceField == null) continue;
                    var instanceValue = instanceField.GetValue(null);
                    if (instanceValue == null) continue;
                    asset.setForDeletion = true;
                    EditorUtility.SetDirty(asset);
                    type.GetMethodRecursive("ReloadAssetList", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Invoke(instanceValue, new object[] { });
                    break;
                }
            }

            return AssetDeleteResult.DidNotDelete;
        }

        [OnOpenAsset]
        private static bool OnOpenAsset(int instanceId, int line)
        {
            var temp = EditorUtility.InstanceIDToObject(instanceId) as GenericScriptableObject;
            if (temp == null) return false;
            return OpenScriptableObject(temp);
        }

        private static bool OpenScriptableObject(GenericScriptableObject asset)
        {
            var editorType = ToolUtilities.GetCustomEditorType(asset.GetType());
            if (editorType == null) return false;
            var showWindow = editorType.GetMethod("ShowWindow", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            if (showWindow == null)
            {
                Debug.LogError(editorType + " has no ShowWindow static function");
                return false;
            }
            var temp = showWindow.Invoke(null, null);
            var data = System.Convert.ChangeType(temp, editorType);
            editorType.GetMethodRecursive("LoadGUID", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Invoke(data, new object[] { asset.guid });
            return true;
        }
    }
}
