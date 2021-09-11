using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Transparency : MonoBehaviour
{
    [Header("Master Transparency")]
    [SerializeField] bool setInitial;
    [Range(0,1)]
    [SerializeField] float initialTransp = 1;
    [SerializeField] GameObject[] obj;

    [Header("Object Transparency")]
    [Range(0,2)]
    [Tooltip("Sprite, Image or Text")]
    [SerializeField] int SIT = 0;
    [Range(0, 100)] 
    [SerializeField] int transparency = 100;

    SpriteRenderer[] SR;
    Image img;
    [SerializeField] bool fromStart = true;
    [SerializeField] float alpha;

    void Awake()
    {
        if (setInitial == true)
        {
            SR = new SpriteRenderer[obj.Length];

            for (int i = 0; i < obj.Length; i++) {
                SR[i] = obj[i].GetComponent<SpriteRenderer>();
                SR[i].color = new Color(SR[0].color.r, SR[0].color.g, SR[0].color.b, initialTransp);
            }
        }
    }

    private void Start()
    {
        if (setInitial == false)
        {
            if (SIT == 0)
            {
                SR = new SpriteRenderer[1];
                SR[0] = GetComponent<SpriteRenderer>();
                alpha = SR[0].color.a;
            }
            else if (SIT == 1)
            {
                img = GetComponent<Image>();

                if (fromStart)
                    alpha = img.color.a;
            }
        }
    }

    void Update()
    {
        if (setInitial == false)
        {
            if (SIT == 0)
                SR[0].color = new Color(SR[0].color.r, SR[0].color.g, SR[0].color.b, (transparency * alpha) / 100);
            else if (SIT == 1)
                img.color = new Color(img.color.r, img.color.g, img.color.b, (transparency * alpha) / 100);
        }
    }
}
