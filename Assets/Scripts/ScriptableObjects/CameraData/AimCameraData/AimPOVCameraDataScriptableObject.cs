using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.ScriptableObjects.CameraData.AimCameraData
{
    public class AimPOVCameraDataScriptableObject : BaseAimCameraDataScriptableObject
    {
        public CinemachinePOV.RecenterTargetMode recenterTarget;

        public AxisState verticalAxis;

        public AxisState.Recentering verticalRecentering;

        public AxisState horizontalAxis;

        public AxisState.Recentering horizontalRecentering;

        public override void ApplyCameraData(CinemachineVirtualCamera camera)
        {
            var comp = camera.AddCinemachineComponent<CinemachinePOV>();

            comp.m_RecenterTarget = recenterTarget;

            comp.m_VerticalAxis = verticalAxis;

            comp.m_VerticalRecentering = verticalRecentering;

            comp.m_HorizontalAxis = horizontalAxis;

            comp.m_HorizontalRecentering = horizontalRecentering;
        }
    }
}
