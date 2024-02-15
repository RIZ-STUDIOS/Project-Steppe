using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectSteppe
{
    [CreateAssetMenu(menuName = "AI/State/Chase", order = 2)]
    public class AIChase : AIState
    {
        public override AIState Tick(AIController controller)
        {
            controller.playerTarget = controller.combatController.playerTarget;

            if (controller.playerTarget == null)
            {
                if (controller.debugEnabled)
                    Debug.Log("Returning to idle");
                controller.navmesh.isStopped = true;
                return SwitchState(controller, controller.idle);
            }

            if (controller.navmesh.isStopped)
            {
                controller.navmesh.isStopped = false;
            }

            if (!controller.isMoving && controller.distanceFromTarget < controller.navmesh.stoppingDistance)
            {
                return SwitchState(controller, controller.attack);
            }

            NavMeshPath path = new();
            controller.navmesh.CalculatePath(controller.playerTarget.position, path);
            controller.navmesh.SetPath(path);
            controller.animator.RotateToTarget(controller);
            return this;
        }
    }
}
