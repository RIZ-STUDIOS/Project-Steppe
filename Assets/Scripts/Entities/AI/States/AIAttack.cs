using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    [CreateAssetMenu(menuName = "AI/State/Attack", order = 4)]
    public class AIAttack : AIState
    {
        public override AIState Tick(AIController controller)
        {
            if (controller.distanceFromTarget > controller.navmesh.stoppingDistance)
            {
                return SwitchState(controller, controller.chase);
            }            

            return SwitchState(controller, controller.stance);
        }
    }
}
