using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;

    [Header("Jumping")]
    public float jumpPower = 7f;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    void Start()
    {

    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        GroundCheck();
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

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
