using UnityEngine;

namespace ProjectSteppe
{
    public class FramerateLock : MonoBehaviour
    {
        public int refreshLock;

        // Start is called before the first frame update
        void Start()
        {
            //uint refreshRate = Screen.currentResolution.refreshRateRatio.numerator / 1000;
            //QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = refreshLock;
        }

        private void OnValidate()
        {
            if(Application.isPlaying)
            Application.targetFrameRate = refreshLock;
        }
    }
}
