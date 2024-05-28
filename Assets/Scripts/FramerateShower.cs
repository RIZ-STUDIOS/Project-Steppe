using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectSteppe
{
    public class FramerateShower : MonoBehaviour
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
                tmp.text = (1.0f / Time.unscaledDeltaTime).ToString();
            }
        }
    }
}
