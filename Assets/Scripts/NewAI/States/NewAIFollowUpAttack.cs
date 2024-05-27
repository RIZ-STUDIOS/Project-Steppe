using System.Collections;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName = "AI/New States/Follow Up Attack")]
    public class NewAIFollowUpAttack : NewAIAttackState
    {
        [SerializeField]
        private string animatorVariable;

        [SerializeField]
        private float attackDuration;

        [SerializeField]
        private string comboName;

        [SerializeField]
        private bool removeCombo;

        private Coroutine attackCoroutine;

        public override void Execute()
        {
            controller.NavMeshAgent.isStopped = true;
            controller.animator.SetTrigger("ForceAnimation");
            controller.animator.SetTrigger(animatorVariable);
            attackCoroutine = controller.StartCoroutine(AttackDurationCoroutine());
        }

        private IEnumerator AttackDurationCoroutine()
        {
            yield return new WaitForSeconds(attackDuration);
            attackFinished = true;
            controller.NavMeshAgent.nextPosition = controller.transform.position;
            controller.NavMeshAgent.isStopped = false;
            if (!string.IsNullOrEmpty(comboName) && removeCombo)
                attackHandler.unusuableStates.Remove(comboName);
        }

        public override bool UseAttack()
        {
            return attackHandler.unusuableStates.Contains(comboName);
        }

        public override void OnForceExit()
        {
            if (attackCoroutine != null)
            {
                controller.StopCoroutine(attackCoroutine);
                controller.NavMeshAgent.nextPosition = controller.transform.position;
                controller.NavMeshAgent.isStopped = false;
                if (!string.IsNullOrEmpty(comboName) && removeCombo)
                    attackHandler.unusuableStates.Remove(comboName);
            }
        }
    }
}
