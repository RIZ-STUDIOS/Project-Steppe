using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI.States
{
    [CreateAssetMenu(menuName = "AI/States/Idle")]
    public class AIIdleState : AIState
    {
        [SerializeField]
        private AIState chaseState;

        public override void Execute()
        {
            if(controller.NavMeshAgent.hasPath) controller.NavMeshAgent.ResetPath();

            if (controller.targetEntity && controller.targetEntity.EntityHealth.Health > 0)
            {
                controller.SwitchAIState(chaseState);
            }
        }
    }
}
