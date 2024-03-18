using Cinemachine;
using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectSteppe.Entities.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class NewPlayerMovemenet : EntityBehaviour
    {
        private Vector2 moveVector;

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

        private PlayerManager playerManager;
        private float speed;

        private CinemachineVirtualCamera virtualCamera;

        protected override void Awake()
        {
            base.Awake();
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            playerManager = GetComponent<PlayerManager>();
            virtualCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            HandleGravity();
            CheckGrounded();
            HandleMovement();
        }

        private void HandleGravity()
        {
            if (grounded)
            {
                if (playerManager.HasCapability(PlayerCapability.Move))
                {
                    if (jumpPressed)
                    {
                        if (jumping)
                        {
                            jumpPressed = false;
                            return;
                        }

                        jumping = true;
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

            characterController.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
        }

        #region INPUT
        private void OnJump(InputValue value)
        {
            jumpPressed = value.isPressed;
        }

        private void OnMove(InputValue value)
        {
            moveVector = value.Get<Vector2>();
        }
        #endregion
    }
}
