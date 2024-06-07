using Cinemachine;
using ProjectSteppe.ScriptableObjects.CameraData;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [FormerlySerializedAs("vCam")]
        public CinemachineVirtualCamera thirdPersonVCam;
        public CinemachineVirtualCamera targetLockVCam;

        public Transform MainCameraTransform => mainCamera.transform;

        public Camera mainCamera;

        [SerializeField]
        private CameraDataScriptableObject thirdPersonFollow;

        [SerializeField]
        private CameraDataScriptableObject lockFramingTransposer;=

        private void Start()
        {
            thirdPersonFollow.ApplyCameraData(thirdPersonVCam);
            //lockFramingTransposer.ApplyCameraData(targetLockVCam);
        }

        public void SwitchToThirdPersonFollow()
        {
            thirdPersonVCam.Priority = 10;
            targetLockVCam.Priority = 0;
        }

        public void SwitchToLockFramingTransposer()
        {
            targetLockVCam.Priority = 10;
            thirdPersonVCam.Priority = 0;
        }
    }
}
