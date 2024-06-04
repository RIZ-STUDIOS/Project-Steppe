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
        [SerializeField] private AudioSource checkpointLoop;

        public override string InteractText => Interacted ? "<sprite=9>Rest" : "<sprite=9>Kindle Respite";

        public override bool OneTime => false;
        public override bool Interacted { get; set; }

        public UnityEvent<CheckpointInteractable> OnCheckpointActivate;
        public UnityEvent OnCheckpointFirstInteraction;

        public int levelIndex;

        public Transform spawnPoint;

        public float flickerMinIntensity;
        public float flickerMaxIntensity;
        public float flickerSpeed;
        private Light firelight;

        private void Awake()
        {
            firelight = GetComponentInChildren<Light>();
        }

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
            if (player.bossDead) GameManager.Instance.RespawnCharacter();
            player.PlayerUsableItemSlot.currentUsable.Recharge();
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

            firelight.colorTemperature = 3000;
            firelight.intensity = 6;

            initialBlastSound.Play();
            checkpointLoop.Play();

            StartCoroutine(FlickerFlame());
        }

        private IEnumerator FlickerFlame()
        {
            while (true)
            {
                float previousIntensity = firelight.intensity;
                float newIntensity = Random.Range(flickerMinIntensity, flickerMaxIntensity);
                float timer = 0;
                while (true)
                {
                    if (Mathf.Abs((firelight.intensity - newIntensity)) < 0.05)
                    {
                        firelight.intensity = newIntensity;
                        break;
                    }

                    timer += Time.deltaTime * flickerSpeed;
                    firelight.intensity = Mathf.Lerp(previousIntensity, newIntensity, timer);

                    yield return null;
                }
            }
        }
    }
}
