using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.ScriptableObjects.CameraData.BodyCameraData
{
    public class BodyHardLockCameraDataScriptableObject : BaseBodyCameraDataScriptableObject
    {
        public float damping;

        public override void ApplyCameraData(CinemachineVirtualCamera camera)
        {
            var comp = camera.AddCinemachineComponent<CinemachineHardLockToTarget>();

            comp.m_Damping = damping;
        }
    }
}
