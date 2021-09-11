using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateTime : MonoBehaviour
{
    public void TimeAcceleration(string time)
    {
        switch (time)
        {
            case "0.5x":
                Time.timeScale = 0.5f;
                break;

            case "1x":
                Time.timeScale = 1;
                break;

            case "2x":
                Time.timeScale = 2;
                break;

            case "5x":
                Time.timeScale = 5;
                break;

            case "8x":
                Time.timeScale = 8;
                break;

            case "10x":
                Time.timeScale = 10;
                break;

            default:
                return;
        }
    }
}
