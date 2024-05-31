using ProjectSteppe.ZedExtensions;
using UnityEngine;

namespace ProjectSteppe.UI
{
    public class PlayerDetailsUI : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        [SerializeField] private CanvasGroup healthUI;
        [SerializeField] private CanvasGroup balanceUI;

        private bool detailsShown;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ShowPlayerDetails()
        {
            if (detailsShown) return;
            detailsShown = true;
            StartCoroutine(healthUI.FadeIn());
            StartCoroutine(balanceUI.FadeIn());
        }

        public void HideBalance()
        {
            StartCoroutine(balanceUI.FadeOut());
        }

        public void HidePlayerDetails()
        {
            if (!detailsShown) return;
            detailsShown = false;
            StartCoroutine(healthUI.FadeOut());
            StartCoroutine(balanceUI.FadeOut());
        }
    }
}
