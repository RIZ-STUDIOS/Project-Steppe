using ProjectSteppe.ZedExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.UI
{
    public class BossDetailsUI : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ShowBossDetails()
        {
            StartCoroutine(canvasGroup.FadeIn());
        }

        public void HideBossDetails()
        {
            StartCoroutine(canvasGroup.FadeOut());
        }
    }
}
