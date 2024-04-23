using Cinemachine;

namespace ProjectSteppe.ScriptableObjects.CameraData.AimCameraData
{
    public class AimFollowTargetCameraDataScriptableObject : BaseAimCameraDataScriptableObject
    {
        public float damping;

        public override void ApplyCameraData(CinemachineVirtualCamera camera)
        {
            var comp = camera.AddCinemachineComponent<CinemachineSameAsFollowTarget>();
            comp.m_Damping = damping;
        }
    }
}
