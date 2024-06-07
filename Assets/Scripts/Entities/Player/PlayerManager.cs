using Cinemachine;
using ProjectSteppe.Currencies;
using ProjectSteppe.Managers;
using ProjectSteppe.UI;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerManager : MonoBehaviour
    {
        private TargetLock playerTargetLock;
        private Animator playerAnimator;
        private PlayerCamera playerCamera;
        private PlayerUIManager playerUI;
        private StarterAssetsInputs playerInput;
        private PlayerInteractor playerInteractor;
        private Entity playerEntity;
        private PlayerMovementController playerMovementController;
        private PlayerInventory playerInventory;
        private PlayerUsableItemSlot playerUsableItemSlot;
        private CinemachineVirtualCamera virtualCamera;
        private PlayerStatisticHandler statisticHandler;
        private CurrencyContainer currencyContainer;

        public TargetLock PlayerTargetLock => this.GetComponentIfNull(ref playerTargetLock);
        public Animator PlayerAnimator => this.GetComponentIfNull(ref playerAnimator);
        public PlayerCamera PlayerCamera => this.GetComponentIfNull(ref playerCamera);
        public PlayerUIManager PlayerUI => this.GetComponentIfNull(ref playerUI);
        public StarterAssetsInputs PlayerInput => this.GetComponentIfNull(ref playerInput);
        public PlayerInteractor PlayerInteractor => this.GetComponentIfNull(ref playerInteractor);
        public Entity PlayerEntity => this.GetComponentIfNull(ref playerEntity);
        public PlayerMovementController PlayerMovement => this.GetComponentIfNull(ref playerMovementController);
        public PlayerInventory PlayerInventory => this.GetComponentIfNull(ref playerInventory);
        public PlayerUsableItemSlot PlayerUsableItemSlot => this.GetComponentIfNull(ref playerUsableItemSlot);
        public CinemachineVirtualCamera VirtualCamera
        {
            get
            {
                if (!virtualCamera)
                {
                    virtualCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
                }
                return virtualCamera;
            }
        }
        public PlayerStatisticHandler StatisticHandler => this.GetComponentIfNull(ref statisticHandler);
        public CurrencyContainer CurrencyContainer => this.GetComponentIfNull(ref currencyContainer);


        private PlayerCapability capabilities = (PlayerCapability)0b111111;

        public UnityEvent onCapabilityChange;

        [System.NonSerialized]
        public bool bossDead;

        [System.NonSerialized]
        public bool gettingUp;

        private CharacterController characterController;

        private void Awake()
        {
            GameManager.Instance.playerManager = this;
        }

        public void BossDead()
        {
            bossDead = true;
        }

        public void EnableCapability(PlayerCapability capability)
        {
            this.capabilities |= capability;
            onCapabilityChange.Invoke();
        }

        public void DisableCapability(PlayerCapability capability)
        {
            this.capabilities &= ~capability;
            onCapabilityChange.Invoke();
        }

        public void DisableAllCapabilities()
        {
            this.capabilities = 0;
            onCapabilityChange.Invoke();
        }

        public bool HasCapability(PlayerCapability capability)
        {
            return this.capabilities.HasFlag(capability);
        }

        public void DoHit()
        {
            PlayerAnimator.SetTrigger("ForceAnimation");
            PlayerAnimator.SetTrigger("Hit");
        }

        private void DisableCharacterController()
        {
            if (!characterController)
                characterController = GetComponent<CharacterController>();
            characterController.enabled = false;
        }

        private void EnableCharacterController()
        {
            characterController.enabled = true;
        }
    }

    [System.Flags]
    public enum PlayerCapability
    {
        Move = 1,
        Rotate = 2,
        Dash = 4,
        Drink = 8,
        Sit = 16,
        Attack = 32
    }
}
