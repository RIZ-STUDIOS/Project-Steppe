using ProjectSteppe.ScriptableObjects.CameraData.AimCameraData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectSteppe.Editor.CameraDataSubEditors
{
    public class AimHardLookAtCameraDataSubEditor : AimCameraDataSubEditor
    {
        public AimHardLookAtCameraDataSubEditor(VisualElement rootVisualElement) : base(rootVisualElement, CameraDataEditorWindow.AimDataType.HardLook, typeof(AimHardLookAtCameraDataScriptableObject))
        {
        }

        public override BaseAimCameraDataScriptableObject CreateScriptableObject()
        {
            return ScriptableObject.CreateInstance<AimHardLookAtCameraDataScriptableObject>();
        }

        protected override void CreateGUI()
        {

        }

        protected override void LoadData(bool isNull, BaseAimCameraDataScriptableObject asset)
        {

        }
    }
}
