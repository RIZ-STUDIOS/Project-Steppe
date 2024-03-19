using Cinemachine;
using ProjectSteppe.Utilities;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.HID;

namespace ProjectSteppe.Entities.Player
{
    public class TargetLock : MonoBehaviour
    {
        private StarterAssetsInputs _input;

        [System.NonSerialized]
        public Transform lookAtTransform;
        private NewPlayerMovemenet playerMovement;
        private PlayerManager playerManager;

        /*[SerializeField]
        private Transform bossTransform;*/

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

        public UnityEvent onLockStateChange;

        private void Awake()
        {
            _input = GetComponent<StarterAssetsInputs>();
            playerMovement = GetComponent<NewPlayerMovemenet>();
            playerManager = GetComponent<PlayerManager>();
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

            if(lockOn && !lookAtTransform)
            {
                StopLockOn();
            }
        }

        private void StartLockOn()
        {
            var hits = ConeCastExtension.ConeCastAll(playerMovement.playerCamera.transform.position, maxConeRadius, playerMovement.playerCamera.transform.forward, maxConeLength, coneAngle, targetLockLayer);
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
            //playerMovement.strafe = lockOn;

            playerManager.PlayerAnimator.SetBool("Strafing", lockOn);

            onLockStateChange.Invoke();
        }

        private void SetLockTarget(Transform target)
        {
            lookAtTransform = target;
            playerManager.PlayerCamera.vCam.LookAt = target;
            if (target)
            {
                playerManager.PlayerCamera.SwitchToLockFramingTransposer();
            }
            else
            {
                playerManager.PlayerCamera.SwitchToThirdPersonFollow();
            }
        }
    }
}
