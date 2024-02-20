using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public static class MonoUtilities
    {
        public static T GetComponentIfNull<T>(this Behaviour behaviour, ref T component, bool includeChildren = true, bool includeInactive = true) where T : Behaviour
        {
            if (component == null)
            {
                if (includeChildren)
                    component = behaviour.GetComponentInChildren<T>(includeInactive);
                else
                    component = behaviour.GetComponent<T>();
            }

            return component;
        }
        public static T[] GetComponentsIfNull<T>(this Behaviour behaviour, ref T[] component, bool includeChildren = true, bool includeInactive = true) where T : Behaviour
        {
            if (component == null)
            {
                if (includeChildren)
                    component = behaviour.GetComponentsInChildren<T>(includeInactive);
                else
                    component = behaviour.GetComponents<T>();
            }

            return component;
        }
    }
}
