using ProjectSteppe.Interactions;
using System.Collections;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        private PlayerManager playerManager;

        private Interactable _currentInteractable;
        public Interactable CurrentInteractable
        {
            get { return _currentInteractable; }
            set
            {
                _currentInteractable = value;
                if (_currentInteractable != null)
                {
                    CurrentInteractable.InteractSetup(playerManager);
                    onCurrentInteractableChange?.Invoke(value.InteractText);
                }
                else playerManager.PlayerUI.interactPrompt.HidePrompt();
            }
        }

        public System.Action<string> onCurrentInteractableChange;
        public System.Action<float> onInteractionEnded;

        [SerializeField] private float interactRadius = 2;

        private bool displayingMessage;

        private void OnMessagePromptChange(bool isShowing)
        {
            displayingMessage = isShowing;
        }

        private void Awake()
        {
            playerManager = GetComponentInParent<PlayerManager>();

            onCurrentInteractableChange += playerManager.PlayerUI.interactPrompt.ShowPrompt;
            onInteractionEnded += LookForInteractable;

            playerManager.PlayerUI.messagePrompt.onMessagePromptChange += OnMessagePromptChange;
        }

        private void OnInteract()
        {
            if (CurrentInteractable != null)
            {
                CurrentInteractable.Interact();
                CurrentInteractable = null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                if (displayingMessage) return;

                var interactable = other.GetComponent<Interactable>();

                if (interactable.OneTime && interactable.Interacted) return;

                if (interactable != CurrentInteractable) CurrentInteractable = interactable;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                if (other.GetComponent<Interactable>() == CurrentInteractable)
                {
                    CurrentInteractable = null;
                }
            }
        }

        public void LookForInteractable(float seconds)
        {
            StartCoroutine(LookForInteractableIEnumerator(seconds));
        }

        public IEnumerator LookForInteractableIEnumerator(float seconds = 0)
        {
            yield return new WaitForSeconds(seconds);

            Collider[] colliders = Physics.OverlapSphere(transform.position, interactRadius, 1 << LayerMask.NameToLayer("Interactable"));

            float interactableDistance = 999;
            Interactable closestInteractable = null;

            foreach (var collider in colliders)
            {
                float colliderDistance = Vector3.Distance(collider.transform.position, transform.position);
                if (colliderDistance < interactableDistance)
                {
                    interactableDistance = colliderDistance;
                    closestInteractable = collider.GetComponent<Interactable>();
                }
            }

            CurrentInteractable = closestInteractable;
        }
    }
}
