using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    [CreateAssetMenu(menuName = "New AI State/Idle", order = 3)]
    public class AIIdle : AIState
    {
        public override AIState Tick(AIController controller)
        {
            controller.playerTarget = controller.combatController.playerTarget;
            if (controller.playerTarget != null)
            {
                Debug.Log("Player Targetted");
                return SwitchState(controller, controller.chase);
            }
            else
            {
                Debug.Log("No Targets");
                return this;
            }
        }
    }
}
