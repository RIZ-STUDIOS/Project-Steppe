using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName = "AI/States/Simple Attack")]
    public class AISimpleAttack : AIAttackState
    {
        [SerializeField]
        [FormerlySerializedAs("animatorVariable")]
        private string triggerName;

        [SerializeField]
        private float attackDuration;

        private Coroutine attackCoroutine;

        public override void Execute()
        {
            controller.NavMeshAgent.isStopped = true;
            controller.animator.SetTrigger("ForceAnimation");
            controller.animator.SetTrigger(triggerName);
            attackCoroutine = controller.StartCoroutine(AttackDurationCoroutine());
        }

        private IEnumerator AttackDurationCoroutine()
        {
            if (controller.AIEntity.EntityAttacking.currentAttack.balanceBlockPassthrough)
                controller.TurnOnUnblockable();
            yield return new WaitForSeconds(attackDuration);
                controller.TurnOffUnblockable();
            attackFinished = true;
            controller.NavMeshAgent.nextPosition = controller.transform.position;
            controller.NavMeshAgent.isStopped = false;
        }

        public override bool CanUseAttack()
        {
            return true;
        }

        public override void OnForceExit()
        {
            base.OnForceExit();
            if (attackCoroutine != null)
            {
                controller.StopCoroutine(attackCoroutine);
                controller.NavMeshAgent.nextPosition = controller.transform.position;
                if(controller.NavMeshAgent.enabled)
                controller.NavMeshAgent.isStopped = false;
            }
        }
    }
}
