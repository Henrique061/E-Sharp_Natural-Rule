using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Keys.keysList.Add("Jump", KeyCode.Space);
        Keys.keysList.Add("Dash", KeyCode.RightShift);
        Keys.keysList.Add("Crouch", KeyCode.S);
        Keys.keysList.Add("Pause", KeyCode.Escape);

        Keys.buttonsList.Add("Jump", "0");
        Keys.buttonsList.Add("Dash", "1");
        Keys.buttonsList.Add("Crouch", "2");
        Keys.buttonsList.Add("Pause", "7");
    }
}
