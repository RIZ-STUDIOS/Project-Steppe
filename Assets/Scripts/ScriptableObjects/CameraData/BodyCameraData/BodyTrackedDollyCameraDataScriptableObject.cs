using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.ScriptableObjects.CameraData.BodyCameraData
{
    public class BodyTrackedDollyCameraDataScriptableObject : BaseBodyCameraDataScriptableObject
    {
        public CinemachinePathBase path;

        public float pathPosition;

        public CinemachinePathBase.PositionUnits positionUnits;

        public Vector3 pathOffset;

        public float xDamping;
        public float yDamping;
        public float zDamping;

        public CinemachineTrackedDolly.CameraUpMode cameraUp;

        public float pitchDamping;
        public float yawDamping;
        public float rollDamping;

        public bool autoDollyEnabled;
        public float autoDollyPositionOffset;
        public int autoDollySearchRadius;
        public int autoDollySearchResolution;

        public override void ApplyCameraData(CinemachineVirtualCamera camera)
        {
            var comp = camera.AddCinemachineComponent<CinemachineTrackedDolly>();

            comp.m_Path = path;
            comp.m_PathPosition = pathPosition;
            comp.m_PositionUnits = positionUnits;

            comp.m_PathOffset = pathOffset;
            comp.m_XDamping= xDamping;
            comp.m_YDamping= yDamping;
            comp.m_ZDamping= zDamping;

            comp.m_CameraUp = cameraUp;

            if(cameraUp != CinemachineTrackedDolly.CameraUpMode.Default)
            {
                comp.m_PitchDamping = pitchDamping;
                comp.m_YawDamping = yawDamping;

                if(cameraUp == CinemachineTrackedDolly.CameraUpMode.Path || cameraUp == CinemachineTrackedDolly.CameraUpMode.FollowTarget)
                {
                    comp.m_RollDamping = rollDamping;
                }
            }

            comp.m_AutoDolly = new CinemachineTrackedDolly.AutoDolly(autoDollyEnabled, autoDollyPositionOffset, autoDollySearchRadius, autoDollySearchResolution);
        }
    }
}
