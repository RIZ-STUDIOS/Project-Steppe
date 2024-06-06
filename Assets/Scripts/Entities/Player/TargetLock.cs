using Cinemachine;
using ProjectSteppe.Managers;
using ProjectSteppe.Utilities;
using StarterAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
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

        private PlayerInput playerInput;

        [Header("Target Lock Close Up")]
        [SerializeField]
        private float startDistance = 8;

        [SerializeField]
        private float endDistance = 4;

        [Space]

        [System.NonSerialized]
        public bool lockOn;

        private bool justLocked;

        [SerializeField]
        [FormerlySerializedAs("maxConeLength")]
        private float lockOnDistance;

        [SerializeField]
        private LayerMask checkObjectVisibilityLayer;

        [SerializeField]
        private float lockOffDistance;

        private Camera playerCamera;

        [SerializeField]
        private Transform playerTargetLockLookAt;

        [SerializeField]
        private float cameraMoveLockOnTimer;

        private float cameraMoveLockOnTime;

        public UnityEvent onLockStateChange;

        private bool canLock = true;

        public Vector3 CameraToTargetDistance { get; private set; }

        private const float _threshold = 0.3f;

        private TargetLockTarget storedTarget;

        private void Awake()
        {
            _input = GetComponent<StarterAssetsInputs>();
            playerInput = GetComponent<PlayerInput>();
            playerMovement = GetComponent<PlayerMovementController>();
            playerManager = GetComponent<PlayerManager>();
            playerCamera = playerManager.PlayerCamera.mainCamera;
            playerManager.PlayerEntity.EntityHealth.onKill.AddListener(() =>
            {
                StopLockOn();
                canLock = false;
            });
        }

        private void Start()
        {
            playerTargetLockLookAt.parent = null;
            playerManager.PlayerCamera.targetLockVCam.LookAt = playerTargetLockLookAt;
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
                if (!lockOn)
                    TryLockOn();
                else
                {
                    StopLockOn();
                    justLocked = true;
                }
            }

            if (lookAtTransform)
            {
                CameraToTargetDistance = lookAtTransform.position - playerCamera.transform.position;
            }

            if (lockOn && !lookAtTransform)
            {
                TryLockOn();
            }

            if (lookAtTransform && Vector3.Distance(transform.position, lookAtTransform.position) > lockOffDistance)
            {
                StopLockOn();
                justLocked = true;
            }

            if (cameraMoveLockOnTime <= 0 && lockOn && _input.targetLook.sqrMagnitude >= _threshold)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = playerInput.currentControlScheme == "KeyboardMouse" ? 1.0f : Time.deltaTime;

                AttemptLockOn(_input.targetLook);
                cameraMoveLockOnTime = cameraMoveLockOnTimer;
                justLocked = true;
                _input.targetLook = Vector2.zero;

                //Debug.Log($"X: {_input.look.x * deltaTimeMultiplier} Y: {_input.look.y * deltaTimeMultiplier}");
            }

            if (cameraMoveLockOnTime > 0)
            {
                cameraMoveLockOnTime -= Time.deltaTime;
            }

            if (lookAtTransform)
            {
                playerTargetLockLookAt.position = Vector3.MoveTowards(playerTargetLockLookAt.position, lookAtTransform.position, 90 * Time.deltaTime);

                var distance = Vector3.Distance(transform.position, lookAtTransform.position);
                if (distance < startDistance)
                {
                    playerManager.PlayerCamera.cinemachineCameraOffset.m_Offset.y = -Mathf.Lerp(0, Mathf.Abs(lookAtTransform.position.y - playerManager.PlayerCamera.targetLockVCam.Follow.position.y) / 2f + .35f, 1 - ((distance - endDistance) / (startDistance - endDistance)));
                }
                else
                {
                    playerManager.PlayerCamera.cinemachineCameraOffset.m_Offset.y = 0;
                }
            }
            else
            {
                var closest = GetClosestTarget();
                if (closest)
                {
                    playerTargetLockLookAt.position = closest.transform.position;
                }
            }
        }

        private void TryLockOn()
        {
            AttemptLockOn();
            justLocked = true;
        }

        private void AttemptLockOn(Vector2 offset)
        {
            if (lockOn)
            {
                var targetLockTarget = GetClosestTarget(offset);
                if (targetLockTarget && targetLockTarget.lookAtTransform != lookAtTransform)
                {
                    SetLockTarget(targetLockTarget);
                    SetLockOn(true);
                }
            }
            else
            {
                var targetLockTarget = GetClosestTarget(offset);
                if (targetLockTarget)
                {
                    SetLockTarget(targetLockTarget);
                    SetLockOn(true);
                }
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
            return GetClosestTarget(Vector2.zero);
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

            if(!playerManager.PlayerMovement.sprinting)
            playerManager.PlayerAnimator.SetBool("Strafing", lockOn);

            onLockStateChange.Invoke();
        }

        private void SetLockTarget(TargetLockTarget target)
        {
            var prevTarget = currentTargetLock;
            currentTargetLock = target;
            if(!prevTarget && currentTargetLock)
            {
                playerTargetLockLookAt.position = currentTargetLock.transform.position;
            }
            if (target && target != prevTarget)
            {
                playerManager.PlayerCamera.SwitchToLockFramingTransposer();
            }
            else if(!target)
            {
                playerManager.PlayerCamera.SwitchToThirdPersonFollow();
            }
        }

        private TargetLockTarget GetClosestTarget()
        {
            return GetClosestTarget(Vector2.zero);
        }

        private TargetLockTarget GetClosestTarget(Vector2 dir)
        {
            var viewPortPosition = new Vector3(0.5f, 0.5f, 0);
            if (currentTargetLock)
                viewPortPosition = currentTargetLock.ViewPortPosition;// + new Vector3(dir.x, dir.y);
            bool anyDir = dir == Vector2.zero;
            // True = Right False = Left
            bool leftRight = dir.x > 0f;

            float distance = float.MaxValue;
            TargetLockTarget targetLockTarget = null;

            foreach (var target in GameManager.Instance.visibleTargets)
            {
                if (target.ViewPortPosition.z >= lockOnDistance || target == currentTargetLock) continue;
                var cameraPos = GameManager.Instance.playerManager.PlayerCamera.MainCameraTransform.position;
                var dis = (target.transform.position - cameraPos);
                if (Physics.Raycast(cameraPos, dis, out var hitInfo, lockOnDistance, checkObjectVisibilityLayer))
                {
                    if (target.ViewPortPosition.z > hitInfo.distance)
                        continue;
                }
                Vector2 temp = viewPortPosition;
                Vector2 temp2 = target.ViewPortPosition;
                if (anyDir || (((leftRight && temp2.x > temp.x) || (!leftRight && temp2.x < temp.x))))
                {
                    var tempDistance = Vector2.Distance(temp, temp2);
                    if (tempDistance < distance)
                    {
                        distance = tempDistance;
                        targetLockTarget = target;
                    }
                }
            }

            return targetLockTarget;
        }

        //Animation
        public void StoreCurrentTarget()
        {
            storedTarget = currentTargetLock;
            StopLockOn();
            canLock = false;
        }

        public void RestoreTarget()
        {
            if (storedTarget)
            {
                SetLockTarget(storedTarget);
                SetLockOn(true);
            }
            canLock = true;
        }
    }
}
