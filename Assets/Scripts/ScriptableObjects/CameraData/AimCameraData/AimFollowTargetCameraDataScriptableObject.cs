using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.ScriptableObjects.CameraData.AimCameraData
{
    public class AimFollowTargetCameraDataScriptableObject : BaseAimCameraDataScriptableObject
    {
        public float damping;

        public override void ApplyCameraData(CinemachineVirtualCamera camera)
        {
            DeleteComponentInCamera(camera);

            var comp = camera.AddCinemachineComponent<CinemachineSameAsFollowTarget>();
            comp.m_Damping = damping;
        }
    }
}
