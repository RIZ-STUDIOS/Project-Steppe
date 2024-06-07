using ProjectSteppe.UI.Menus;
using UnityEngine;

namespace ProjectSteppe.UI
{
    public class PlayerUIManager : MonoBehaviour
    {
        public ContextScreenUI contextScreen;
        public InteractPromptUI interactPrompt;
        public MessagePromptUI messagePrompt;
        public AreaPromptUI areaPrompt;
        public PlayerDetailsUI playerDetails;
        public CheckpointUI checkpointUI;

        private void Awake()
        {
            contextScreen = GetComponentInChildren<ContextScreenUI>();
            interactPrompt = GetComponentInChildren<InteractPromptUI>();
            messagePrompt = GetComponentInChildren<MessagePromptUI>();
            areaPrompt = GetComponentInChildren<AreaPromptUI>();
            playerDetails = GetComponentInChildren<PlayerDetailsUI>();
            checkpointUI = GetComponentInChildren<CheckpointUI>();
        }
    }
}
