using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class GeneralOptions : MonoBehaviour
{
    [SerializeField]
    private List<ResItem> resolutions = new List<ResItem>();
    [SerializeField]
    private TMP_Text resText;
    private int resolutionIndex;
    private void Start()
    {
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                resolutionIndex = i;

                UpdateResolution();
            }
        }
    }

    private void UpdateResolution()
    {
        resText.text = "< " + resolutions[resolutionIndex].horizontal.ToString() + " x " + resolutions[resolutionIndex].vertical.ToString() + " >";
        Screen.SetResolution(resolutions[resolutionIndex].horizontal, resolutions[resolutionIndex].vertical, false);
    }
    public void ResLeft()
    {
        resolutionIndex--;
        if (resolutionIndex < 0)
        {
            resolutionIndex = 0;
        }
        UpdateResolution();
    }
    public void ResRight()
    {
        resolutionIndex++;
        if (resolutionIndex > resolutions.Count - 1)
        {
            resolutionIndex = resolutions.Count - 1;
        }
        UpdateResolution();
    }


    [System.Serializable]
    public class ResItem
    {
        public int horizontal, vertical;
    }
}