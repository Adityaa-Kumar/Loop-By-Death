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
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        GroundCheck();
        Gravity();
        Flip();

        if (isDead)
        {
            Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();

            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            Instantiate(playerCorpse, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }

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

    // public void Die()
    // {
    //     LoopMgr.GetComponent<LoopManager>().DieButton();
    // }

    public void Jump(InputAction.CallbackContext context)
    {
        if (GroundCheck())
        {
            if (context.performed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);

            }
            else if (context.canceled)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower * 0.5f);

            }
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
        if (isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;

            
        }
    }

    bool GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            return true;
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
