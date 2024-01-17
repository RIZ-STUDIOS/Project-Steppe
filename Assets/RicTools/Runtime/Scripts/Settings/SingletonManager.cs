using RicTools.Managers;
using RicTools.ScriptableObjects;
using TypeReferences;
using UnityEngine;

namespace RicTools.Settings
{
    [System.Serializable]
    internal class SingletonManager
    {
        [Inherits(typeof(SingletonGenericManager<>), ShortName = true, AllowAbstract = false, IncludeBaseType = false, ShowAllTypes = true)]
        public TypeReference manager;

        public DataManagerScriptableObject data;
    }

    [System.Serializable]
    internal class SingletonPrefabManager
    {
        public GameObject prefab;
    }
}
