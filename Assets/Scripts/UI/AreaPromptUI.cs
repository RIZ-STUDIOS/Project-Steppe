using ProjectSteppe.ZedExtensions;
using TMPro;
using UnityEngine;

namespace ProjectSteppe
{
    public class AreaPromptUI : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private TextMeshProUGUI areaPromptTMP;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            areaPromptTMP = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void DisplayArea(string areaName)
        {
            areaPromptTMP.text = areaName;

            StartCoroutine(canvasGroup.FadeInThenOut(fadeSpeedMod: 0.75f, duration: 1f));
        }
    }
}
