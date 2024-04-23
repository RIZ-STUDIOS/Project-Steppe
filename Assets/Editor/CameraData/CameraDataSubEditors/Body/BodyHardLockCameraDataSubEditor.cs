using ProjectSteppe.ScriptableObjects.CameraData.BodyCameraData;
using RicTools.Editor.Utilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectSteppe.Editor.CameraDataSubEditors
{
    public class BodyHardLockCameraDataSubEditor : BodyCameraDataSubEditor
    {
        public EditorContainer<float> damping = new EditorContainer<float>();

        public BodyHardLockCameraDataSubEditor(VisualElement rootVisualElement) : base(rootVisualElement, CameraDataEditorWindow.BodyDataType.HardLock, typeof(BodyHardLockCameraDataScriptableObject))
        {
        }

        public override BaseBodyCameraDataScriptableObject CreateScriptableObject()
        {
            var asset = ScriptableObject.CreateInstance<BodyHardLockCameraDataScriptableObject>();

            asset.damping = damping;

            return asset;
        }

        protected override void CreateGUI()
        {
            {
                var element = rootVisualElement.AddFloatField(damping, "Damping");

                RegisterLoadChange(element, damping);
            }
        }

        protected override void LoadData(bool isNull, BaseBodyCameraDataScriptableObject asset)
        {
            var aim = asset as BodyHardLockCameraDataScriptableObject;
            if (isNull)
            {
                damping.Reset();
            }
            else
            {
                damping.Value = aim.damping;
            }
        }
    }
}
