using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderSound : MonoBehaviour
{
    public AudioSource sound;
    bool hold;
    private void Update()
    {
        if (hold == true)
            sound.Play();
    }
}
