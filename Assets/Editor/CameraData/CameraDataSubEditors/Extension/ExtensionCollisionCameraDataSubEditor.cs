using ProjectSteppe.ScriptableObjects.CameraData.ExtensionCameraData;
using System;
using UnityEngine.UIElements;

namespace ProjectSteppe.Editor.CameraDataSubEditors
{
    public class ExtensionCollisionCameraDataSubEditor : ExtensionCameraDataSubEditor
    {
        public ExtensionCollisionCameraDataSubEditor(VisualElement rootVisualElement, CameraDataEditorWindow.ExtensionDataType extensionDataType, Type componentType) : base(rootVisualElement, extensionDataType, componentType)
        {
        }

        public override BaseExtensionCameraDataScriptableObject CreateScriptableObject()
        {
            throw new System.NotImplementedException();
        }

        protected override void CreateGUI()
        {
            throw new System.NotImplementedException();
        }

        protected override void LoadData(bool isNull, BaseExtensionCameraDataScriptableObject asset)
        {
            throw new System.NotImplementedException();
        }
    }
}
