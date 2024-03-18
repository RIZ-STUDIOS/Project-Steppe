using UnityEngine;

namespace ProjectSteppe.Items
{
    public abstract class UsableItem : MonoBehaviour
    {
        public string title;
        public int charges;

        public System.Action onUse;

        public bool CanUseQuery()
        {
            if (charges > 0 || charges == -1) return true;

            return false;
        }

        public virtual void OnUse()
        {
            if (!CanUseQuery()) return;

            if (charges != -1)
            {
                charges--;
                if (charges < 0) charges = 0;
            }

            onUse?.Invoke();
        }

        public virtual void OnAnimationEnd()
        {

        }
    }
}
