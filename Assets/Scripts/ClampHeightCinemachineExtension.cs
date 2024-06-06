using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class ClampHeightCinemachineExtension : CinemachineExtension
    {
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Noise)
            {
                var euler = state.RawOrientation.eulerAngles;
                while (euler.x > 180)
                    euler.x -= 360;
                while (euler.x < -180)
                    euler.x += 360;
                var prev = euler.x;
                euler.x = Mathf.Clamp(euler.x, -30f, 50);
                if(euler.x != prev)
                {
                    state.PositionCorrection = state.RawOrientation * Vector3.forward;
                }
                state.RawOrientation = Quaternion.Euler(euler.x, euler.y, 0);
            }
        }
    }
}
