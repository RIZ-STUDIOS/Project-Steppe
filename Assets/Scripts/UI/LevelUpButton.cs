using ProjectSteppe.Entities.Player;
using ProjectSteppe.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ProjectSteppe
{
    public class LevelUpButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI nameTMP;

        [SerializeField]
        private TextMeshProUGUI valueTMP;        

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

        private UIPlayerInput playerInput;
        private PlayerStatisticHandler playerStats;
        private PlayerStatistic playerStat;
        private int currentValue;

        [SerializeField]
        private TextMeshProUGUI costTMP;
        private int cost;

        public UnityEvent<int> OnValueChange;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            playerInput = UIPlayerInput.Instance;
            nameTMP.text = Enum.GetName(typeof(PlayerStatisticType), statType);           
        }

        public void ActivateButtons()
        {
            playerStats = GetComponentInParent<PlayerStatisticHandler>();
            playerStat = playerStats.statistics.Find(s => s.type == statType);

            currentValue = playerStat.Level;
            valueTMP.text = currentValue.ToString();
        }

        public void OnSelect(BaseEventData baseEventData)
        {
            descriptionBox.SetActive(true);
            nameTMP.color = selectedColor;
            valueTMP.color = selectedColor;

            playerInput.playerInput.UI.Navigate.performed += OnNavigate;
        }

        public void OnDeselect(BaseEventData baseEventData)
        {
            descriptionBox.SetActive(false);
            nameTMP.color = Color.white;
            valueTMP.color = Color.white;

            playerInput.playerInput.UI.Navigate.performed -= OnNavigate;
        }

        private void OnNavigate(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();

            if (input.x < 0)
            {
                shifterLeft.Play("Pulse");
                currentValue--;
                if (currentValue < playerStat.Level) currentValue = playerStat.Level;
                else OnValueChange.Invoke(-1);
            }
            else if (input.x > 0)
            {
                shifterRight.Play("Pulse");
                currentValue++;
                OnValueChange.Invoke(1);
            }

            valueTMP.text = currentValue.ToString();
        }
    }
}
