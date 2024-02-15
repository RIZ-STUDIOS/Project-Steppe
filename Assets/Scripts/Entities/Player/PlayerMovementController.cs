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
    public class PlayerMovementController : MonoBehaviour
    {
        [Header("Gravity")]
        [SerializeField]
        private float playerGravity = -10;

        [SerializeField]
        private float terminalVelocity = -53f;

        [Header("Movement")]

        [SerializeField]
        private float walkSpeed = 2f;

        [SerializeField]
        private float speedChangeRate = 10;

        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Header("Camera")]

        [SerializeField]
        private Camera playerCamera;

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

        [Header("Events")]
        public UnityEvent onJump;
        public UnityEvent onDash;

        private float verticalVelocity;
        private bool usingGravity = true;

        private bool grounded;
        private float speed;
        private float animationBlend;
        private float targetRotation;

        public bool Grounded => grounded;

        private StarterAssetsInputs _input;
        private CharacterController characterController;
        private Animator animator;
        private PlayerInput playerInput;

        private CinemachineVirtualCamera virtualCamera;

        private int _animIDJump;
        private int _animIDMotionSpeed;
        private int _animIDSpeed;
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;
        private float _rotationVelocity;

        private const float _threshold = 0.01f;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
            playerInput = GetComponent<PlayerInput>();
            animator = GetComponent<Animator>();
            virtualCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            _animIDJump = Animator.StringToHash("Jumping");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDSpeed = Animator.StringToHash("Speed");
        }

        private void Update()
        {
            CheckJump();
            CheckGrounded();
            CheckMovement();
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void CheckJump()
        {
            if (Grounded)
            {
                if (!_input.jump)
                {
                    usingGravity = true;
                    animator.SetBool(_animIDJump, false);
                }

                if(verticalVelocity < 0)
                {
                    verticalVelocity = -2;
                }

                if (_input.jump)
                {
                    usingGravity = false;
                    verticalVelocity = 0;
                    animator.SetBool(_animIDJump, true);
                    onJump.Invoke();
                }
            }
            else
            {
                _input.jump = false;
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
            float targetSpeed = walkSpeed;

            if (_input.move == Vector2.zero) targetSpeed = 0;

            float currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0.0f, characterController.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * speedChangeRate);

                // round speed to 3 decimal places
                speed = Mathf.Round(speed * 1000f) / 1000f;
            }
            else
            {
                speed = targetSpeed;
            }

            if (_input.blocking) speed *= 0.9f;

            Debug.Log(speed);

            animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);
            if (animationBlend < 0.01f) animationBlend = 0f;

            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            if(_input.move != Vector2.zero)
            {
                targetRotation = playerCamera.transform.eulerAngles.y + Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;

                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity,
                        RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            characterController.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0, usingGravity ? verticalVelocity : 0, 0) * Time.deltaTime);

            animator.SetFloat(_animIDSpeed, animationBlend);
            animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
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
