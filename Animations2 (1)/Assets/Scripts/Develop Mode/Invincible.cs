using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour
{
    public void DamageImunity(string confirm)
    {
        var gameObject = GameObject.Find("Mi_Prefab");

        if (confirm == "yes")
            gameObject.GetComponent<Health>().enabled = false;

        else
            gameObject.GetComponent<Health>().enabled = true;
    }
}
