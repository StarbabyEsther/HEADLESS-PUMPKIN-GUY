using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{

    Rigidbody2D rb;

    Vector2 direction;
    bool onGround;

    public float speed;
    public float JumpForce;
    public int light_level;
    public int candle_count;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
    }

    void OnMove(InputValue movement)
    {
        direction = movement.Get<Vector2>();
    }

    void OnJump()
    {
        if(onGround == true)
        {
         rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
        }
    }
    
   void OnCollisionEnter2D(Collision2D collision)
{
    // Access the GameObject the collider is attached to.
    GameObject go = collision.gameObject;

        // Does the GameObject we collided with have the tag "Ground"?
        if (go.tag == "Ground")
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
}