using ProjectSteppe.Managers;
using ProjectSteppe.Utilities;
using StarterAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ProjectSteppe.Entities.Player
{
    public class TargetLock : MonoBehaviour
    {
        private StarterAssetsInputs _input;

        public Transform lookAtTransform => currentTargetLock ? currentTargetLock.transform : null;

        private TargetLockTarget currentTargetLock;

        private PlayerMovementController playerMovement;
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
        [FormerlySerializedAs("maxConeLength")]
        private float lockOnDistance;

        [SerializeField]
        private float lockOffDistance;

        [SerializeField]
        private float coneAngle;

        [SerializeField]
        private Camera playerCamera;

        public UnityEvent onLockStateChange;

        private bool canLock = true;

        public Vector3 CameraToTargetDistance { get; private set; }

        private void Awake()
        {
            _input = GetComponent<StarterAssetsInputs>();
            playerMovement = GetComponent<PlayerMovementController>();
            playerManager = GetComponent<PlayerManager>();
            playerManager.PlayerEntity.EntityHealth.onKill.AddListener(() =>
            {
                StopLockOn();
                canLock = false;
            });
        }

        private void Update()
        {
            if (_input.targetLock && justLocked)
            {
                _input.targetLock = false;
            }

            if (!_input.targetLock && justLocked)
            {
                justLocked = false;
            }

            if (_input.targetLock && !justLocked && canLock)
            {
                AttemptLockOn();
                justLocked = true;
            }

            if (lookAtTransform)
            {
                CameraToTargetDistance = lookAtTransform.position - playerCamera.transform.position;
            }

            if (lockOn && !lookAtTransform)
            {
                StopLockOn();
            }

            if(lookAtTransform && Vector3.Distance(transform.position, lookAtTransform.position) > lockOnDistance)
            {
                StopLockOn();
            }
        }

        private void AttemptLockOn()
        {
            if (lockOn)
            {
                var targetLockTarget = StartLockOn();
                if (targetLockTarget && targetLockTarget.lookAtTransform != lookAtTransform)
                {
                    SetLockTarget(targetLockTarget);
                    SetLockOn(true);
                }
                else
                {
                    StopLockOn();
                }
            }
            else
            {
                var targetLockTarget = StartLockOn();
                if (targetLockTarget)
                {
                    SetLockTarget(targetLockTarget);
                    SetLockOn(true);
                }
            }
        }

        private TargetLockTarget StartLockOn()
        {
            /*var hits = ConeCastExtension.ConeCastAll(playerMovement.playerCamera.transform.position, maxConeRadius, playerMovement.playerCamera.transform.forward, lockOnDistance, coneAngle, targetLockLayer);
            int index = -1;
            float angle = float.MaxValue;
            for (int i = 0; i < hits.Length; i++)
            {
                var hit = hits[i];
                var temp = hit.collider.GetComponentInParent<TargetLockTarget>();
                if (!temp) return null;
                Vector3 hitPoint = hit.point;
                Vector3 directionToHit = hitPoint - transform.position;
                float angleToHit = Vector3.Angle(playerMovement.playerCamera.transform.forward, directionToHit);

                if (angleToHit < angle && (temp && temp.lookAtTransform != lookAtTransform))
                {
                    index = i;
                    angle = angleToHit;
                }
            }
            if (index == -1) return null;
            var lookHit = hits[index];
            var targetLockTarget = lookHit.collider.GetComponentInParent<TargetLockTarget>();*/
            return GetClosestTarget();
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

        private void SetLockTarget(TargetLockTarget target)
        {
            currentTargetLock = target;
            playerManager.PlayerCamera.vCam.LookAt = lookAtTransform;
            if (target)
            {
                playerManager.PlayerCamera.SwitchToLockFramingTransposer();
            }
            else
            {
                playerManager.PlayerCamera.SwitchToThirdPersonFollow();
            }
        }

        private TargetLockTarget GetClosestTarget()
        {
            var viewPortPosition = new Vector3(0.5f, 0.5f, 0);
            if (currentTargetLock)
                viewPortPosition = currentTargetLock.ViewPortPosition;

            float distance = float.MaxValue;
            TargetLockTarget targetLockTarget = null;

            foreach(var target in GameManager.Instance.visibleTargets)
            {
                if (target.ViewPortPosition.z >= lockOnDistance || target == currentTargetLock) continue;
                Vector2 temp = viewPortPosition;
                Vector2 temp2 = target.ViewPortPosition;

                var tempDistance = Vector2.Distance(temp, temp2);
                if(tempDistance < distance)
                {
                    distance = tempDistance;
                    targetLockTarget = target;
                }
            }

            return targetLockTarget;
        }
    }
}
