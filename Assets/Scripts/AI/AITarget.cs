using ProjectSteppe.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.AI
{
    public class AITarget : MonoBehaviour
    {
        [System.NonSerialized]
        public List<AIController> nearbyControllers = new List<AIController>();

        public void UpdateControllersList()
        {
            if (nearbyControllers.Count == 0)
                GetComponent<PlayerManager>().PlayerUI.playerDetails.HideBalance();
        }
    }
}
