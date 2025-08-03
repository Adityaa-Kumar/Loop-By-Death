using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 7f;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    [Header("Coyote Time")]
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private bool isGrounded;

    [Header("Jump Buffering")]
    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    [Header("Miscellaneous")]
    public Rigidbody2D rb;
    public bool isDead;
    bool isFacingRight = true;
    public GameObject playerCorpse;
    GameObject LoopMgr;

    void Start()
    {
        LoopMgr = GameObject.Find("LoopManager");
        LoopMgr.GetComponent<LoopManager>().ReduceLoops();
    }

    void FixedUpdate()
    {
        isGrounded = GroundCheck();
        UpdateCoyoteTime();
        jumpBufferCounter -= Time.fixedDeltaTime;

        // Movement
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        Gravity();
        Flip();

        // Handle buffered jump if allowed
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            jumpBufferCounter = 0f;
            coyoteTimeCounter = 0f;
        }

        // Handle death
        if (isDead)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Instantiate(playerCorpse, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        // Loop manager check
        if (LoopMgr.GetComponent<LoopManager>().loops == 0 && this.gameObject)
        {
            Destroy(this.gameObject);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!isDead)
        {
            horizontalMovement = context.ReadValue<Vector2>().x;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else if (context.canceled && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    void Gravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    void Flip()
    {
        if ((isFacingRight && horizontalMovement < 0) || (!isFacingRight && horizontalMovement > 0))
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    bool GroundCheck()
    {
        return Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);
    }

    void UpdateCoyoteTime()
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
