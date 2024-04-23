using Cinemachine;
using ProjectSteppe.ScriptableObjects.CameraData.AimCameraData;
using RicTools.Editor.Utilities;
using UnityEngine;
using UnityEngine.UIElements;
using static Cinemachine.AxisState;

namespace ProjectSteppe.Editor.CameraDataSubEditors
{
    public class AimPOVCameraDataSubEditor : AimCameraDataSubEditor
    {
        public EditorContainer<CinemachinePOV.RecenterTargetMode> recenterTarget = new EditorContainer<CinemachinePOV.RecenterTargetMode>(CinemachinePOV.RecenterTargetMode.None);
        public EditorAxisState verticalAxisState = new EditorAxisState(0, SpeedMode.MaxSpeed, 300, 0.1f, 0.1f, "Mouse Y", 0, true, -70, 70, false, new EditorAxisState.Recentering(false, 1, 2));
        public EditorAxisState horizontalAxisState = new EditorAxisState(0, SpeedMode.MaxSpeed, 300, 0.1f, 0.1f, "Mouse X", 0, false, -180, 180, true, new EditorAxisState.Recentering(false, 1, 2));

        public AimPOVCameraDataSubEditor(VisualElement rootVisualElement) : base(rootVisualElement, CameraDataEditorWindow.AimDataType.POV, typeof(AimPOVCameraDataScriptableObject))
        {
        }

        public override BaseAimCameraDataScriptableObject CreateScriptableObject()
        {
            var asset = ScriptableObject.CreateInstance<AimPOVCameraDataScriptableObject>();

            asset.recenterTarget = recenterTarget;

            SaveAxisState(verticalAxisState, ref asset.verticalAxis, ref asset.verticalRecentering);
            SaveAxisState(horizontalAxisState, ref asset.horizontalAxis, ref asset.horizontalRecentering);

            return asset;
        }

        protected override void CreateGUI()
        {
            {
                var element = rootVisualElement.AddEnumField(recenterTarget, "Recenter Target");

                RegisterLoadChange(element, recenterTarget);
            }

            AddAxisState(verticalAxisState, "Vertical");
            AddAxisState(horizontalAxisState, "Horizontal");
        }

        private void SaveAxisState(EditorAxisState editorAxisState, ref AxisState axisState, ref AxisState.Recentering recentering)
        {
            axisState.Value = editorAxisState.Value;
            axisState.m_MinValue = editorAxisState.m_MinValue;
            axisState.m_MaxValue = editorAxisState.m_MaxValue;
            axisState.m_MaxSpeed = editorAxisState.m_MaxSpeed;
            axisState.m_SpeedMode = editorAxisState.m_SpeedMode;
            axisState.m_AccelTime = editorAxisState.m_AccelTime;
            axisState.m_DecelTime = editorAxisState.m_DecelTime;
            axisState.m_InputAxisName = editorAxisState.m_InputAxisName;
            axisState.m_InputAxisValue = editorAxisState.m_InputAxisValue;
            axisState.m_InvertInput = editorAxisState.m_InvertInput;
            axisState.m_Wrap = editorAxisState.m_Wrap;

            recentering.m_enabled = editorAxisState.recentering.m_enabled;
            recentering.m_RecenteringTime = editorAxisState.recentering.m_RecenteringTime;
            recentering.m_WaitTime = editorAxisState.recentering.m_WaitTime;
        }

