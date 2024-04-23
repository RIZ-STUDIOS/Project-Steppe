using Cinemachine;
using UnityEngine;

namespace ProjectSteppe.ScriptableObjects.CameraData.BodyCameraData
{
    public class Body3rdFollowCameraDataScriptableObject : BaseBodyCameraDataScriptableObject
    {
        public Vector3 damping;
        public Vector3 shoulderOffset;
        public float verticalArmLength;
        public float cameraSide;
        public float cameraDistance;
        public LayerMask cameraCollisionFilter;
        public string ignoreTag;
        public float cameraRadius;
        public float dampingIntoCollision;
        public float dampingFromCollision;

        public override void ApplyCameraData(CinemachineVirtualCamera camera)
        {
            var comp = camera.AddCinemachineComponent<Cinemachine3rdPersonFollow>();

            comp.Damping = damping;
            comp.ShoulderOffset = shoulderOffset;
            comp.VerticalArmLength = verticalArmLength;
            comp.CameraSide = cameraSide;
            comp.CameraDistance = cameraDistance;
            comp.CameraCollisionFilter = cameraCollisionFilter;
            comp.IgnoreTag = ignoreTag;
            comp.CameraRadius = cameraRadius;
            comp.DampingIntoCollision = dampingIntoCollision;
            comp.DampingFromCollision = dampingFromCollision;
        }
    }
}
