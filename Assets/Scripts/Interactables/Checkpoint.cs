using ProjectSteppe.Entities.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace ProjectSteppe.Interactables
{
    public class Checkpoint : Interactable
    {
        [SerializeField] private Material discoveredMaterial;

        private bool discovered;

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            interactText = discovered ? "<sprite=8>Rest" : "<sprite=8>Rekindle";
        }

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

            player.PlayerInput.OnInteraction.RemoveListener(FirstCheckpointInteract);

            player.PlayerUI.interactPrompt.HidePrompt();

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

            player.PlayerInput.OnInteraction.AddListener(CheckpointInteract);
            player.PlayerUI.interactPrompt.ShowPrompt("<sprite=8>Rest");
        }

        private void CheckpointInteract()
        {
            player.PlayerUI.messagePrompt.ShowMessage("...");
            player.PlayerInteractor.CurrentInteractable = null;
        }
    }
}
