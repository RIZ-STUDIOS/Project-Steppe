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
        private new Camera camera;

        [SerializeField]
        private float speed = 10;

        [SerializeField]
        private float verticalSpeed = 10;

        [SerializeField]
        private float minimumFOV = 20;

        [SerializeField]
        private float maximumFOV = 90;

        [SerializeField]
        private float fovIncrement = 5;

        [SerializeField]
        private float speedDownMultipler = 0.5f;

        private float speedMultiplier = 1;

        private Vector2 moveVector;
        private float verticalMovement;

        private Vector2 lookVector;

        private float pitch, yaw;

        private const float _threshold = 0.01f;

        [System.NonSerialized]
        public MenuBase menu;

        public static FreeCamera instance;

        private bool playerMovementEnabled;

        private List<Canvas> canvases = new List<Canvas>();
        private List<Canvas> worldCanvas = new List<Canvas>();

        private ScreenshotTaker screenshotTaker;

        private void Awake()
        {
            camera = GetComponent<Camera>();
            screenshotTaker = GetComponent<ScreenshotTaker>();
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
            input.Player.Screenshot.performed += Screenshot_performed;
            input.Player.SlowDown.performed += SlowDown_performed;
            input.Player.SlowDown.canceled += SlowDown_canceled;
            input.Player.Zoom.performed += Zoom_performed;
            input.Player.ToggleCanvas.performed += ToggleCanvas_performed;
        }

        private void ToggleCanvas_performed(InputAction.CallbackContext obj)
        {
            foreach(var canvas in worldCanvas)
            {
                canvas.enabled = !canvas.enabled;
            }
        }

        private void Zoom_performed(InputAction.CallbackContext obj)
        {
            var val = obj.ReadValue<float>();
            var fov = camera.fieldOfView;
            if(val > 0)
            {
                fov += fovIncrement;
                if(fov > maximumFOV)
                    fov = maximumFOV;
            }
            else if(val < 0)
            {
                fov -= fovIncrement;
                if (fov < minimumFOV)
                    fov = minimumFOV;
            }
            camera.fieldOfView = fov;
        }

        private void SlowDown_canceled(InputAction.CallbackContext obj)
        {
            speedMultiplier = 1;
        }

        private void SlowDown_performed(InputAction.CallbackContext obj)
        {
            speedMultiplier = speedDownMultipler;
        }

        private void Screenshot_performed(InputAction.CallbackContext obj)
        {
            screenshotTaker.TakeScreenshot();
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

            transform.rotation = Quaternion.Euler(0, yaw, 0);
            transform.Translate(new Vector3(moveVector.x, 0, moveVector.y) * speed * speedMultiplier * Time.unscaledDeltaTime);
            transform.Translate(new Vector3(0, verticalMovement, 0) * verticalSpeed * speedMultiplier * Time.unscaledDeltaTime);
            transform.rotation = Quaternion.Euler(pitch, yaw, 0);
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
            canvases.Clear();
            worldCanvas.Clear();
            camera.fieldOfView = 40;

            var c = GameObject.FindObjectsOfType<Canvas>();

            foreach(var canvas in c)
            {
                if(canvas.renderMode != RenderMode.WorldSpace && canvas.enabled)
                {
                    canvas.enabled = false;
                    canvases.Add(canvas);
                }
                if(canvas.renderMode == RenderMode.WorldSpace && canvas.enabled)
                {
                    worldCanvas.Add(canvas);
                }
            }
        }

        private void OnDisable()
        {
            GameManager.Instance.playerManager.PlayerMovement.enabled = playerMovementEnabled;
            brain.enabled = true;
            input.Disable();

            foreach (var canvas in canvases)
            {
                canvas.enabled = true;
            }

            foreach(var canvas in worldCanvas)
            {
                canvas.enabled = true;
            }
        }
    }
}
