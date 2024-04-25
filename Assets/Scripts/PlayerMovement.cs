using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // SerializeField allows you to modify variables in Unity interface
    [SerializeField] public float speed;
    [SerializeField ]public float jumpSpeed;
    
    //Declared sp we can manipulate these components on the player
    private Rigidbody2D rb;
    private Animator anim;

    //For Player movement
    private float moveInput;
    private bool facingRight = true;

    //Jumping Variables
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public int jumpCount;

    //Dashing Variables
    [SerializeField] public float dashDistance = 15f;
    [SerializeField] public float dashTime;
    bool isDashing;

    //Access Trap script
    private Trap trapScript;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        trapScript = FindObjectOfType<Trap>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Boolean checks if Ground Check object (set at Player's feet) is touching the ground 
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //Indented inside of if statement to intro dash mechanic
        if (!isDashing)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput*speed, rb.velocity.y);
            anim.SetBool("run", moveInput !=0);
            anim.SetBool("grounded", isGrounded);
        }

        //Flips character around when direction chances
        if (facingRight == false && moveInput > 0) 
        {
            Flip();
        } else if (facingRight == true && moveInput < 0) {
            Flip();
        }

        //Jump Logic
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount == 0 && isGrounded){
            Jump();
            anim.SetTrigger("jump");
            jumpCount = 0;
        }

        //Dashing Logic, see coroutine below as well
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashing = true;
            anim.SetBool("dash", isDashing);
            StartCoroutine(Dash());
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        jumpCount += 1;
    }

    void Flip() 
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    //Dashing Coroutine
    IEnumerator Dash()
    {
        if (facingRight)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(dashDistance, 0f), ForceMode2D.Impulse);
        }
        if (!facingRight)
        {
            rb.velocity = new Vector2(-(rb.velocity.x), 0f);
            rb.AddForce(new Vector2(-(dashDistance), 0f), ForceMode2D.Impulse);
        }
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        anim.SetBool("dash", isDashing);
        rb.gravityScale = gravity;
    }

    //Dying + Respawning
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Trap") && trapScript != null)
        {
            StartCoroutine(DieAndRespawn());
        }
    }

    void MiniJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed/2);
    }

    //interesting bug to return to - if player is mid-death and tries to swap gravity they will turn upside down, interrupt the animation, but not fall upwards. Player still dies and respawns.
    //solution would have the player be unable to do literally anything once they hit a trap, including manipulating gravity.
    IEnumerator DieAndRespawn()
    {
        MiniJump();
        yield return new WaitForSeconds(0.1f);
        rb.constraints = RigidbodyConstraints2D.FreezePosition; 
        anim.SetTrigger("die");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(trapScript.Respawn);
        
        //ensures gravity/player rotation returns to normal in case character is upside down when they die
        Physics2D.gravity = new Vector2(0f,-30f);
        transform.eulerAngles = new Vector3(0,0,0);

    }
}
