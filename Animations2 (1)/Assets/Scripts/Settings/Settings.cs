using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [Header("Showing Up")]
    [SerializeField] GameObject settings;
    [SerializeField] Animator anim;
    [SerializeField] PauseMenu pause;
    [SerializeField] GameObject options;

    [Header("Selectables")]
    [SerializeField] TMP_Dropdown screen;
    [SerializeField] TMP_Dropdown resolution;
    [SerializeField] Toggle fx;

    //[Header("Verification")]
    //[SerializeField] bool paused = true;

    private void Awake()
    {
        if (screen != null)
        {
            if (Screen.fullScreen == true)
            {
                screen.SetValueWithoutNotify(0);
            }
            else
                screen.SetValueWithoutNotify(1);
        }

        if (resolution != null) 
        {
            if (Screen.height == 720)
                resolution.SetValueWithoutNotify(1);
            else if (Screen.height == 540)
                resolution.SetValueWithoutNotify(2);
            else
                resolution.SetValueWithoutNotify(0);
        }

        if (!PlayerPrefs.HasKey("camfx"))
        {
            PlayerPrefs.SetInt("camfx", 1);
            fx.isOn = true;
        }
        else {
            if (fx != null)
            {
                if (PlayerPrefs.GetInt("camfx") == 1)
                    fx.SetIsOnWithoutNotify(true);
                else
                    fx.SetIsOnWithoutNotify(false);
            }
        }

    }

    private void Update()
    {
        if (options != null)
            pause.settingUp = options.activeSelf;
    }

    #region showing
    public void ShowSettings(){
        if (settings.activeSelf)
        {
            StartCoroutine("SettingsDelay"); // settings off
        }
        else
        {
            settings.SetActive(true); // settings on
            anim.SetTrigger("Down");
        }

    }
    public void ForceShut()
    {
        pause.settingUp = false;
        if (settings.activeSelf)
            StartCoroutine("SettingsDelay"); // settings off unpausing
        pause.Resume();
    }

    IEnumerator SettingsDelay() {
        anim.SetTrigger("Up");
        yield return new WaitForSecondsRealtime(0.3f);
        settings.SetActive(false);
    }

    
    #endregion

    #region screen

    public void CameraFX(Toggle cam) {
        if (cam.isOn == true)
            PlayerPrefs.SetInt("camfx", 1);
        else
            PlayerPrefs.SetInt("camfx", 0);
    }

    public void ScreenMode() 
    {
        if (Screen.fullScreen == false)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }

    public void ScreenSize() {
        if (resolution.value == 0)
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        else if (resolution.value == 1)
            Screen.SetResolution(1280, 720, Screen.fullScreen);
        else if (resolution.value == 2)
            Screen.SetResolution(960, 540, Screen.fullScreen);
    }
    #endregion
}
