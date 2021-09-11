using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DaltonismChanger : MonoBehaviour
{
    [SerializeField] Toggle daltonism;
    [SerializeField] string album;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(album.ToLower() + "_album"))
        {
            PlayerPrefs.SetInt(album.ToLower() + "_album", 0);
            daltonism.SetIsOnWithoutNotify(false);
        }
        else
        {
            if (PlayerPrefs.GetInt(album.ToLower() + "_album") == 1)
                daltonism.SetIsOnWithoutNotify(true);
            else
                daltonism.SetIsOnWithoutNotify(false);
        }
    }

    public void Daltonism(bool isDaltonic) {
        if (isDaltonic)
            PlayerPrefs.SetInt(album.ToLower() + "_album", 1);
        else
            PlayerPrefs.SetInt(album.ToLower() + "_album", 0);
    }
}
