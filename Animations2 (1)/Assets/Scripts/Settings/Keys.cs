using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Keys : MonoBehaviour
{
    [HideInInspector] public static Dictionary<string, KeyCode> keysList = new Dictionary<string, KeyCode>();
    [HideInInspector] public static Dictionary<string, string> buttonsList = new Dictionary<string, string>();

    TMP_Text Kjump, Kdash, Kcrouch, Kpause;
    TMP_Text Bjump, Bdash, Bcrouch, Bpause;

    private GameObject currentKey, changeColor, currentButton, changeColor2;

    private void Awake()
    {
        if (Kjump == null)
        {
            Kjump = GameObject.Find("JumpK").GetComponent<TMP_Text>();
            Kdash = GameObject.Find("DashK").GetComponent<TMP_Text>();
            Kcrouch = GameObject.Find("CrouchK").GetComponent<TMP_Text>();
            Kpause = GameObject.Find("PauseK").GetComponent<TMP_Text>();
        }

        if (Bjump == null)
        {
            Bjump = GameObject.Find("JumpB").GetComponent<TMP_Text>();
            Bdash = GameObject.Find("DashB").GetComponent<TMP_Text>();
            Bcrouch = GameObject.Find("CrouchB").GetComponent<TMP_Text>();
            Bpause = GameObject.Find("PauseB").GetComponent<TMP_Text>();
        }

        if (!PlayerPrefs.HasKey("JumpK"))
        {
            keysList.Add("Jump", KeyCode.Space);
            keysList.Add("Dash", KeyCode.LeftShift);
            keysList.Add("Crouch", KeyCode.S);
            keysList.Add("Pause", KeyCode.Escape);

            PlayerPrefs.SetString("JumpK", keysList["Jump"].ToString());
            PlayerPrefs.SetString("DashK", keysList["Dash"].ToString());
            PlayerPrefs.SetString("CrouchK", keysList["Crouch"].ToString());
            PlayerPrefs.SetString("PauseK", keysList["Pause"].ToString());
        }
        else
        {
            keysList["Jump"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpK"));
            Kjump.text = PlayerPrefs.GetString("JumpK");
            keysList["Dash"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DashK"));
            Kdash.text = PlayerPrefs.GetString("DashK");
            keysList["Crouch"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CrouchK"));
            Kcrouch.text = PlayerPrefs.GetString("CrouchK");
            keysList["Pause"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PauseK"));
            Kpause.text = PlayerPrefs.GetString("PauseK");
        }

        if (!PlayerPrefs.HasKey("JumpB"))
        {
            buttonsList.Add("Jump", "0");
            buttonsList.Add("Dash", "1");
            buttonsList.Add("Crouch", "2");
            buttonsList.Add("Pause", "7");

            PlayerPrefs.SetString("JumpB", buttonsList["Jump"]);
            PlayerPrefs.SetString("DashB", buttonsList["Dash"]);
            PlayerPrefs.SetString("CrouchB", buttonsList["Crouch"]);
            PlayerPrefs.SetString("PauseB", buttonsList["Pause"]);
        }
        else
        {
            buttonsList["Jump"] = PlayerPrefs.GetString("JumpB");
            if (PlayerPrefs.GetString("JumpB") == "0")
                Bjump.text = "A";
            else if (PlayerPrefs.GetString("JumpB") == "1")
                Bjump.text = "B";
            else if (PlayerPrefs.GetString("JumpB") == "2")
                Bjump.text = "X";
            else if (PlayerPrefs.GetString("JumpB") == "3")
                Bjump.text = "Y";
            else if (PlayerPrefs.GetString("JumpB") == "4")
                Bjump.text = "LB";
            else if (PlayerPrefs.GetString("JumpB") == "5")
                Bjump.text = "RB";
            else if (PlayerPrefs.GetString("JumpB") == "6")
                Bjump.text = "Back";
            else if (PlayerPrefs.GetString("JumpB") == "7")
                Bjump.text = "Start";
            else if (PlayerPrefs.GetString("JumpB") == "8")
                Bjump.text = "LT";
            else if (PlayerPrefs.GetString("JumpB") == "9")
                Bjump.text = "RT";

            buttonsList["Dash"] = PlayerPrefs.GetString("DashB");
            if (PlayerPrefs.GetString("DashB") == "0")
                Bdash.text = "A";
            else if (PlayerPrefs.GetString("DashB") == "1")
                Bdash.text = "B";
            else if (PlayerPrefs.GetString("DashB") == "2")
                Bdash.text = "X";
            else if (PlayerPrefs.GetString("DashB") == "3")
                Bdash.text = "Y";
            else if (PlayerPrefs.GetString("DashB") == "4")
                Bdash.text = "LB";
            else if (PlayerPrefs.GetString("DashB") == "5")
                Bdash.text = "RB";
            else if (PlayerPrefs.GetString("DashB") == "6")
                Bdash.text = "Back";
            else if (PlayerPrefs.GetString("DashB") == "7")
                Bdash.text = "Start";
            else if (PlayerPrefs.GetString("DashB") == "8")
                Bdash.text = "LT";
            else if (PlayerPrefs.GetString("DashB") == "9")
                Bdash.text = "RT";

            buttonsList["Crouch"] = PlayerPrefs.GetString("CrouchB");
            if (PlayerPrefs.GetString("CrouchB") == "0")
                Bcrouch.text = "A";
            else if (PlayerPrefs.GetString("CrouchB") == "1")
                Bcrouch.text = "B";
            else if (PlayerPrefs.GetString("CrouchB") == "2")
                Bcrouch.text = "X";
            else if (PlayerPrefs.GetString("CrouchB") == "3")
                Bcrouch.text = "Y";
            else if (PlayerPrefs.GetString("CrouchB") == "4")
                Bcrouch.text = "LB";
            else if (PlayerPrefs.GetString("CrouchB") == "5")
                Bcrouch.text = "RB";
            else if (PlayerPrefs.GetString("CrouchB") == "6")
                Bcrouch.text = "Back";
            else if (PlayerPrefs.GetString("CrouchB") == "7")
                Bcrouch.text = "Start";
            else if (PlayerPrefs.GetString("CrouchB") == "8")
                Bcrouch.text = "LT";
            else if (PlayerPrefs.GetString("CrouchB") == "9")
                Bcrouch.text = "RT";

            buttonsList["Pause"] = PlayerPrefs.GetString("PauseB");
            if (PlayerPrefs.GetString("PauseB") == "0")
                Bpause.text = "A";
            else if (PlayerPrefs.GetString("PauseB") == "1")
                Bpause.text = "B";
            else if (PlayerPrefs.GetString("PauseB") == "2")
                Bpause.text = "X";
            else if (PlayerPrefs.GetString("PauseB") == "3")
                Bpause.text = "Y";
            else if (PlayerPrefs.GetString("PauseB") == "4")
                Bpause.text = "LB";
            else if (PlayerPrefs.GetString("PauseB") == "5")
                Bpause.text = "RB";
            else if (PlayerPrefs.GetString("PauseB") == "6")
                Bpause.text = "Back";
            else if (PlayerPrefs.GetString("PauseB") == "7")
                Bpause.text = "Start";
            else if (PlayerPrefs.GetString("PauseB") == "8")
                Bpause.text = "LT";
            else if (PlayerPrefs.GetString("PauseB") == "9")
                Bpause.text = "RT";
        }
    }

    private void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                if (e.keyCode.ToString().Equals("A")) { }
                else if (e.keyCode.ToString().Equals("D")) { }
                else if (e.keyCode.ToString().Equals("LeftArrow")) { }
                else if (e.keyCode.ToString().Equals("RightArrow")) { }
                else if (e.keyCode.ToString().Equals("LeftCommand")) { }
                else if (e.keyCode.ToString().Equals(keysList["Jump"].ToString()))
                {

                }
                else if (e.keyCode.ToString().Equals(keysList["Dash"].ToString()))
                {

                }
                else if (e.keyCode.ToString().Equals(keysList["Crouch"].ToString()))
                {

                }
                else if (e.keyCode.ToString().Equals(keysList["Pause"].ToString()))
                {

                }
                else
                {
                    keysList[currentKey.name.Remove(currentKey.name.Length - 1)] = e.keyCode;
                    currentKey.GetComponent<TMP_Text>().text = e.keyCode.ToString();
                    PlayerPrefs.SetString(currentKey.name, e.keyCode.ToString());
                }
                changeColor.GetComponent<Image>().color = Color.white;
                currentKey = null;
            }
        }

        if (currentButton != null)
        {

            string cur = "";

            if (cur.Equals(""))
            {
                for (int i = 0; i <= 9; i++)
                {
                    if (
                        i.ToString() == buttonsList["Jump"]
                        ||
                        i.ToString() == buttonsList["Dash"]
                        ||
                        i.ToString() == buttonsList["Crouch"]
                        ||
                        i.ToString() == buttonsList["Pause"] 
                        ) {}
                    else if (Input.GetAxis(i.ToString()) > 0.2f)
                    {
                        cur = i.ToString();
                        switch (i) {
                            case 0:
                                currentButton.GetComponent<TMP_Text>().text = "A";
                                break;
                            case 1:
                                currentButton.GetComponent<TMP_Text>().text = "B";
                                break;
                            case 2:
                                currentButton.GetComponent<TMP_Text>().text = "X";
                                break;
                            case 3:
                                currentButton.GetComponent<TMP_Text>().text = "Y";
                                break;
                            case 4:
                                currentButton.GetComponent<TMP_Text>().text = "LB";
                                break;
                            case 5:
                                currentButton.GetComponent<TMP_Text>().text = "RB";
                                break;
                            case 6:
                                currentButton.GetComponent<TMP_Text>().text = "Back";
                                break;
                            case 7:
                                currentButton.GetComponent<TMP_Text>().text = "Start";
                                break;
                            case 8:
                                    currentButton.GetComponent<TMP_Text>().text = "LT";
                                break;
                            case 9:
                                    currentButton.GetComponent<TMP_Text>().text = "RT";
                                break;
                        }
                            buttonsList[currentButton.name.Remove(currentButton.name.Length - 1)] = cur;
                            PlayerPrefs.SetString(currentButton.name, cur);

                        changeColor2.GetComponent<Image>().color = Color.white;
                        currentButton = null;
                    }
                }
            }



            //Event e = Event.current;
            //if (e.isKey)
            //{
            //    if (e.keyCode.ToString().Equals("JoystickButton0"))
            //    {
            //        buttonsList[currentButton.name.Remove(currentButton.name.Length - 1)] = e.keyCode;
            //        currentButton.GetComponent<TMP_Text>().text = "A";
            //        PlayerPrefs.SetString(currentButton.name, e.keyCode.ToString());
            //    }
            //    else if (e.keyCode.ToString().Equals("JoystickButton1"))
            //    {
            //        buttonsList[currentButton.name.Remove(currentButton.name.Length - 1)] = e.keyCode;
            //        currentButton.GetComponent<TMP_Text>().text = "B";
            //        PlayerPrefs.SetString(currentButton.name, e.keyCode.ToString());
            //    }
            //    else if (e.keyCode.ToString().Equals("JoystickButton2"))
            //    {
            //        buttonsList[currentButton.name.Remove(currentButton.name.Length - 1)] = e.keyCode;
            //        currentButton.GetComponent<TMP_Text>().text = "X";
            //        PlayerPrefs.SetString(currentButton.name, e.keyCode.ToString());
            //    }
            //    else if (e.keyCode.ToString().Equals("JoystickButton3"))
            //    {
            //        buttonsList[currentButton.name.Remove(currentButton.name.Length - 1)] = e.keyCode;
            //        currentButton.GetComponent<TMP_Text>().text = "Y";
            //        PlayerPrefs.SetString(currentButton.name, e.keyCode.ToString());
            //    }
            //    else if (e.keyCode.ToString().Equals("JoystickButton6"))
            //    {
            //        buttonsList[currentButton.name.Remove(currentButton.name.Length - 1)] = e.keyCode;
            //        currentButton.GetComponent<TMP_Text>().text = "Back";
            //        PlayerPrefs.SetString(currentButton.name, e.keyCode.ToString());
            //    }
            //    else if (e.keyCode.ToString().Equals("JoystickButton7"))
            //    {
            //        buttonsList[currentButton.name.Remove(currentButton.name.Length - 1)] = e.keyCode;
            //        currentButton.GetComponent<TMP_Text>().text = "Start";
            //        PlayerPrefs.SetString(currentButton.name, e.keyCode.ToString());
            //    }
            /*else if (e.keyCode.ToString().Equals(buttonsList["Jump"].ToString())) {
            }
            else if (e.keyCode.ToString().Equals(buttonsList["Dash"].ToString()))
            {
            }
            else if (e.keyCode.ToString().Equals(buttonsList["Crouch"].ToString()))
            {
            }
            else if (e.keyCode.ToString().Equals(buttonsList["Pause"].ToString()))
            {
            }*/
            //}
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        if (clicked != null)
        {
            clicked.GetComponent<Image>().color = Color.white;
        }
        if (changeColor != null)
            changeColor.GetComponent<Image>().color = Color.white;

        currentKey = clicked.transform.GetChild(0).gameObject;
        changeColor = clicked;

        changeColor.GetComponent<Image>().color = new Color(0.735849f, 0.7184941f, 0.7184941f);
    }

    public void ChangeButton(GameObject clicked)
    {
        if (clicked != null)
        {
            clicked.GetComponent<Image>().color = Color.white;
        }
        if (changeColor2 != null)
            changeColor2.GetComponent<Image>().color = Color.white;

        currentButton = clicked.transform.GetChild(0).gameObject;
        changeColor2 = clicked;

        changeColor2.GetComponent<Image>().color = new Color(0.735849f, 0.7184941f, 0.7184941f);
    }
}
