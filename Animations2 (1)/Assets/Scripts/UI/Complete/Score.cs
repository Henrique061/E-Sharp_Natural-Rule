using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TMP_Text Title;
    string songName;

    string pp;

    [SerializeField] Sound scoreSound;
    [SerializeField] Image[] notes;

    [SerializeField] Color32 secondColor;
    Color firstColor;

    [SerializeField] GameObject player;
    Health health;

    private void Awake()
    {
        songName = Title.text.Replace("\"", "").Replace(" ", "");
        pp = songName + "_" + StaticStore.difficulty.ToUpper();

        if (player == null) {
            player = GameObject.Find("Mi_Prefab");
        }
        health = player.GetComponent<Health>();
        firstColor = notes[2].color;

        if (scoreSound != null)
        {
            scoreSound.source = gameObject.AddComponent<AudioSource>();
            scoreSound.source.clip = scoreSound.clip;
            scoreSound.source.outputAudioMixerGroup = scoreSound.audioMixerGroup;
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey(pp))
            PlayerPrefs.SetInt(pp, 0);
        else
            Debug.Log(PlayerPrefs.GetInt(pp));

        

        if (scoreSound != null)
            scoreSound.source.Play();

        if (health.health <= Mathf.FloorToInt((float)health.totalHealth / 3))
        {
            notes[1].color = secondColor;
            if (PlayerPrefs.GetInt(pp) < 1)
                PlayerPrefs.SetInt(pp, 1);
        }
        else
        {
            notes[1].color = firstColor;
        }

        if (health.health <= Mathf.FloorToInt(((float)health.totalHealth / 3) * 2))
        {
            notes[0].color = secondColor;
            if (PlayerPrefs.GetInt(pp) < 2 && health.health > Mathf.FloorToInt((float)health.totalHealth / 3))
                PlayerPrefs.SetInt(pp, 2);
        }
        else
        {
            notes[0].color = firstColor;
            if (PlayerPrefs.GetInt(pp) < 3)
                PlayerPrefs.SetInt(pp, 3);
        }

        
        Debug.Log(pp + " " + PlayerPrefs.HasKey(pp)+" "+PlayerPrefs.GetInt(pp));
    }
}
