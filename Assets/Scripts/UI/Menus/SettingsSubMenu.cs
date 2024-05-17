using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectSteppe.UI.Menus
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SettingsSubMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject defaultObjectSelect;

        [SerializeField]
        private SettingsMenuTab tab;

        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            Hide();
        }

        public void Show()
        {
            tab.Select();
            EventSystem.current.SetSelectedGameObject(defaultObjectSelect);
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            tab.Unselect();
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
