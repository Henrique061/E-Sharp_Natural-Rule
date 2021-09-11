using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDifficulty : MonoBehaviour
{

    private void Awake()
    {
        this.GetComponent<TextMeshProUGUI>().text = StaticStore.difficulty.ToUpper()+" MODE";
    }
}
