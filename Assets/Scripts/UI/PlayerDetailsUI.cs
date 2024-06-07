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

        [Obsolete]
        public void ShowPlayerDetails()
        {
            ShowHealthBar();
            //StartCoroutine(canvasGroup.FadeIn());
            //StartCoroutine(healthUI.FadeIn());
            //StartCoroutine(balanceUI.FadeIn());
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

        [Obsolete]
        public void HidePlayerDetails()
        {
            HideHealthBar();
            HideBalance();
            //StartCoroutine(canvasGroup.FadeOut());
            //StartCoroutine(healthUI.FadeOut());
            //StartCoroutine(balanceUI.FadeOut());
        }
    }
}
