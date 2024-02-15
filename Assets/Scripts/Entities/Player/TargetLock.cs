using Cinemachine;
using ProjectSteppe.Utilities;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace ProjectSteppe.Entities.Player
{
    public class TargetLock : MonoBehaviour
    {
        private StarterAssetsInputs _input;

        private Transform lookAtTransform;
        private PlayerMovementController playerMovement;

        /*[SerializeField]
        private Transform bossTransform;*/

        [SerializeField]
        private CinemachineVirtualCamera thirdPersonVirtualCamera;

        [SerializeField]
        private CinemachineVirtualCamera targetLockVirtualCamera;

        [System.NonSerialized]
        public bool lockOn;

        private bool justLocked;

        [SerializeField]
        private LayerMask targetLockLayer;

        [SerializeField]
        private float maxConeRadius;

        [SerializeField]
        private float maxConeLength;

        [SerializeField]
        private float coneAngle;

        private void Awake()
        {
            _input = GetComponent<StarterAssetsInputs>();
            playerMovement = GetComponent<PlayerMovementController>();
            if (!thirdPersonVirtualCamera)
                thirdPersonVirtualCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
            if (!targetLockVirtualCamera)
                targetLockVirtualCamera = GameObject.FindGameObjectWithTag("TargetLockCamera").GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            if(_input.targetLock && justLocked)
            {
                _input.targetLock = false;
            }

            if(!_input.targetLock && justLocked)
            {
                justLocked = false;
            }

            if(_input.targetLock && !justLocked)
            {
                if (lockOn)
                    StopLockOn();
                else
                    StartLockOn();
                justLocked = true;
            }

            if (lockOn && lookAtTransform)
            {
                Vector3 dir = lookAtTransform.position - transform.position;
                var d = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime).eulerAngles;
                transform.rotation = Quaternion.Euler(0, d.y, 0);
            }
        }

        private void StartLockOn()
        {
            var hits = ConeCastExtension.ConeCastAll(transform.position, maxConeRadius, playerMovement.playerCamera.transform.forward, maxConeLength, coneAngle, targetLockLayer);
            int index = -1;
            float angle = float.MaxValue;
            for (int i = 0; i < hits.Length; i++)
            {
                var hit = hits[i];
                if (!hit.collider.GetComponentInParent<TargetLockTarget>()) return;
                Vector3 hitPoint = hit.point;
                Vector3 directionToHit = hitPoint - transform.position;
                float angleToHit = Vector3.Angle(playerMovement.playerCamera.transform.forward, directionToHit);

                if (angleToHit < angle)
                {
                    index = i;
                    angle = angleToHit;
                }
            }
            if (index == -1) return;
            var lookHit = hits[index];
            var targetLockTarget = lookHit.collider.GetComponentInParent<TargetLockTarget>();
            SetLockTarget(targetLockTarget.lookAtTransform);
            SetLockOn(true);
        }

        private void StopLockOn()
        {
            SetLockTarget(null);
            SetLockOn(false);
        }

        private void SetLockOn(bool lockOn)
        {
            this.lockOn = lockOn;
            thirdPersonVirtualCamera.enabled = !lockOn;
            targetLockVirtualCamera.enabled = lockOn;
            //playerMovement.strafe = lockOn;
        }

        private void SetLockTarget(Transform target)
        {
            lookAtTransform = target;
            targetLockVirtualCamera.LookAt = target;
        }
    }
}
