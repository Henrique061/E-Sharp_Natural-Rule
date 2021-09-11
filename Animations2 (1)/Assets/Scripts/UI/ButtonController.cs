using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [Tooltip("The button that will be selected at start")]
    [SerializeField] GameObject selectedButton;

    GameObject currentSelectedButton;
    GameObject[] disableButtons;
    bool paused;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedButton);

        disableButtons = GameObject.FindGameObjectsWithTag("Button");
    }

    private void Update()
    {
        if (PauseMenu.gamePaused)
        {
            paused = true;

            foreach (GameObject b in disableButtons)
                b.GetComponent<Button>().interactable = false;
        }

        else
        {
            if (paused)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(currentSelectedButton);
                paused = false;
            }

            currentSelectedButton = EventSystem.current.currentSelectedGameObject;

            foreach (GameObject b in disableButtons)
                b.GetComponent<Button>().interactable = true;
        }
    }
}
