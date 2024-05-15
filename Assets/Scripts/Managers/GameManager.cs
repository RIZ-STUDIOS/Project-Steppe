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
        TMP_FontAsset font;

        [SerializeField]
        TMP_SpriteAsset ps5;

        [SerializeField]
        TMP_SpriteAsset xbox;

        [SerializeField]
        TMP_SpriteAsset kbm;

        TextMeshProUGUI[] sceneTMPs;

        public bool hasSecondCheckpoint;

        protected override void Awake()
        {
            base.Awake();
            InputUser.onChange += OnDeviceChange;
            SceneManager.sceneLoaded += GetTMPUGUIs;

            GetTMPUGUIs(default, 0);
            OnDeviceChange(default, 0, InputSystem.devices[0]);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void RespawnCharacter()
        {
            if (!hasSecondCheckpoint) LoadingManager.LoadScene(1);
            else LoadingManager.LoadScene(3);
        }

        public void HasSecondCheckpoint()
        {
            hasSecondCheckpoint = true;
        }

        private void GetTMPUGUIs(Scene newScene, LoadSceneMode mode)
        {
            sceneTMPs = GameObject.FindObjectsByType<TextMeshProUGUI>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        }

        private void OnDeviceChange(InputUser user, InputUserChange userChange, InputDevice device)
        {
            if (device != null)
            {
                TMP_SpriteAsset spriteAsset = kbm;

                Debug.Log(device.description.interfaceName);
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
    }
}
