using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace RicTools.Utilities
{
    public static class RicUtilities
    {
        public static void CreateAssetFolder(string folderPath)
        {
#if UNITY_EDITOR
            if (Path.HasExtension(folderPath))
                folderPath = Path.GetDirectoryName(folderPath);
            folderPath = folderPath.Replace("\\", "/");
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                string[] dirs = folderPath.Split('/');
                string path = dirs[0];
                for (int i = 1; i < dirs.Length; i++)
                {
                    if (!AssetDatabase.IsValidFolder(path + $"/{dirs[i]}"))
                    {
                        AssetDatabase.CreateFolder(path, dirs[i]);
                    }
                    path += $"/{dirs[i]}";
                }
            }
#endif
        }

        // https://stackoverflow.com/questions/457676/check-if-a-class-is-derived-from-a-generic-class
        public static bool IsSubclassOfRawGeneric(System.Type generic, System.Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        public static string GetScriptableObjectPath(System.Type type)
        {
            var name = type.Name;
            name = name.Replace("ScriptableObject", "");
            return $"{PathConstants.ASSETS_FOLDER}/{PathConstants.SCRIPTABLES_FOLDER}/{name}s";
        }

        public static void Set<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }
    }
}
