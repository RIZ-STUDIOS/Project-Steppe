using ProjectSteppe.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.UI
{
    public class PlayerUIManager : MonoBehaviour
    {
        public ContextScreenUI contextScreen;
        public PlayerDetailsUI playerDetails;
        public BossDetailsUI bossDetails;
        public InteractPromptUI interactPrompt;
        public MessagePromptUI messagePrompt;
        public AreaPromptUI areaPrompt;

        private void Awake()
        {
            contextScreen = GetComponentInChildren<ContextScreenUI>();
            playerDetails = GetComponentInChildren<PlayerDetailsUI>();
            bossDetails = GetComponentInChildren<BossDetailsUI>();
            interactPrompt = GetComponentInChildren<InteractPromptUI>();
            messagePrompt = GetComponentInChildren<MessagePromptUI>();
            areaPrompt = GetComponentInChildren<AreaPromptUI>();
        }
    }
}
