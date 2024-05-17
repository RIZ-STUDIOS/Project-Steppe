using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectSteppe.UI.Menus
{
    public class SettingsMenuTab : MonoBehaviour
    {
        private TextMeshProUGUI text;

        [SerializeField]
        private GameObject underlineGameObject;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        public void Select()
        {
            underlineGameObject.SetActive(true);
            text.color = new Color(0.8980392f, 0.2117647f, 0.3294118f);
        }

        public void Unselect()
        {
            underlineGameObject.SetActive(false);
            text.color = new Color(0.5450981f, 0.4823529f, 0.3803922f);
        }
    }
}
