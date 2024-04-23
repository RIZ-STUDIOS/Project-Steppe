using ProjectSteppe.Items;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerUsableItemSlot : MonoBehaviour
    {
        public UsableItem currentUsable;
        private PlayerManager playerManager;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
        }

        public void OnUsable()
        {
            if (playerManager.HasCapability(PlayerCapability.Drink))
                currentUsable.OnUse();
        }

        public void OnAnimationEnd()
        {
            currentUsable.OnAnimationEnd();
        }
    }
}
