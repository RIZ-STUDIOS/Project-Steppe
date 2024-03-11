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

        public override void OnUse()
        {
            if (healing) return;

            base.OnUse();
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

                GetComponentInParent<Entity>().EntityHealth.HealHealth(healPerFrame);

                yield return null;
            }

            healing = false;
        }
    }
}
