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
            float animationLength = 0;
            foreach (var animationData in animationDatas)
            {
                controller.animator.SetTrigger(animationData.triggerName);
                animationLength = animationData.animationLength;
            }
            foreach(var animationData in animationDatas)
            {
                yield return new WaitForSeconds(animationData.animationLength);
                controller.transform.LookAt(controller.targetTransform);
                var rot = controller.transform.eulerAngles;
                rot.x = 0;
                rot.z = 0;
                controller.transform.eulerAngles = rot;
            }
                //yield return new WaitForSeconds(animationLength);
            controller.NavMeshAgent.nextPosition = controller.transform.position;
            controller.NavMeshAgent.isStopped = false;
            FinishAttack();
        }

        public override bool UseAttack()
        {
            return true;
        }

        public override void OnForceExit()
        {
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
        }
    }
}
