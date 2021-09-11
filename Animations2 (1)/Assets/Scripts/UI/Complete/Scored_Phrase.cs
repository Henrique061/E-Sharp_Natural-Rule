using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scored_Phrase : MonoBehaviour
{
    [SerializeField] string[] phrase;
    [SerializeField] GameObject player;
    Health health;
    int score = 2;
    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        if (player == null) player = GameObject.Find("Mi_Prefab");
        health = player.GetComponent<Health>();
    }
    private void Update()
    {
        if (health.health >= 4) score = 2;
        else if (health.health >= 2 && health.health <= 3) score = 1;
        else score = 0;

        text.text = phrase[score];
    }

}
