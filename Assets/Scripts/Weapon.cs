using ProjectSteppe.Entities;
using ProjectSteppe.ScriptableObjects;
using RicTools.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectSteppe
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("weapon")] private WeaponScriptableObject weaponSo;
        [SerializeField, FormerlySerializedAs("collisions")] private List<Collider> weaponColliders;

        [SerializeField, ReadOnly(AvailableMode.Play)]
        private bool startEnabled;

        public bool WeaponEnabled => _weaponEnabled;

        [System.NonSerialized]
        public Entity parentEntity;

        private bool _weaponEnabled;

        private void Start()
        {
            foreach (var collider in weaponColliders)
            {
                collider.isTrigger = true;
            }

            WeaponSwingAction(startEnabled);
        }

        public void EnableColliders()
        {
            WeaponSwingAction(true);
        }

        public void DisableColliders()
        {
            WeaponSwingAction(false);
        }

        public void ToggleAttack(float startTime, float endTime)
        {
            StartCoroutine(AttackActivator(startTime, endTime));
        }

        private IEnumerator AttackActivator(float startTiem, float endTime)
        {
            yield return Delay(startTiem);
            EnableColliders();
            yield return Delay(endTime);
            DisableColliders();
        }

        private IEnumerator Delay(float duration)
        {
            yield return new WaitForSeconds(duration);
        }

        private void WeaponSwingAction(bool state)
        {
            _weaponEnabled = state;
            foreach (Collider col in weaponColliders)
            {
                col.enabled = _weaponEnabled;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var hitbox = other.GetComponent<HitBox>();
            if (!hitbox) return;
            if (!hitbox.IsValidHit(parentEntity)) return;

            Debug.Log("Hit " + other.name);

            // Damage
        }
    }
}
