using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    [CreateAssetMenu(menuName = "AI/State/Attack Stance", order = 3)]
    public class AIAttackStance : AIState
    {
        public List<AIAttackActionScriptableObject> attacks;
        protected List<AIAttackActionScriptableObject> potentialAttacks;
        private AIAttackActionScriptableObject currentAttack;
        private AIAttackActionScriptableObject previousAttack;

        protected bool hasValidAttack = false;
        protected bool canCombo = false;
        protected int chanceToCombo = 25;
        bool hasRolledCombo = false;

        protected float minDistanceFromTarget = 5f;

        public override AIState Tick(AIController controller)
        {
            if (!controller.navmesh.enabled)
                controller.navmesh.enabled = true;

            if (controller.playerTarget == null)
                return SwitchState(controller, controller.idle);

            if (!hasValidAttack)
            {
                NewAttack(controller);
            }

            if (controller.distanceFromTarget > minDistanceFromTarget)
                return SwitchState(controller, controller.chase);

            return this;
        }

        public virtual void NewAttack(AIController controller)
        {
            potentialAttacks = new List<AIAttackActionScriptableObject>();

            foreach (var potentialAttack in potentialAttacks)
            {
                if (potentialAttack.minDistanceToTarget > controller.distanceFromTarget)
                    continue;
                if (potentialAttack.maxDistanceToTarget < controller.distanceFromTarget)
                    continue;

                potentialAttacks.Add(potentialAttack);
            }

            if (potentialAttacks.Count <= 0) return;

            float totalWeight = 0;

            foreach (var attack in potentialAttacks)
            {
                totalWeight += attack.weight;
            }

            float randomWeightValue = Random.Range(1, totalWeight + 1);
            float processed = 0;

            foreach (var attack in potentialAttacks)
            {
                processed = attack.weight;

                if(randomWeightValue <= processed)
                {
                    currentAttack = attack;
                    previousAttack = currentAttack;
                    hasValidAttack = true;
                }
            }
        }

        protected virtual bool RollForCombo(int chance)
        {
            int roll = Random.Range(0, 100);
            if (roll < chanceToCombo)
            {
                return true;
            }
            return false;
        }

        protected override void ResetState(AIController controller)
        {
            base.ResetState(controller);
            hasRolledCombo = false;
            hasValidAttack = false;
        }
    }
}
