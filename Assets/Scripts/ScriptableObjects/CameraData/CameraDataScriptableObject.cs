using Cinemachine;
using ProjectSteppe.ScriptableObjects.CameraData.AimCameraData;
using ProjectSteppe.ScriptableObjects.CameraData.BodyCameraData;
using ProjectSteppe.ScriptableObjects.CameraData.ExtensionCameraData;
using RicTools.EditorAttributes;
using RicTools.ScriptableObjects;

namespace ProjectSteppe.ScriptableObjects.CameraData
{
    [DefaultScriptableObjectName(nameof(presetName))]
    public class CameraDataScriptableObject : GenericScriptableObject
    {
        public string presetName;

        public CinemachineVirtualCameraBase.StandbyUpdateMode standbyUpdateMode;

        public float verticalFov;
        public float nearClipPlane;
        public float farClipPlane;
        public float dutch;

        public BaseBodyCameraDataScriptableObject bodyCameraData;
        public BaseAimCameraDataScriptableObject aimCameraData;

        public BaseExtensionCameraDataScriptableObject[] extensions;

        public void ApplyCameraData(CinemachineVirtualCamera camera)
        {
            camera.m_StandbyUpdate = standbyUpdateMode;

            camera.m_Lens.FieldOfView = verticalFov;
            camera.m_Lens.NearClipPlane = nearClipPlane;
            camera.m_Lens.FarClipPlane = farClipPlane;
            camera.m_Lens.Dutch = dutch;

            DeleteComponentInCamera(CinemachineCore.Stage.Body, camera);
            DeleteComponentInCamera(CinemachineCore.Stage.Aim, camera);

            if (bodyCameraData)
            {
                bodyCameraData.ApplyCameraData(camera);
            }

            if (aimCameraData)
            {
                aimCameraData.ApplyCameraData(camera);
            }

            /*foreach (var comp in camera.GetComponents<CinemachineExtension>())
            {
                Destroy(comp);
            }

            if (extensions != null)
            {
                foreach (var extension in extensions)
                {
                    extension.ApplyCameraData(camera);
                }
            }*/
        }

        private void DeleteComponentInCamera(CinemachineCore.Stage stage, CinemachineVirtualCamera camera)
        {
            var comp = camera.GetCinemachineComponent(stage);

            if (comp)
            {
                comp.enabled = false;
                RuntimeUtility.DestroyObject(comp);
            }

            camera.InvalidateComponentPipeline();
        }
    }
}