        private void AddAxisState(EditorAxisState editorAxisState, string prefix)
        {
            VisualElement editorAxisVisualElement = new VisualElement();

            {
                var axisFoldout = editorAxisVisualElement.AddFoldout(prefix + " Axis");
                axisFoldout.value = true;

                {
                    var element = axisFoldout.AddFloatField(editorAxisState.Value, "Value");

                    RegisterLoadChange(element, editorAxisState.Value);
                }

                {
                    var visualElement = new VisualElement();
                    {
                        var element = visualElement.AddFloatField(editorAxisState.m_MinValue, "Value Range");
                        RegisterLoadChange(element, editorAxisState.m_MinValue);


                        element.style.flexGrow = 1;
                    }

                    {
                        visualElement.AddLabel("to").style.unityTextAlign = TextAnchor.MiddleCenter;
                    }

                    {
                        var element = visualElement.AddFloatField(editorAxisState.m_MaxValue, "");

                        element.style.flexGrow = 1;

                        RegisterLoadChange(element, editorAxisState.m_MaxValue);
                    }

                    {
                        var element = visualElement.AddToggle(editorAxisState.m_Wrap, "");

                        //element.style.width = new StyleLength(new Length(0.2f, LengthUnit.Percent));

                        RegisterLoadChange(element, editorAxisState.m_Wrap);
                    }

                    visualElement.AddLabel("Wrap").style.unityTextAlign = TextAnchor.MiddleCenter;

                    visualElement.style.display = DisplayStyle.Flex;
                    visualElement.style.flexDirection = FlexDirection.Row;

                    axisFoldout.Add(visualElement);
                }

                {
                    var visualElement = new VisualElement();

                    {
                        var element = visualElement.AddFloatField(editorAxisState.m_MaxSpeed, "Speed");

                        element.style.flexGrow = 1;

                        RegisterLoadChange(element, editorAxisState.m_MaxSpeed);
                    }

                    visualElement.AddLabel("as").style.unityTextAlign = TextAnchor.MiddleCenter;

                    {
                        var element = visualElement.AddEnumField(editorAxisState.m_SpeedMode, "");

                        element.style.flexGrow = 1;

                        RegisterLoadChange(element, editorAxisState.m_SpeedMode);
                    }

                    visualElement.style.display = DisplayStyle.Flex;
                    visualElement.style.flexDirection = FlexDirection.Row;

                    axisFoldout.Add(visualElement);
                }

                {
                    var visualElement = new VisualElement();

                    {
                        var element = visualElement.AddFloatField(editorAxisState.m_AccelTime, "Accel Time");

                        element.style.flexGrow = 1;

                        RegisterLoadChange(element, editorAxisState.m_AccelTime);
                    }

                    {
                        var element = visualElement.AddFloatField(editorAxisState.m_DecelTime, "Decel Time");

                        element.style.flexGrow = 1;

                        RegisterLoadChange(element, editorAxisState.m_DecelTime);
                    }

                    visualElement.style.display = DisplayStyle.Flex;
                    visualElement.style.flexDirection = FlexDirection.Row;

                    axisFoldout.Add(visualElement);
                }

                {
                    var element = axisFoldout.AddTextField(editorAxisState.m_InputAxisName, "Input Axis Name");

                    RegisterLoadChange(element, editorAxisState.m_InputAxisName);
                }

                {
                    var visualElement = new VisualElement();

                    {
                        var element = visualElement.AddFloatField(editorAxisState.m_InputAxisValue, "Input Axis Value");

                        element.style.flexGrow = 1;

                        RegisterLoadChange(element, editorAxisState.m_InputAxisValue);
                    }

                    {
                        var element = visualElement.AddToggle(editorAxisState.m_InvertInput, "");

                        RegisterLoadChange(element, editorAxisState.m_InvertInput);
                    }

                    visualElement.AddLabel("Invert").style.unityTextAlign = TextAnchor.MiddleCenter;

                    visualElement.style.display = DisplayStyle.Flex;
                    visualElement.style.flexDirection = FlexDirection.Row;

                    axisFoldout.Add(visualElement);
                }
            }

            {
                var recenteringFoldout = editorAxisVisualElement.AddFoldout(prefix + " Recentering");
                recenteringFoldout.value = false;

                {
                    var element = recenteringFoldout.AddToggle(editorAxisState.recentering.m_enabled, "Enabled");

                    RegisterLoadChange(element, editorAxisState.recentering.m_enabled);
                }

                {
                    var element = recenteringFoldout.AddFloatField(editorAxisState.recentering.m_WaitTime, "Wait Time");

                    element.RegisterValueChangedCallback(callback =>
                    {
                        if (editorAxisState.recentering.m_WaitTime < 0)
                            editorAxisState.recentering.m_WaitTime.Value = 0;

                        element.value = editorAxisState.recentering.m_WaitTime;
                    });

                    RegisterLoadChange(element, editorAxisState.recentering.m_WaitTime);
                }

                {
                    var element = recenteringFoldout.AddFloatField(editorAxisState.recentering.m_RecenteringTime, "Recentering Time");

                    element.RegisterValueChangedCallback(callback =>
                    {
                        if (editorAxisState.recentering.m_RecenteringTime < 0)
                            editorAxisState.recentering.m_RecenteringTime.Value = 0;

                        element.value = editorAxisState.recentering.m_RecenteringTime;
                    });

                    RegisterLoadChange(element, editorAxisState.recentering.m_RecenteringTime);
                }
            }

            rootVisualElement.Add(editorAxisVisualElement);
        }

        protected override void LoadData(bool isNull, BaseAimCameraDataScriptableObject asset)
        {
            var aim = asset as AimPOVCameraDataScriptableObject;
            if (isNull)
            {
                recenterTarget.Reset();
            }
            else
            {
                recenterTarget.Value = aim.recenterTarget;
            }

            if (verticalAxisState != null)
                verticalAxisState.LoadData(isNull, isNull ? new() : aim.verticalAxis, isNull ? new() : aim.verticalRecentering);
            if (horizontalAxisState != null)
                horizontalAxisState.LoadData(isNull, isNull ? new() : aim.horizontalAxis, isNull ? new() : aim.horizontalRecentering);
        }

