using ProjectSteppe.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName ="AI/States/Chain Attack State")]
    public class AIChainAttackState : AIAttackState
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
                controller.AIEntity.EntityAttacking.currentAttack = attackScriptableObject;
                if(animationData.attackScriptableObject)
                    controller.AIEntity.EntityAttacking.currentAttack = animationData.attackScriptableObject;
                yield return new WaitForSeconds(animationData.animationLength);
                if (animationData.canRotateAfter)
                {
                    controller.RotateTowards(controller.targetEntity.transform);
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
            public AttackScriptableObject attackScriptableObject;
            public bool canRotateAfter;
        }
    }
}
