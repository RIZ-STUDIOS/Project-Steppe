using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.UI
{
    public class BossUI : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ShowBossUI()
        {
            canvasGroup.alpha = 1f;
        }

        public void HideBossUI()
        {
            canvasGroup.alpha = 0f;
        }
    }
}
