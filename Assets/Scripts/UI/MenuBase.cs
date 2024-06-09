using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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

        public UIPlayerInput playerInput;

        private bool justSwitched;

        public bool JustSwitched => justSwitched;

        public static void SetMenu(MenuBase menu, bool forceShow = false)
        {
            var prevMenu = CurrentMenu;
            CurrentMenu = menu;
            if (prevMenu)
            {
                prevMenu.UnsubscribeInputEvents();
                prevMenu.HideMenu();
            }

            if (menu)
            {
                menu.previousMenu = prevMenu;
                if (forceShow)
                    ShowCurrentMenu();
            }
        }

        protected static void ShowCurrentMenu()
        {
            if (!CurrentMenu) return;
            CurrentMenu.SubscribeInputEvents();
            CurrentMenu.justSwitched = true;
            CurrentMenu.ShowMenu();
        }

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            eventSystem = EventSystem.current;
            playerInput = UIPlayerInput.Instance;
        }

        protected virtual void Start()
        {
        }

        protected virtual void Update()
        {
            if (justSwitched)
                justSwitched = false;
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

        protected void HideMenuCoroutine(float speed, System.Action onFinish = null)
        {
            if (showHideCoroutine != null)
            {
                StopCoroutine(showHideCoroutine);
                showHideCoroutine = null;
            }

            showHideCoroutine = StartCoroutine(HideMenuIEnumerator(speed, onFinish));
        }

        protected void ShowMenuCoroutine(float speed, System.Action onFinish = null)
        {
            if (showHideCoroutine != null)
            {
                StopCoroutine(showHideCoroutine);
                showHideCoroutine = null;
            }

            showHideCoroutine = StartCoroutine(FadeInCanvas(speed, onFinish));
        }

        private IEnumerator HideMenuIEnumerator(float speed, System.Action onFinish)
        {
            yield return FadeOutCanvas(speed);
            onFinish?.Invoke();
            ShowCurrentMenu();
        }

        protected IEnumerator FadeInCanvas(float speed, System.Action onFinish)
        {
            float alpha = canvasGroup.alpha;
            while (alpha < 1)
            {
                alpha += Time.deltaTime * speed;
                canvasGroup.alpha = alpha;
                yield return null;
            }
            alpha = 1;
            canvasGroup.alpha = alpha;
            canvasGroup.blocksRaycasts = true;
            onFinish?.Invoke();
        }

        protected IEnumerator FadeOutCanvas(float speed)
        {
            float alpha = canvasGroup.alpha;
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

        private void UnsubscribeInputEvents()
        {
            playerInput.playerInput.UI.Cancel.started -= OnCancelStarted;
            playerInput.playerInput.UI.Cancel.performed -= OnCancelPerformed;
            playerInput.playerInput.UI.Cancel.canceled -= OnCancelCanceled;

            playerInput.playerInput.UI.Submit.started -= OnSubmitStarted;
            playerInput.playerInput.UI.Submit.performed -= OnSubmitPerformed;
            playerInput.playerInput.UI.Submit.canceled -= OnSubmitCanceled;

            playerInput.playerInput.UI.RightBumper.started -= OnRightBumperStarted;
            playerInput.playerInput.UI.RightBumper.performed -= OnRightBumperPerformed;
            playerInput.playerInput.UI.RightBumper.canceled -= OnRightBumperCanceled;

            playerInput.playerInput.UI.LeftBumper.started -= OnLeftBumperStarted;
            playerInput.playerInput.UI.LeftBumper.performed -= OnLeftBumperPerformed;
            playerInput.playerInput.UI.LeftBumper.canceled -= OnLeftBumperCanceled;
        }

        private void SubscribeInputEvents()
        {
            playerInput.playerInput.UI.Cancel.started += OnCancelStarted;
            playerInput.playerInput.UI.Cancel.performed += OnCancelPerformed;
            playerInput.playerInput.UI.Cancel.canceled += OnCancelCanceled;

            playerInput.playerInput.UI.Submit.started += OnSubmitStarted;
            playerInput.playerInput.UI.Submit.performed += OnSubmitPerformed;
            playerInput.playerInput.UI.Submit.canceled += OnSubmitCanceled;

            playerInput.playerInput.UI.RightBumper.started += OnRightBumperStarted;
            playerInput.playerInput.UI.RightBumper.performed += OnRightBumperPerformed;
            playerInput.playerInput.UI.RightBumper.canceled += OnRightBumperCanceled;

            playerInput.playerInput.UI.LeftBumper.started += OnLeftBumperStarted;
            playerInput.playerInput.UI.LeftBumper.performed += OnLeftBumperPerformed;
            playerInput.playerInput.UI.LeftBumper.canceled += OnLeftBumperCanceled;
        }

        private void OnDestroy()
        {
            UnsubscribeInputEvents();
        }

        protected virtual void OnCancelStarted(InputAction.CallbackContext callbackContext)
        {

        }

        protected virtual void OnCancelPerformed(InputAction.CallbackContext callbackContext)
        {

        }

        protected virtual void OnCancelCanceled(InputAction.CallbackContext callbackContext)
        {

        }

        protected virtual void OnSubmitStarted(InputAction.CallbackContext callbackContext)
        {

        }

        protected virtual void OnSubmitPerformed(InputAction.CallbackContext callbackContext)
        {

        }

        protected virtual void OnSubmitCanceled(InputAction.CallbackContext callbackContext)
        {

        }

        protected virtual void OnRightBumperStarted(InputAction.CallbackContext callbackContext)
        {

        }

        protected virtual void OnRightBumperPerformed(InputAction.CallbackContext callbackContext)
        {

        }

        protected virtual void OnRightBumperCanceled(InputAction.CallbackContext callbackContext)
        {

        }

        protected virtual void OnLeftBumperStarted(InputAction.CallbackContext callbackContext)
        {

        }

        protected virtual void OnLeftBumperPerformed(InputAction.CallbackContext callbackContext)
        {

        }

        protected virtual void OnLeftBumperCanceled(InputAction.CallbackContext callbackContext)
        {

        }
    }
}
