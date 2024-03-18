using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.ScriptableObjects.CameraData.BodyCameraData
{
    public class BodyFramingTransposerCameraDataScriptableObject : BaseBodyCameraDataScriptableObject
    {
        public Vector3 trackedObjectOffset;
        public float lookaheadTime;
        public float lookaheadSmoothing;
        public bool lookaheadIgnoreY;

        public float xDamping;
        public float yDamping;
        public float zDamping;
        public bool targetMovementOnly;

        public float screenX;
        public float screenY;
        public float cameraDistance;

        public float deadZoneWidth;
        public float deadZoneHeight;
        public float deadZoneDepth;

        public bool unlimitedSoftZone;
        public float softZoneWidth;
        public float softZoneHeight;
        public float biasX;
        public float biasY;
        public bool centerOnActivate;

        public override void ApplyCameraData(CinemachineVirtualCamera camera)
        {
            var comp = camera.AddCinemachineComponent<CinemachineFramingTransposer>();

            comp.m_TrackedObjectOffset = trackedObjectOffset;
            comp.m_LookaheadTime = lookaheadTime;
            comp.m_LookaheadSmoothing = lookaheadSmoothing;
            comp.m_LookaheadIgnoreY = lookaheadIgnoreY;

            comp.m_XDamping = xDamping;
            comp.m_YDamping = yDamping;
            comp.m_ZDamping = zDamping;
            comp.m_TargetMovementOnly = targetMovementOnly;

            comp.m_ScreenX = screenX;
            comp.m_ScreenY = screenY;
            comp.m_CameraDistance = cameraDistance;

            comp.m_DeadZoneWidth = deadZoneWidth;
            comp.m_DeadZoneHeight = deadZoneHeight;
            comp.m_DeadZoneDepth = deadZoneDepth;

            comp.m_UnlimitedSoftZone = unlimitedSoftZone;
            comp.m_SoftZoneWidth = softZoneWidth;
            comp.m_SoftZoneHeight = softZoneHeight;
            comp.m_BiasX = biasX;
            comp.m_BiasY = biasY;
            comp.m_CenterOnActivate = centerOnActivate;
        }
    }
}
