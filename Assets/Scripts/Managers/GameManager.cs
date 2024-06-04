using ProjectSteppe.Entities.Player;
using ProjectSteppe.Saving;
using RicTools.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

namespace ProjectSteppe.Managers
{
    public class GameManager : GenericManager<GameManager>
    {
        [SerializeField]
        private TMP_FontAsset font;

        [SerializeField]
        private TMP_SpriteAsset ps5;

        [SerializeField]
        private TMP_SpriteAsset xbox;

        [SerializeField]
        private TMP_SpriteAsset kbm;

#if UNITY_EDITOR
        [SerializeField]
        private InputType debugInput;
#endif

        TextMeshProUGUI[] sceneTMPs;

        public int defaultGameSceneIndex = SceneConstants.LEVEL_1_INDEX;

        protected override bool DontDestroyManagerOnLoad => true;

        public AvailableInventoryItemsScriptableObject availableItems;

        public PlayerManager playerManager;

        public int currentDeviceIndex = 0;

        protected override void Awake()
        {
            base.Awake();
            if (!SaveHandler.InitSave()) SaveHandler.LoadGame();

            InputUser.onChange += OnDeviceChange;
            SceneManager.sceneLoaded += GetTMPUGUIs;

            GetTMPUGUIs(default, 0);
            OnDeviceChange(default, 0, InputSystem.devices[currentDeviceIndex]);

            SceneManager.sceneLoaded += OnSceneLoaded;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void RespawnCharacter()
        {
            LoadingManager.LoadScene(SaveHandler.CurrentSave.currentSceneIndex);
        }

        private void GetTMPUGUIs(Scene newScene, LoadSceneMode mode)
        {
            sceneTMPs = FindObjectsByType<TextMeshProUGUI>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        }

        private void OnDeviceChange(InputUser user, InputUserChange userChange, InputDevice device)
        {
            if (device == null) return;

#if UNITY_EDITOR
            if (debugInput != InputType.None)
            {
                UpdateDebugInput();
                return;
            }
#endif

            if (userChange == InputUserChange.DevicePaired || userChange == InputUserChange.Added)
            {
                TMP_SpriteAsset spriteAsset;
                switch (device.description.interfaceName)
                {
                    case "HID":
                        spriteAsset = ps5;

                        break;

                    case "XInput":
                        spriteAsset = xbox;

                        break;

                    default:
                        spriteAsset = kbm;

                        break;
                }

                foreach (var tmp in sceneTMPs)
                {
                    if (tmp)
                        tmp.spriteAsset = spriteAsset;
                }
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            GetTMPUGUIs(default, 0);
            OnDeviceChange(default, 0, InputSystem.devices[currentDeviceIndex]);
        }

#if UNITY_EDITOR
        private void UpdateDebugInput()
        {
            TMP_SpriteAsset spriteAsset;
            if (sceneTMPs == null) return;
            if (debugInput == InputType.None)
            {
                return;
            }
            switch (debugInput)
            {
                default:
                case InputType.Kb:
                    spriteAsset = kbm;
                    break;
                case InputType.Xbox:
                    spriteAsset = xbox;
                    break;
                case InputType.PS:
                    spriteAsset = ps5;
                    break;
            }
            foreach (var tmp in sceneTMPs)
            {
                if (tmp)
                    tmp.spriteAsset = spriteAsset;
            }
        }

        private void OnValidate()
        {
            UpdateDebugInput();
        }
#endif

#if UNITY_EDITOR
        private enum InputType
        {
            None,
            Kb,
            PS,
            Xbox
        }
#endif
    }
}
