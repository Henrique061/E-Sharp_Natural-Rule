using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    #region Variables

    public static bool gamePaused = false; // the condition that pauses or resume the game

    [Header("UI")]

    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject resumeDelayUI;
    float prevAxisValue = 0;
    [Tooltip("The button that will be selected when activating the pause")]
    [SerializeField] GameObject selectedButton;
    [HideInInspector] public bool settingUp;

    [Header("Animations")]

    [SerializeField] Animator anim; // anim for Pause Menu
    [SerializeField] Animator anim2; // anim for Settings Menu accesible inside Pause M.

    bool settingsClosed = true;

    [HideInInspector]
    public bool canPause = true;

    [Header("TimelineDirector Script")]
    [SerializeField] TimelineDirector[] pauseTimeline;
    [SerializeField] GameObject player;
    PlayerMovement pm;


    #endregion


    // AWAKE /////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
        if (player == null)
            player = GameObject.Find("Mi_Prefab");
        if (pm == null)
            pm = player.GetComponent<PlayerMovement>();
    }

    // UPDATE ////////////////////////////////////////////////////////////////////////////

    void Update()
    {

        bool temp = false;

        if (Input.GetAxis(Keys.buttonsList["Pause"]) > prevAxisValue)
        {
            temp = true;
        }

        prevAxisValue = Input.GetAxis(Keys.buttonsList["Pause"]);

        if ((Input.GetKeyDown(Keys.keysList["Pause"]) || Input.GetButtonDown(Keys.buttonsList["Pause"]) || temp) && canPause && !settingUp)
        {
            if (gamePaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
            
        }
    }

    // PAUSE /////////////////////////////////////////////////////////////////////////////
    public void Pause()
    {
        canPause = false;
        gamePaused = true;

        pauseUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedButton);
        // unity is dumb and need to null the selected button to automatically select an intended button

        for (int i = 0; i < pauseTimeline.Length; i++)
        {
            pauseTimeline[i].TimelinePause();
        }
        pm.enable = false;
        Time.timeScale = 0;

        FindObjectOfType<AudioManager>().PlayOnly("PauseGame");
        StartCoroutine("PauseDelay");
    }

    IEnumerator PauseDelay() {
        yield return new WaitForSecondsRealtime(1.5f);
        canPause = true;
    }

    // RESUME ////////////////////////////////////////////////////////////////////////////
    public void Resume()
    {
        canPause = false;

        FindObjectOfType<AudioManager>().StopSound("PauseGame");
        StartCoroutine("ResumeDelay");
    }


    IEnumerator ResumeDelay()
    {
        if (settingsClosed == false)
            StartCoroutine("ResumeSettings");

        anim.SetTrigger("Out");

        yield return new WaitForSecondsRealtime(1.75f); // delay after resuming the game
        resumeDelayUI.SetActive(false);

        pauseUI.SetActive(false);

        resumeDelayUI.SetActive(false); // "wait" // note: Gabriel

        for (int i = 0; i < pauseTimeline.Length; i++)
        {
            pauseTimeline[i].TimelinePause();
        }

        Time.timeScale = 1;
        pm.enable = true;

        gamePaused = false;
        canPause = true;

        FindObjectOfType<AudioManager>().UnPauseAll();
    }

    
    /*IEnumerator ResumeSettings()
    {
        anim2.SetTrigger("Out");
        yield return new WaitForSecondsRealtime(1f);

        settingsClosed = true;
        settingsMenuUI.SetActive(false);
    }*/

    // OPTIONS ///////////////////////////////////////////////////////////////////////////
    public void Options()
    {
        /*Debug.Log("Options called");

        if (settingsClosed)
        {
            Debug.Log("Era pra abri");
            settingsMenuUI.SetActive(true);
            settingsClosed = !settingsClosed;
        }

        else if (!settingsClosed)
        {
            Debug.Log("Era pra fexa");

            settingsClosed = !settingsClosed;
            StartCoroutine("ResumeSettings");
        }*/
    }

    public void ResumeTime() {
        Time.timeScale = 1;
        FindObjectOfType<AudioManager>().StopSound("PauseGame");
        resumeDelayUI.SetActive(false); // "wait" // note: Gabriel

        for (int i = 0; i < pauseTimeline.Length; i++)
        {
            pauseTimeline[i].TimelinePause();
        }

        gamePaused = false;
        canPause = true;

        FindObjectOfType<AudioManager>().UnPauseAll();
        //settingsMenuUI.SetActive(false);
        resumeDelayUI.SetActive(false);
        pm.enable = true;
    }

    //StateMachineBehaviour.OnStateExit(Animator anim, AnimatorStateInfo normalizedTime, )

}