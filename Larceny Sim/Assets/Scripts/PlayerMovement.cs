using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Accesses the sprites rigidbody
    private Rigidbody2D myRigidBody;

    private Animator myAnimator;

    // sets the speed that the player walks
    [SerializeField]
    private float movementSpeed;

    // sets the bool for attacking
    private bool attack;

    // indicates if the player is facing right
    private bool facingRight;


    // sets variables for the interaction between the raycast of the player and the enemy
    RaycastHit2D whatIHit;
    public bool interact = false;
    public Transform lineStart, lineEnd;

    // handles the raycast for the player 
    void Raycasting()
    {
        Debug.DrawLine(lineStart.position, lineEnd.position, Color.green);

        // Raycast only affects anything with the layer "Enemy"
        if(Physics2D.Linecast(lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer("Enemy")))
        {
            whatIHit = Physics2D.Linecast(lineStart.position, lineEnd.position, 1 << LayerMask.NameToLayer("Enemy"));
            interact = true;
        }
        else
        {
            interact = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && interact == true)
        {
            Destroy(whatIHit.collider.gameObject);
        }

    }





    // Start is called before the first frame update
    void Start()
    {
        // sets the player facing right at the start
        facingRight = true;
        // Calls for the rigidbody
        myRigidBody = GetComponent<Rigidbody2D>();
        // references the animator on the player
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInput();
        Raycasting();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Checks for input that affects the horizontal axis
        float horizontal = Input.GetAxis("Horizontal");

        // Updates movement 
        HandleMovemnt(horizontal);

        // Calls the HandleAttacks function to start the attack animation
        HandleAttacks();

        // Calls the ResetValues function to stop the attack animation
        ResetValues();

        // Flips the player when they go in that direction
        Flip(horizontal);
    }
    
    // Handles the players movement
    private void HandleMovemnt(float horizontal)
    {
        // Stops the player from moving and attacking at the same time
        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            // Moves the player
            myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y);
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    // Handles the attack animation
    private void HandleAttacks()
    {
        // Checks if the player has attacked and also makes sure that the player cant attack again until the attack animation is over
        if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myAnimator.SetTrigger("attack");
            // Sets the players velocity to 0 when attacking
            myRigidBody.velocity = Vector2.zero;
        }
    }


    // Handles inputs for things like attacking and jumping
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attack = true;
        }
    }

    // Flips the player sprite horizontally
    private void Flip(float horizontal)
    {
        // if the horizontal is greater than 0 AND is NOT facing right OR the horizontal is less than 0 AND IS facing right
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            // Flip player sprite
            facingRight = !facingRight;

            // Gets the players scale and multiplies it by -1
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    // turns attacking off
    private void ResetValues()
    {
        attack = false;
    }



}
