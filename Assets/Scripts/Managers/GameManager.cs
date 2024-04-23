using RicTools.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectSteppe.Managers
{
    public class GameManager : GenericManager<GameManager>
    {
        public bool hasSecondCheckpoint;

        private void Awake()
        {
            InputSystem.onDeviceChange += OnDeviceChange;
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

        private void OnDeviceChange(InputDevice device, InputDeviceChange deviceChange)
        {
            Debug.Log(deviceChange.ToString());
        }
    }
}
