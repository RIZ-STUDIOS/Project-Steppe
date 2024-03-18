using ProjectSteppe.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Items.UsableItems
{
    public class HealingUsableItem : UsableItem
    {
        public float healAmount;
        public float healSpeed;

        private bool healing;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponentInParent<Animator>();
        }

        public override void OnUse()
        {
            if (healing) return;

            bool canUse = CanUseQuery();

            base.OnUse();
            if(!canUse) return;
            animator.SetTrigger("ForceAnimation");
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

                GetComponentInParent<EntityHealth>().HealHealth(healPerFrame);

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
