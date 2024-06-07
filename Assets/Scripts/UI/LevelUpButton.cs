using ProjectSteppe.Entities.Player;
using ProjectSteppe.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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

        private UIPlayerInput playerInput;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            playerInput = GetComponentInParent<CheckpointUI>().playerInput;
            statName.text = Enum.GetName(typeof(PlayerStatisticType), statType);
        }

        public void OnSelect(BaseEventData baseEventData)
        {
            descriptionBox.SetActive(true);
            statName.color = selectedColor;
            statValue.color = selectedColor;

            playerInput.playerInput.UI.Navigate.performed += OnNavigate;
        }

        public void OnDeselect(BaseEventData baseEventData)
        {
            descriptionBox.SetActive(false);
            statName.color = Color.white;
            statValue.color = Color.white;

            playerInput.playerInput.UI.Navigate.performed -= OnNavigate;
        }

        private void OnNavigate(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();

            if (input.x < 0)
            {
                shifterLeft.SetTrigger("Pulse");
            }
            else if (input.x > 0)
            {
                shifterRight.SetTrigger("Pulse");
            }
        }
    }
}
