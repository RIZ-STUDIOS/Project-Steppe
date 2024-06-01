using ProjectSteppe.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class PlayerCapabilityHandler : MonoBehaviour
    {
        PlayerManager playerManager;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
        }

        public void DisableAllActions()
        {
            playerManager.DisableCapability(PlayerCapability.Move);
            playerManager.DisableCapability(PlayerCapability.Rotate);
            playerManager.DisableCapability(PlayerCapability.Attack);
            playerManager.DisableCapability(PlayerCapability.Dash);
            playerManager.DisableCapability(PlayerCapability.Drink);
            playerManager.DisableCapability(PlayerCapability.Sit);
        }
    }
}
