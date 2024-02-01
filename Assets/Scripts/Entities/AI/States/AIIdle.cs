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
            if (controller.playerTarget != null)
            {
                Debug.Log("Player Targetted");
                return this;
            }
            else
            {
                Debug.Log("No Targets");
                controller.combatController.FindTarget(controller);
                return this;
            }
        }
    }
}
