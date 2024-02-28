using Cinemachine;
using ProjectSteppe.ScriptableObjects.CameraData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.Entities.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        public CinemachineVirtualCamera vCam;

        public Transform mainCameraTransform;

        [SerializeField]
        private CameraDataScriptableObject thirdPersonFollow;

        [SerializeField]
        private CameraDataScriptableObject lockFramingTransposer;

        private Coroutine lerpCameraCoroutine;

        [SerializeField]
        [Range(0.00001f, 1)]
        private float lerpTime = 1;

        private void Start()
        {
            thirdPersonFollow.ApplyCameraData(vCam);
        }

        public void SwitchToThirdPersonFollow()
        {
            if (lerpCameraCoroutine != null)
            {
                StopCoroutine(lerpCameraCoroutine);
                vCam.enabled = true;
                vCam.PreviousStateIsValid = false;
                lerpCameraCoroutine = null;
            }
            var startPos = mainCameraTransform.position;
            var startRot = mainCameraTransform.rotation;
            thirdPersonFollow.ApplyCameraData(vCam);
            vCam.PreviousStateIsValid = false;

            lerpCameraCoroutine = StartCoroutine(LepCameraIEnumerator(startPos, startRot));
        }

        private IEnumerator LepCameraIEnumerator(Vector3 startPosition, Quaternion startRosition)
        {
            yield return null;
            vCam.enabled = false;
            var endPosition = mainCameraTransform.position;
            var endRotation = mainCameraTransform.rotation;
            Vector3 pos = startPosition;
            Quaternion rot = startRosition;
            float time = 0;
            while(pos != endPosition || rot != endRotation)
            {
                pos = Vector3.Lerp(startPosition, endPosition, time);
                rot = Quaternion.Lerp(startRosition, endRotation, time);

                time += Time.deltaTime / lerpTime;

                mainCameraTransform.position = pos;
                mainCameraTransform.rotation = rot;

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
            var startPos = mainCameraTransform.position;
            var startRot = mainCameraTransform.rotation;
            lockFramingTransposer.ApplyCameraData(vCam);
            vCam.PreviousStateIsValid = false;


            lerpCameraCoroutine = StartCoroutine(LepCameraIEnumerator(startPos, startRot));
        }
    }
}
