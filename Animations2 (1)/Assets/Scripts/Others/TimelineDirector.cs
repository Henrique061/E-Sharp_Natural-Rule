using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimelineDirector : MonoBehaviour
{
    [SerializeField] PlayableDirector timeline;

    bool pause = false;

    float count = 1;
    float targetCount = 1;

    [Header("For game over")]
    [SerializeField] bool hasGameOver = true;
    [SerializeField] AnimationClip deathAnim;
    float deathAnimLength;

    [Header("For game win")]
    [SerializeField] GameObject ScoreScreen;
    [SerializeField] bool score;
    [SerializeField] PlayerMovement pm;

    [Header("Post Effect")]
    [SerializeField] GameObject postEffect;
    [SerializeField] GameObject colorblind;
    [SerializeField] string album;

    [Header("Buttons")]
    [SerializeField] GameObject selectedScoreMenu;

    PauseMenu gamePaused;
    bool showedMenu; // to get selected the button only one time when score menu appears


    // Start is called before the first frame update
    void Awake()
    {
        gamePaused = GameObject.Find("Pause_Prefab").GetComponent<PauseMenu>();

        if(timeline == null)
        timeline = GetComponent<PlayableDirector>();

        if(hasGameOver)
            deathAnimLength = deathAnim.length + 1;
        if (pm == null)
        {
            GameObject.Find("Mi_Prefab").GetComponent<PlayerMovement>();
        }

        if (postEffect == null)
            postEffect = GameObject.Find("PostEffects");

        gamePaused.canPause = true;
        showedMenu = false;
    }

    private void Update()
    {
        if (!timeline.playableGraph.IsValid())
        {
            timeline.RebuildGraph();
        }

        if (score) TimelineWin();

        if (PlayerPrefs.GetInt("camfx") == 1)
            postEffect.SetActive(true);
        else
            postEffect.SetActive(false);

        if (colorblind != null && PlayerPrefs.HasKey(album.ToLower() + "_album"))
        {
                if (PlayerPrefs.GetInt(album.ToLower() + "_album") == 1)
                    colorblind.SetActive(true);
                else
                    colorblind.SetActive(false);
        }
    }

    public void TimelinePlay() {
        timeline.Play();
    }
    public void TimelineStop() {
        timeline.time = 0;
        timeline.Stop();
        timeline.Evaluate();
    }
    public void TimelinePause()
    {
        if (pause == false)
        {
            Time.timeScale = 0;
            timeline.playableGraph.GetRootPlayable(0).SetSpeed(0);
            pause = true;
        }
        else 
        {
            Time.timeScale = 1;
            timeline.playableGraph.GetRootPlayable(0).SetSpeed(1);
            pause = false;
        }
    }
    public void TimelineGameOver() {
        if (targetCount > 0)
        {
            count -= Time.deltaTime;
            targetCount = count / (deathAnimLength * 2);
        }
        if (!timeline.playableGraph.IsValid())
        {
            Debug.Log("Rebuilding");
            timeline.RebuildGraph();
        }
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(targetCount);
    }

    void TimelineWin() {
        gamePaused.canPause = false;
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(0);
        ScoreScreen.SetActive(true);

        if (!showedMenu)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(selectedScoreMenu);
            showedMenu = true;
        }
        pm.enable = false;
    }
}
