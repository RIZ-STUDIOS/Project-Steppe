using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MasterVolumeButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Image image;
    public void OnSelect(BaseEventData baseEventData)
    {
        image.enabled = true;
    }

    public void OnDeselect(BaseEventData baseEventData)
    {
        image.enabled = false;
    }
    private void Start()
    {
        image = GetComponentInParent<Image>();
    }
}
