using ProjectSteppe.Entities.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectSteppe.Interactions.Interactables
{
    public class CheckpointInteractable : Interactable
    {
        public bool startDiscovered;
        private bool discovered;

        [SerializeField] private ParticleSystem[] particles;

        public override string InteractText => discovered ? "<sprite=8>Rest" : "<sprite=8>Kindle Respite";

        private void Awake()
        {
            if (startDiscovered)
            {
                discovered = true;
                ActivateCheckpoint();
            }
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

            player.DisableCapability(PlayerCapability.Move);
            player.DisableCapability(PlayerCapability.Rotate);
            player.DisableCapability(PlayerCapability.Dash);

            player.PlayerAnimator.SetTrigger("ForceAnimation");
            player.PlayerAnimator.SetTrigger("ActivateCheckpoint");

            yield return new WaitForSeconds(2.567f);

            ActivateCheckpoint();

            IEnumerator respite = player.PlayerUI.contextScreen.PlayRespiteFound();
            yield return respite;

            player.EnableCapability(PlayerCapability.Move);
            player.EnableCapability(PlayerCapability.Rotate);
            player.EnableCapability(PlayerCapability.Dash);

            player.PlayerInteractor.onInteractionEnded?.Invoke(0);
        }

        private void CheckpointInteract()
        {
            if (player.bossDead) SceneManager.LoadScene(1);
            player.PlayerEntity.EntityHealth.ResetHealth();
            player.PlayerUI.messagePrompt.ShowMessage("...");
            player.PlayerAnimator.SetTrigger("ForceAnimation");
            player.PlayerAnimator.SetBool("Sitting", true);
        }

        private void ActivateCheckpoint()
        {
            foreach (var particle in particles)
            {
                particle.Play();
            }

            var light = GetComponentInChildren<Light>();

            light.colorTemperature = 3000;
            light.intensity = 1;
        }
    }
}
