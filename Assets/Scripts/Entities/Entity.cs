using UnityEngine;

namespace ProjectSteppe.Entities
{
    public class Entity : MonoBehaviour
    {
        private Weapon currentWeapon;

        [SerializeField]
        private Weapon startWeapon;

        public Weapon CurrentWeapon => currentWeapon;

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
                currentWeapon.parentEntity = this;
        }
    }
}
