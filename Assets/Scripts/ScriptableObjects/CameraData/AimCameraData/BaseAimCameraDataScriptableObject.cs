using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.ScriptableObjects.CameraData.AimCameraData
{
    public class BaseAimCameraDataScriptableObject : ScriptableObject
    {
        public virtual void ApplyCameraData(CinemachineVirtualCamera camera)
        {

        }

        protected void DeleteComponentInCamera(CinemachineVirtualCamera camera)
        {
            var comp = camera.GetCinemachineComponent(CinemachineCore.Stage.Aim);
            comp.enabled = false;
            RuntimeUtility.DestroyObject(comp);
            camera.InvalidateComponentPipeline();
        }
    }
}
