using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

namespace ProjectSteppe.Entities.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController : EntityBehaviour
    {
        [Header("Gravity")]
        [SerializeField]
        private float playerGravity = -10;

        [SerializeField]
        private float terminalVelocity = -53f;

        [SerializeField]
        private float jumpForce = 15;

        [Header("Movement")]

        [SerializeField]
        private float walkSpeed = 2f;

        [SerializeField]
        private float speedChangeRate = 10;

        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Header("Camera")]

        public Camera playerCamera;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Header("Ground check")]
        [SerializeField]
        private float groundedOffset = -0.14f;

        [SerializeField]
        private float groundedRadius;

        [SerializeField]
        private LayerMask groundLayers;

        [Header("Dashing")]

        [SerializeField]
        private float dashSpeed = 2f;

        [SerializeField]
        private float dashTime = 2f;

        [SerializeField]
        private float dashCooldown = 2f;

        private float dashTimer;
        private float dashCooldownTimer;

        [Header("Events")]
        public UnityEvent onJump;
        public UnityEvent onDashStart;
        public UnityEvent onDashEnd;
        public UnityEvent onGround;
        public System.Action<float, float, float, float> onMoveAnimator;

        public float verticalVelocity;
        private bool usingGravity = true;

        private bool grounded;
        private float speed;
        private float animationBlend;
        private float targetRotation;

        public bool Grounded => grounded;

        private StarterAssetsInputs _input;
        private CharacterController characterController;
        //private Animator animator;
        private PlayerInput playerInput;
        private PlayerManager playerManager;
        private PlayerMovementController playerMovement;

        private CinemachineVirtualCamera virtualCamera;
        
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;
        private float _rotationVelocity;

        private const float _threshold = 0.01f;

        private bool dashing;

        private Vector3 moveDirection;

        [System.NonSerialized]
        public bool jumping;

        protected override void Awake()
        {
            base.Awake();
            characterController = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
            playerInput = GetComponent<PlayerInput>();
            //animator = GetComponent<Animator>();
            virtualCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
            playerManager = GetComponent<PlayerManager>();
            playerMovement = GetComponent<PlayerMovementController>();
        }

        private void Update()
        {
            CheckJump();
            CheckGrounded();
            CheckDash();
            CheckMovement();
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void CheckDash()
        {
            if (_input.dash)
            {
                if (!dashing && dashCooldownTimer <= 0 && playerManager.HasCapability(PlayerCapability.Dash))
                {
                    dashing = true;
                    dashTimer = 0;
                    Entity.EntityHealth.SetInvicible(true);
                    onDashStart.Invoke();
                }
                _input.dash = false;
            }

            if (dashing)
            {
                dashTimer += Time.deltaTime;
                if(dashTimer >= dashTime)
                {
                    DisableDashing();
                    onDashEnd.Invoke();
                }
            }
            else
            {
                if(dashCooldownTimer > 0)
                dashCooldownTimer -= Time.deltaTime;
            }
        }

        public void OnPlayerCapability()
        {
            if (!playerManager.HasCapability(PlayerCapability.Dash))
            {
                DisableDashing();
            }
        }

        private void DisableDashing()
        {
            dashing = false;
            dashCooldownTimer = dashCooldown;
            Entity.EntityHealth.SetInvicible(false);
        }

        private void CheckJump()
        {
            if (Grounded)
            {
                if (playerManager.HasCapability(PlayerCapability.Move))
                {
                    if (_input.jump)
                    {
                        if (jumping)
                        {
                            _input.jump = false;
                            return;
                        }

                        jumping = true;
                        onJump.Invoke();
                        Entity.EntityHealth.SetInvicible(true);
                        verticalVelocity = jumpForce;
                    }

                    if (!_input.jump)
                    {
                        if (jumping && verticalVelocity < 0)
                        {
                            jumping = false;
                            onGround?.Invoke();
                            Entity.EntityHealth.SetInvicible(false);
                        }
                    }
                }

                if (verticalVelocity < 0)
                {
                    verticalVelocity = -2;
                }
            }
            else
            {

            }

            if(verticalVelocity > terminalVelocity && usingGravity)
            {
                verticalVelocity += playerGravity * Time.deltaTime;
            }
        }

        private void CheckGrounded()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset,
                transform.position.z);
            grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers,
                QueryTriggerInteraction.Ignore);
        }

        private void CheckMovement()
        {
            float targetSpeed = dashing ? dashSpeed : walkSpeed;

            if(!dashing && (_input.move == Vector2.zero || !playerManager.HasCapability(PlayerCapability.Move))) targetSpeed = 0;

            float currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0.0f, characterController.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            /*if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * speedChangeRate);

                // round speed to 3 decimal places
                //speed = Mathf.Round(speed * 1000f) / 1000f;
            }
            else
            {
            }*/
            speed = targetSpeed;

            animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);
            if (animationBlend < 0.01f) animationBlend = 0f;

            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            if((_input.move != Vector2.zero || dashing) && playerManager.HasCapability(PlayerCapability.Rotate))
            {
                if (playerManager.PlayerTargetLock.lockOn)
                {
                    targetRotation = playerCamera.transform.eulerAngles.y;
                }
                if (!dashing)
                {
                    if (!playerManager.PlayerTargetLock.lockOn)
                    {
                        targetRotation = playerCamera.transform.eulerAngles.y + Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
                    }
                    else
                    {
                    }
                this.moveDirection = inputDirection;
                }

                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity,
                        RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            if (playerManager.PlayerTargetLock.lockOn && !dashing)
            {
                targetDirection = Quaternion.Euler(0, targetRotation + Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg, 0) * Vector3.forward;
            }
            else if(playerManager.PlayerTargetLock.lockOn && dashing)
            {
                targetDirection = Quaternion.Euler(0, targetRotation + Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg, 0) * Vector3.forward;
            }

            //if (jumping) targetDirection.y = Mathf.Sqrt(5 * 2 * -9.8f);

            characterController.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0, verticalVelocity, 0) * Time.deltaTime);            


            // FOR ANIMATOR //
            float velX;
            float velY;

            if (playerManager.PlayerTargetLock.lockOn)
            {
                velX = _input.move.x;
                velY = _input.move.y;

                if (dashing)
                {
                    var d = moveDirection.normalized;
                    velX = d.x > 0 ? 1 : 0;
                    velY = d.z > 0? 1 : 0;
                }
            }
            else
            {
                velX = 0;
                velY = animationBlend / playerMovement.walkSpeed;
            }

            onMoveAnimator?.Invoke(animationBlend, inputMagnitude, velX, velY);
        }

        

        private void CameraRotation()
        {
            if (!virtualCamera.enabled) return;
            // if there is an input and camera position is not fixed
            if (_input.look.sqrMagnitude >= _threshold)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = playerInput.currentControlScheme == "KeyboardMouse" ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            virtualCamera.Follow.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + 0,
                _cinemachineTargetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
