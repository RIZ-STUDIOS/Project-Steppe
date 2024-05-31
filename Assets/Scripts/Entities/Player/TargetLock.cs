using ProjectSteppe.Utilities;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ProjectSteppe.Entities.Player
{
    public class TargetLock : MonoBehaviour
    {
        private StarterAssetsInputs _input;

        [System.NonSerialized]
        public Transform lookAtTransform;
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

        public UnityEvent onLockStateChange;

        private bool canLock = true;

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
                    SetLockTarget(targetLockTarget.lookAtTransform);
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
                    SetLockTarget(targetLockTarget.lookAtTransform);
                    SetLockOn(true);
                }
            }
        }

        private TargetLockTarget StartLockOn()
        {
            var hits = ConeCastExtension.ConeCastAll(playerMovement.playerCamera.transform.position, maxConeRadius, playerMovement.playerCamera.transform.forward, lockOnDistance, coneAngle, targetLockLayer);
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
            var targetLockTarget = lookHit.collider.GetComponentInParent<TargetLockTarget>();
            return targetLockTarget;
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
