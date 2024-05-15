using Cinemachine;
using ProjectSteppe.ScriptableObjects.CameraData.BodyCameraData;
using RicTools.Editor.Utilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectSteppe.Editor.CameraDataSubEditors
{
    public class BodyTrackedDollyCameraDataSubEditor : BodyCameraDataSubEditor
    {
        public EditorContainer<CinemachinePathBase> path = new EditorContainer<CinemachinePathBase>();

        public EditorContainer<float> pathPosition = new EditorContainer<float>();

        public EditorContainer<CinemachinePathBase.PositionUnits> positionUnits = new EditorContainer<CinemachinePathBase.PositionUnits>();

        public EditorContainer<Vector3> pathOffset = new EditorContainer<Vector3>();

        public EditorContainer<float> xDamping = new EditorContainer<float>();
        public EditorContainer<float> yDamping = new EditorContainer<float>();
        public EditorContainer<float> zDamping = new EditorContainer<float>(1);

        public EditorContainer<CinemachineTrackedDolly.CameraUpMode> cameraUp = new EditorContainer<CinemachineTrackedDolly.CameraUpMode>();

        public EditorContainer<float> pitchDamping = new EditorContainer<float>();
        public EditorContainer<float> yawDamping = new EditorContainer<float>();
        public EditorContainer<float> rollDamping = new EditorContainer<float>();

        public EditorContainer<bool> autoDollyEnabled = new EditorContainer<bool>();
        public EditorContainer<float> autoDollyPositionOffset = new EditorContainer<float>();
        public EditorContainer<int> autoDollySearchRadius = new EditorContainer<int>(2);
        public EditorContainer<int> autoDollySearchResolution = new EditorContainer<int>(5);

        public BodyTrackedDollyCameraDataSubEditor(VisualElement rootVisualElement) : base(rootVisualElement, CameraDataEditorWindow.BodyDataType.TrackedDolly, typeof(BodyTrackedDollyCameraDataScriptableObject))
        {
        }

        public override BaseBodyCameraDataScriptableObject CreateScriptableObject()
        {
            var asset = ScriptableObject.CreateInstance<BodyTrackedDollyCameraDataScriptableObject>();

            asset.path = path;
            asset.pathPosition = pathPosition;
            asset.positionUnits = positionUnits;
            asset.pathOffset = pathOffset;

            asset.xDamping = xDamping;
            asset.yDamping = yDamping;
            asset.zDamping = zDamping;

            asset.cameraUp = cameraUp;

            asset.pitchDamping = pitchDamping;
            asset.yawDamping = yawDamping;
            asset.rollDamping = rollDamping;

            asset.autoDollyEnabled = autoDollyEnabled;
            asset.autoDollyPositionOffset = autoDollyPositionOffset;
            asset.autoDollySearchRadius = autoDollySearchRadius;
            asset.autoDollySearchResolution = autoDollySearchResolution;

            return asset;
        }

        protected override void CreateGUI()
        {
            {
                var element = rootVisualElement.AddObjectField(path, "Path");

                RegisterLoadChange(element, path);
            }

            {
                var element = rootVisualElement.AddFloatField(pathPosition, "Path Position");

                RegisterLoadChange(element, pathPosition);
            }

            {
                var element = rootVisualElement.AddEnumField(positionUnits, "Position Units");

                RegisterLoadChange(element, positionUnits);
            }

            {
                var element = rootVisualElement.AddVector3Field(pathOffset, "Path Offset");

                RegisterLoadChange(element, pathOffset);
            }

            {
                var element = rootVisualElement.AddSlider(xDamping, 0, 20, "X Damping");

                RegisterLoadChange(element, xDamping);
            }

            {
                var element = rootVisualElement.AddSlider(yDamping, 0, 20, "Y Damping");

                RegisterLoadChange(element, yDamping);
            }

            {
                var element = rootVisualElement.AddSlider(zDamping, 0, 20, "Z Damping");

                RegisterLoadChange(element, zDamping);
            }

            {
                var pitchYawVisualElement = new VisualElement();
                var rollVisualElement = new VisualElement();

                pitchYawVisualElement.ToggleClass("hidden", cameraUp.Value == CinemachineTrackedDolly.CameraUpMode.Default);
                rollVisualElement.ToggleClass("hidden", cameraUp.Value != CinemachineTrackedDolly.CameraUpMode.Path && cameraUp.Value != CinemachineTrackedDolly.CameraUpMode.FollowTarget);

                {
                    var element = rootVisualElement.AddEnumField(cameraUp, "Camera Up");

                    element.RegisterValueChangedCallback(callback =>
                    {
                        pitchYawVisualElement.ToggleClass("hidden", cameraUp.Value == CinemachineTrackedDolly.CameraUpMode.Default);
                        rollVisualElement.ToggleClass("hidden", cameraUp.Value != CinemachineTrackedDolly.CameraUpMode.Path && cameraUp.Value != CinemachineTrackedDolly.CameraUpMode.FollowTarget);

                    });

                    RegisterLoadChange(element, cameraUp);
                }

                {
                    var element = pitchYawVisualElement.AddSlider(pitchDamping, 0, 20, "Pitch Damping");

                    RegisterLoadChange(element, pitchDamping);
                }

                {
                    var element = pitchYawVisualElement.AddSlider(yawDamping, 0, 20, "Yaw Damping");

                    RegisterLoadChange(element, yawDamping);
                }

                {
                    var element = rollVisualElement.AddSlider(rollDamping, 0, 20, "Roll Damping");

                    RegisterLoadChange(element, rollDamping);
                }

                rootVisualElement.Add(pitchYawVisualElement);
                rootVisualElement.Add(rollVisualElement);
            }

            {
                var autoDollyFoldout = rootVisualElement.AddFoldout("Auto Dolly");

                {
                    var element = autoDollyFoldout.AddToggle(autoDollyEnabled, "Enabled");

                    RegisterLoadChange(element, autoDollyEnabled);
                }

                {
                    var element = autoDollyFoldout.AddFloatField(autoDollyPositionOffset, "Position Offset");

                    RegisterLoadChange(element, autoDollyPositionOffset);
                }

                {
                    var element = autoDollyFoldout.AddIntField(autoDollySearchRadius, "Search Radius");

                    RegisterLoadChange(element, autoDollySearchRadius);
                }

                {
                    var element = autoDollyFoldout.AddIntField(autoDollySearchResolution, "Search Resolution");

                    RegisterLoadChange(element, autoDollySearchResolution);
                }
            }
        }

        protected override void LoadData(bool isNull, BaseBodyCameraDataScriptableObject asset)
        {
            var aim = asset as BodyTrackedDollyCameraDataScriptableObject;

            if (isNull)
            {
                path.Reset();
                pathPosition.Reset();
                positionUnits.Reset();
                pathOffset.Reset();
                xDamping.Reset();
                yDamping.Reset();
                zDamping.Reset();
                cameraUp.Reset();
                pitchDamping.Reset();
                yawDamping.Reset();
                rollDamping.Reset();
                autoDollyEnabled.Reset();
                autoDollyPositionOffset.Reset();
                autoDollySearchRadius.Reset();
                autoDollySearchResolution.Reset();
            }
            else
            {
                path.Value = aim.path;
                pathPosition.Value = aim.pathPosition;
                positionUnits.Value = aim.positionUnits;
                pathOffset.Value = aim.pathOffset;
                xDamping.Value = aim.xDamping;
                yDamping.Value = aim.yDamping;
                zDamping.Value = aim.zDamping;
                cameraUp.Value = aim.cameraUp;
                pitchDamping.Value = aim.pitchDamping;
                yawDamping.Value = aim.yawDamping;
                rollDamping.Value = aim.rollDamping;
                autoDollyEnabled.Value = aim.autoDollyEnabled;
                autoDollyPositionOffset.Value = aim.autoDollyPositionOffset;
                autoDollySearchRadius.Value = aim.autoDollySearchRadius;
                autoDollySearchResolution.Value = aim.autoDollySearchResolution;
            }
        }
    }
}
