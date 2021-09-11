using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteprefs : MonoBehaviour
{
    public void deletePrefs() {
        PlayerPrefs.DeleteAll();
    }
}
