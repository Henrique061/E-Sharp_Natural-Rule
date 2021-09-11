using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Test : MonoBehaviour
{
    [SerializeField] PauseMenu pm;
    [SerializeField] string testName;
    TMP_Text texty;

    private void Awake()
    {
        texty = this.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (testName == "canPause")
            texty.text = testName + ": " + pm.canPause;
        if (testName == "gamePaused")
            texty.text = testName + ": " + PauseMenu.gamePaused;
        if(testName == "Button Pause")
            texty.text = testName + ": " + Input.GetAxis(Keys.buttonsList["Pause"]);
    }
}
