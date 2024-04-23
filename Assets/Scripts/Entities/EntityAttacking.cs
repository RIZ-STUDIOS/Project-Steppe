using ProjectSteppe.ScriptableObjects;
using UnityEngine;

namespace ProjectSteppe.Entities
{
    public class EntityAttacking : EntityBehaviour
    {
        private Weapon currentWeapon;

        [SerializeField]
        private Weapon startWeapon;

        public Weapon CurrentWeapon => currentWeapon;

        public AttackScriptableObject currentAttack;

        private void Start()
        {
            SetWeapon(startWeapon);
        }

        public void SetWeapon(Weapon weapon)
        {
            if (currentWeapon)
            {
                currentWeapon.parentEntity = null;
            }

            currentWeapon = weapon;
            if (currentWeapon)
                currentWeapon.parentEntity = Entity;
        }

        public void EnableWeaponCollision()
        {
            if (!currentWeapon) return;

            currentWeapon.EnableColliders();
        }

        public void DisableWeaponCollision()
        {
            if (!currentWeapon) return;

            currentWeapon.DisableColliders();
        }
    }
}
