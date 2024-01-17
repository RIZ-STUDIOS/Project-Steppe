using RicTools.Utilities;
using UnityEditor;
using UnityEngine;

namespace RicTools.Settings
{
    [System.Serializable]
    [ExcludeFromPreset]
    internal class RicTools_RuntimeSettings : ScriptableObject
    {
        private static RicTools_RuntimeSettings s_Instance;


        public static SingletonManager[] singletonManagers
        {
            get { return instance.m_singletonManagers; }
        }
        public SingletonManager[] m_singletonManagers = new SingletonManager[] { };

        public static SingletonPrefabManager[] singletonPrefabManagers
        {
            get { return instance.m_singletonPrefabManagers; }
        }
        public SingletonPrefabManager[] m_singletonPrefabManagers = new SingletonPrefabManager[] { };

        public static RicTools_RuntimeSettings instance
        {
            get
            {
                if (RicTools_RuntimeSettings.s_Instance == null)
                {
                    RicTools_RuntimeSettings.s_Instance = Resources.Load<RicTools_RuntimeSettings>(PathConstants.RUNTIME_SETTINGS_FILE_PATH);
#if UNITY_EDITOR
                    if (!s_Instance)
                    {
                        s_Instance = ScriptableObject.CreateInstance<RicTools_RuntimeSettings>();
                        RicUtilities.CreateAssetFolder(PathConstants.RUNTIME_SETTINGS_PATH);
                        AssetDatabase.CreateAsset(s_Instance, $"{PathConstants.RUNTIME_SETTINGS_PATH}/{PathConstants.RUNTIME_SETTINGS_NAME}.asset");
                        AssetDatabase.SaveAssets();
                    }
#endif
                }

                return RicTools_RuntimeSettings.s_Instance;
            }
        }

        public static RicTools_RuntimeSettings LoadDefaultSettings()
        {
            if (s_Instance == null)
            {
                RicTools_RuntimeSettings settings = Resources.Load<RicTools_RuntimeSettings>(PathConstants.RUNTIME_SETTINGS_FILE_PATH);
                if (settings != null)
                    s_Instance = settings;
            }

            return s_Instance;
        }

        public static RicTools_RuntimeSettings GetSettings()
        {
            if (RicTools_RuntimeSettings.instance == null) return null;

            return RicTools_RuntimeSettings.instance;
        }

#if UNITY_EDITOR
        public static SerializedObject GetSerializedObject()
        {
            return new SerializedObject(instance);
        }
#endif
    }
}
