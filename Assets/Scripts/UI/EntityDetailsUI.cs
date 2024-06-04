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

        private Coroutine showCoroutine;

        private bool shown;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ShowDetails()
        {
            if(showCoroutine != null)
            {
                StopCoroutine(showCoroutine);
                showCoroutine = null;
            }
            showCoroutine = StartCoroutine(canvasGroup.FadeIn());
        }

        public void HideDetails()
        {
            if (showCoroutine != null)
            {
                StopCoroutine(showCoroutine);
                showCoroutine = null;
            }
            showCoroutine = StartCoroutine(canvasGroup.FadeOut());
        }
    }
}
