using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    Rigidbody2D rb;
    Animator animator;

    Vector2 direction;
    Vector2 startPosition;
    bool onGround;
    bool vulnerable;

    public float speed;
    public float JumpForce;
    public int light_level;
    public int candle_count;
    public int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        vulnerable = true;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);

        if (health <= 0)
        {
            Debug.Log("dead");
        }

        //Control running and jumping animations
        float movementSpeed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Velocity", movementSpeed);
        animator.SetBool("IsGrounded", onGround);
    }

    void OnMove(InputValue movement)
    {
        direction = movement.Get<Vector2>();
        if(direction.x < 0)
        { 
         direction.x = 0;
        }
    }

    void OnJump()
    {
        if(onGround == true)
        {
         rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);

            //Trigger jump animation
            onGround = false;
            animator.SetBool("IsGrounded", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Access the GameObject the collider is attached to.
        GameObject go = collision.gameObject;

        // Does the GameObject we collided with have the tag "Ground"?
        if (go.tag == "Ground" || go.tag == "Spike")
        {
            // Yes, we are on the ground.
            onGround = true;
        }
        else if (go.tag == "Candle")
        {
            // Assuming we have collided with a health Game Object.
            candle_count += 1;
            light_level += 1;

            // Destroy the other GameObject.
            Object.Destroy(go);
        }
        else if (go.tag == "Spike")
        {
            if (vulnerable == true)
            {
                //vulnerable = false;
                health -= 1;
                
                 
            }
            
        }
    }

void OnCollisionExit2D(Collision2D collision)
{
    // Access the GameObject the collider is attached to.
    GameObject go = collision.gameObject;

    // Does the GameObject we collided with have the tag "Ground"?
    if (go.tag == "Ground")
    {
        // Yes, we were on the ground.
        onGround = false;
    }
}
    //Death
    public void Die()
    {
        transform.position = startPosition;
    }
}