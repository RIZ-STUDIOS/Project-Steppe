using ProjectSteppe.ScriptableObjects.CameraData.AimCameraData;
using RicTools.Editor.Utilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectSteppe.Editor.CameraDataSubEditors
{
    [System.Serializable]
    public class AimSameAsTargetCameraDataSubEditor : AimCameraDataSubEditor
    {
        public EditorContainer<float> damping = new EditorContainer<float>();

        public AimSameAsTargetCameraDataSubEditor(VisualElement rootVisualElement) : base(rootVisualElement, CameraDataEditorWindow.AimDataType.FollowTarget, typeof(AimFollowTargetCameraDataScriptableObject))
        {
        }

        public override BaseAimCameraDataScriptableObject CreateScriptableObject()
        {
            var so = ScriptableObject.CreateInstance<AimFollowTargetCameraDataScriptableObject>();
            so.damping = damping;
            return so;
        }

        protected override void LoadData(bool isNull, BaseAimCameraDataScriptableObject asset)
        {
            AimFollowTargetCameraDataScriptableObject aim = asset as AimFollowTargetCameraDataScriptableObject;
            if (isNull)
            {
                damping.Reset();
            }
            else
            {
                damping.Value = aim.damping;
            }
        }

        protected override void CreateGUI()
        {
            var element = rootVisualElement.AddFloatField(damping, "Damping");

            RegisterLoadChange(element, damping);
        }
    }
}
