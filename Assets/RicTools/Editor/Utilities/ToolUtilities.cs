using RicTools.Editor.Windows;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace RicTools.Editor.Utilities
{
    public static class ToolUtilities
    {
        public static bool TryGetActiveFolderPath(out string path)
        {
            var _tryGetActiveFolderPath = typeof(ProjectWindowUtil).GetMethod("TryGetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic);

            object[] args = new object[] { null };
            bool found = (bool)_tryGetActiveFolderPath.Invoke(null, args);
            if (found)
                path = (string)args[0];
            else
                path = "Assets";

            return found;
        }

        public static string GetActiveFolderPath()
        {
            return (string)typeof(ProjectWindowUtil).GetMethod("GetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, null);
        }

        public static string GetUniquePathNameAtSelectedPath(string fileName)
        {
            return (string)typeof(AssetDatabase).GetMethod("GetUniquePathNameAtSelectedPath", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[] { fileName });
        }

        // https://gist.github.com/allanolivei/9260107
        public static string GetSelectedPathOrFallback()
        {
            string path = "Assets";

            foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                    break;
                }
            }
            return path;
        }

        public static void AddStylesheet(this VisualElement root, params string[] styleSheets)
        {
            foreach (var sheet in styleSheets)
            {
                var stylesheet = (StyleSheet)EditorGUIUtility.Load(sheet);

                if (stylesheet == null)
                {
                    Debug.LogError($"Couldnt load stylesheet: '{sheet}'");
                    continue;
                }

                root.styleSheets.Add(stylesheet);
            }
        }

        public static List<AssetData<T>> FindAssetsByType<T>() where T : Object
        {
            var assets = AssetDatabase.FindAssets($"t:{typeof(T).Name}");

            var assetsList = new List<AssetData<T>>();

            foreach (var assetGuid in assets)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
                var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                if (asset)
                    assetsList.Add(new AssetData<T>(asset, assetPath));
            }

            return assetsList;
        }

        internal static bool HasCustomEditor(System.Type type)
        {
            return GetCustomEditorType(type) != null;
        }

        internal static System.Type GetCustomEditorType(System.Type type)
        {
            var editorTypes = GetCustomEditorTypes();
            foreach (var editorType in editorTypes)
            {
                var genericTypeArguments = editorType.GetTypeInfo().BaseType.GetTypeInfo().GenericTypeArguments;
                if (genericTypeArguments.Length == 0) continue;
                if (genericTypeArguments[0] == type || type.IsSubclassOf(genericTypeArguments[0])) return editorType;
            }

            return null;
        }

        internal static List<System.Type> GetCustomEditorTypes()
        {
            return TypeCache.GetTypesDerivedFrom(typeof(GenericEditorWindow<>)).ToList();
        }
    }

    public sealed class AssetData<T>
    {
        public T asset;
        public string fileLocation;

        internal AssetData(T asset, string fileLocation)
        {
            this.asset = asset;
            this.fileLocation = fileLocation;
        }
    }
}
