using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MainMenuButtons : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Image buttonImg;

    private void Start()
    {
        buttonImg = GetComponentInChildren<Image>();
    }
    public void OnSelect(BaseEventData baseEventData)
    {
        buttonImg.enabled = true;
    }

    public void OnDeselect(BaseEventData baseEventData)
    {
        buttonImg.enabled = false;
    }
}
