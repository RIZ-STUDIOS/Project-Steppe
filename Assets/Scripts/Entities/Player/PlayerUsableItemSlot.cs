using ProjectSteppe.Items;
using StarterAssets;
using UnityEngine;
using UnityEngine.Windows;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerUsableItemSlot : MonoBehaviour
    {
        public UsableItem currentUsable;

        public void OnUsable()
        {
            currentUsable.OnUse();
        }
    }
}
