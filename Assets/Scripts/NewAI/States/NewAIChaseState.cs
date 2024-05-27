using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName ="AI/New States/Chase")]
    public class NewAIChaseState : NewAIState
    {
        [SerializeField]
        private NewAIState idleState;

        [SerializeField]
        private NewAIState attackHandlerState;

        public override void Execute()
        {
            if (!controller.targetTransform)
            {
                controller.SwitchAIState(idleState);
                return;
            }

            if(Vector3.Distance(controller.transform.position, controller.targetTransform.position) <= controller.distanceToTargetToAttack)
            {
                controller.SwitchAIState(attackHandlerState);
                return;
            }

            controller.SetPathTo(controller.targetTransform);
        }
    }
}
