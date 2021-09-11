using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sol_Ribbon_Follow : MonoBehaviour
{
    [SerializeField] Transform FollowParent;
    [Tooltip("se follow = true, as fitas vão seguir Sol")]
    [SerializeField] bool follow;
    bool xpositivo, ypositivo;
    [SerializeField] float speed = 1;
    float xdis, ydis;
    Vector2 pos;
    Vector3 limit;

    void Awake()
    {
        limit = new Vector3(
            transform.position.x - FollowParent.position.x,
            transform.position.y -  FollowParent.position.y,
            0
            );
    }
    void Update()
    {   if (this.tag == "Ribbon") {
            pos = new Vector2(FollowParent.position.x + limit.x, FollowParent.position.y + limit.y);
            xdis = FollowParent.position.x - transform.position.x;
            ydis = FollowParent.position.y - transform.position.y ;

            if (xdis < 0) xpositivo = true;
            else xpositivo = false;

            if (ydis < 0) ypositivo = true;
            else ypositivo = false;
        }

        if (follow == true && this.tag != "Ribbon") {
            Follow();
        }
        else if (follow == true && this.tag == "Ribbon") {
            FollowRibbon();
        }
    }

    public void Follow()
    {
        //faz objeto seguir Sol
        transform.position = new Vector3(FollowParent.position.x + limit.x, FollowParent.position.y + limit.y, FollowParent.position.z + limit.z);
    }

    public void FollowRibbon() {
        if (
            (xpositivo == true && ypositivo == true && transform.position.x >= pos.x && transform.position.y >= pos.y)
            ||
            (xpositivo == true && ypositivo == false && transform.position.x >= pos.x && transform.position.y <= pos.y)
            ||
            (xpositivo == false && ypositivo == true && transform.position.x <= pos.x && transform.position.y >= pos.y)
            ||
            (xpositivo == false && ypositivo == false && transform.position.x <= pos.x && transform.position.y <= pos.y)
            )
        {
            //transform.position = Vector2.MoveTowards(transform.position,pos, Time.deltaTime * speed);
            transform.Translate(new Vector2(xdis, ydis) * (Time.deltaTime * speed), Space.World);
            //transform.position = new Vector2(pos.x, pos.y);
        }
    }
}
