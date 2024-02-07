using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class AIAnimator : MonoBehaviour
    {
        public Animator animController;

        public void RotateToTarget(AIController controller)
        {
            if (controller.isMoving)
            {
                animController.transform.rotation = controller.navmesh.transform.rotation;
            }
        }

        public void SetStates(AIController controller)
        {
            animController.SetBool("Chase", controller.isMoving);
            animController.SetBool("Idle", !controller.isMoving);
        }
    }
}
