using ProjectSteppe.Entities.Player;
using ProjectSteppe.Managers;
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

        public override string InteractText => Interacted ? "<sprite=8>Rest" : "<sprite=8>Kindle Respite";

        public override bool OneTime => false;
        public override bool Interacted { get; protected set; }

        public UnityEvent<CheckpointInteractable> OnCheckpointActivate;
        public UnityEvent OnCheckpointFirstInteraction;

        public int levelIndex;

        public Transform spawnPoint;

        public void DiscoveredQuery()
        {
            if (StartDiscovered)
            {
                Interacted = true;
                ActivateCheckpoint();
            }
        }

        public override void Interact()
        {
            if (!Interacted) FirstCheckpointInteract();
            else CheckpointInteract();

            OnCheckpointActivate.Invoke(this);
        }

        private void FirstCheckpointInteract()
        {
            OnCheckpointFirstInteraction.Invoke();
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

            ActivateCheckpoint();

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
