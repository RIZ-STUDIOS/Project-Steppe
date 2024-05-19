using ProjectSteppe.Entities.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ProjectSteppe.Interactions.Interactables
{
    public class CheckpointInteractable : Interactable
    {
        public string id;

        [SerializeField]
        private bool _startDiscovered;
        public bool StartDiscovered
        {
            get { return _startDiscovered; }
            set { _startDiscovered = value; }
        }

        [SerializeField] private ParticleSystem[] particles;
        [SerializeField] private AudioSource initialBlastSound;
        [SerializeField] private AudioSource checkpointLoop;

        public override string InteractText => Interacted ? "<sprite=8>Rest" : "<sprite=8>Kindle Respite";

        public override bool OneTime => false;
        public override bool Interacted { get; set; }

        public UnityEvent<CheckpointInteractable> OnCheckpointActivate;
        public UnityEvent OnCheckpointFirstInteraction;

        public int levelIndex;

        public Transform spawnPoint;

        public void DiscoveredQuery()
        {
            if (StartDiscovered)
            {
                Interacted = true;
                ActivateCheckpointEffects();
            }
        }

        public override void Interact()
        {
            if (!Interacted) FirstCheckpointInteract();
            else CheckpointInteract();
        }

        private void FirstCheckpointInteract()
        {
            StartCoroutine(OnCheckpointFirstInteract());
        }

        private IEnumerator OnCheckpointFirstInteract()
        {
            Interacted = true;

            player.DisableCapability(PlayerCapability.Move);
            player.DisableCapability(PlayerCapability.Rotate);
            player.DisableCapability(PlayerCapability.Dash);
            player.DisableCapability(PlayerCapability.Attack);

            player.PlayerAnimator.SetTrigger("ForceAnimation");
            player.PlayerAnimator.SetTrigger("ActivateCheckpoint");

            yield return new WaitForSeconds(2.567f);

            OnCheckpointActivate.Invoke(this);
            ActivateCheckpointEffects();

            IEnumerator respite = player.PlayerUI.contextScreen.PlayRespiteFound();
            yield return respite;

            player.EnableCapability(PlayerCapability.Move);
            player.EnableCapability(PlayerCapability.Rotate);
            player.EnableCapability(PlayerCapability.Dash);
            player.EnableCapability(PlayerCapability.Attack);

            player.PlayerInteractor.onInteractionEnded?.Invoke(.25f);
        }

        private void CheckpointInteract()
        {
            if (player.bossDead) SceneManager.LoadScene(SceneConstants.LEVEL_1_INDEX);
            player.PlayerEntity.EntityHealth.ResetHealth();
            player.PlayerUI.messagePrompt.ShowMessage("...");
            player.PlayerAnimator.SetTrigger("ForceAnimation");
            player.PlayerAnimator.SetBool("Sitting", true);
        }

        private void ActivateCheckpointEffects()
        {
            foreach (var particle in particles)
            {
                particle.Play();
            }

            var light = GetComponentInChildren<Light>();

            light.colorTemperature = 3000;
            light.intensity = 1;

            initialBlastSound.Play();
            checkpointLoop.Play();
        }
    }
}
