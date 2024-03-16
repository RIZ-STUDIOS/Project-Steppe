using ProjectSteppe.ZedExtensions;
using System.Collections;
using System.Collections.Generic;
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
        }

        public void HidePlayerDetails()
        {
            StartCoroutine(canvasGroup.FadeOut());
        }
    }
}
