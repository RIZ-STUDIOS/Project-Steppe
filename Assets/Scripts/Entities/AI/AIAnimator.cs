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

        public void PlayTargetAnimation(string animationName)
        {
            animController.SetInteger("AttackOption", Random.Range(0,4));
            animController.SetInteger("FinisherOption", Random.Range(0,5));
            animController.SetBool("IsAttacking", true);
        }

        public void StoppedAttacking()
        {
            animController.SetBool("IsAttacking", false);
        }
    }
}
