using ProjectSteppe.ScriptableObjects.CameraData.BodyCameraData;
using RicTools.Editor.Utilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectSteppe.Editor.CameraDataSubEditors
{
    public class BodyFramingTransposerCameraDataSubEditor : BodyCameraDataSubEditor
    {
        public EditorContainer<Vector3> trackedObjectOffset = new EditorContainer<Vector3>();
        public EditorContainer<float> lookaheadTime = new EditorContainer<float>();
        public EditorContainer<float> lookaheadSmoothing = new EditorContainer<float>();
        public EditorContainer<bool> lookaheadIgnoreY = new EditorContainer<bool>();

        public EditorContainer<float> xDamping = new EditorContainer<float>(1);
        public EditorContainer<float> yDamping = new EditorContainer<float>(1);
        public EditorContainer<float> zDamping = new EditorContainer<float>(1);
        public EditorContainer<bool> targetMovementOnly = new EditorContainer<bool>(true);

        public EditorContainer<float> screenX = new EditorContainer<float>(0.5f);
        public EditorContainer<float> screenY = new EditorContainer<float>(0.5f);
        public EditorContainer<float> cameraDistance = new EditorContainer<float>(10);

        public EditorContainer<float> deadZoneWidth = new EditorContainer<float>();
        public EditorContainer<float> deadZoneHeight = new EditorContainer<float>();
        public EditorContainer<float> deadZoneDepth = new EditorContainer<float>();

        public EditorContainer<bool> unlimitedSoftZone = new EditorContainer<bool>();
        public EditorContainer<float> softZoneWidth = new EditorContainer<float>(0.8f);
        public EditorContainer<float> softZoneHeight = new EditorContainer<float>(0.8f);
        public EditorContainer<float> biasX = new EditorContainer<float>();
        public EditorContainer<float> biasY = new EditorContainer<float>();
        public EditorContainer<bool> centerOnActivate = new EditorContainer<bool>(true);

        public BodyFramingTransposerCameraDataSubEditor(VisualElement rootVisualElement) : base(rootVisualElement, CameraDataEditorWindow.BodyDataType.FramingTransposer, typeof(BodyFramingTransposerCameraDataScriptableObject))
        {
        }

        public override BaseBodyCameraDataScriptableObject CreateScriptableObject()
        {
            var asset = ScriptableObject.CreateInstance<BodyFramingTransposerCameraDataScriptableObject>();

            asset.trackedObjectOffset = trackedObjectOffset;
            asset.lookaheadTime = lookaheadTime;
            asset.lookaheadSmoothing = lookaheadSmoothing;
            asset.lookaheadIgnoreY = lookaheadIgnoreY;

            asset.xDamping = xDamping;
            asset.yDamping = yDamping;
            asset.zDamping = zDamping;
            asset.targetMovementOnly = targetMovementOnly;

            asset.screenX = screenX;
            asset.screenY = screenY;
            asset.cameraDistance = cameraDistance;

            asset.deadZoneWidth = deadZoneWidth;
            asset.deadZoneHeight = deadZoneHeight;
            asset.deadZoneDepth = deadZoneDepth;

            asset.unlimitedSoftZone = unlimitedSoftZone;
            asset.softZoneWidth = softZoneWidth;
            asset.softZoneHeight = softZoneHeight;
            asset.biasX = biasX;
            asset.biasY = biasY;
            asset.centerOnActivate = centerOnActivate;

            return asset;
        }

        protected override void CreateGUI()
        {
            {
                var element = rootVisualElement.AddVector3Field(trackedObjectOffset, "Tracked Object Offset");

                element.RegisterValueChangedCallback(callback =>
                {
                    var value = trackedObjectOffset.Value;

                    if (value.x < 0)
                        value.x = 0;

                    if (value.y < 0)
                        value.y = 0;

                    if (value.z < 0)
                        value.z = 0;

                    trackedObjectOffset.Value = value;
                    element.value = trackedObjectOffset;
                });

                RegisterLoadChange(element, trackedObjectOffset);
            }

            {
                var element = rootVisualElement.AddSlider(lookaheadTime, 0, 1, "Lookahead Time");

                RegisterLoadChange(element, lookaheadTime);
            }

            {
                var element = rootVisualElement.AddSlider(lookaheadSmoothing, 0, 30, "Lookahead Smoothing");

                RegisterLoadChange(element, lookaheadSmoothing);
            }

            {
                var element = rootVisualElement.AddToggle(lookaheadIgnoreY, "Lookahead Ignore Y");

                RegisterLoadChange(element, lookaheadIgnoreY);
            }

            rootVisualElement.AddSeparator().style.backgroundImage = null;

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
                var element = rootVisualElement.AddToggle(targetMovementOnly, "Target Movement Only");

                RegisterLoadChange(element, targetMovementOnly);
            }

            rootVisualElement.AddSeparator().style.backgroundImage = null;

            {
                var element = rootVisualElement.AddSlider(screenX, -0.5f, 1.5f, "Screen X");

                RegisterLoadChange(element, screenX);
            }

            {
                var element = rootVisualElement.AddSlider(screenY, -0.5f, 1.5f, "Screen Y");

                RegisterLoadChange(element, screenY);
            }

            {
                var element = rootVisualElement.AddFloatField(cameraDistance, "Camera Distance");

                element.RegisterValueChangedCallback(callback =>
                {
                    if (cameraDistance.Value < 0.01f)
                        cameraDistance.Value = 0.01f;

                    element.value = cameraDistance;
                });

                RegisterLoadChange(element, cameraDistance);
            }

            rootVisualElement.AddSeparator().style.backgroundImage = null;

            {
                var element = rootVisualElement.AddSlider(deadZoneWidth, 0, 2, "Dead Zone Width");

                RegisterLoadChange(element, deadZoneWidth);
            }

            {
                var element = rootVisualElement.AddSlider(deadZoneHeight, 0, 2, "Dead Zone Height");

                RegisterLoadChange(element, deadZoneHeight);
            }

            {
                var element = rootVisualElement.AddFloatField(deadZoneDepth, "Dead Zone Depth");

                element.RegisterValueChangedCallback(callback =>
                {
                    if (deadZoneDepth.Value < 0)
                        deadZoneDepth.Value = 0;

                    element.value = deadZoneDepth.Value;
                });

                RegisterLoadChange(element, deadZoneDepth);
            }

            rootVisualElement.AddSeparator().style.backgroundImage = null;

            {
                var element = rootVisualElement.AddToggle(unlimitedSoftZone, "Unlimited Soft Zone");

                RegisterLoadChange(element, unlimitedSoftZone);
            }

            {
                var element = rootVisualElement.AddSlider(softZoneWidth, 0, 2, "Soft Zone Width");

                RegisterLoadChange(element, softZoneWidth);
            }

            {
                var element = rootVisualElement.AddSlider(softZoneHeight, 0, 2, "Soft Zone Height");

                RegisterLoadChange(element, softZoneHeight);
            }

            {
                var element = rootVisualElement.AddSlider(biasX, -0.5f, 0.5f, "Bias X");

                RegisterLoadChange(element, biasX);
            }

            {
                var element = rootVisualElement.AddSlider(biasY, -0.5f, 0.5f, "Bias Y");

                RegisterLoadChange(element, biasY);
            }

            {
                var element = rootVisualElement.AddToggle(centerOnActivate, "Center On Activate");

                RegisterLoadChange(element, centerOnActivate);
            }
        }

        protected override void LoadData(bool isNull, BaseBodyCameraDataScriptableObject asset)
        {
            var body = asset as BodyFramingTransposerCameraDataScriptableObject;
            if (isNull)
            {
                trackedObjectOffset.Reset();
                lookaheadTime.Reset();
                lookaheadSmoothing.Reset();
                lookaheadIgnoreY.Reset();

                xDamping.Reset();
                yDamping.Reset();
                zDamping.Reset();
                targetMovementOnly.Reset();

                screenX.Reset();
                screenY.Reset();
                cameraDistance.Reset();

                deadZoneWidth.Reset();
                deadZoneHeight.Reset();
                deadZoneDepth.Reset();

                unlimitedSoftZone.Reset();
                softZoneWidth.Reset();
                softZoneHeight.Reset();
                biasX.Reset();
                biasY.Reset();
                centerOnActivate.Reset();
            }
            else
            {
                trackedObjectOffset.Value = body.trackedObjectOffset;
                lookaheadTime.Value = body.lookaheadTime;
                lookaheadSmoothing.Value = body.lookaheadSmoothing;
                lookaheadIgnoreY.Value = body.lookaheadIgnoreY;

                xDamping.Value = body.xDamping;
                yDamping.Value = body.yDamping;
                zDamping.Value = body.zDamping;
                targetMovementOnly.Value = body.targetMovementOnly;

                screenX.Value = body.screenX;
                screenY.Value = body.screenY;
                cameraDistance.Value = body.cameraDistance;

                deadZoneWidth.Value = body.deadZoneWidth;
                deadZoneHeight.Value = body.deadZoneHeight;
                deadZoneDepth.Value = body.deadZoneDepth;

                unlimitedSoftZone.Value = body.unlimitedSoftZone;
                softZoneWidth.Value = body.softZoneWidth;
                softZoneHeight.Value = body.softZoneHeight;
            }
        }
    }
}
