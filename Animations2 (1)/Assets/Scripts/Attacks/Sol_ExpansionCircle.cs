using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sol_ExpansionCircle : MonoBehaviour
{
    //variaveis
    [HideInInspector]public float destruct = 2f;
    public Transform secondObj;

    [Tooltip("se secondObj != null somente")]
    public float goal;

    void Start()
    {
        if (secondObj == null)
        {
            Destroy(gameObject, destruct);
        }
        
    }
    private void Update()
    {
        if(secondObj!=null)
        {
            if (secondObj.transform.localScale.x <= goal)
            {
                Destroy(gameObject);
            }
        }
    }
}
