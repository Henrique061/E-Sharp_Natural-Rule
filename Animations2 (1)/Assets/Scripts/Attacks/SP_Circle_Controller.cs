using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Circle_Controller : MonoBehaviour
{
    //objeto
    [SerializeField] GameObject obj;
    ConstantMovement move;
    Sol_ExpansionCircle expansion;

    //condição
    [SerializeField] bool pulse = false;
    [SerializeField] float speed = 6.9f;
    public Vector3 Axis = new Vector3(0.1f, 0.1f, 0);
    [SerializeField] float expandDifference = -0.002f;
    [SerializeField] float autoDestruct = 2f;

    //evita que o mesmo circulo seja chamado duas vezes.
    //só pode ser chamado de novo depois que pulse se tornar false novamente
    int check = 0;

    private void Awake()
    {
        move = obj.GetComponent<ConstantMovement>();

        expansion = obj.GetComponent<Sol_ExpansionCircle>();
    }

    void Update()
    {
        move.speed = speed;
        if (pulse == true)
        {
            check++;
            Pulse();
        }
        else {
            check = 0;
        }
    }

    public void Pulse() {
        if (check == 1) {
            if(Axis.x != 0 && Axis.y != 0)
            {
                move.ScaleAxis = Axis;
                move.expandDifference = expandDifference;
                expansion.destruct = autoDestruct;
            }
            
            Instantiate(obj, transform.position, Quaternion.identity);
        }
    } 
}
