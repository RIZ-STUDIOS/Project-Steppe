using ProjectSteppe.Entities;
using System.Collections;
using UnityEngine;

namespace ProjectSteppe.Items.UsableItems
{
    public class HealingUsableItem : UsableItem
    {
        public float healAmount;
        public float healSpeed;

        private bool healing;

        private Animator animator;

        private EntityHealth entityHealth;

        private void Awake()
        {
            animator = GetComponentInParent<Animator>();
            entityHealth = GetComponentInParent<EntityHealth>();
        }

        public override void OnUse()
        {
            if (healing || entityHealth.Health >= entityHealth.MaxHealth) return;

            bool canUse = CanUseQuery();

            base.OnUse();
            if (!canUse) return;
            //animator.SetTrigger("ForceAnimation");
            animator.SetTrigger("Heal");
            StartCoroutine(HealEntity());
        }

        private IEnumerator HealEntity()
        {
            healing = true;
            float heal = 0;

            float healPerFrame;
            while (heal < healAmount)
            {
                healPerFrame = healAmount / healSpeed * Time.deltaTime;
                heal += healPerFrame;

                entityHealth.HealHealth(healPerFrame);

                yield return null;
            }
        }

        // Heal Animation

        public override void OnAnimationEnd()
        {
            healing = false;
        }
    }
}
