using ProjectSteppe.Entities.Player;
using ProjectSteppe.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class Checkpoint : MonoBehaviour
    {
        private bool discovered;

        private PlayerManager player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!discovered)
                {
                    player = other.GetComponent<PlayerManager>();

                    Debug.Log(player + " is here!");
                    player.PlayerUI.interactPrompt.ShowPrompt("<sprite=8> Rekindle");

                    player.PlayerInput.OnInteraction.AddListener(OnCheckpointFirstInteract);
                }
                else
                {
                    player.PlayerUI.interactPrompt.ShowPrompt("<sprite=8> Rest");
                    
                    player.PlayerInput.OnInteraction.AddListener(OnCheckpointInteract);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!discovered) player.PlayerInput.OnInteraction.RemoveListener(OnCheckpointFirstInteract);
                else player.PlayerInput.OnInteraction.RemoveListener(OnCheckpointInteract);

                player.PlayerUI.interactPrompt.HidePrompt();
            }
        }

        private void OnCheckpointFirstInteract()
        {
            discovered = true;

            StartCoroutine(player.PlayerUI.contextScreen.PlayRespiteFound());

            player.PlayerInput.OnInteraction.RemoveListener(OnCheckpointFirstInteract);
            player.PlayerInput.OnInteraction.AddListener(OnCheckpointInteract);
        }

        private void OnCheckpointInteract()
        {
            player.PlayerUI.messagePrompt.ShowMessage("...");
        }
    }
}
