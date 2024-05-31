using ProjectSteppe.Items;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerUsableItemSlot : MonoBehaviour
    {
        public UsableItem currentUsable;
        private PlayerManager playerManager;

        [SerializeField]
        private Transform hipPouchSlot;

        [SerializeField]
        private Transform handPouchSlot;

        [SerializeField]
        private FollowObject pouchFollowObject;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            SwitchToHipPouch();
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

        public void SwitchToHandPouch()
        {
            pouchFollowObject.transform.position = handPouchSlot.position;
            pouchFollowObject.transform.rotation = handPouchSlot.rotation;
            pouchFollowObject.ChangeFakeParent(handPouchSlot);
        }

        public void SwitchToHipPouch()
        {
            pouchFollowObject.transform.position = hipPouchSlot.position;
            pouchFollowObject.transform.rotation = hipPouchSlot.rotation;
            pouchFollowObject.ChangeFakeParent(hipPouchSlot);
        }
    }
}