        [System.Serializable]
        public class EditorAxisState
        {
            public EditorContainer<float> Value = new EditorContainer<float>(0);
            public EditorContainer<SpeedMode> m_SpeedMode = new EditorContainer<SpeedMode>(SpeedMode.MaxSpeed);
            public EditorContainer<float> m_MaxSpeed = new EditorContainer<float>(0);
            public EditorContainer<float> m_AccelTime = new EditorContainer<float>(0);
            public EditorContainer<float> m_DecelTime = new EditorContainer<float>(0);
            public EditorContainer<string> m_InputAxisName = new EditorContainer<string>();
            public EditorContainer<float> m_InputAxisValue;
            public EditorContainer<bool> m_InvertInput;
            public EditorContainer<float> m_MinValue;
            public EditorContainer<float> m_MaxValue;
            public EditorContainer<bool> m_Wrap;
            public Recentering recentering;

            public EditorAxisState(float value, SpeedMode speedMode, float maxSpeed, float accelTime, float decelTime, string inputAxisName, float inputAxisValue, bool invertInput, float minValue, float maxValue, bool wrap, Recentering recentering)
            {
                Value = new EditorContainer<float>(value);
                m_SpeedMode = new EditorContainer<SpeedMode>(speedMode);
                m_MaxSpeed = new EditorContainer<float>(maxSpeed);
                m_AccelTime = new EditorContainer<float>(accelTime);
                m_DecelTime = new EditorContainer<float>(decelTime);
                m_InputAxisName = new EditorContainer<string>(inputAxisName);
                m_InputAxisValue = new EditorContainer<float>(inputAxisValue);
                m_InvertInput = new EditorContainer<bool>(invertInput);
                m_MinValue = new EditorContainer<float>(minValue);
                m_MaxValue = new EditorContainer<float>(maxValue);
                m_Wrap = new EditorContainer<bool>(wrap);
                this.recentering = recentering;
            }

            public void LoadData(bool isNull, AxisState axisState, AxisState.Recentering recentering)
            {
                if (isNull)
                {
                    Value.Reset();
                    m_SpeedMode.Reset();
                    m_MaxSpeed.Reset();
                    m_AccelTime.Reset();
                    m_DecelTime.Reset();
                    m_InputAxisName.Reset();
                    m_InputAxisValue.Reset();
                    m_InvertInput.Reset();
                    m_MinValue.Reset();
                    m_MaxValue.Reset();
                    m_Wrap.Reset();
                }
                else
                {
                    Value.Value = axisState.Value;
                    m_SpeedMode.Value = axisState.m_SpeedMode;
                    m_MaxSpeed.Value = axisState.m_MaxSpeed;
                    m_AccelTime.Value = axisState.m_AccelTime;
                    m_DecelTime.Value = axisState.m_DecelTime;
                    m_InputAxisName.Value = axisState.m_InputAxisName;
                    m_InputAxisValue.Value = axisState.m_InputAxisValue;
                    m_InvertInput.Value = axisState.m_InvertInput;
                    m_MinValue.Value = axisState.m_MinValue;
                    m_MaxValue.Value = axisState.m_MaxValue;
                    m_Wrap.Value = axisState.m_Wrap;
                }

                this.recentering.LoadData(isNull, recentering);
            }

            [System.Serializable]
            public class Recentering
            {
                public EditorContainer<bool> m_enabled = new EditorContainer<bool>(false);
                public EditorContainer<float> m_WaitTime = new EditorContainer<float>(1);
                public EditorContainer<float> m_RecenteringTime = new EditorContainer<float>(2);

                public Recentering(bool enabled, float waitTime, float recenteringTime)
                {
                    m_enabled = new EditorContainer<bool>(enabled);
                    m_WaitTime = new EditorContainer<float>(waitTime);
                    m_RecenteringTime = new EditorContainer<float>(recenteringTime);
                }

                public void LoadData(bool isNull, AxisState.Recentering recentering)
                {
                    if (isNull)
                    {
                        m_enabled.Reset();
                        m_WaitTime.Reset();
                        m_RecenteringTime.Reset();
                    }
                    else
                    {
                        m_enabled.Value = recentering.m_enabled;
                        m_WaitTime.Value = recentering.m_WaitTime;
                        m_RecenteringTime.Value = recentering.m_RecenteringTime;
                    }
                }
            }
        }
    }
}
