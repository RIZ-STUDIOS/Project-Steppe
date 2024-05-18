using RicTools.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
using ProjectSteppe.Saving;

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

        TextMeshProUGUI[] sceneTMPs;

        public int defaultGameSceneIndex;

        protected override bool DontDestroyManagerOnLoad => true;

        protected override void Awake()
        {
            base.Awake();
            if (!SaveHandler.InitSave()) SaveHandler.LoadGame();

            InputUser.onChange += OnDeviceChange;
            SceneManager.sceneLoaded += GetTMPUGUIs;

            GetTMPUGUIs(default, 0);
            OnDeviceChange(default, 0, InputSystem.devices[0]);

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
            if (device != null)
            {
                TMP_SpriteAsset spriteAsset = kbm;

                //Debug.Log(device.description.interfaceName);
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
                    tmp.spriteAsset = spriteAsset;
                }
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            GetTMPUGUIs(default, 0);
            OnDeviceChange(default, 0, InputSystem.devices[0]);
        }
    }
}
