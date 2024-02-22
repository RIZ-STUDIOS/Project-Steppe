using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RicTools;
using RicTools.ScriptableObjects;
using RicTools.EditorAttributes;
using Cinemachine;

namespace ProjectSteppe.ScriptableObjects
{
    [DefaultScriptableObjectName(nameof(presetName))]
    public class CameraDataScriptableObject : GenericScriptableObject
    {
        public string presetName;

        public CinemachineVirtualCameraBase.StandbyUpdateMode standbyUpdateMode;

        public float verticalFov;
        public float nearClipPlane;
        public float farClipPlane;
        public float dutch;

        public void ApplyCameraData(CinemachineVirtualCamera camera)
        {
            camera.m_StandbyUpdate = standbyUpdateMode;

            camera.m_Lens.FieldOfView = verticalFov;
            camera.m_Lens.NearClipPlane = nearClipPlane;
            camera.m_Lens.FarClipPlane = farClipPlane;
            camera.m_Lens.Dutch = dutch;
        }
    }
}