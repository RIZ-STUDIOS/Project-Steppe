using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class DestroyOnRest : MonoBehaviour, IReloadable
    {
        public void OnReload()
        {
            Destroy(gameObject);
        }
    }
}
