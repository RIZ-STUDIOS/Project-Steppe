using System.Collections;
using UnityEngine;

namespace ProjectSteppe.ZedExtensions
{
    public static class CanvasUtils
    {
        public static void InstantShow(this CanvasGroup canvasGroup, bool isInteractable = false, bool canBlockRaycasts = false)
        {
            canvasGroup.alpha = 1;
            ToggleCanvasGroupInteraction(canvasGroup, true, isInteractable, canBlockRaycasts);
        }

        public static void InstantHide(this CanvasGroup canvasGroup, bool isInteractable = false, bool canBlockRaycasts = false)
        {
            canvasGroup.alpha = 0;
            ToggleCanvasGroupInteraction(canvasGroup, false, isInteractable, canBlockRaycasts);
        }

        public static IEnumerator FadeIn(this CanvasGroup canvasGroup, bool isInteractable = false, bool canBlockRaycasts = false, float fadeSpeedMod = 2)
        {
            IEnumerator lerpIn = LerpCanvasAlphaOverTime(canvasGroup, 1);
            yield return lerpIn;

            ToggleCanvasGroupInteraction(canvasGroup, true, isInteractable, canBlockRaycasts);
        }

        public static IEnumerator FadeOut(this CanvasGroup canvasGroup, bool isInteractable = false, bool canBlockRaycasts = false, float fadeSpeedMod = 2)
        {
            ToggleCanvasGroupInteraction(canvasGroup, false, isInteractable, canBlockRaycasts);

            IEnumerator lerpOut = LerpCanvasAlphaOverTime(canvasGroup, 0, fadeSpeedMod);
            yield return lerpOut;
        }

        public static IEnumerator FadeInThenOut(this CanvasGroup canvasGroup, bool isInteractable = false, bool canBlockRaycasts = false, float fadeSpeedMod = 2, float duration = 0.5f)
        {
            IEnumerator lerpIn = LerpCanvasAlphaOverTime(canvasGroup, 1, fadeSpeedMod);
            yield return lerpIn;

            ToggleCanvasGroupInteraction(canvasGroup, true, isInteractable, canBlockRaycasts);

            yield return new WaitForSeconds(duration);

            ToggleCanvasGroupInteraction(canvasGroup, false, isInteractable, canBlockRaycasts);

            IEnumerator lerpOut = LerpCanvasAlphaOverTime(canvasGroup, 0, fadeSpeedMod);
            yield return lerpOut;
        }

        private static IEnumerator LerpCanvasAlphaOverTime(CanvasGroup canvasGroup, float targetValue, float lerpSpeedMod = 3)
        {
            Vector2 lerpVector = new Vector2(canvasGroup.alpha, targetValue);

            float progress = 0;
            while (progress < 1)
            {
                progress += Time.deltaTime * lerpSpeedMod;

                canvasGroup.alpha = Mathf.Lerp(lerpVector.x, lerpVector.y, progress);

                yield return null;
            }

            yield return lerpVector.y;
        }

        private static void ToggleCanvasGroupInteraction(CanvasGroup canvasGroup, bool togglingOn, bool isInteractable, bool canBlockRaycasts)
        {
            if (togglingOn)
            {
                if (isInteractable) canvasGroup.interactable = true;
                if (canBlockRaycasts) canvasGroup.blocksRaycasts = true;
            }
            else
            {
                if (isInteractable) canvasGroup.interactable = false;
                if (canBlockRaycasts) canvasGroup.blocksRaycasts = false;
            }
        }
    }
}
