using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Particles : MonoBehaviour
{
    ParticleSystem ps;

    GameObject player;
    PlayerMovement pm;
    PlayerCollision pc;

    private void Awake()
    {
        if (player == null) {
            player = GameObject.Find("Mi_Prefab");
            pm = player.GetComponent<PlayerMovement>();
            pc = player.GetComponent<PlayerCollision>();
        }

        ps = this.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        Config();
    }

    private void Config(){

        if (pm.invincible || pc.invunerable)
        {
            if (this.tag != "Health")
            {
                var collider = ps.collision;
                collider.enabled = false;
                //Debug.Log("Invincible");
            }
        }
        else {
            var collider = ps.collision;
            collider.enabled = true;
        }
    }
}
