using ProjectSteppe.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName = "AI/States/Block State")]
    public class BlockAIAttack : AIAttackState
    {
        [SerializeField]
        private string triggerName;

        [SerializeField]
        private float animationLength;

        private Coroutine attackCoroutine;

        public override bool CanUseAttack()
        {
            return true;
        }

        public override void Execute()
        {
            controller.AIEntity.EntityAttacking.DisableWeaponCollision();
            controller.AIEntity.EntityBlock.StartBlock();
            controller.NavMeshAgent.isStopped = true;
            controller.animator.SetTrigger("ForceAnimation");
            controller.animator.SetTrigger(triggerName);
            attackHandler.blocking = true;
            attackCoroutine = controller.StartCoroutine(AttackDurationCoroutine());
        }

        private IEnumerator AttackDurationCoroutine()
        {
            yield return new WaitForSeconds(animationLength);
            attackFinished = true;
            controller.NavMeshAgent.nextPosition = controller.transform.position;
            controller.NavMeshAgent.isStopped = false;
            controller.AIEntity.EntityBlock.EndBlock();
            attackHandler.blocking = false;
        }

        public override void OnForceExit()
        {
            base.OnForceExit();
            if (attackCoroutine != null)
            {
                controller.StopCoroutine(attackCoroutine);
                controller.NavMeshAgent.nextPosition = controller.transform.position;
                controller.NavMeshAgent.isStopped = false;
                controller.AIEntity.EntityBlock.EndBlock();
                attackHandler.blocking = false;
            }
        }
    }
}
