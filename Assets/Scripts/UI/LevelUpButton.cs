using ProjectSteppe.Entities.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectSteppe
{
    public class LevelUpButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI statName;

        [SerializeField]
        private TextMeshProUGUI statValue;        

        [SerializeField]
        private PlayerStatisticType statType;

        [SerializeField]
        private GameObject descriptionBox;

        private Button button;

        [SerializeField]
        private Color selectedColor;

        [SerializeField]
        private Animator shifterLeft;

        [SerializeField]
        private Animator shifterRight;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            statName.text = Enum.GetName(typeof(PlayerStatisticType), statType);
        }

        public void OnSelect(BaseEventData baseEventData)
        {
            descriptionBox.SetActive(true);
            statName.color = selectedColor;
            statValue.color = selectedColor;
        }

        public void OnDeselect(BaseEventData baseEventData)
        {
            descriptionBox.SetActive(false);
            statName.color = Color.white;
            statValue.color = Color.white;
        }
    }
}
