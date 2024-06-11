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

        [SerializeField]
        private ParticleSystem trailFX;

        [SerializeField]
        private Renderer weaponMeshRenderer;

        private Color unblockableColor;

        private List<Material> weaponMaterials = new List<Material>();

        private Coroutine unblockCoroutine;

        [SerializeField]
        private Color normalTrailColor = new Color(1, 1, 1, .1f);

        [SerializeField]
        private Color unblockableTrailColor = new Color(1, .5f, 0, .1f);

        private void Awake()
        {
            if(!weaponMeshRenderer)
                weaponMeshRenderer = GetComponent<Renderer>();
            weaponMaterials.AddRange(weaponMeshRenderer.materials);
            unblockableColor = weaponMaterials[0].GetColor("_EmissionColor");
            ForceHideUnblockable();
        }

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
            trailFX.Play();
        }

        public void DisableColliders()
        {
            WeaponSwingAction(false);
            trailFX.Stop();
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
                    hitbox.ParentEntity.EntityHealth.DamageBalance(parentEntity.EntityAttacking.currentAttack.balanceDamage * parentEntity.EntityAttacking.damageMultiplier);

                    if (parentEntity.EntityAttacking.currentAttack.balanceBlockPassthrough)
                    {
                        hitbox.ParentEntity.EntityHealth.DamageHealth(parentEntity.EntityAttacking.currentAttack.healthDamage * parentEntity.EntityAttacking.damageMultiplier);
                    }
                    hitbox.ParentEntity.EntityBlock.OnBlockAttack.Invoke();
                }
                else
                {
                    if (parentEntity.EntityAttacking.currentAttack.balanceBlockPassthrough)
                    {
                        hitbox.ParentEntity.EntityHealth.DamageBalance(parentEntity.EntityAttacking.currentAttack.balanceDamage * parentEntity.EntityAttacking.damageMultiplier);
                        hitbox.ParentEntity.EntityHealth.DamageHealth(parentEntity.EntityAttacking.currentAttack.healthDamage * parentEntity.EntityAttacking.damageMultiplier);
                        hitbox.ParentEntity.EntityBlock.ChangeBlockColor(false);
                    }
                    else
                    {
                        parentEntity.EntityHealth.DamageBalance(parentEntity.EntityAttacking.currentAttack.perfectBlockBalanceDamage * parentEntity.EntityAttacking.damageMultiplier);
                        hitbox.ParentEntity.EntityAttacking.CurrentWeapon.onParry?.Invoke();
                        hitbox.ParentEntity.EntityBlock.OnParryAttack.Invoke();
                    }
                }

                hitbox.ParentEntity.EntityBlock.PlayBlockFX();

                hitbox.ParentEntity.EntityAttacking.CurrentWeapon.onBlockEnd?.Invoke();
            }
            else
            {
                hitbox.ParentEntity.EntityHealth.DamageHealth(parentEntity.EntityAttacking.currentAttack.healthDamage * parentEntity.EntityAttacking.damageMultiplier);
                hitbox.ParentEntity.EntityHealth.DamageBalance(parentEntity.EntityAttacking.currentAttack.balanceDamage * parentEntity.EntityAttacking.damageMultiplier);
            }

            hitEntity = hitbox.ParentEntity;

            if (ignoreHitCoroutine != null) StopCoroutine(ignoreHitCoroutine);
            ignoreHitCoroutine = StartCoroutine(IgnoreHits());
        }

        public void ResetHitTimer()
        {
            if (ignoreHitCoroutine != null) StopCoroutine(ignoreHitCoroutine);
            hitEntity = null;
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

        public void ShowUnblockable()
        {
            if (unblockCoroutine != null)
            {
                StopCoroutine(unblockCoroutine);
                unblockCoroutine = null;
            }
            unblockCoroutine = StartCoroutine(ShowUnblockableCoroutine());
        }

        private void ForceHideUnblockable()
        {
            foreach (var material in weaponMaterials)
            {
                //material.DisableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", Color.black);
            }
        }

        public void HideUnblockable()
        {
            if (unblockCoroutine != null)
            {
                StopCoroutine(unblockCoroutine);
                unblockCoroutine = null;
            }
            unblockCoroutine = StartCoroutine(HideUnblockableCoroutine());
        }

        private IEnumerator HideUnblockableCoroutine()
        {
            float timer = 0;
            var startColor = weaponMaterials[0].GetColor("_EmissionColor");
            while (timer < 1)
            {
                foreach (var material in weaponMaterials)
                {
                    material.SetColor("_EmissionColor", Color.Lerp(startColor, Color.black, timer));
                }
                timer += Time.deltaTime * 15;
                yield return null;
            }
            foreach (var material in weaponMaterials)
            {
                material.SetColor("_EmissionColor", Color.black);
            }

            var fxMain = trailFX.main;
            fxMain.startColor = normalTrailColor;
        }

        private IEnumerator ShowUnblockableCoroutine()
        {
            float timer = 0;
            var startColor = weaponMaterials[0].GetColor("_EmissionColor");

            while (timer < 1)
            {
                foreach (var material in weaponMaterials)
                {
                    material.SetColor("_EmissionColor", Color.Lerp(startColor, unblockableColor, timer));
                }
                timer += Time.deltaTime * 15;
                yield return null;
            }
            foreach (var material in weaponMaterials)
            {
                material.SetColor("_EmissionColor", unblockableColor);
            }

            var fxMain = trailFX.main;
            fxMain.startColor = unblockableTrailColor;
        }
    }
}
