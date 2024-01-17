using RicTools.Utilities;
using UnityEditor;
using UnityEngine;

namespace RicTools.Editor.Settings
{
    [System.Serializable]
    [ExcludeFromPreset]
    internal class RicTools_EditorSettings : ScriptableObject
    {
        private static RicTools_EditorSettings s_Instance;

        public static string version
        {
            get { return "2.0.3"; }
        }

        public static RicTools_EditorSettings instance
        {
            get
            {
                if (RicTools_EditorSettings.s_Instance == null)
                {
                    RicTools_EditorSettings.s_Instance = Resources.Load<RicTools_EditorSettings>(PathConstants.EDITOR_SETTINGS_FILE_PATH);
                    if (!s_Instance)
                    {
                        s_Instance = ScriptableObject.CreateInstance<RicTools_EditorSettings>();
                        RicUtilities.CreateAssetFolder(PathConstants.EDITOR_SETTINGS_PATH);
                        AssetDatabase.CreateAsset(s_Instance, $"{PathConstants.EDITOR_SETTINGS_PATH}/{PathConstants.EDITOR_SETTINGS_NAME}.asset");
                        AssetDatabase.SaveAssets();
                    }
                }

                return RicTools_EditorSettings.s_Instance;
            }
        }

        public static RicTools_EditorSettings LoadDefaultSettings()
        {
            if (s_Instance == null)
            {
                RicTools_EditorSettings settings = Resources.Load<RicTools_EditorSettings>(PathConstants.EDITOR_SETTINGS_FILE_PATH);
                if (settings != null)
                    s_Instance = settings;
            }

            return s_Instance;
        }

        public static RicTools_EditorSettings GetSettings()
        {
            if (RicTools_EditorSettings.instance == null) return null;

            return RicTools_EditorSettings.instance;
        }

        public static SerializedObject GetSerializedObject()
        {
            return new SerializedObject(instance);
        }
    }
}
