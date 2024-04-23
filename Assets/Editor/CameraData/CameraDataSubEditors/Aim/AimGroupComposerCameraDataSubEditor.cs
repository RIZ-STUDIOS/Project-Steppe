using Cinemachine;
using ProjectSteppe.ScriptableObjects.CameraData.AimCameraData;
using RicTools.Editor.Utilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectSteppe.Editor.CameraDataSubEditors
{
    public class AimGroupComposerCameraDataSubEditor : AimCameraDataSubEditor
    {
        public EditorContainer<Vector3> trackedObjectOffset = new EditorContainer<Vector3>();
        public EditorContainer<float> lookaheadTime = new EditorContainer<float>();
        public EditorContainer<float> lookaheadSmoothing = new EditorContainer<float>();
        public EditorContainer<bool> lookaheadIgnoreY = new EditorContainer<bool>();

        public EditorContainer<float> horizontalDamping = new EditorContainer<float>(0.5f);
        public EditorContainer<float> verticalDamping = new EditorContainer<float>(0.5f);

        public EditorContainer<float> screenX = new EditorContainer<float>(0.5f);
        public EditorContainer<float> screenY = new EditorContainer<float>(0.5f);

        public EditorContainer<float> deadZoneWidth = new EditorContainer<float>();
        public EditorContainer<float> deadZoneHeight = new EditorContainer<float>();
        public EditorContainer<float> softZoneWidth = new EditorContainer<float>(0.8f);
        public EditorContainer<float> softZoneHeight = new EditorContainer<float>(0.8f);
        public EditorContainer<float> biasX = new EditorContainer<float>();
        public EditorContainer<float> biasY = new EditorContainer<float>();

        public EditorContainer<bool> centerOnActivate = new EditorContainer<bool>(true);

        public EditorContainer<float> groupFramingSize = new EditorContainer<float>(0.8f);

        public EditorContainer<CinemachineGroupComposer.FramingMode> framingMode = new EditorContainer<CinemachineGroupComposer.FramingMode>(CinemachineGroupComposer.FramingMode.HorizontalAndVertical);

        public EditorContainer<float> frameDamping = new EditorContainer<float>(2);

        public EditorContainer<CinemachineGroupComposer.AdjustmentMode> adjustmentMode = new EditorContainer<CinemachineGroupComposer.AdjustmentMode>(CinemachineGroupComposer.AdjustmentMode.ZoomOnly);

        public EditorContainer<float> maxDollyIn = new EditorContainer<float>(5000);
        public EditorContainer<float> maxDollyOut = new EditorContainer<float>(5000);
        public EditorContainer<float> minimumDistance = new EditorContainer<float>(1);
        public EditorContainer<float> maximumDistance = new EditorContainer<float>(5000);

        public EditorContainer<float> minimumFov = new EditorContainer<float>(3);
        public EditorContainer<float> maximumFov = new EditorContainer<float>(60);

        public AimGroupComposerCameraDataSubEditor(VisualElement rootVisualElement) : base(rootVisualElement, CameraDataEditorWindow.AimDataType.GroupComposer, typeof(AimGroupComposerCameraDataScriptableObject))
        {
        }

        public override BaseAimCameraDataScriptableObject CreateScriptableObject()
        {
            var asset = ScriptableObject.CreateInstance<AimGroupComposerCameraDataScriptableObject>();

            asset.trackedObjectOffset = trackedObjectOffset;
            asset.lookaheadTime = lookaheadTime;
            asset.lookaheadSmoothing = lookaheadSmoothing;
            asset.lookaheadIgnoreY = lookaheadIgnoreY;

            asset.horizontalDamping = horizontalDamping;
            asset.verticalDamping = verticalDamping;

            asset.screenX = screenX;
            asset.screenY = screenY;

            asset.deadZoneWidth = deadZoneWidth;
            asset.deadZoneHeight = deadZoneHeight;
            asset.softZoneWidth = softZoneWidth;
            asset.softZoneHeight = softZoneHeight;
            asset.biasX = biasX;
            asset.biasY = biasY;

            asset.centerOnActivate = centerOnActivate;

            asset.groupFramingSize = groupFramingSize;

            asset.framingMode = framingMode;

            asset.frameDamping = frameDamping;

            asset.adjustmentMode = adjustmentMode;

            asset.maxDollyIn = maxDollyIn;
            asset.maxDollyOut = maxDollyOut;
            asset.minimumDistance = minimumDistance;
            asset.maximumDistance = maximumDistance;

            asset.minimumFov = minimumFov;
            asset.maximumFov = maximumFov;

            return asset;
        }

        protected override void CreateGUI()
        {
            {
                var element = rootVisualElement.AddVector3Field(trackedObjectOffset, "Tracked Object Offset");

                RegisterLoadChange(element, trackedObjectOffset);
            }

            rootVisualElement.AddSeparator().style.backgroundImage = null;

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
                var element = rootVisualElement.AddSlider(horizontalDamping, 0, 20, "Horizontal Damping");
                RegisterLoadChange(element, horizontalDamping);
            }

            {
                var element = rootVisualElement.AddSlider(verticalDamping, 0, 20, "Vertical Damping");
                RegisterLoadChange(element, verticalDamping);
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
                var element = rootVisualElement.AddSlider(deadZoneWidth, 0, 2, "Dead Zone Width");
                RegisterLoadChange(element, deadZoneWidth);
            }

            {
                var element = rootVisualElement.AddSlider(deadZoneHeight, 0, 2, "Dead Zone Height");
                RegisterLoadChange(element, deadZoneHeight);
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

            rootVisualElement.AddSeparator().style.backgroundImage = null;

            {
                var element = rootVisualElement.AddFloatField(groupFramingSize, "Group Framing Size");

                element.RegisterValueChangedCallback(callback =>
                {
                    if (groupFramingSize.Value < 0.001f)
                        groupFramingSize.Value = 0.001f;

                    element.value = groupFramingSize;
                });

                RegisterLoadChange(element, groupFramingSize);
            }

            {
                var element = rootVisualElement.AddEnumField(framingMode, "Framing Mode");

                RegisterLoadChange(element, framingMode);
            }

            {
                var element = rootVisualElement.AddSlider(frameDamping, 0, 20, "Frame Damping");

                RegisterLoadChange(element, frameDamping);
            }

            VisualElement dollyVisualElement = null;
            VisualElement zoomViualElement = null;

            {
                var element = rootVisualElement.AddEnumField(adjustmentMode, "Adjustment Mode", () =>
                {
                    dollyVisualElement.ToggleClass("hidden", adjustmentMode.Value == CinemachineGroupComposer.AdjustmentMode.ZoomOnly);
                    zoomViualElement.ToggleClass("hidden", adjustmentMode.Value == CinemachineGroupComposer.AdjustmentMode.DollyOnly);
                });

                RegisterLoadChange(element, adjustmentMode);
            }

            {
                dollyVisualElement = new VisualElement();

                {
                    var element = dollyVisualElement.AddFloatField(maxDollyIn, "Max Dolly In");

                    RegisterLoadChange(element, maxDollyIn);
                }

                {
                    var element = dollyVisualElement.AddFloatField(maxDollyOut, "Max Dolly Out");

                    RegisterLoadChange(element, maxDollyOut);
                }

                {
                    var element = dollyVisualElement.AddFloatField(minimumDistance, "Minimum Distance");

                    RegisterLoadChange(element, minimumDistance);
                }

                {
                    var element = dollyVisualElement.AddFloatField(maximumDistance, "Maximum Distance");

                    RegisterLoadChange(element, maximumDistance);
                }

                rootVisualElement.Add(dollyVisualElement);
            }

            {
                zoomViualElement = new VisualElement();

                {
                    var element = zoomViualElement.AddSlider(minimumFov, 1, 179, "Minimum FOV");

                    RegisterLoadChange(element, minimumFov);
                }

                {
                    var element = zoomViualElement.AddSlider(maximumFov, 1, 179, "Maximum FOV");

                    RegisterLoadChange(element, maximumFov);
                }

                rootVisualElement.Add(zoomViualElement);
            }

            dollyVisualElement.ToggleClass("hidden", adjustmentMode.Value == CinemachineGroupComposer.AdjustmentMode.ZoomOnly);
            zoomViualElement.ToggleClass("hidden", adjustmentMode.Value == CinemachineGroupComposer.AdjustmentMode.DollyOnly);
        }

        protected override void LoadData(bool isNull, BaseAimCameraDataScriptableObject asset)
        {
            var aim = asset as AimGroupComposerCameraDataScriptableObject;
            if (isNull)
            {
                trackedObjectOffset.Reset();
                lookaheadTime.Reset();
                lookaheadSmoothing.Reset();
                lookaheadIgnoreY.Reset();

                horizontalDamping.Reset();
                verticalDamping.Reset();

                screenX.Reset();
                screenY.Reset();

                deadZoneWidth.Reset();
                deadZoneHeight.Reset();
                softZoneWidth.Reset();
                softZoneHeight.Reset();
                biasX.Reset();
                biasY.Reset();

                centerOnActivate.Reset();

                groupFramingSize.Reset();

                framingMode.Reset();
                frameDamping.Reset();

                adjustmentMode.Reset();

                maxDollyIn.Reset();
                maxDollyOut.Reset();
                minimumDistance.Reset();
                maximumDistance.Reset();

                minimumFov.Reset();
                maximumFov.Reset();
            }
            else
            {
                trackedObjectOffset.Value = aim.trackedObjectOffset;
                lookaheadTime.Value = aim.lookaheadTime;
                lookaheadSmoothing.Value = aim.lookaheadSmoothing;
                lookaheadIgnoreY.Value = aim.lookaheadIgnoreY;
                horizontalDamping.Value = aim.horizontalDamping;
                verticalDamping.Value = aim.verticalDamping;
                screenX.Value = aim.screenX;
                screenY.Value = aim.screenY;
                deadZoneWidth.Value = aim.deadZoneWidth;
                deadZoneHeight.Value = aim.deadZoneHeight;
                softZoneWidth.Value = aim.softZoneWidth;
                softZoneHeight.Value = aim.softZoneHeight;
                biasX.Value = aim.biasX;
                biasY.Value = aim.biasY;
                centerOnActivate.Value = aim.centerOnActivate;

                groupFramingSize.Value = aim.groupFramingSize;

                framingMode.Value = aim.framingMode;
                frameDamping.Value = aim.frameDamping;

                adjustmentMode.Value = aim.adjustmentMode;

                maxDollyIn.Value = aim.maxDollyIn;
                maxDollyOut.Value = aim.maxDollyOut;
                minimumDistance.Value = aim.minimumDistance;
                maximumDistance.Value = aim.maximumDistance;

                minimumFov.Value = aim.minimumFov;
                maximumFov.Value = aim.maximumFov;
            }
        }
    }
}
