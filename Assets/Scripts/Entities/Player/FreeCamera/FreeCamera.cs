using Cinemachine;
using ProjectSteppe.Inputs;
using ProjectSteppe.Managers;
using ProjectSteppe.UI;
using ProjectSteppe.UI.Menus;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace ProjectSteppe
{
    public class FreeCamera : MonoBehaviour
    {
        private CinemachineBrain brain;
        private FreeCameraInput input;

        [SerializeField]
        private float speed = 5;

        private Vector2 moveVector;
        private float verticalMovement;

        private Vector2 lookVector;

        private float pitch, yaw;

        private const float _threshold = 0.01f;

        [System.NonSerialized]
        public MenuBase menu;

        public static FreeCamera instance;

        private bool playerMovementEnabled;

        private void Awake()
        {
            brain = GetComponent<CinemachineBrain>();
            input = new FreeCameraInput();
            instance = this;
            input.Player.Move.performed += Move_performed;
            input.Player.Move.canceled += Move_canceled;
            input.Player.Vertical.performed += Vertical_performed;
            input.Player.Vertical.canceled += Vertical_canceled;
            input.Player.Look.performed += Look_performed;
            input.Player.Look.canceled += Look_canceled;
            input.Player.Back.performed += Back_performed;
        }

        private void Back_performed(InputAction.CallbackContext obj)
        {
            enabled = false;
            MenuBase.SetMenu(menu, true);
        }

        private void Look_canceled(InputAction.CallbackContext obj)
        {
            lookVector = obj.ReadValue<Vector2>();
        }

        private void Look_performed(InputAction.CallbackContext obj)
        {
            lookVector = obj.ReadValue<Vector2>();
        }

        private void Vertical_canceled(InputAction.CallbackContext obj)
        {
            verticalMovement = obj.ReadValue<float>();
        }

        private void Vertical_performed(InputAction.CallbackContext obj)
        {
            verticalMovement = obj.ReadValue<float>();
        }

        private void Move_canceled(InputAction.CallbackContext obj)
        {
            moveVector = obj.ReadValue<Vector2>();
        }

        private void Move_performed(InputAction.CallbackContext obj)
        {
            moveVector = obj.ReadValue<Vector2>();
        }

        private void Update()
        {
            if (lookVector.sqrMagnitude >= _threshold)
            {
                float deltaTimeMultiplier = UIPlayerInput.Instance.controlScheme == UIPlayerInput.ControlScheme.KeyboardMouse ? 1.0f : Time.unscaledDeltaTime;

                yaw += lookVector.x * deltaTimeMultiplier;
                pitch += lookVector.y * deltaTimeMultiplier;
            }

            yaw = ClampAngle(yaw, float.MinValue, float.MaxValue);
            pitch = ClampAngle(pitch, -80, 80);

            transform.rotation = Quaternion.Euler(pitch, yaw, 0);
            transform.Translate(new Vector3(moveVector.x, verticalMovement, moveVector.y) * speed * Time.unscaledDeltaTime);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            while (lfAngle < -360f) lfAngle += 360f;
            while (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnEnable()
        {
            GameManager.Instance.playerManager.GetComponent<StarterAssetsInputs>().respondToData = false;
            brain.enabled = false;
            input.Enable();
            pitch = transform.eulerAngles.x;
            yaw = transform.eulerAngles.y;
            playerMovementEnabled = GameManager.Instance.playerManager.PlayerMovement.enabled;
            GameManager.Instance.playerManager.PlayerMovement.enabled = false;
        }

        private void OnDisable()
        {
            GameManager.Instance.playerManager.PlayerMovement.enabled = playerMovementEnabled;
            brain.enabled = true;
            input.Disable();
        }
    }
}
