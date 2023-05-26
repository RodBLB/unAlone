using System.Runtime.InteropServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveAndJump : MonoBehaviour
{
    private float horizontalMove;


    [SerializeField] private bool isJumpingRequired;
    private bool isFalling;
    [SerializeField] private bool isGrounded;
    private bool isCeiled;

    private Vector2 zeroVelocity = Vector2.zero;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float initialGravityScale;

    [SerializeField] private float speed = 6f;
    [SerializeField] private float movementSmoothing = 0.2f;
    [SerializeField] private float jumpForce = 6.5f;
    [SerializeField] private float jumpForceCeiling = 2f;
    [SerializeField] private float velocityThreshold = 0.15f;
    [SerializeField] private float fallGravityMultiplier = 2.2f;
    [SerializeField] private float lowJumpGravityMultiplier = 2.5f;
    [SerializeField] private LayerMask groundLayers;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckWidth;
    [SerializeField] private float groundCheckHeight;
    public bool isDamaged2;
    public bool isDamaged3;
    public bool isDamaged4;

    public GameObject Bush;
    public GameObject Boar;
    public GameObject Nut;

    public Animator animator;

    private float damCount = 0.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        initialGravityScale = rb.gravityScale;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("WildBoar"))
        {
            isDamaged3 = col.gameObject.GetComponent<WildBoarBehaviour>().isDamagedBoar;
            Debug.Log(isDamaged3 + " wildboar");
        }
        else if (col.CompareTag("Bush"))
        {
            isDamaged2 = col.gameObject.GetComponent<EnemyBehaviour>().isDamaged;
            Debug.Log(isDamaged2 + " Bush");
        }
        else if (col.CompareTag("Nut"))
        {
            isDamaged4 = col.gameObject.GetComponent<NutBehaviour>().isDamagedNut;
            Debug.Log(isDamaged3 + " Nut");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("WildBoar"))
        {
            isDamaged3 = col.gameObject.GetComponent<WildBoarBehaviour>().isDamagedBoar;
            Debug.Log(isDamaged3 + " wildboar");
        }
        else if (col.CompareTag("Bush"))
        {
            isDamaged2 = col.gameObject.GetComponent<EnemyBehaviour>().isDamaged;
            Debug.Log(isDamaged2 + " Bush");
        }
        else if (col.CompareTag("Nut"))
        {
            isDamaged4 = col.gameObject.GetComponent<NutBehaviour>().isDamagedNut;
            Debug.Log(isDamaged3 + " Nut");
        }
    }


    void Update()
    {
        /*isDamaged2 = Bush.GetComponent<EnemyBehaviour>().isDamaged;
        isDamaged3 = Boar.GetComponent<WildBoarBehaviour>().isDamagedBoar;
        isDamaged4 = Nut.GetComponent<NutBehaviour>().isDamagedNut;*/

        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (horizontalMove > 0)
        {
            animator.SetBool("isWalking", true);
            spriteRenderer.flipX = false;
        }
        else if (horizontalMove < 0)
        {
            animator.SetBool("isWalking", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            isJumpingRequired = true;
        }
    }

  

    void FixedUpdate()
    {
        float tempSpeed = speed;

        if (isJumpingRequired)
        {
            if (isGrounded)
            {
                isJumpingRequired = false;
                rb.velocity = Vector2.up * jumpForce;
            }
        }

        if (rb.velocity.y < -velocityThreshold)
        {
            isFalling = true;
            animator.SetBool("isFalling", true);
            rb.gravityScale = initialGravityScale * fallGravityMultiplier;
        }
        else
        {
            isFalling = false;
            animator.SetBool("isFalling", false);
        }

        if (Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckWidth, groundCheckHeight), 0f, groundLayers) != null)
        {
            if (isGrounded == false)
            {
                isGrounded = true;
                animator.SetBool("isGrounded", true);

                isFalling = false;
                animator.SetBool("isFalling", false);
            }
        }
        else
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);

        }
        
        if (!isDamaged2 && !isDamaged3 && !isDamaged4)
        {
            
            Vector2 targetVelocity = new Vector2(horizontalMove * tempSpeed, rb.velocity.y);
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref zeroVelocity, movementSmoothing);
            animator.SetBool("isDamaged", false);
        }

        if (isDamaged2 == true || isDamaged3 == true || isDamaged4 == true)
        {
            print("touché");

            if (damCount <= 2f)
            {
                if (horizontalMove != 0) 
                {
                    horizontalMove = 0;
                }
                
                damCount += 1 * Time.deltaTime;

                if (damCount < 0.05f)
                {
                    animator.SetBool("isDamaged", false);
                }
                else
                {
                    animator.SetBool("isDamaged", true);
                }
            }
            else
            {
                //Debug.Log("horizontalMove");
                Vector2 targetVelocity = new Vector2(horizontalMove * tempSpeed, rb.velocity.y);
                rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref zeroVelocity, movementSmoothing);
                animator.SetBool("isDamaged", false);
            }
        }
        else
        {
            damCount = 0f;
        }

    }


    void OnDrawGizmos()
    {
        Gizmos.color = new Color32(0, 255, 0, 90);
        Gizmos.DrawCube(groundCheck.position, new Vector2(groundCheckWidth, groundCheckHeight));
    }

}
