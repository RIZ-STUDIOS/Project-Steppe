using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ProjectSteppe.Items;
using TMPro;
public class Pause : MonoBehaviour
{
    public InventoryItemScriptableObject so;
    public GameObject pauseMenu;
    private PlayerInput playerInput;

    private InputAction pauseAction;
    [HideInInspector]
    public InputAction cancelAction;

    public bool paused = false;

    public GameObject itemButton;
    public RectTransform[] spawnPos;
    private int currentSpawnIndex = 0;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        pauseAction = playerInput.actions["Pause"];
        cancelAction = playerInput.actions["Cancel"];;
    }
    private void Update()
    {
        CheckPause();
    }
    private void CheckPause()
    {
        if (pauseAction.triggered)
        {
            paused = !paused;
            Instantiate(itemButton, spawnPos[currentSpawnIndex]);
            currentSpawnIndex++;
        }

        if (paused)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }
    }
}
