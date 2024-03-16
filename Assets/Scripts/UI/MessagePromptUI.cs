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
        private CanvasGroup canvasGroup;
        private Button button;
        private TextMeshProUGUI messageTMP;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            button = GetComponentInChildren<Button>();
            messageTMP = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ShowMessage(string message)
        {
            messageTMP.text = message;
            StartCoroutine(canvasGroup.FadeIn(true, true));

            GetComponentInParent<PlayerManager>().PlayerInput.OnInteraction.AddListener(HideMessage);
        }

        public void HideMessage()
        {
            StartCoroutine(canvasGroup.FadeOut(true, true));
            GetComponentInParent<PlayerManager>().PlayerInput.OnInteraction.RemoveListener(HideMessage);
        }
    }
}
