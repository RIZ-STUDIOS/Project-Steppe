using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ProjectSteppe.Entities
{
    public class EntityCollision : MonoBehaviour
    {
        private EntityHealth entityHealth;

        public float damageBufferDuration = 0.1f;

        private bool canDamage = true;

        private void Awake()
        {
            entityHealth = GetComponent<EntityHealth>();
        }

        private void Start()
        {
            IgnoreSelfCollisions();
        }

        private void OnTriggerEnter(Collider other)
        {
            var weapon = other.GetComponent<Weapon>();
            if (weapon != null)
            {
                if (canDamage) entityHealth.DamagePosture(20);
                weapon.DisableColliders();
            }
        }

        private IEnumerator DamageBuffer()
        {
            canDamage = false;
            float timer = 0;
            while (timer < damageBufferDuration)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            canDamage = true;
        }

        private void IgnoreSelfCollisions()
        {
            Collider[] entityCollisions = GetComponentsInChildren<Collider>();
            List<Collider> ignoreColliders = new List<Collider>(entityCollisions);

            foreach (var collider in ignoreColliders)
            {
                foreach (var otherCollider in ignoreColliders)
                {
                    Physics.IgnoreCollision(collider, otherCollider, true);
                }
            }
        }
    }
}
