using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablingSettings : MonoBehaviour
{
    int counter = 0;

    void Start()
    {
        counter++;
        if (counter == 1) {
            gameObject.SetActive(false);
        }
    }
}
