using Cinemachine;
using ProjectSteppe.ScriptableObjects.CameraData;
using System.Collections;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        public CinemachineVirtualCamera vCam;

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
            thirdPersonFollow.ApplyCameraData(vCam);
            cinemachineCameraOffset = vCam.GetComponent<CinemachineCameraOffset>();
        }

        public void SwitchToThirdPersonFollow()
        {
            cinemachineCameraOffset.m_Offset.y = 0;
            if (lerpCameraCoroutine != null)
            {
                StopCoroutine(lerpCameraCoroutine);
                vCam.enabled = true;
                vCam.PreviousStateIsValid = false;
                lerpCameraCoroutine = null;
            }
            var startPos = MainCameraTransform.position;
            var startRot = MainCameraTransform.rotation;
            thirdPersonFollow.ApplyCameraData(vCam);
            vCam.PreviousStateIsValid = false;

            lerpCameraCoroutine = StartCoroutine(LepCameraIEnumerator(startPos, startRot));
        }

        private IEnumerator LepCameraIEnumerator(Vector3 startPosition, Quaternion startRosition)
        {
            yield return null;
            vCam.enabled = false;
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

            vCam.enabled = true;
        }

        public void SwitchToLockFramingTransposer()
        {
            if (lerpCameraCoroutine != null)
            {
                StopCoroutine(lerpCameraCoroutine);
                vCam.enabled = true;
                vCam.PreviousStateIsValid = false;
                lerpCameraCoroutine = null;
            }
            var startPos = MainCameraTransform.position;
            var startRot = MainCameraTransform.rotation;
            lockFramingTransposer.ApplyCameraData(vCam);
            vCam.PreviousStateIsValid = false;


            lerpCameraCoroutine = StartCoroutine(LepCameraIEnumerator(startPos, startRot));
        }
    }
}
