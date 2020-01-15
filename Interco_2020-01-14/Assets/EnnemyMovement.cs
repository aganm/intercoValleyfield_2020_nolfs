using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyMovement : MonoBehaviour
{
    public float walkSpeed;      // Walkspeed
    public float maxLeft = 0.0f;       // Define maxLeft
    public float maxRight = 5.0f;      // Define maxRight
    public float jumpHeight;
    float walkingDirection = 1.0f;
    Vector2 walkAmount;
    float originalX; // Original float value
    private Rigidbody2D rBody;


    // Start is called before the first frame update
    void Start()
    {
        
        rBody = GetComponent<Rigidbody2D>();
        this.originalX = this.transform.position.x;
        maxLeft = transform.position.x - 2.5f;
        maxRight = transform.position.x + 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
        if (walkingDirection > 0.0f && transform.position.x >= maxRight)
        {
            rBody.velocity = new Vector2(rBody.velocity.x, jumpHeight);
            walkingDirection = -1.0f;
        }
        else if (walkingDirection < 0.0f && transform.position.x <= maxLeft)
        {
            rBody.velocity = new Vector2(rBody.velocity.x, jumpHeight);
            walkingDirection = 1.0f;
        }
        transform.Translate(walkAmount);
    }
}
