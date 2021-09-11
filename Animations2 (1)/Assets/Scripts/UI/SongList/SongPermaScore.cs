using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SongPermaScore : MonoBehaviour
{
    [SerializeField] GameObject New;
    [SerializeField] GameObject Score;

    [SerializeField] TMP_Text title;
    string titleName;

    string pp;

    [SerializeField] Image[] scoreImgs;
    [SerializeField] Color emptyColor;
    Color fullColor;

    private void Awake()
    {
        titleName = title.text.Replace(" ", "");

    }

    private void Update()
    {
        SetMode();

        SetScore();

        if(PlayerPrefs.HasKey(pp))
        Debug.Log(pp + " " + PlayerPrefs.GetInt(pp));
    }

    private void SetMode() {
        pp = titleName + "_" + StaticStore.difficulty.ToUpper();

        if (!PlayerPrefs.HasKey(pp) || PlayerPrefs.GetInt(pp) == 0)
        {
            New.SetActive(true);
            Score.SetActive(false);
        }
        else
        {
            New.SetActive(false);
            Score.SetActive(true);

            fullColor = scoreImgs[0].color;
        }
    }
    private void SetScore() {
        if (PlayerPrefs.GetInt(pp) <= 2)
            scoreImgs[2].color = emptyColor;
        else
            scoreImgs[2].color = fullColor;

        if (PlayerPrefs.GetInt(pp) == 1)
            scoreImgs[1].color = emptyColor;
        else
            scoreImgs[1].color = fullColor;
    }
}
