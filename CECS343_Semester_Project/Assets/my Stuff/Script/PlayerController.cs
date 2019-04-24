using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // movement variables
    [SerializeField]
    private float maxSpeed;
    private float move;
    
    // jumping variables
    bool grounded = false;
    float groundCheckRadius = 0.05f;
    public LayerMask groundLayer; // The layer that would trigger groundCheck if player is on ground
    public Transform groundCheck;
    public float jumpHeight;

    // Body variables
    Rigidbody2D pRB;
    Animator anim;
    bool facingRight;
    Vector2 bodyVelocity;

    // Start is called before the first frame update
    void Start()
    {
        // Initializing variables
        pRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        facingRight = true;
        jumpHeight *= .1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Checking if jump button is pressed, and it is touching ground
        // if true jump
        if (grounded && Input.GetButton("Jump"))
        {
            grounded = false;
            anim.SetBool("isGrounded", grounded); // isGrounded is variable for Animator
            bodyVelocityUpdate(pRB.velocity.x, jumpHeight);
        }

    }

    // Called just before performing any physics calculations
    // Physics code go to ensure they all work in sync
    void FixedUpdate()
    {
        UpdateGround();
        MoveLeftRight();

        // Flipping character
        if (move > 0 && !facingRight)
        {
            flip();
        }
        else if(move < 0 && facingRight)
        {
            flip();
        }
    }


    /*********************************************/
    /*********************************************/
    // Flips character based on the direction of player movement
    void flip()
    {
        facingRight = !facingRight;
        Vector2 pScale = transform.localScale;
        pScale.x *= -1;
        transform.localScale = pScale;

    }
    // Updates velocity for rigidbody
    void bodyVelocityUpdate(float x, float y)
    {
        bodyVelocity.Set(x, y);
        pRB.velocity = bodyVelocity;
    }

    // Falling or grounded
    void UpdateGround()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); // check if we are grounded - if no, then we are in air
        anim.SetBool("isGrounded", grounded); // isGrounded is variable for Animator
        anim.SetFloat("verticalSpeed", pRB.velocity.y); // verticalSpeed is variable for Animator
    }

    // Move left or right
    void MoveLeftRight()
    {
        move = Input.GetAxis("Horizontal"); // GetAxis would give float value going up to one or negative one.
        anim.SetFloat("speed", Mathf.Abs(move)); // "speed" is variable for Animator
        bodyVelocityUpdate(move * maxSpeed, pRB.velocity.y);
    }
}
