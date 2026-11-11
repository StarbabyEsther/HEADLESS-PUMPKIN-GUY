
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour
{

    Rigidbody2D rb;
    Animator animator;

    Vector2 direction;
    Vector2 startPosition;
    bool onGround;
    bool vulnerable;
    public AudioClip CandleSoundEffect;
    public SoundEffectScript soundManager;

    public float target_time = 5.0f;
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

        // Check if player is dead or has fallen off the platform
        if (health <= 0 || transform.position.y < -10f)
        {
            Debug.Log("dead");
            Time.timeScale = 1;
            SceneManager.LoadScene("Main Menu");
        }

        if (vulnerable == false)
        {
            target_time -= Time.deltaTime;
            if (target_time <= 0.0f)
            {
                vulnerable = true;
                target_time = 3.0f;
            }
        }

        //Control running and jumping animations
        float movementSpeed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Velocity", movementSpeed);
        animator.SetBool("IsGrounded", onGround);
    }

    void OnMove(InputValue movement)
    {
        direction = movement.Get<Vector2>();
        if (direction.x < 0)
        {
            direction.x = 0;
        }
    }

    void OnJump()
    {
        if (onGround == true)
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

            // Play pickup sound
            if (soundManager != null && CandleSoundEffect != null)
            {
                soundManager.Play(CandleSoundEffect);
                Debug.Log("Candle sound played!");
            }
            else
            {
                Debug.LogError("Sound Manager or Candle Sound Effect missing!");
            }

            // Destroy the other GameObject.
            Object.Destroy(go);
        }
        else if (go.tag == "KillBox")
        {
            health = 0;
        }
        if (go.tag == "Spike" || go.tag == "SpikeWall")
        {
            if (vulnerable == true)
            {
                //vulnerable = false;
                health -= 1;
                vulnerable = false;
                Debug.Log(health);
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
