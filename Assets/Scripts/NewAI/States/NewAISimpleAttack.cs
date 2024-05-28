using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName = "AI/New States/Simple Attack")]
    public class NewAISimpleAttack : NewAIAttackState
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
            yield return new WaitForSeconds(attackDuration);
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
                controller.NavMeshAgent.isStopped = false;
            }
        }
    }
}
