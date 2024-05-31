using ProjectSteppe.ZedExtensions;
using UnityEngine;

namespace ProjectSteppe.UI
{
    public class EntityDetailsUI : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        [SerializeField]
        private EntityHealthUI healthUI;

        [SerializeField]
        private EntityBalanceUI balanceUI;  

        private bool shown;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ShowDetails()
        {
            if (shown) return;
            shown = true;
            StartCoroutine(canvasGroup.FadeIn());
        }

        public void HideDetails()
        {
            if (!shown) return;
            shown = false;
            StartCoroutine(canvasGroup.FadeOut());
        }
    }
}
