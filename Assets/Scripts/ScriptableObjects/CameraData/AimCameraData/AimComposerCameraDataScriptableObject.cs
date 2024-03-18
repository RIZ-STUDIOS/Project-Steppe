using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.ScriptableObjects.CameraData.AimCameraData
{
    public class AimComposerCameraDataScriptableObject : BaseAimCameraDataScriptableObject
    {
        public Vector3 trackedObjectOffset;
        public float lookaheadTime;
        public float lookaheadSmoothing;
        public bool lookaheadIgnoreY;

        public float horizontalDamping;
        public float verticalDamping;

        public float screenX;
        public float screenY;

        public float deadZoneWidth;
        public float deadZoneHeight;
        public float softZoneWidth;
        public float softZoneHeight;
        public float biasX;
        public float biasY;

        public bool centerOnActivate;

        public override void ApplyCameraData(CinemachineVirtualCamera camera)
        {
            var comp = camera.AddCinemachineComponent<CinemachineComposer>();

            comp.m_TrackedObjectOffset = trackedObjectOffset;
            comp.m_LookaheadTime = lookaheadTime;
            comp.m_LookaheadSmoothing = lookaheadSmoothing;
            comp.m_LookaheadIgnoreY = lookaheadIgnoreY;

            comp.m_HorizontalDamping = horizontalDamping;
            comp.m_VerticalDamping = verticalDamping;

            comp.m_ScreenX = screenX;
            comp.m_ScreenY = screenY;

            comp.m_DeadZoneWidth = deadZoneWidth;
            comp.m_DeadZoneHeight = deadZoneHeight;
            comp.m_SoftZoneWidth = softZoneWidth;
            comp.m_SoftZoneHeight = softZoneHeight;
            comp.m_BiasX = biasX;
            comp.m_BiasY = biasY;

            comp.m_CenterOnActivate = centerOnActivate;
        }
    }
}
