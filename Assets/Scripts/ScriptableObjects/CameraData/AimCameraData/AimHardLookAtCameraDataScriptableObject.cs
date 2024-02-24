using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
