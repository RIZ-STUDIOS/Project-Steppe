using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

namespace ProjectSteppe
{
    public class AICombatController : MonoBehaviour
    {
        public Transform playerTarget;
        public float currentRecoveryTime = 0f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<ThirdPersonController>())
            {
                playerTarget = other.transform;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<ThirdPersonController>())
            {
                playerTarget = null;
            }
        }

        public void ManageRecovery(AIController controller)
        {
            if (currentRecoveryTime > 0)
            {
                if (!controller.isMoving)
                {
                    currentRecoveryTime -= Time.deltaTime;
                }
            }
        }

        public void AddRecovery(float time)
        {
            currentRecoveryTime += time;
        }
    }
}
