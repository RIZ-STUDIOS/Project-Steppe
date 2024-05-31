using UnityEngine;

namespace ProjectSteppe.Items
{
    public abstract class UsableItem : MonoBehaviour
    {
        public string title;
        private int _charges;

        public int Charges { get { return _charges; } set
            {
                _charges = value;
                onChargesChange?.Invoke();
            }
        }

        public System.Action onUse;
        public System.Action onChargesChange;

        public bool CanUseQuery()
        {
            if (Charges > 0 || Charges == -1) return true;

            return false;
        }

        public virtual void OnUse()
        {
            if (!CanUseQuery()) return;

            if (Charges != -1)
            {
                Charges--;
                if (Charges < 0) Charges = 0;
            }

            onUse?.Invoke();
        }

        public virtual void OnAnimationEnd()
        {

        }

        public virtual void Recharge()
        {

        }
    }
}
