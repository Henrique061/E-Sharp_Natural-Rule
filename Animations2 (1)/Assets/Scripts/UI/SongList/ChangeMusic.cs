using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeMusic : MonoBehaviour
{
    [SerializeField] AudioSource sfx;
    [SerializeField] AudioSource difficulty;

    void changeMusic(AudioSource music) {
    }

    public void changeSound() {
        sfx.Play();
    }

    public void difficultyChange() {
        difficulty.Play();
    }
}
