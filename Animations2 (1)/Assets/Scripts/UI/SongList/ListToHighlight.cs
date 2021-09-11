using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ListToHighlight : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI SongText, ComposerText;

    private void Awake()
    {
        SongText.text = "Select song";
        ComposerText.text = "to play";
    }

    public void changeName(TMP_Text song) {
        SongText.text = song.text;
    }

    public void changeComposer(string composer) {
        ComposerText.text = composer;
    }
}
