using ProjectSteppe.Items;
using StarterAssets;
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

        private StarterAssetsInputs _input;

        private void Awake()
        {
            _input = GetComponent<StarterAssetsInputs>();
            playerManager = GetComponent<PlayerManager>();
            SwitchToHipPouch();
        }

        private void Update()
        {
            if (_input.useable)
            {
                if (playerManager.HasCapability(PlayerCapability.Drink))
                    currentUsable.OnUse();
                _input.useable = false;
            }
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
