using ProjectSteppe.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class Checkpoint : MonoBehaviour
    {
        private bool discovered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!discovered)
                {
                    discovered = true;
                    other.GetComponentInChildren<ContextScreenUI>().TriggerRespite();
                }
            }
        }
    }
}
