using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName = "AI/New States/Simple Attack")]
    public class NewAISimpleAttack : NewAIAttackState
    {
        [SerializeField]
        private string animatorVariable;

        [SerializeField]
        private float attackDuration;

        [SerializeField]
        private string comboName;

        private Coroutine attackCoroutine;

        [SerializeField]
        private bool removeCombo;

        public override void Execute()
        {
            if(!string.IsNullOrEmpty(comboName))
            attackHandler.unusuableStates.Add(comboName);
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
            return string.IsNullOrEmpty(comboName) || !attackHandler.unusuableStates.Contains(comboName);
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
