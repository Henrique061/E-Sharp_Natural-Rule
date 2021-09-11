using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffects : MonoBehaviour
{

    [SerializeField] GameObject postFX;

    public void SwitchPostFX(bool isOpened)
    {
        postFX.SetActive(!isOpened);
    }
}
