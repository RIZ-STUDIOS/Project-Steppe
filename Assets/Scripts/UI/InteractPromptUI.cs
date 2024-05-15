using ProjectSteppe.ZedExtensions;
using TMPro;
using UnityEngine;

namespace ProjectSteppe.UI
{
    public class InteractPromptUI : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private TextMeshProUGUI interactTMP;

        private Coroutine displayCoroutine;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            interactTMP = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ShowPrompt(string message)
        {
            interactTMP.text = message;
            displayCoroutine = StartCoroutine(canvasGroup.FadeIn());
        }

        public void HidePrompt()
        {
            if (displayCoroutine != null) StopCoroutine(displayCoroutine);
            canvasGroup.InstantHide();
            //StartCoroutine(canvasGroup.FadeOut(fadeSpeedMod: 6));
        }
    }
}
