using Cinemachine;
using ProjectSteppe.ScriptableObjects.CameraData.AimCameraData;
using RicTools.Editor.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using static Cinemachine.AxisState;

namespace ProjectSteppe.Editor.CameraDataSubEditors
{
    public class AimPOVCameraDataSubEditor : AimCameraDataSubEditor
    {
        public CinemachinePOV.RecenterTargetMode recenterTarget;

        public AimPOVCameraDataSubEditor(VisualElement rootVisualElement) : base(rootVisualElement, CameraDataEditorWindow.AimDataType.POV, typeof(AimPOVCameraDataScriptableObject))
        {
        }

        public override BaseAimCameraDataScriptableObject CreateScriptableObject()
        {
            var asset = ScriptableObject.CreateInstance<AimPOVCameraDataScriptableObject>();

            return asset;
        }

        protected override void CreateGUI()
        {

        }

        private void AddAxisState(EditorAxisState editorAxisState)
        {

        }

        protected override void LoadData(bool isNull, BaseAimCameraDataScriptableObject asset)
        {
            if (isNull)
            {

            }
            else
            {

            }
        }

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
            public Recentering recentering = new Recentering();

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

            public class Recentering
            {
                public EditorContainer<bool> m_enabled = new EditorContainer<bool>(false);
                public EditorContainer<float> m_WaitTime = new EditorContainer<float>(1);
                public EditorContainer<float> m_RecenteringTime = new EditorContainer<float>(2);

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
