using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectSteppe.UI.Menus
{
    public class SettingsSubMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject defaultObjectSelect;

        public void Show()
        {
            EventSystem.current.SetSelectedGameObject(defaultObjectSelect);
        }
    }
}
