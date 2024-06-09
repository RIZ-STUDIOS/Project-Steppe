using ProjectSteppe.Managers;
using ProjectSteppe.ZedExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class OnLoadFadeInUI : MonoBehaviour
    {
        private CanvasGroup cg;

        private void Awake()
        {
            FadeIn();
        }

        private void Start()
        {
            GameManager.Instance.onLoadFadeIn = this;
        }

        public void FadeIn()
        {
            StartCoroutine(FadeInCoroutine());
        }

        public IEnumerator FadeInCoroutine()
        {
            cg = GetComponent<CanvasGroup>();
            cg.alpha = 1f;
            yield return cg.FadeOut(fadeSpeedMod: 1);
        }
    }
}
