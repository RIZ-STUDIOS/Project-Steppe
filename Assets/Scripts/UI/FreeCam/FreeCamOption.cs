using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectSteppe.UI
{
    public class FreeCamOption : MonoBehaviour
    {
        public System.Action onLeftAction;
        public System.Action onRightAction;

        [SerializeField]
        private TextMeshProUGUI optionText;

        public void UpdateText(string text)
        {
            optionText.text = text;
        }
    }
}
