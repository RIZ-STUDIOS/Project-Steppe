using Cinemachine;
using ProjectSteppe.Inputs;
using ProjectSteppe.Interactions.Interactables;
using ProjectSteppe.Managers;
using ProjectSteppe.UI;
using ProjectSteppe.UI.Menus;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Windows;

namespace ProjectSteppe
{
    public class FreeCamera : MonoBehaviour
    {
        private CinemachineBrain brain;
        public FreeCameraInput input;
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

        [Header("Canvas Related")]

        [SerializeField]
        private Canvas freeCamCanvas;

        [SerializeField]
        private RectTransform controlsRectTransform;

        [SerializeField]
        private FreeCamOptionsMenu options;

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
        private List<ParticleSystem> particleSystems = new List<ParticleSystem>();

        private ScreenshotTaker screenshotTaker;

        [SerializeField]
        private VolumeProfile volumeProfile;
        private DepthOfField depthOfField;

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
            //input.Player.Zoom.performed += Zoom_performed;
            input.Player.ToggleCanvas.performed += ToggleCanvas_performed;
            volumeProfile.TryGet(out depthOfField);
        }

        private void ToggleCanvas_performed(InputAction.CallbackContext obj)
        {
            foreach (var canvas in worldCanvas)
            {
                canvas.enabled = !canvas.enabled;
            }

            foreach (var ps in particleSystems)
            {
                ps.gameObject.SetActive(!ps.gameObject.activeSelf);
            }
        }

        private void Zoom_performed(InputAction.CallbackContext obj)
        {
            var val = obj.ReadValue<float>();
            var fov = camera.fieldOfView;
            if (val > 0)
            {
                fov += fovIncrement;
                if (fov > maximumFOV)
                    fov = maximumFOV;
            }
            else if (val < 0)
            {
                fov -= fovIncrement;
                if (fov < minimumFOV)
                    fov = minimumFOV;
            }
            camera.fieldOfView = fov;
        }

        public void SetSlowMode(bool value)
        {
            speedMultiplier = value ? speedDownMultipler : 1;
        }

        public void SetDoF(bool value)
        {
            depthOfField.active = value;
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
            particleSystems.Clear();
            camera.fieldOfView = 40;

            var c = GameObject.FindObjectsOfType<Canvas>();

            foreach (var canvas in c)
            {
                if (canvas.renderMode != RenderMode.WorldSpace && canvas.enabled)
                {
                    canvas.enabled = false;
                    canvases.Add(canvas);
                }
                if (canvas.renderMode == RenderMode.WorldSpace && canvas.enabled)
                {
                    worldCanvas.Add(canvas);
                }
            }

            var p = GameObject.FindObjectsOfType<InventoryInteractable>();

            foreach (var ps in p)
            {
                var d = ps.GetComponentInChildren<ParticleSystem>(true);
                if (d.gameObject.activeSelf)
                {
                    particleSystems.Add(d);
                }
            }

            freeCamCanvas.enabled = true;
            var pos = controlsRectTransform.anchoredPosition;
            pos.x = -controlsRectTransform.sizeDelta.x / 2f;
            controlsRectTransform.anchoredPosition = pos;

            options.SelectFirstElement();
        }

        private void OnDisable()
        {
            GameManager.Instance.playerManager.PlayerMovement.enabled = playerMovementEnabled;
            brain.enabled = true;
            input.Disable();
            options.DisableOptions();

            foreach (var canvas in canvases)
            {
                canvas.enabled = true;
            }

            foreach (var canvas in worldCanvas)
            {
                canvas.enabled = true;
            }

            foreach (var ps in particleSystems)
            {
                ps.gameObject.SetActive(true);
            }

            freeCamCanvas.enabled = false;
            SetDoF(true);
        }
    }
}
