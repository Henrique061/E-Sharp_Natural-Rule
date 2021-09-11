using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    #region Variables
    PlayerMovement pm;
    [SerializeField] bool tutorial;
    [SerializeField] float colliTime = 0.3f;

    [SerializeField] Material damageWhite; // makes the flash in white
    private Material damageDefault; // makes the player return to default sprite

    SpriteRenderer[] childrenSR;
    SpriteRenderer sr;

    Health healthPoints;

    [HideInInspector]
    public bool invunerable = false;

    public CameraShake cameraShake;

    // Exclusive variables for Save Menu
    public GameObject buttons;
    bool slotSelected = false;

    public Animator saveScreenAnim;

    [SerializeField] float invencibleTime = 0.4f;
    [SerializeField] int numberOfFlashes = 3;

    [SerializeField] Sound healthy;
    [SerializeField] Sound damage;

    #endregion

    private void Awake()
    {
        healthy.source = gameObject.AddComponent<AudioSource>();
        healthy.source.clip = healthy.clip;
        healthy.source.outputAudioMixerGroup = healthy.audioMixerGroup;

        damage.source = gameObject.AddComponent<AudioSource>();
        damage.source.clip = damage.clip;
        damage.source.outputAudioMixerGroup = damage.audioMixerGroup;
    }

    private void Update()
    {
        if (slotSelected && Input.GetButtonDown("Pause"))
        {
            //saveScreenAnim.SetTrigger("Exit");
            StartCoroutine("Sagaseta");


            buttons.SetActive(false);
            slotSelected = !slotSelected;
        }
    }

    private void Start()
    {
        pm = gameObject.GetComponent<PlayerMovement>();
        childrenSR = GetComponentsInChildren<SpriteRenderer>(); // get material of all children
        sr = GetComponent<SpriteRenderer>(); // to set the default material

        //damageWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        damageDefault = sr.material;

        healthPoints = GetComponent<Health>(); // to get the variables from PlayerHealth Script
    }

    #region Collisions Detection

    private void OnTriggerEnter2D(Collider2D collision) // detect collision with triggers
    {
        CollisionProcess(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CollisionProcess(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) // detect collision with colliders
    {
        CollisionProcess(collision.gameObject);
    }

    void OnParticleCollision(GameObject other)
    {
        CollisionProcess(other);
    }

    #endregion

    void CollisionProcess(GameObject collider) // call the collision functions
    {
        if (collider.CompareTag("Damage") || collider.CompareTag("Ribbon"))
            DamagePlayer();

        if (collider.CompareTag("Slots"))
            SaveCollision();

        if (collider.CompareTag("Health"))
            GainHealth();
    }

    #region Damage

    void DamagePlayer()
    {
        if(!pm.invincible && !healthPoints.dead){
            damage.source.Play();

            if (!tutorial)
            {
                healthPoints.health--;
            }

            StartCoroutine("DamageInvunerable"); // to make player invunerable for a period time

            pm.invincibleTime = colliTime;
        }
        
        pm.invincible = true;
    }

    IEnumerator DamageInvunerable()
    {
        invunerable = true; // to declare for player not be invencible again while dashing
        Physics2D.IgnoreLayerCollision(8, 0, true);
            
        StartCoroutine(cameraShake.ShakeCamera(.1f, .2f));

        StartCoroutine("FlashMaterial"); // to make the flash effect while invunerable

        yield return new WaitForSeconds(invencibleTime); // the time that player will be invencible

        Physics2D.IgnoreLayerCollision(8, 0, false);

        invunerable = false; // to declare for player be invencible while dashing
    }

    IEnumerator FlashMaterial()
    {
        for (int i = 0; i < numberOfFlashes; i++) // loop to make flash i times
        {
            foreach (SpriteRenderer flash in childrenSR) // loop to change material of children
                flash.material = damageWhite;

            yield return new WaitForSeconds(invencibleTime / (numberOfFlashes * 2)); // wait time for change to white to default

            foreach (SpriteRenderer flash in childrenSR)// loop to change material of children
                flash.material = damageDefault;

            yield return new WaitForSeconds(invencibleTime / (numberOfFlashes * 2)); // wait time for change to deafult to white, or close de loop
        }
    }

    #endregion

    #region Save

    void SaveCollision() // call the collision functions
    {
        
        if (!slotSelected)
        {
            Debug.Log("to ali");
            buttons.SetActive(true);
            slotSelected = !slotSelected;
        }
        
    }

    IEnumerator Sagaseta()
    {
        saveScreenAnim.SetTrigger("Exit");
        yield return new WaitForSecondsRealtime(1.75f); // delay after resuming the game
        //buttons.SetActive(false);
    }
    #endregion

    #region Health
    void GainHealth() {

        healthy.source.Play();

        if (healthPoints.health < healthPoints.totalHealth) {
            healthPoints.health++;
        }
    }
    #endregion
}