using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvements : MonoBehaviour
{
    public Rigidbody2D rb;

    //Mouvement attr
    public float playerSpeed;

    //Animation attr
    private Animator anim;
    private char playerState;

    //Jump attr
    public int jumpForce;
    private bool IsGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public int extraJumpsValue;
    public int extraJumps;

    //Dash attr
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();
        playerState = 'R';
        //Get RigidBody2D
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //locking z axis
        Quaternion rot = transform.rotation;
        rot.z = 0;
        transform.rotation = rot;
        //Set true if is grounded
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // Movements to the right
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * playerSpeed * Time.deltaTime, 0f, 0f));
            anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("standing", 0f);
            playerState = 'R';
        }

        // Movements to the left
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * playerSpeed * Time.deltaTime, 0f, 0f));
            anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("standing", 0f);
            playerState = 'L';
        }

        //Jump Mouvements
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {

            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && IsGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (IsGrounded)
        {
            Debug.Log("Is Grounded");
            extraJumps = extraJumpsValue;
        }

        // Standing to the right
        if (Input.GetAxisRaw("Horizontal") == 0 && playerState == 'R')
        {
            anim.SetFloat("standing", 1f);
            anim.SetFloat("MoveX", 0f);
        }
        // Standing to the left
        if (Input.GetAxisRaw("Horizontal") == 0 && playerState == 'L')
        {
            anim.SetFloat("standing", -1f);
            anim.SetFloat("MoveX", 0f);
        }

        //////Dashing
        ////if(direction == 0)
        ////{
        ////    if (Input.GetKeyDown(KeyCode.Q))
        ////    {
        ////        direction = 1;
        ////    }
        ////    else if(Input.GetKeyDown(KeyCode.RightArrow))
        ////    {
        ////        direction = 2;
        ////    }
        ////}
        //else if(dashTime <= 0)
        //{
        //    direction = 0;
        //    dashTime = startDashTime;
        //    rb.velocity = Vector2.zero;
        //}
        //else
        //{
        //    dashTime -= Time.deltaTime;
        //    if(direction == 1) { rb.velocity = Vector2.left * dashSpeed; }
        //    else if (direction == 2) { rb.velocity = Vector2.right * dashSpeed; }
        //}

    }
}
