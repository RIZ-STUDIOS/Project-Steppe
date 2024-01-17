using RicTools.ScriptableObjects;

namespace RicTools.Managers
{
    public abstract class DataGenericManager<ManagerType, DataType> : SingletonGenericManager<ManagerType> where ManagerType : DataGenericManager<ManagerType, DataType> where DataType : DataManagerScriptableObject
    {
        public DataType data;
    }
}
