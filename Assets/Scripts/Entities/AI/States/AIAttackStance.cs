using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectSteppe
{
    [CreateAssetMenu(menuName = "AI/State/Attack Stance", order = 3)]
    public class AIAttackStance : AIState
    {
        public List<AIAttackActionScriptableObject> attacks;
        protected List<AIAttackActionScriptableObject> potentialAttacks;
        private AIAttackActionScriptableObject currentAttack;
        private AIAttackActionScriptableObject previousAttack;

        public bool hasValidAttack = false;
        protected bool canCombo = false;
        protected int chanceToCombo = 25;
        bool hasRolledCombo = false;

        protected float minDistanceFromTarget = 2f;

        public override AIState Tick(AIController controller)
        {
            if (!controller.navmesh.enabled)
                controller.navmesh.enabled = true;

            if (controller.playerTarget == null)
                return SwitchState(controller, controller.idle);

            var dir = controller.playerTarget.transform.position - controller.transform.position;
            Debug.DrawRay(controller.transform.position, dir * 5, Color.green);

            controller.transform.forward = Vector3.Lerp(controller.transform.forward, dir, 3 * Time.fixedDeltaTime).normalized;
            
            /*var qEul = Quaternion.Euler(dir);

            var q = Quaternion.Lerp(controller.transform.rotation, qEul, .1f);

            controller.transform.rotation = qEul;*/

            //if (controller.canRotate) controller.transform.LookAt(controller.playerTarget.transform);
            
            if (!hasValidAttack || hasValidAttack && controller.combatController.currentRecoveryTime < 0)
            {
                if (controller.debugEnabled)
                    Debug.Log("Selecting an Attack...");
                NewAttack(controller);
            }

            if (controller.distanceFromTarget > minDistanceFromTarget)
            {
                if (hasValidAttack && controller.combatController.currentRecoveryTime > 0)
                {
                    return SwitchState(controller, controller.idle);
                }
                return SwitchState(controller, controller.chase);
            }

            return this;
        }

        public virtual void NewAttack(AIController controller)
        {
            potentialAttacks = new List<AIAttackActionScriptableObject>();

            foreach (var potentialAttack in attacks)
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
                    if (controller.debugEnabled)
                        Debug.Log("Selected attack " + currentAttack.attackAnimation);
                    PlayAttack(controller);
                }
            }
        }

        private void PlayAttack(AIController controller)
        {
            controller.animator.PlayTargetAnimation(currentAttack.attackAnimation);
            controller.combatController.AddRecovery(currentAttack.attackRecovery);
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
