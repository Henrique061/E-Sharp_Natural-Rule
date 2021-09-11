using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    #region Physics Variables

    [Header("Physics Parameters")]

    public static Rigidbody2D rb;
    public PolygonCollider2D playerCollider;

    [Space(7)]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    private Vector3 velocity = Vector3.zero;

    #endregion

    #region Walk Variables

    [Header("Walk Parameters")]

    public float walkSpeed = 2.5f;

    float horizontalInput = 0f;
    bool canWalk = true;

    #endregion

    #region Jump Variables

    [Header("Jump Parameters")]

    [SerializeField] float jumpForce = 23;

    private bool canJump = false;
    [SerializeField] float jumpDuration = 0.02f;
    [SerializeField] float jumpRate = 0.65f;

    float jumpCooldown = 0f;
    private bool grounded;
    const float groundedRadius = .2f;
    float prevAxisValueJump = 0;

    #endregion

    #region Dash Variables

    [Header("Dash Parameters")]

    [Tooltip("Cooldown to dash again after performing a dash")]
    [SerializeField] float dashRate = 0.5f;

    [Tooltip("Total duration of the dash that will be active")]
    [SerializeField] float dashDuration = 0.07f;

    [Tooltip("Speed of the player on dash")]
    [SerializeField] float dashSpeed = 60f;

    [Tooltip("The layer that player will pass through when dashing")]
    [SerializeField] int dashThroughLayer = 0;

    [Tooltip("The time player can turn yourself after activating the dash")]
    [SerializeField] float dashTurnDelay = 0.01f;

    [HideInInspector]
    public bool facingRight = true;

    bool playDashSound = true;
    bool playDashAnim = true;
    bool canDash = true;
    bool isDashing = false;
    float dashCooldown = 0f;
    float dashTimer = 0f;
    float prevAxisValueDash = 0;

    PlayerCollision alreadyInvunerable; // to verify if player is already invencible, due to damage collision

    #endregion

    #region Crouch Variables

    private bool canCrouch = true;
    float crouchInput = 0f;

    bool isCrouched = false;

    #endregion

    #region Trail Variables

    private TrailRenderer[] dashTrails;

    #endregion

    #region Animation Variables

    [Header("Animation")]

    public Animator animator;

    #endregion

    #region Others Variables

    UnityEvent onLandEvent;
    [HideInInspector] public bool invincible;
    [HideInInspector] public float invincibleTime = 0.2f;

    [SerializeField] bool jump = true, dash = true, lower = true;

    [HideInInspector] public bool enable = true;

    #endregion

    private void Awake()
    {
        if (!Keys.keysList.ContainsKey("Jump")) {
            Keys.keysList["Jump"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpK"));
            Keys.keysList["Dash"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DashK"));
            Keys.keysList["Crouch"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CrouchK"));
            Keys.keysList["Pause"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PauseK"));

            Keys.buttonsList["Jump"] = PlayerPrefs.GetString("JumpB");
            Keys.buttonsList["Dash"] = PlayerPrefs.GetString("DashB");
            Keys.buttonsList["Crouch"] = PlayerPrefs.GetString("CrouchB");
            Keys.buttonsList["Pause"] = PlayerPrefs.GetString("PauseB");
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        alreadyInvunerable = GetComponent<PlayerCollision>();

        dashTrails = GetComponentsInChildren<TrailRenderer>();

        if (onLandEvent == null)
            onLandEvent = new UnityEvent();
    }

    // UPDATE ////////////////////////////////////////////////////////////////////////////
    private void Update() // FOR DETECT INPUTS
    {
        if (enable) {
            if (invincible)
            {
                Invincible();
            }
            if (!grounded) invincible = true;

            if (!PauseMenu.gamePaused) // works only if the game is not paused
            {
                // Walk button down
                horizontalInput = Input.GetAxisRaw("Horizontal");
                Move();

                // Jump button down
                bool jump = false;
                if (Input.GetAxis(Keys.buttonsList["Jump"]) > prevAxisValueJump)
                {
                    jump = true;
                }
                prevAxisValueJump = Input.GetAxis(Keys.buttonsList["Jump"]);

                if ((Input.GetKeyDown(Keys.keysList["Jump"]) || Input.GetButtonDown(Keys.buttonsList["Jump"]) || jump) && grounded && jumpCooldown <= 0f)
                    canJump = true;

                if (jumpCooldown >= 0f)
                    jumpCooldown -= Time.deltaTime;

                // Dash button down
                bool dashh = false;
                if (Input.GetAxis(Keys.buttonsList["Dash"]) > prevAxisValueDash)
                {
                    dashh = true;
                }
                prevAxisValueDash = Input.GetAxis(Keys.buttonsList["Dash"]);

                if (!isDashing && dashCooldown <= 0f && dash && grounded)
                    if ((Input.GetKeyDown(Keys.keysList["Dash"]) || Input.GetButtonDown(Keys.buttonsList["Dash"]) || dashh) && canDash)
                    {
                        isDashing = true;
                        dashCooldown = dashRate;
                    }

                if (isDashing)
                {
                    Dash();
                }

                if (dashCooldown >= 0f)
                    dashCooldown -= Time.deltaTime;

                // Crouch button down

                if (Input.GetKey(Keys.keysList["Crouch"]) || Input.GetAxis(Keys.buttonsList["Crouch"]) > 0.2f)
                    crouchInput = 1;
                else
                    crouchInput = 0;
            }
        }
        

    }

    // FIXED UPDATE //////////////////////////////////////////////////////////////////////
    private void FixedUpdate() // FOR INPUT COMMANDS
    {
        if (enable) {
            Grounded();

            if (jump)
                Jump();
            else canJump = false;

            if (lower)
                Crouch();
            else canCrouch = false;
        }
    }

    // GROUND CHECK //////////////////////////////////////////////////////////////////////
    void Grounded()
    {
        bool wasGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;

                if (!wasGrounded)
                    onLandEvent.Invoke();
            }
        }
    }

    // MOVE //////////////////////////////////////////////////////////////////////////////
    void Move()
    {
        if (isCrouched)
            return;

        if (canWalk)
        {
            animator.SetFloat("Run", Mathf.Abs(horizontalInput));

            // Left
            if (horizontalInput < 0)
            {
                if (facingRight)
                    Flip();

                transform.Translate(new Vector2(-walkSpeed, 0) * (Time.deltaTime * 5), Space.Self);
                //rb.velocity = new Vector2(rb.transform.right.x * -5, rb.velocity.y);
            }

            // Right
            else if (horizontalInput > 0)
            {
                if (!facingRight)
                    Flip();

                transform.Translate(new Vector2(walkSpeed, 0) * (Time.deltaTime * 5), Space.Self);
                //rb.velocity = new Vector2(rb.transform.right.x * 5, rb.velocity.y);
            }

            // stand
            //else
            //    rb.velocity = new Vector2(0, rb.velocity.y);
        }

    }

    void Flip() // to flip the player after walking to opposite direction
    {
        facingRight = !facingRight;

        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;

    }

    // JUMP //////////////////////////////////////////////////////////////////////////////
    void Jump()
    {
        if (canJump && crouchInput <= 0)
        {
            invincibleTime = jumpDuration;
            //FindObjectOfType<AudioManager>().PlaySound("PlayerJump");

            animator.SetTrigger("Jump");

            rb.velocity = new Vector2(rb.transform.up.x, rb.transform.up.y) * jumpForce;

            if (!alreadyInvunerable.invunerable)
                StartCoroutine("DashInvunerable");

            jumpCooldown = jumpRate;

        }

        canJump = false;


    }

    // DASH //////////////////////////////////////////////////////////////////////////////
    void Dash()
    {
        if (dashTimer >= dashDuration)
        {
            rb.velocity = Vector2.zero;
            foreach (TrailRenderer trail in dashTrails)
                trail.emitting = false;

            playDashSound = true;
            playDashAnim = true;
            isDashing = false;
            canWalk = true;
            canCrouch = true;
            dashTimer = 0f;

        }

        else
        {
            if (playDashSound)
            {
                //FindObjectOfType<AudioManager>().PlaySound("PlayerDash");
                playDashSound = false;
            }

            if (playDashAnim)
            {
                animator.SetTrigger("Dash");
                playDashAnim = false;
            }

            if (!alreadyInvunerable.invunerable)
                StartCoroutine("DashInvunerable");

            if (!invincible)
            {

                invincibleTime = dashDuration + 0.1f;

                invincible = true;
            }

            DashTrail();

            if (dashTimer >= dashTurnDelay)
                canWalk = false;

            canJump = false;
            canCrouch = false;

            dashTimer += Time.deltaTime;

            if (facingRight)
                rb.velocity = new Vector2(rb.transform.right.x, rb.transform.right.y) * dashSpeed;

            else
                rb.velocity = new Vector2(rb.transform.right.x, rb.transform.right.y) * -dashSpeed;

        }
    }

    void DashTrail()
    {
        foreach (TrailRenderer trail in dashTrails)
            trail.emitting = true;

    }

    void Invincible()
    {
        invincibleTime -= Time.deltaTime;
        if (invincibleTime <= 0)
        {
            invincible = false;
        }
    }

    IEnumerator DashInvunerable()
    {
        Physics2D.IgnoreLayerCollision(8, dashThroughLayer, true);
        // 8 = Player layer ... dashThroughLayer = In inspector ... True = Enabled

        /*if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
        {*/
        yield return new WaitForSeconds(dashDuration);
        /*}
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) {

            yield return new WaitForSeconds(dashDuration);
        }*/

        Physics2D.IgnoreLayerCollision(8, dashThroughLayer, false);
        // 8 = Player layer ... dashThroughLayer = In inspector ... False = Disabled
    }

    // CROUCH ////////////////////////////////////////////////////////////////////////////
    void Crouch()
    {
        if (canCrouch && grounded && crouchInput > 0)
        {
            isCrouched = true;
            canDash = false;

            animator.SetBool("Lower", true);
        }

        else
        {
            isCrouched = false;
            canDash = true;

            animator.SetBool("Lower", false);
        }

        canCrouch = true;
    }

}
