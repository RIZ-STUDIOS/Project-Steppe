using ProjectSteppe.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectSteppe.UI
{
    public class UsableItemSlotUI : MonoBehaviour
    {
        public PlayerUsableItemSlot playerSlot;

        public TextMeshProUGUI itemTitle;
        public TextMeshProUGUI itemCharges;

        private void Awake()
        {
            playerSlot.currentUsable.onUse += UpdateSlotUI;
            UpdateSlotUI();
        }

        private void UpdateSlotUI()
        {
            itemTitle.text = playerSlot.currentUsable.title;
            itemCharges.text = playerSlot.currentUsable.charges.ToString();
        }
    }
}
