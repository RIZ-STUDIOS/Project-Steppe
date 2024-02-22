using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe.ScriptableObjects.CameraData.BodyCameraData
{
    public class BaseBodyCameraDataScriptableObject : ScriptableObject
    {
        public virtual void ApplyCameraData(CinemachineVirtualCamera camera)
        {

        }

        protected void DeleteComponentInCamera(CinemachineVirtualCamera camera)
        {
            var comp = camera.GetCinemachineComponent(CinemachineCore.Stage.Body);
            comp.enabled = false;
            RuntimeUtility.DestroyObject(comp);
            camera.InvalidateComponentPipeline();
        }
    }
}
