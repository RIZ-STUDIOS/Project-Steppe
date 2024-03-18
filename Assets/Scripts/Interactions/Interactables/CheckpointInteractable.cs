using ProjectSteppe.Entities.Player;
using System.Collections;
using UnityEngine;

namespace ProjectSteppe.Interactions.Interactables
{
    public class CheckpointInteractable : Interactable
    {
        [SerializeField] private Material discoveredMaterial;

        private bool discovered;

        public override string InteractText => discovered ? "<sprite=8>Rest" : "<sprite=8>Kindle Respite";

        public override void Interact()
        {
            if (!discovered) FirstCheckpointInteract();
            else CheckpointInteract();
        }

        private void FirstCheckpointInteract()
        {
            StartCoroutine(OnCheckpointFirstInteract());
        }

        private IEnumerator OnCheckpointFirstInteract()
        {
            discovered = true;

            player.DisableCapability(PlayerCapability.Move);
            player.DisableCapability(PlayerCapability.Rotate);
            player.DisableCapability(PlayerCapability.Dash);

            GetComponent<MeshRenderer>().material = discoveredMaterial;
            GetComponentInChildren<ParticleSystem>().Play();
            GetComponentInChildren<Light>().intensity = 1;

            IEnumerator respite = player.PlayerUI.contextScreen.PlayRespiteFound();
            yield return respite;

            player.EnableCapability(PlayerCapability.Move);
            player.EnableCapability(PlayerCapability.Rotate);
            player.EnableCapability(PlayerCapability.Dash);

            player.PlayerInteractor.onInteractionEnded?.Invoke(0);
        }

        private void CheckpointInteract()
        {
            player.PlayerUI.messagePrompt.ShowMessage("...");
        }
    }
}
