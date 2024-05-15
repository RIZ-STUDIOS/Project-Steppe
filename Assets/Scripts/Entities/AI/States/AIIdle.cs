using UnityEngine;

namespace ProjectSteppe
{
    [CreateAssetMenu(menuName = "AI/State/Idle", order = 3)]
    public class AIIdle : AIState
    {
        public override AIState Tick(AIController controller)
        {
            controller.playerTarget = controller.combatController.playerTarget;
            if (controller.playerTarget != null)
            {
                if (controller.combatController.currentRecoveryTime > 0)
                    return this;
                if (controller.debugEnabled)
                    Debug.Log("Player Targetted");
                return SwitchState(controller, controller.chase);
            }
            else
            {
                if (controller.debugEnabled)
                    Debug.Log("No Targets");
                return this;
            }
        }
    }
}
