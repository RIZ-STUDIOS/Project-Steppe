using ProjectSteppe.Entities;
using RicTools.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectSteppe
{
    public class Weapon : MonoBehaviour
    {
        //[SerializeField, FormerlySerializedAs("weapon")] private WeaponScriptableObject weaponSo;
        [SerializeField, FormerlySerializedAs("collisions")] private List<Collider> weaponColliders;

        [SerializeField, ReadOnly(AvailableMode.Play)]
        private bool startEnabled;

        public bool WeaponEnabled => _weaponEnabled;

        [System.NonSerialized]
        public Entity parentEntity;

        private bool _weaponEnabled;

        [SerializeField] private Entity hitEntity;

        [SerializeField] private float ignoreTimer = 0.5f;

        public System.Action onBlock;
        public System.Action onParry;
        public System.Action onBlockEnd;

        private Coroutine ignoreHitCoroutine;

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

        private IEnumerator AttackActivator(float startTime, float endTime)
        {
            yield return Delay(startTime);
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
            var destructible = other.GetComponent<DestructibleProp>();
            if (destructible) destructible.ToppleProp(parentEntity.transform.forward);

            var hitbox = other.GetComponent<HitBox>();
            if (!hitbox) return;
            if (!hitbox.IsValidHit(parentEntity)) return;
            if (hitbox.ParentEntity == hitEntity) return;

            Debug.Log("Hit " + other.name);

            float hitAngle = Vector3.Angle(hitbox.ParentEntity.transform.forward, parentEntity.transform.forward);

            if (hitbox.ParentEntity.EntityHealth.IsInvicible()) return;

            // Damage
            if (hitbox.ParentEntity.EntityBlock.IsBlocking && hitAngle > 90)
            {
                hitbox.ParentEntity.EntityBlock.ChangeBlockColor(hitbox.ParentEntity.EntityBlock.IsPerfectBlock());

                if (!hitbox.ParentEntity.EntityBlock.IsPerfectBlock())
                {
                    hitbox.ParentEntity.EntityHealth.DamageBalance(parentEntity.EntityAttacking.currentAttack.balanceDamage);
                    hitbox.ParentEntity.EntityBlock.OnBlockAttack.Invoke();
                }
                else
                {
                    parentEntity.EntityHealth.DamageBalance(hitbox.ParentEntity.EntityAttacking.currentAttack.balanceDamage * (1 - hitbox.ParentEntity.EntityAttacking.currentAttack.balanceBlockPassthrough));
                    hitbox.ParentEntity.EntityHealth.DamageBalance(hitbox.ParentEntity.EntityAttacking.currentAttack.balanceDamage * hitbox.ParentEntity.EntityAttacking.currentAttack.balanceBlockPassthrough);
                    if (hitbox.ParentEntity.EntityAttacking.currentAttack.balanceBlockPassthrough == 0)
                    {
                        hitbox.ParentEntity.EntityAttacking.CurrentWeapon.onParry?.Invoke();
                        hitbox.ParentEntity.EntityBlock.OnParryAttack.Invoke();
                    }
                }

                hitbox.ParentEntity.EntityBlock.PlayBlockFX();

                hitbox.ParentEntity.EntityAttacking.CurrentWeapon.onBlockEnd?.Invoke();
            }
            else
            {
                hitbox.ParentEntity.EntityHealth.DamageHealth(parentEntity.EntityAttacking.currentAttack.healthDamage);
                hitbox.ParentEntity.EntityHealth.DamageBalance(parentEntity.EntityAttacking.currentAttack.balanceDamage);
            }

            hitEntity = hitbox.ParentEntity;

            if (ignoreHitCoroutine != null) StopCoroutine(ignoreHitCoroutine);
            ignoreHitCoroutine = StartCoroutine(IgnoreHits());
        }

        private IEnumerator IgnoreHits()
        {
            float timer = 0;
            while (timer < ignoreTimer)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            hitEntity = null;
        }
    }
}
