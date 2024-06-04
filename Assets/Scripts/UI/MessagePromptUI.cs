using ProjectSteppe.Entities.Player;
using ProjectSteppe.ZedExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSteppe.UI
{
    public class MessagePromptUI : MonoBehaviour
    {
        public bool isShowing;

        [SerializeField]
        private AudioSource closeAudio;

        private PlayerManager playerManager;
        private CanvasGroup canvasGroup;
        private Button button;
        private TextMeshProUGUI messageTMP;

        private Coroutine displayCoroutine;

        private void Awake()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            canvasGroup = GetComponent<CanvasGroup>();
            button = GetComponentInChildren<Button>();
            messageTMP = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ShowMessage(string message)
        {
            isShowing = true;

            messageTMP.text = message;
            displayCoroutine = StartCoroutine(canvasGroup.FadeIn(true, true));

            playerManager.PlayerInput.OnInteraction.AddListener(HideMessage);
        }

        public void HideMessage()
        {
            isShowing = false;

            if (displayCoroutine != null) StopCoroutine(displayCoroutine);

            canvasGroup.InstantHide(true, true);
            //StartCoroutine(canvasGroup.FadeOut(true, true, fadeSpeedMod: 6));

            GetComponentInParent<PlayerManager>().PlayerInput.OnInteraction.RemoveListener(HideMessage);
            GetComponentInParent<PlayerManager>().PlayerAnimator.SetBool("Sitting", false);

            playerManager.PlayerInteractor.onInteractionEnded?.Invoke(.25f);
            closeAudio.Play();
        }
    }
}
