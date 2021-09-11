using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGravity : MonoBehaviour
{
    #region Physics Variables

    public float gravity;
    float initGravity;

    float distance;

    float forceValue;
    Vector3 forceDirection;

    Rigidbody2D rb;

    #endregion

    #region Plattform Variables

    public float plattformMass;
    public GameObject plattformCenter;

    #endregion

    #region Player Variables
    Vector3 playerInitialSize;

    float playerMass;

    Vector3 lookDirection;
    float lookAngle;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.gravityScale = 0;
        playerMass = rb.mass;

        playerInitialSize = transform.localScale;
        transform.localScale = new Vector3(playerInitialSize.x * 80 / 100, playerInitialSize.y * 80 / 100);
        transform.localPosition = new Vector2(-2.5f, 2.1f);
    }

    void FixedUpdate()
    {
        if (!PauseMenu.gamePaused)
        {
            #region Gravity

            // calculates the distance between de plattform center and the player
            distance = Vector3.Distance(plattformCenter.transform.position, transform.position);

            // calculates force, according to universal gravitational law
            forceValue = gravity * ((plattformMass * playerMass) / (distance * distance));

            // vector of the force, from plattform center to the player
            forceDirection = (transform.position - plattformCenter.transform.position).normalized;
            // normalzed = "removes" the magnitude of the vector, working only with the direction of the vector

            // add the force against the player, according to the direction and the value of the force
            rb.AddForce(forceDirection * forceValue);

            #endregion

            #region Look Angle

            // calculates the vector between the plattform center and the player
            lookDirection = plattformCenter.transform.position - transform.position;

            // set the angle between x and y of the player to the right position
            lookAngle = -(Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg);
            // atan2 = returns the value of the tangent of x and y in radians, whose Tan is x/y
            // Rad2Deg = conversion of radians to degrees (RADian2DEGree)

            // makes the player rotate itself, accordingly to the lookAngle variable
            transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);

            #endregion
        }
    }
}
