using StarterAssets;
using UnityEngine;

namespace ProjectSteppe
{
    public class CameraZoom : MonoBehaviour
    {
        private Vector3 zoomOffset;
        private StarterAssetsInputs _input;

        [Range(0.1f, 0.8f)] public float zoomScale = 0.2f;
        public float maxCameraZoom = 7.5f;
        public float minimumDivider = 3f;

        private void Start()
        {
            _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
            zoomOffset = Vector3.zero;
        }

        /*private void Update()
        {
            Zoom();
            ResetZoom();
        }

        public void Zoom()
        {
            
            if (_input.zoom > 0 && zoomOffset.y > -maxCameraZoom / minimumDivider) //dividing keeps it outside from player clipping!
            {
                zoomOffset += new Vector3(0f, -zoomScale, zoomScale);
            }
            else if (_input.zoom < 0 && zoomOffset.y < maxCameraZoom)
            {
                zoomOffset += new Vector3(0f, zoomScale, -zoomScale);
            }
        }
        public void ResetZoom()
        {
            if (_input.resetZoom)
            {
                zoomOffset = Vector3.zero;
            }
        }*/
    }
}
