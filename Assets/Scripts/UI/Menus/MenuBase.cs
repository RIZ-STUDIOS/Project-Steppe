using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectSteppe.UI.Menus
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class MenuBase : MonoBehaviour
    {
        public static MenuBase CurrentMenu;
        protected CanvasGroup canvasGroup;
        protected MenuBase previousMenu;

        private Coroutine showHideCoroutine;

        protected EventSystem eventSystem;

        public static void SetMenu(MenuBase menu)
        {
            var prevMenu = CurrentMenu;
            CurrentMenu = menu;
            if (prevMenu)
            {
                prevMenu.HideMenu();
            }

            menu.previousMenu = prevMenu;
        }

        protected static void ShowCurrentMenu()
        {
            CurrentMenu.ShowMenu();
        }

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            eventSystem = EventSystem.current;
        }

        protected virtual void HideMenu()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            ShowCurrentMenu();
        }

        protected virtual void ShowMenu()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }

        protected void HideMenuCoroutine(float speed)
        {
            if (showHideCoroutine != null)
            {
                StopCoroutine(showHideCoroutine);
                showHideCoroutine = null;
            }

            showHideCoroutine = StartCoroutine(HideMenuIEnumerator(speed));
        }

        protected void ShowMenuCoroutine(float speed)
        {
            if (showHideCoroutine != null)
            {
                StopCoroutine(showHideCoroutine);
                showHideCoroutine = null;
            }

            showHideCoroutine = StartCoroutine(FadeInCanvas(speed));
        }

        private IEnumerator HideMenuIEnumerator(float speed)
        {
            yield return FadeOutCanvas(speed);
            ShowCurrentMenu();
        }

        protected IEnumerator FadeInCanvas(float speed)
        {
            float alpha = 0;
            while (alpha < 1)
            {
                alpha += Time.deltaTime * speed;
                canvasGroup.alpha = alpha;
                yield return null;
            }
            alpha = 1;
            canvasGroup.alpha = alpha;
            canvasGroup.blocksRaycasts = true;
        }

        protected IEnumerator FadeOutCanvas(float speed)
        {
            float alpha = 1;
            while (alpha > 0)
            {
                alpha -= Time.deltaTime * speed;
                canvasGroup.alpha = alpha;
                yield return null;
            }
            alpha = 0;
            canvasGroup.alpha = alpha;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
