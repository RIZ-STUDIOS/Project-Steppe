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
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.1f);
    }

    public void OnDeselect(BaseEventData baseEventData)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
    }
    private void Start()
    {
        image = GetComponentInParent<Image>();
    }
}
