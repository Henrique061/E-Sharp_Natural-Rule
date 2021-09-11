using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SP_Particles_Controller : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particle;
    
    [Header("Main")]
    [SerializeField] float duration = 5f;
    [SerializeField] float startSize = 0.3f;
    [SerializeField] float startSpeed = 20f;
    [SerializeField] bool gravity = false;
    [SerializeField] float lifeTime = 2f;

    [Header("Emission")]
    [SerializeField] int emissionBurstCount = 1;
    [SerializeField] int rateOverTime = 0;
    [SerializeField] int rateOverDistance = 0;

    [Header("Shape")]
    [SerializeField] float shapeArc = 360f;

    [Header("Renderer")]
    [SerializeField] string sortingLayerName = "Default";

    [Header("Other")]
    [SerializeField] bool randomize;
    [SerializeField] Vector2Int randomizeMinMax;
    int randomizeCount;

    [SerializeField] bool shoot;
    [SerializeField] bool forever;
    [SerializeField] int typeOfParticle = 0;
    /*
    0 FOR BE
    1 for BR
    2 for LD
     */
    [SerializeField] int typeOfSprite = 0;
    /*
    0 for Note1
    1 for Note2
    2 for Note3
    3 for Crystal
     */
    [SerializeField] int particlesNum = 5;
    bool check;

    void Awake()
    {
        randomizeCount = randomizeMinMax.x;
    }

    void Update()
    {
        if (forever == false)
        {
            if (shoot == true)
            {
                if (rateOverTime == 0)
                {
                    if (check == false)
                    {
                        Configurate();
                        Shoot();
                        check = true;
                    }
                }
                else
                {
                    Configurate();
                    Shoot();
                    check = false;
                }
            }
            else
            {
                check = false;
            }
        }
        else {
            if (shoot) {
                Configurate();
                Shoot();
            }
        }
        
    }
    public void Configurate() {
        //CONFIGURACOES
        //random sprite
        if (randomize == true)
        {
            if (randomizeCount <= randomizeMinMax.y)
            {
                randomizeCount++;
            }
            if(randomizeCount > randomizeMinMax.y)
            {
                randomizeCount = randomizeMinMax.x;
            }
            typeOfSprite = randomizeCount;
        }

        //main
        var main = particle[(typeOfParticle * particlesNum) + typeOfSprite].main;
        main.startSpeed = new ParticleSystem.MinMaxCurve(startSpeed);
        main.startSize = new ParticleSystem.MinMaxCurve(startSize);
        if (gravity == false)
        {
            main.gravityModifier = new ParticleSystem.MinMaxCurve(0);
        }
        else
        {
            main.gravityModifier = new ParticleSystem.MinMaxCurve(3f);
        }
        main.startLifetime = new ParticleSystem.MinMaxCurve(lifeTime);

        //emission
        var emission = particle[(typeOfParticle * particlesNum) + typeOfSprite].emission;
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(rateOverTime);
        emission.rateOverDistance = new ParticleSystem.MinMaxCurve(rateOverDistance);
        emission.SetBurst(0, new ParticleSystem.Burst(0f, emissionBurstCount));

        //shape
        var shape = particle[(typeOfParticle * particlesNum) + typeOfSprite].shape;
        shape.arc = shapeArc;

        //renderer
        var renderer = particle[(typeOfParticle * particlesNum) + typeOfSprite].GetComponent<ParticleSystemRenderer>();
        renderer.sortingLayerName = sortingLayerName;
    }
    public void Shoot() {
        //CHAMA PARTICULA
        var tempParticle = VFXController.playvfx(particle[(typeOfParticle * particlesNum) + typeOfSprite], transform.position, transform.rotation);
        Destroy(tempParticle.gameObject, duration);
    }
}
