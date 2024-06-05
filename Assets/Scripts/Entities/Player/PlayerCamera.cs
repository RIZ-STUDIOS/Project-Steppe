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
        private CameraDataScriptableObject lockFramingTransposer;

        [System.NonSerialized]
        public CinemachineCameraOffset cinemachineCameraOffset;

        private Coroutine lerpCameraCoroutine;

        [SerializeField]
        [Range(0.00001f, 1)]
        private float lerpTime = 1;

        private void Start()
        {
            thirdPersonFollow.ApplyCameraData(thirdPersonVCam);
            lockFramingTransposer.ApplyCameraData(targetLockVCam);
            cinemachineCameraOffset = targetLockVCam.GetComponent<CinemachineCameraOffset>();
        }

        public void SwitchToThirdPersonFollow()
        {
            cinemachineCameraOffset.m_Offset.y = 0;
            targetLockVCam.Priority = 0;
            thirdPersonVCam.Priority = 10;
            /*cinemachineCameraOffset.m_Offset.y = 0;
            if (lerpCameraCoroutine != null)
            {
                StopCoroutine(lerpCameraCoroutine);
                thirdPersonVCam.enabled = true;
                thirdPersonVCam.PreviousStateIsValid = false;
                lerpCameraCoroutine = null;
            }
            var startPos = MainCameraTransform.position;
            var startRot = MainCameraTransform.rotation;
            thirdPersonFollow.ApplyCameraData(thirdPersonVCam);
            thirdPersonVCam.PreviousStateIsValid = false;

            lerpCameraCoroutine = StartCoroutine(LepCameraIEnumerator(startPos, startRot));*/
        }

        private IEnumerator LepCameraIEnumerator(Vector3 startPosition, Quaternion startRosition)
        {
            yield return null;
            thirdPersonVCam.enabled = false;
            var endPosition = MainCameraTransform.position;
            var endRotation = MainCameraTransform.rotation;
            Vector3 pos = startPosition;
            Quaternion rot = startRosition;
            float time = 0;
            while ((pos != endPosition || rot != endRotation) && time < 1)
            {
                pos = Vector3.Lerp(startPosition, endPosition, time);
                rot = Quaternion.Lerp(startRosition, endRotation, time);

                time += Time.deltaTime / lerpTime;

                MainCameraTransform.position = pos;
                MainCameraTransform.rotation = rot;

                yield return null;
            }

            thirdPersonVCam.enabled = true;
        }

        public void SwitchToLockFramingTransposer()
        {
            thirdPersonVCam.Priority = 0;
            targetLockVCam.Priority = 10;
            /*if (lerpCameraCoroutine != null)
            {
                StopCoroutine(lerpCameraCoroutine);
                thirdPersonVCam.enabled = true;
                thirdPersonVCam.PreviousStateIsValid = false;
                lerpCameraCoroutine = null;
            }
            var startPos = MainCameraTransform.position;
            var startRot = MainCameraTransform.rotation;
            lockFramingTransposer.ApplyCameraData(thirdPersonVCam);
            thirdPersonVCam.PreviousStateIsValid = false;


            lerpCameraCoroutine = StartCoroutine(LepCameraIEnumerator(startPos, startRot));*/
        }
    }
}
