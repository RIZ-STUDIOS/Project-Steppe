using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

namespace ProjectSteppe
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponScriptableObject weapon;
        [SerializeField] private List<Collider> collisions;

        public void EnableWeapon()
        {
            WeaponSwingAction(true);
        }

        public void DisableWeapon()
        {
            WeaponSwingAction(false);
        }

        public void ToggleAttack(float duration)
        {
            StartCoroutine(AttackActivator(duration));
        }

        private IEnumerator AttackActivator(float duration)
        {
            EnableWeapon();
            yield return StartCoroutine(Delay(duration));
            DisableWeapon();
        }

        private IEnumerator Delay(float duration)
        {
            yield return new WaitForSeconds(duration);
        }

        private void WeaponSwingAction(bool state)
        {
            foreach (Collider col in collisions)
            {
                col.enabled = state;
            }
        }
    }
}
