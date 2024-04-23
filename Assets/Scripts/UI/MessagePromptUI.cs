using ProjectSteppe.Entities.Player;
using ProjectSteppe.ZedExtensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectSteppe.UI
{
    public class MessagePromptUI : MonoBehaviour
    {
        /// <summary>
        /// bool: true if showing, false if hiding prompt.
        /// </summary>
        public System.Action<bool> onMessagePromptChange;

        private PlayerManager playerManager;
        private CanvasGroup canvasGroup;
        private Button button;
        private TextMeshProUGUI messageTMP;

        private void Awake()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            canvasGroup = GetComponent<CanvasGroup>();
            button = GetComponentInChildren<Button>();
            messageTMP = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ShowMessage(string message)
        {
            messageTMP.text = message;
            StartCoroutine(canvasGroup.FadeIn(true, true));

            playerManager.PlayerInput.OnInteraction.AddListener(HideMessage);

            onMessagePromptChange?.Invoke(true);
        }

        public void HideMessage()
        {
            onMessagePromptChange.Invoke(false);

            StartCoroutine(canvasGroup.FadeOut(true, true, fadeSpeedMod: 6));

            GetComponentInParent<PlayerManager>().PlayerInput.OnInteraction.RemoveListener(HideMessage);
            GetComponentInParent<PlayerManager>().PlayerAnimator.SetBool("Sitting", false);
            playerManager.PlayerInteractor.onInteractionEnded?.Invoke(0);
        }
    }
}
