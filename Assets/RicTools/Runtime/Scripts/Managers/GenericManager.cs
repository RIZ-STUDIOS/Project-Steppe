using UnityEngine;

namespace RicTools.Managers
{
    /// <summary>
    /// A generic manager that can be expanded on by other classes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GenericManager<T> : MonoBehaviour where T : GenericManager<T>
    {
        private static T _instance;

        public static T Instance => _instance;

        protected virtual bool DestroyIfFound => true;

        protected virtual bool DontDestroyManagerOnLoad => false;

        protected virtual void Awake()
        {
            SetInstance();
        }

        protected bool SetInstance()
        {
            if (_instance != null && DestroyIfFound && _instance != this)
            {
                Destroy(this);
                return false;
            }
            if (DontDestroyManagerOnLoad)
                DontDestroyOnLoad(gameObject);
            _instance = this as T;
            return true;
        }
    }
}
