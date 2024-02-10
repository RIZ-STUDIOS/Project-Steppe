using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    [CreateAssetMenu(menuName = "New AI State/Attack", order = 4)]
    public class AIAttack : AIState
    {
        public override AIState Tick(AIController controller)
        {
            Debug.Log("iphone");

            if (Vector3.Distance(controller.transform.position, controller.playerTarget.position) > controller.navmesh.stoppingDistance)
            {
                return SwitchState(controller, controller.chase);
            }

            return this;
        }
    }
}
