using ProjectSteppe.ZedExtensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectSteppe.UI
{
    public class InteractPromptUI : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private TextMeshProUGUI interactTMP;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            interactTMP = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ShowPrompt(string message)
        {
            interactTMP.text = message;
            StartCoroutine(canvasGroup.FadeIn());
        }

        public void HidePrompt()
        {
            StartCoroutine(canvasGroup.FadeOut());
        }
    }
}
