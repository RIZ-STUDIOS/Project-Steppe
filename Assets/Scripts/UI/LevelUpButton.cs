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

        private Button button;

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
            Debug.Log("YUP!");
        }
    }
}
