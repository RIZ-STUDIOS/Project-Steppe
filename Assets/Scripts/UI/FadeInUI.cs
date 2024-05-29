using ProjectSteppe.ZedExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class FadeInUI : MonoBehaviour
    {
        private CanvasGroup cg;

        private void Awake()
        {
            cg = GetComponent<CanvasGroup>();
            cg.alpha = 1f;

            StartCoroutine(cg.FadeOut(fadeSpeedMod:1));
        }
    }
}
