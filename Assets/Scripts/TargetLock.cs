using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class TargetLock : MonoBehaviour
    {
        public GameObject mainCamera;
        public GameObject targetLockCamera;

        public bool lockOn;

        private void Update()
        {
            if (lockOn)
            {
                mainCamera.SetActive(false);
                targetLockCamera.SetActive(true);
            }
            else
            {
                mainCamera.SetActive(true);
                targetLockCamera.SetActive(false);
            }
        }
    }
}
