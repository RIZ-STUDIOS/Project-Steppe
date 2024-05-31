using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName ="AI/States/Chase")]
    public class AIChaseState : AIState
    {
        [SerializeField]
        private AIState idleState;

        [SerializeField]
        private AIState attackHandlerState;

        public override void OnEnter()
        {
            controller.animator.SetBool("Chase", true);
        }

        public override void Execute()
        {
            if (!controller.targetTransform)
            {
                controller.SwitchAIState(idleState);
                return;
            }

            if(Vector3.Distance(controller.transform.position, controller.targetTransform.transform.position) <= controller.distanceToTargetToAttack)
            {
                controller.SwitchAIState(attackHandlerState);
                return;
            }

            controller.SetPathToTarget();
        }

        public override void OnExit()
        {
            controller.animator.SetBool("Chase", false);
        }
    }
}
