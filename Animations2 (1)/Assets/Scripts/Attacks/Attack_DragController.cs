using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_DragController : MonoBehaviour
{
    [Header("Objects and Pivots")]
    [SerializeField] GameObject[] objects;
    [SerializeField] GameObject[] petalsPivots;
    Attack_Object_Drag[] atk;

    [Header("Moves individually")]
    [Tooltip("Ativa e desativa movimento")]
    [SerializeField] bool moveGoBack;

    [Space(9)]

    [Tooltip("Escolha o numero do objeto no final de seu nome")]
    [SerializeField] int checkNum1;
    [Tooltip("Escolha se vai para frente (true) ou para trás (false)")]
    [SerializeField] bool checkGoBack1;

    [Space(9)]

    [Tooltip("Escolha o numero do objeto no final de seu nome")]
    [SerializeField] int checkNum2;
    [Tooltip("Escolha se vai para frente (true) ou para trás (false)")]
    [SerializeField] bool checkGoBack2;

    [Space(9)]

    [Tooltip("Escolha o numero do objeto no final de seu nome")]
    [SerializeField] int checkNum3;
    [Tooltip("Escolha se vai para frente (true) ou para trás (false)")]
    [SerializeField] bool checkGoBack3;

    [Space(9)]

    [Tooltip("Escolha o numero do objeto no final de seu nome")]
    [SerializeField] int checkNum4;
    [Tooltip("Escolha se vai para frente (true) ou para trás (false)")]
    [SerializeField] bool checkGoBack4;

    [Header("Move odd numbered objects")]
    [SerializeField] bool move1;
    [Tooltip("Escolha se vai para frente (true) ou para trás (false)")]
    [SerializeField] bool checksome1;

    [Header("Move even numbered objects")]
    [SerializeField] bool move2;
    [Tooltip("Escolha se vai para frente (true) ou para trás (false)")]
    [SerializeField] bool checksome2;

    [Header("Limiter")]
    [SerializeField] float limiter = 7.5f;


    void Awake()
    {
        atk = new Attack_Object_Drag[objects.Length];
        for (int i = 0; i < objects.Length; i++) {
            petalsPivots[i].transform.position = new Vector3(objects[i].transform.position.x, objects[i].transform.position.y, 0);
            atk[i] = objects[i].GetComponent<Attack_Object_Drag>();
            atk[i].SP = petalsPivots[i].transform;
        }

    }

    private void Update()
    {
        Atualize();

        Move();
    }

    public void Atualize() {
        //atualiza os objetos a ficarem com a mesma posição do spawnpoint
        float xpos, ypos;
        for (int i = 0; i < objects.Length; i++)
        {
            //atualiza o minimo do obj
            atk[i].min.x = petalsPivots[i].transform.position.x;
            atk[i].min.y = petalsPivots[i].transform.position.y;

            //atualiza o maximo do obj
            xpos = (atk[i].center.position.x - petalsPivots[i].transform.position.x) / objects[i].GetComponent<Attack_Object_Drag>().limiter;
            atk[i].xpos = xpos;
            ypos = (atk[i].center.position.y - petalsPivots[i].transform.position.y) / objects[i].GetComponent<Attack_Object_Drag>().limiter;
            atk[i].ypos = ypos;

            atk[i].xmov =
                atk[i].xpos / 2;

            atk[i].ymov =
                atk[i].ypos / 2;

            if (xpos < 0)
            {
                atk[i].xpositivo = true;
            }
            else if (xpos > 0)
            {
                atk[i].xpositivo = false;
            }

            if (ypos < 0)
            {
                atk[i].ypositivo = true;
            }
            else if (ypos > 0)
            {
                atk[i].ypositivo = false;
            }


            objects[i].GetComponent<Attack_Object_Drag>().max = new Vector3(
                    petalsPivots[i].transform.position.x + atk[i].xpos,
                    petalsPivots[i].transform.position.y + atk[i].ypos,
                    0
                    );
        }
    }
    public void Move() {
        //move objetos específicos
        if (moveGoBack == true)
        {
            if (checkNum1 > 0 && checkNum1 <= objects.Length)
            {
                atk[checkNum1 - 1].limiter = limiter;
                if (checkGoBack1 == true)
                {
                    atk[checkNum1 - 1].drag = true;

                }
                else
                {
                    atk[checkNum1 - 1].drag = false;
                }
            }

            if (checkNum2 > 0 && checkNum2 <= objects.Length)
            {
                atk[checkNum2 - 1].limiter = limiter;

                if (checkGoBack2 == true)
                {
                    atk[checkNum2 - 1].drag = true;
                }
                else
                {
                    atk[checkNum2 - 1].drag = false;
                }
            }

            if (checkNum3 > 0 && checkNum3 <= objects.Length)
            {
                atk[checkNum3 - 1].limiter = limiter;

                if (checkGoBack2 == true)
                {
                    atk[checkNum3 - 1].drag = true;
                }
                else
                {
                    atk[checkNum3 - 1].drag = false;
                }
            }

            if (checkNum4 > 0 && checkNum4 <= objects.Length)
            {
                atk[checkNum4 - 1].limiter = limiter;

                if (checkGoBack2 == true)
                {
                    atk[checkNum4 - 1].drag = true;
                }
                else
                {
                    atk[checkNum4 - 1].drag = false;
                }
            }
        }

        //move objetos de número ímpar (index número par)
        if (move1 == true)
        {
            for (int i = 0; i < objects.Length; i = i + 2)
            {
                atk[i].limiter = limiter;

                if (checksome1 == true)
                {
                    atk[i].drag = true;
                }
                else
                {
                    atk[i].drag = false;
                }
            }
        }

        //move objetos de número par (index número ímpar)
        if (move2 == true)
        {
            for (int i = 1; i < objects.Length; i = i + 2)
            {
                atk[i].limiter = limiter;

                if (checksome2 == true)
                {
                    atk[i].drag = true;
                }
                else
                {
                    atk[i].drag = false;
                }
            }
        }
    }
}
