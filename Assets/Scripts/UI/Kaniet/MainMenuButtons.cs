using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MainMenuButtons : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Image buttonImg;
    private Animator animator;

    private void Start()
    {
        buttonImg = GetComponentInChildren<Image>();
        animator = GetComponent<Animator>();
    }
    public void OnSelect(BaseEventData baseEventData)
    {
        buttonImg.enabled = true;
        animator.SetBool("isSelected", true);
    }

    public void OnDeselect(BaseEventData baseEventData)
    {
        buttonImg.enabled = false;
        animator.SetBool("isSelected", false);
    }
}
