using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

namespace ProjectSteppe
{
    public class AICombatController : MonoBehaviour
    {
        public Transform playerTarget;

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
    }
}
