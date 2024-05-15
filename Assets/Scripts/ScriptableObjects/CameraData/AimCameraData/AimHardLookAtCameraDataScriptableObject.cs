using Cinemachine;

namespace ProjectSteppe.ScriptableObjects.CameraData.AimCameraData
{
    public class AimHardLookAtCameraDataScriptableObject : BaseAimCameraDataScriptableObject
    {
        public override void ApplyCameraData(CinemachineVirtualCamera camera)
        {
            camera.AddCinemachineComponent<CinemachineHardLookAt>();
        }
    }
}
