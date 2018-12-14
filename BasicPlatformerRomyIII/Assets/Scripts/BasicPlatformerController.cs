using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlatformerController : MonoBehaviour
{
    //Both things that are used to tell if the player is facing right or is jumping (Both are hidden in the inspector but are available to other scripts)
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;

    public Transform groundCheck; //will be used to check if the player is standing on the ground
    //Floats used for movements
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;

    private bool grounded = false;//Tells if the player is on the ground
    //stores component references.
    private Animator anim;
    private Rigidbody2D rb2d;


    // When the game is run these functions will be called.
    void Awake()
    {
        //gets the two components
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Script that checks if the player is on the ground. If the player is on the ground, it will allow the player to jump.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {  //physics stuff
        float h = Input.GetAxis("Horizontal"); //Gets the horizontal axis

        anim.SetFloat("Speed", Mathf.Abs(h)); //speeds up the animations relative to 'h'

        if (h * rb2d.velocity.x < maxSpeed) //checks if the player is within the "Speed limit"(or 'maxSpeed') of the game
            rb2d.AddForce(Vector2.right * h * moveForce); //if true, adds force to the player, therefore moving the player.

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed) //Checks if the player is going to fast/faster that 'maxSpeed'
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y); //limits the player's speed to only 'maxSpeed'

        //basically flips the player in the direction that they are moving in.
        //if player moving left, and IS facing right -> flip to face left.
        if (h > 0 && facingRight)
            Flip();
        //same thing as above but in other direction.
        //i.e. if player moving right, and not facing right -> flip to face right.
        else if (h < 0 && !facingRight)
            Flip();

        if (jump) //Allows the player to jump
        {
            anim.SetTrigger("Jump"); //allows the jump animation to play.
            rb2d.AddForce(new Vector2(0f, jumpForce)); //forces the player to go up
            jump = false;//prevents double jumping
        }
    }

    //This function is used to flip the sprite around if the player is moving in other directions.
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
