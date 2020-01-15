using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement attr
    public float speed = 1f;
    private float horizontalInput;
    private bool lastDirection = false;

    //Jumping attr
    public float jumpSpeed = 3f;
    public Transform groundCheckTransform;
    public float checkRadius;
    public LayerMask whatIsGround;
    public int extraJumpsValue;
    public int extraJumps;
    public bool isJumping;

    //public bool isGrounded;
    private float jumpInput;


    //Swing attr
    public Vector2 ropeHook;
    public bool isSwinging;
    public bool groundCheck;

    //Animation attr
    private Animator animator;
    public float swingForce = 4f;
    private SpriteRenderer playerSprite;

    //Audio attr
    public AudioSource AudioSource;
    public AudioClip[] walkingClips;

    private Rigidbody2D rBody;


    void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Lock Z Axis
        LockZAxis();

        //Set true if is grounded
        //isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, checkRadius, whatIsGround);

        jumpInput = Input.GetAxis("Jump");
        horizontalInput = Input.GetAxis("Horizontal");

        var halfHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        groundCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - halfHeight - 0.04f), Vector2.down, 0.025f);

        if (!isSwinging) //Nest pas en train de se balancer
        {

            isJumping = Input.GetKeyDown(KeyCode.W);
            if (isJumping && !groundCheck && extraJumps > 0)
            {
                rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed); //jump
                extraJumps--;
            }
            else if (isJumping && groundCheck)
            {
                rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed); //jump 
            }

            if (groundCheck)
            {
                extraJumps = extraJumpsValue;
            }
        }
    }

    void FixedUpdate()
    {
        if (horizontalInput < 0f || horizontalInput > 0f) //Si on bouge de gauche a droite
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            playerSprite.flipX = horizontalInput < 0f;

            if (playerSprite.flipX != lastDirection && !isSwinging)
            {
                rBody.velocity = new Vector2(rBody.velocity.x * -1, rBody.velocity.y);

            }

            lastDirection = horizontalInput < 0f;



            if (isSwinging)
            {
                Debug.Log("IsSwinging");
                animator.SetBool("IsSwinging", true);

                var playerToHookDirection = (ropeHook - (Vector2)transform.position).normalized;

                Vector2 perpendicularDirection;
                if (horizontalInput < 0)
                {
                    perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                    var leftPerpPos = (Vector2)transform.position - perpendicularDirection * -2f;
                    Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
                }
                else
                {
                    perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                    var rightPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
                    Debug.DrawLine(transform.position, rightPerpPos, Color.green, 0f);
                }

                var force = perpendicularDirection * swingForce;
                rBody.AddForce(force, ForceMode2D.Force);
            }
            else
            {
                animator.SetBool("IsSwinging", false);

                    var groundForce = speed * 2f;

                    rBody.AddForce(new Vector2((horizontalInput * groundForce - rBody.velocity.x) * groundForce, 0));
                    rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y);

                    if (horizontalInput != 0 && !AudioSource.isPlaying)
                    {
                        AudioSource.PlayOneShot(walkingClips[Random.Range(0, 4)], 0.15f);
                    }
            }
        }
        else //Ne bouge pas de gauche a droite
        {
            animator.SetBool("IsSwinging", false);
            animator.SetFloat("Speed", 0f);
        }

    }

    void LockZAxis()
    {
        //locking z axis
        Quaternion rot = transform.rotation;
        rot.z = 0;
        transform.rotation = rot;
    }
}