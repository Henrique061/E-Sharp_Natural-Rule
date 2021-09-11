using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Object_Drag : MonoBehaviour
{
    /*
     *Quando o toggle é ativado, a pétala muda sua posição de acordo com seu próprio guismo ao invéz de coordenada de mundo
     *desativado, ela volta para sua posição inicial
     *
     *no pai, ele terá um array de toggles de acordo com o numero de objetos que irão fazer a ação acima
     *se der um true no toggle, ele irá ativar esse script para o objeto em especifico
     */

    //center e initial position
    public Transform center;
    public Transform SP;

    //limites
    [HideInInspector] public Vector3 max;
    [HideInInspector] public Vector3 min;
    [HideInInspector] public float limiter = 2;

    //movimento
    [HideInInspector] public float xmov = 0;
    [HideInInspector] public float ymov = 0;

    //posicao taget
    [HideInInspector] public float xpos = 0;
    [HideInInspector] public float ypos = 0;

    //velocidade com a qual ele se move
    public float speed = 5;

    //fala se é ativado ou não
    public bool drag;

    //prevê em que quadrante o objeto está
    [HideInInspector] public bool xpositivo;
    [HideInInspector] public bool ypositivo;

    void Awake()
    {

        xpos = (center.position.x - transform.position.x)/limiter;
        ypos = (center.position.y - transform.position.y)/limiter;

        xmov = xpos / 5;
        ymov = ypos / 5;


        if (xpos < 0)
        {
            xpositivo = true;
        }
        if (ypos < 0)
        {
            ypositivo = true;
        }

        max = new Vector2(transform.position.x + xpos, transform.position.y + ypos);
        
    }

    void Update()
    {

        if (drag == true)
        {
            Go();
        }
        else if (drag == false)
        {
            Back();
        }
    }

    public void Go() {
        //faz o objeto ir em direção ao seu centro
        if (
            (xpositivo == true && ypositivo == true && transform.position.x > max.x && transform.position.y > max.y)
            ||
            (xpositivo == true && ypositivo == false && transform.position.x > max.x && transform.position.y < max.y)
            ||
            (xpositivo == false && ypositivo == true && transform.position.x < max.x && transform.position.y > max.y)
            ||
            (xpositivo == false && ypositivo == false && transform.position.x < max.x && transform.position.y < max.y)
            )
        {
            transform.Translate(new Vector2(xmov, ymov) * (Time.deltaTime * speed), Space.World);
        }
    }

    public void Back() {
        //faz o objeto voltar à sua posição inicial
        if (
            (xpositivo == true && ypositivo == true && transform.position.x < min.x && transform.position.y < min.y)
            ||
            (xpositivo == true && ypositivo == false && transform.position.x < min.x && transform.position.y > min.y)
            ||
            (xpositivo == false && ypositivo == true && transform.position.x > min.x && transform.position.y < min.y)
            ||
            (xpositivo == false && ypositivo == false && transform.position.x > min.x && transform.position.y > min.y)
            )
        {
            transform.Translate(new Vector2(-xmov, -ymov) * (Time.deltaTime * speed), Space.World);
        }
        else if (
            (xpositivo == true && ypositivo == true && transform.position.x >= min.x && transform.position.y >= min.y)
            ||
            (xpositivo == true && ypositivo == false && transform.position.x >= min.x && transform.position.y <= min.y)
            ||
            (xpositivo == false && ypositivo == true && transform.position.x <= min.x && transform.position.y >= min.y)
            ||
            (xpositivo == false && ypositivo == false && transform.position.x <= min.x && transform.position.y <= min.y)
            )
        {
            transform.position = new Vector2(SP.transform.position.x, SP.transform.position.y);
            //transform.position = min;
        }
    }

   
}
