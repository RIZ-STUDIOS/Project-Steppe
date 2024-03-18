using ProjectSteppe.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class Area : MonoBehaviour
    {
        [SerializeField] private string areaName;

        private bool entered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!entered)
                {
                    other.GetComponent<PlayerManager>().PlayerUI.areaPrompt.DisplayArea(areaName);
                    entered = true;
                }
                
            }
        }
    }
}
