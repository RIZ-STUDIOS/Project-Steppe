using Cinemachine;
using UnityEngine;

namespace ProjectSteppe.ScriptableObjects.CameraData.AimCameraData
{
    public class AimGroupComposerCameraDataScriptableObject : BaseAimCameraDataScriptableObject
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

        public float groupFramingSize;

        public CinemachineGroupComposer.FramingMode framingMode;

        public float frameDamping;

        public CinemachineGroupComposer.AdjustmentMode adjustmentMode;

        public float maxDollyIn;
        public float maxDollyOut;
        public float minimumDistance;
        public float maximumDistance;

        public float minimumFov;
        public float maximumFov;

        public override void ApplyCameraData(CinemachineVirtualCamera camera)
        {
            var comp = camera.AddCinemachineComponent<CinemachineGroupComposer>();

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

            comp.m_GroupFramingSize = groupFramingSize;

            comp.m_FramingMode = framingMode;

            comp.m_FrameDamping = frameDamping;

            comp.m_AdjustmentMode = adjustmentMode;

            if (adjustmentMode == CinemachineGroupComposer.AdjustmentMode.DollyOnly || adjustmentMode == CinemachineGroupComposer.AdjustmentMode.DollyThenZoom)
            {
                comp.m_MaxDollyIn = maxDollyIn;
                comp.m_MaxDollyOut = maxDollyOut;
                comp.m_MaximumDistance = maximumDistance;
                comp.m_MinimumDistance = minimumDistance;
            }

            if (adjustmentMode == CinemachineGroupComposer.AdjustmentMode.ZoomOnly || adjustmentMode == CinemachineGroupComposer.AdjustmentMode.DollyThenZoom)
            {
                comp.m_MinimumFOV = minimumFov;
                comp.m_MaximumFOV = maximumFov;
            }
        }
    }
}
