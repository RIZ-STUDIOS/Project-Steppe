using ProjectSteppe.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class HeadLook : MonoBehaviour
    {
        public PlayerManager playerManager;
        private Transform mainCam;

        private void Awake()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            mainCam = playerManager.PlayerCamera.MainCameraTransform;
        }

        private void Update()
        {
            if (Time.timeScale == 0) return;
            if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, 200, LayerMask.GetMask("Environment", "Enemy")))
            {
                transform.position = hit.point;
            }
            else
            {
                transform.localPosition = new Vector3(0, 1.6f, 1);
            }
        }
    }
}
