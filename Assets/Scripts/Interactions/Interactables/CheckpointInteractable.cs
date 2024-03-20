using ProjectSteppe.Entities.Player;
using System.Collections;
using UnityEngine;

namespace ProjectSteppe.Interactions.Interactables
{
    public class CheckpointInteractable : Interactable
    {
        private bool discovered;

        [SerializeField] private ParticleSystem[] particles;

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
            player.DisableCapability(PlayerCapability.Attack);

            player.PlayerAnimator.SetTrigger("ForceAnimation");
            player.PlayerAnimator.SetTrigger("ActivateCheckpoint");

            yield return new WaitForSeconds(2.567f);
            
            foreach (var particle in particles)
            {
                particle.Play();
            }

            GetComponentInChildren<Light>().intensity = 1;

            IEnumerator respite = player.PlayerUI.contextScreen.PlayRespiteFound();
            yield return respite;

            player.EnableCapability(PlayerCapability.Move);
            player.EnableCapability(PlayerCapability.Rotate);
            player.EnableCapability(PlayerCapability.Dash);
            player.EnableCapability(PlayerCapability.Attack);

            player.PlayerInteractor.onInteractionEnded?.Invoke(0);
        }

        private void CheckpointInteract()
        {
            player.PlayerEntity.EntityHealth.ResetHealth();
            player.PlayerUI.messagePrompt.ShowMessage("...");
            player.PlayerAnimator.SetTrigger("ForceAnimation");
            player.PlayerAnimator.SetBool("Sitting", true);
        }
    }
}
