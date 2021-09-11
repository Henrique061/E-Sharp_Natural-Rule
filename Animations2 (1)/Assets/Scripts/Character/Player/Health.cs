using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Health : MonoBehaviour
{

    public int health; // hearts atual
    [HideInInspector] public int totalHealth; // total

    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    [Header("For game over")]
    [SerializeField] GameObject gameOverObj;
    Animator gameOverAnim;
    [SerializeField] PauseMenu pauseMenu;
    [Tooltip("The button that will be automatically selected when game over screen appears")]
    [SerializeField] GameObject selectedGameOver;

    [SerializeField] TimelineDirector timeline;

    [SerializeField] GameObject hitbox;
    Animator anim;
    [SerializeField] AnimationClip deathAnim;
    float deathAnimLength;
    Rigidbody2D rgbd;
    PlayerMovement pm;

    [HideInInspector] public bool dead;

    float count = 1;
    float count2 = 1;
    float targetCount;
    float targetCount2 = 1;
    bool showedGameOverMenu; // // to get selected the button only one time when score menu appears

    private void Awake()
    {
        if (StaticStore.difficulty == "hard") 
        {
            health = Mathf.FloorToInt((float)hearts.Length/2);
            for (int i = hearts.Length-1; i >= health; i--) {
                Destroy(hearts[i]);
            }
        }
        else if (StaticStore.difficulty == "easy") 
        {
            health = hearts.Length;
            for (int i = hearts.Length-1; i >= health; i--)
            {
                Destroy(hearts[i]);
            }
        }

        totalHealth = health;
        if (gameOverObj != null) {
            gameOverAnim = gameOverObj.GetComponent<Animator>();
            anim = GetComponent<Animator>();
            deathAnimLength = deathAnim.length + 1;
            targetCount = deathAnimLength;
            rgbd = GetComponent<Rigidbody2D>();
            pm = GetComponent<PlayerMovement>();
        }

        showedGameOverMenu = false;
    }

    private void Update()
    {
        LoseHealth();

        if (gameOverObj != null && health == 0) {
            Death();
        }
    }

    private void LoseHealth (){
        if (health > totalHealth)
        {
            health = totalHealth;
        }

        for (int i = 0; i < totalHealth; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < totalHealth)
            {
                hearts[i].enabled = true;
            }
            else
                hearts[i].enabled = false;
        }
    }
    private void Death()
    {
        dead = true;
        gameOverObj.SetActive(true);
        if (!showedGameOverMenu)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(selectedGameOver);
            showedGameOverMenu = true;
        }

        anim.SetBool("Death", true);
        rgbd.simulated = false;
        hitbox.SetActive(false);
        pm.enabled = false;
        pauseMenu.enabled = false;


        if (targetCount > 0) {
            count -= Time.deltaTime;
            targetCount = count / (deathAnimLength+20);
        }
        else
        {
            timeline.TimelineGameOver();
            if (targetCount2 > 0)
            {
                count2 -= Time.deltaTime;
                targetCount2 = count2 / deathAnimLength;
            }
            else
            {
                gameOverAnim.SetTrigger("gameOver");
            }
        } 

    }
}
