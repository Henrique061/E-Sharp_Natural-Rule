using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetVolumeToSlider : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    [Header("Master, BGM or SFX")]
    [Range(1,3)]
    [SerializeField] int MBS;
    Slider slider;

    float master, bgm, sfx;
    private void Start()
    {
        slider = GetComponent<Slider>();

        bool masterV = mixer.GetFloat("masterVol", out master);
        bool bgmV = mixer.GetFloat("bgmVol", out bgm);
        bool sfxV = mixer.GetFloat("sfxVol", out sfx);

        if (MBS == 1)
            slider.value = master;
        else if (MBS == 2)
            slider.value = bgm;
        else if (MBS == 3)
            slider.value = sfx;
    }
}
