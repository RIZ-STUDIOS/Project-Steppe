using ProjectSteppe.ZedExtensions;
using System;
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

        public void HideBalance()
        {
            StartCoroutine(balanceUI.FadeOut());
        }

        public void HideHealthBar()
        {
            StartCoroutine(balanceUI.FadeOut());
        }

        public void ShowHealthBar()
        {
            StartCoroutine(healthUI.FadeIn());
        }

        public void ShowBalanceBar()
        {
            StartCoroutine(balanceUI.FadeIn());
        }

        public void HidePlayerDetails()
        {
            StartCoroutine(canvasGroup.FadeOut());
        }
    }
}
