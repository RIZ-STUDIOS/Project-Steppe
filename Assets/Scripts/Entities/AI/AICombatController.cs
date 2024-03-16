using ProjectSteppe.UI;
using UnityEngine;

namespace ProjectSteppe
{
    public class AICombatController : MonoBehaviour
    {
        public Transform playerTarget;
        public float currentRecoveryTime = 0f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerTarget = other.transform;
                
                other.GetComponentInChildren<BossUI>().ShowBossUI();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
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
                    currentRecoveryTime -= Time.fixedDeltaTime;
                }
            }
        }

        public void AddRecovery(float time)
        {
            currentRecoveryTime += time;
        }
    }
}
