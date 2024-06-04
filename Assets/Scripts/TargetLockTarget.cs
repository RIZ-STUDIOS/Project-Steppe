using ProjectSteppe.Managers;
using UnityEngine;

namespace ProjectSteppe
{
    public class TargetLockTarget : MonoBehaviour
    {
        public Transform lookAtTransform;

        private Camera mainCamera;

        private Vector3 viewportPosition;

        public Vector3 ViewPortPosition => viewportPosition;

        private void Awake()
        {
            if (!lookAtTransform)
                lookAtTransform = transform;

            mainCamera = Camera.main;
        }

        private void Update()
        {
            viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
            if(viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1 && viewportPosition.z > 0)
            {
                if (!GameManager.Instance.visibleTargets.Contains(this))
                    GameManager.Instance.visibleTargets.Add(this);
            }
            else
            {
                if (GameManager.Instance.visibleTargets.Contains(this))
                    GameManager.Instance.visibleTargets.Remove(this);
            }
        }

        private void OnDestroy()
        {
            GameManager.Instance.visibleTargets.Remove(this);
        }
    }
}
