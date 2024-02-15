using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSteppe
{
    public class FramerateLock : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            uint refreshRate = Screen.currentResolution.refreshRateRatio.numerator / 1000;
            Application.targetFrameRate = (int)refreshRate;
        }
    }
}
