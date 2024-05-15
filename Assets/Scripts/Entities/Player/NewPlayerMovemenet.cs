using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectSteppe.Entities.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class NewPlayerMovemenet : EntityBehaviour
    {
        private Vector2 moveVector;
        private Vector2 dashMoveVector = Vector2.up;
        private float moveVectorMagnitude;

        private Vector2 lookVector;

        private bool dashPressed;

        [SerializeField]
        private float gravity = -9.8f;

        [SerializeField]
        private float jumpForce;

        private float verticalVelocity;
        private bool grounded;

        private bool jumping;

        [SerializeField]
        private float rotationSpeed;

        private CharacterController characterController;

        private Animator animator;

        private bool jumpPressed;

        [Header("Ground check")]
        [SerializeField]
        private float groundedOffset = -0.14f;

        [SerializeField]
        private float groundedRadius;

        [SerializeField]
        private LayerMask groundLayers;

        [SerializeField]
        private float moveSpeed = 5;

        [SerializeField]
        private float jumpMoveSpeed = 2;

        [SerializeField]
        private float dashMoveSpeed = 20;

        private PlayerManager playerManager;
        private float speed;

        private CinemachineVirtualCamera virtualCamera;
        public Camera playerCamera;

        private float targetRotation;
        private float _rotationVelocity;

        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        private const float _threshold = 0.01f;
        private PlayerInput playerInput;
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        private bool dashing;

        [SerializeField]
        private float dashDuration;

        [SerializeField]
        private float dashCooldown;

        private float dashTimer;

        private float dashCooldownTimer;

        protected override void Awake()
        {
            base.Awake();
            characterController = GetComponent<CharacterController>();
            playerInput = GetComponent<PlayerInput>();
            animator = GetComponent<Animator>();
            playerManager = GetComponent<PlayerManager>();
            virtualCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            HandleGravity();
            HandleDash();
            CheckGrounded();
            HandleMovement();
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void HandleDash()
        {
            if ((dashing && dashPressed) || dashCooldownTimer > 0)
                dashPressed = false;

            if (dashPressed)
            {
                dashing = true;
            }

            if (dashing)
            {
                dashTimer += Time.deltaTime;

                if (dashTimer >= dashDuration)
                {
                    dashing = false;
                    dashTimer = 0;
                    dashCooldownTimer = dashCooldown;
                }
            }
            else
            {
                if (dashCooldownTimer > 0)
                {
                    dashCooldownTimer -= Time.deltaTime;
                }
            }
        }

        private void HandleGravity()
        {
            if (grounded)
            {
                if (playerManager.HasCapability(PlayerCapability.Move) && !dashing)
                {
                    if (jumpPressed)
                    {
                        if (jumping)
                        {
                            jumpPressed = false;
                            return;
                        }

                        jumping = true;
                        animator.ResetTrigger("EndJump");
                        animator.SetTrigger("Jump");
                        verticalVelocity = jumpForce;
                    }


                    if (!jumpPressed)
                    {
                        if (jumping && verticalVelocity < 0)
                        {
                            animator.SetTrigger("EndJump");
                            jumping = false;
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

            verticalVelocity += gravity * Time.deltaTime;
        }

        private void CheckGrounded()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset,
                transform.position.z);
            grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers,
                QueryTriggerInteraction.Ignore);
        }

        private void HandleMovement()
        {
            var targetDirection = Vector3.zero;

            var targetSpeed = dashing ? dashMoveSpeed : jumping ? jumpMoveSpeed : moveSpeed;

            if (!playerManager.HasCapability(PlayerCapability.Move)) targetSpeed = 0;

            speed = targetSpeed * (dashing ? 1 : moveVectorMagnitude);

            if (moveVectorMagnitude != 0 || dashing)
            {
                var targetVector = moveVector;

                if (dashing)
                {
                    targetVector = dashMoveVector;
                }

                targetRotation = playerCamera.transform.eulerAngles.y + Mathf.Atan2(targetVector.x, targetVector.y) * Mathf.Rad2Deg;

                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity,
                        RotationSmoothTime);

                if (dashing)
                {
                    rotation = targetRotation;
                }

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            targetDirection = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;

            targetDirection = targetDirection.normalized;
            animator.SetFloat("MoveDirectionX", moveVector.x);
            animator.SetFloat("MoveDirectionY", moveVector.y);
            animator.SetFloat("Speed", dashing ? 1.5f : moveVectorMagnitude);
            characterController.Move(targetDirection * (speed * Time.deltaTime) + new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
        }

        private void CameraRotation()
        {
            if (!virtualCamera.enabled) return;
            // if there is an input and camera position is not fixed
            if (lookVector.sqrMagnitude >= _threshold)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = playerInput.currentControlScheme == "KeyboardMouse" ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += lookVector.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += lookVector.y * deltaTimeMultiplier;
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

        #region INPUT
        private void OnJump(InputValue value)
        {
            jumpPressed = value.isPressed;
        }

        private void OnMove(InputValue value)
        {
            moveVector = value.Get<Vector2>().normalized;
            moveVectorMagnitude = moveVector.magnitude;
            if (moveVectorMagnitude != 0 && !dashing)
            {
                dashMoveVector = moveVector;
            }
        }

        private void OnLook(InputValue value)
        {
            lookVector = value.Get<Vector2>();
        }

        private void OnDash(InputValue value)
        {
            dashPressed = value.isPressed;
        }
        #endregion
    }
}
