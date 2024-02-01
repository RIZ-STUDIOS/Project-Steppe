using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

namespace ProjectSteppe
{
    public class AICombatController : MonoBehaviour
    {
        [Header("Detection Settings")]
        [SerializeField] private float detectionRadius = 8.5f;

        public void FindTarget(AIController controller)
        {
            if (controller.playerTarget != null) return;

            Collider[] colliders = Physics.OverlapSphere(controller.eyeLevel.transform.position, detectionRadius);

            for (int i = 0; i < colliders.Length; i++)
            {
                ThirdPersonController player = colliders[i].transform.GetComponent<ThirdPersonController>();

                if (player == null) continue;

                if (!Physics.Linecast(controller.eyeLevel.transform.position, player.eyeLevel.transform.position))
                {
                    controller.playerTarget = player.eyeLevel.transform;
                }
                Debug.DrawLine(controller.eyeLevel.transform.position, player.eyeLevel.transform.position, Color.blue);
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}
