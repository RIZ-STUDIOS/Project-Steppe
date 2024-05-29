using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.LowLevel;

namespace ProjectSteppe
{
    public class FramerateCounter : MonoBehaviour
    {
        TextMeshProUGUI tmp;
        float refreshTime = 0.5f;
        float currenTick;

        private void Awake()
        {
            tmp = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (currenTick < refreshTime)
            {
                currenTick += Time.deltaTime;
            }
            else
            {
                currenTick = 0;
                tmp.text = ((int)(1 / Time.smoothDeltaTime)).ToString();
            }
        }
    }
}
