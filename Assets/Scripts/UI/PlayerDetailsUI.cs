using ProjectSteppe.ZedExtensions;
using UnityEngine;

namespace ProjectSteppe.UI
{
    public class PlayerDetailsUI : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        [SerializeField] private CanvasGroup healthUI;
        [SerializeField] private CanvasGroup balanceUI;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ShowPlayerDetails()
        {
            StartCoroutine(canvasGroup.FadeIn());
            //StartCoroutine(healthUI.FadeIn());
            //StartCoroutine(balanceUI.FadeIn());
        }

        public void HideBalance()
        {
            StartCoroutine(balanceUI.FadeOut());
        }

        public void HidePlayerDetails()
        {
            StartCoroutine(canvasGroup.FadeOut());
            //StartCoroutine(healthUI.FadeOut());
            //StartCoroutine(balanceUI.FadeOut());
        }
    }
}
