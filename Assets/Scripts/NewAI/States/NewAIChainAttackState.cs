using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName ="AI/New States/Chain Attack State")]
    public class NewAIChainAttackState : NewAIAttackState
    {
        [SerializeField]
        private AnimationData[] animationDatas;

        private Coroutine attackCoroutine;

        public override void Execute()
        {
            controller.NavMeshAgent.isStopped = true;
            attackCoroutine = controller.StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            controller.animator.SetTrigger("ForceAnimation");
            foreach (var animationData in animationDatas)
            {
                if(!string.IsNullOrEmpty(animationData.triggerName))
                controller.animator.SetTrigger(animationData.triggerName);
            }
            foreach(var animationData in animationDatas)
            {
                yield return new WaitForSeconds(animationData.animationLength);
                if (animationData.canRotateAfter)
                {
                    controller.RotateTowards(controller.targetTransform);
                }
            }
            controller.NavMeshAgent.nextPosition = controller.transform.position;
            controller.NavMeshAgent.isStopped = false;
            FinishAttack();
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
                FinishAttack();
            }
        }

        [System.Serializable]
        public struct AnimationData
        {
            public string triggerName;
            public float animationLength;
            public bool canRotateAfter;
        }
    }
}
