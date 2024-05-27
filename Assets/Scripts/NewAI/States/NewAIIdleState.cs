using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName = "AI/New States/Idle")]
    public class NewAIIdleState : NewAIState
    {
        [SerializeField]
        private NewAIState chaseState;

        public override void Execute()
        {
            if(controller.NavMeshAgent.hasPath) controller.NavMeshAgent.ResetPath();

            if (controller.targetTransform)
            {
                controller.SwitchAIState(chaseState);
            }
        }
    }
}
