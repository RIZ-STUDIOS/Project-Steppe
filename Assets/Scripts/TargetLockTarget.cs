using UnityEngine;

namespace ProjectSteppe
{
    public class TargetLockTarget : MonoBehaviour
    {
        public Transform lookAtTransform;

        private void Awake()
        {
            if (!lookAtTransform)
                lookAtTransform = transform;
        }
    }
}
