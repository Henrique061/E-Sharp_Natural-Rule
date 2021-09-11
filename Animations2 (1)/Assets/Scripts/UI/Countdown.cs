using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] int countdown;
    [SerializeField] int tutorial = 1;
    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown > 0)
        {
            text.fontSize = 190;
            text.text = countdown.ToString();
        }

        if (countdown == 0)
        {
            text.fontSize = 90;

            if (tutorial == 1)
            {
                text.text = "Press [ "+PlayerPrefs.GetString("JumpK")+" / <sprite="+Keys.buttonsList["Jump"]+"> ] to JUMP.\n\nYou'll become invincible and \nwon't be able to jump for half a second.";
            }
            else if (tutorial == 2)
            {
                text.text = "Press [ " + PlayerPrefs.GetString("CrouchK") + " / <sprite=" + Keys.buttonsList["Crouch"] + "> ] to CROUCH.";
            }
            else if (tutorial == 3)
            {
                text.text = "Press [ " + PlayerPrefs.GetString("DashK") + " / <sprite=" + Keys.buttonsList["Dash"] + "> ] to DASH.\n\nYou'll become invincible and \nwon't be able to dash for half a second.";
            }
            else if (tutorial == 4) {
                text.text = "Try everything out now.";
            }
        }
    }
}
