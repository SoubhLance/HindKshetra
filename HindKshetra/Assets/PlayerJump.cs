using UnityEngine;

public class PlayerJump2D : MonoBehaviour
{
    // Jump settings
    public float jumpForce = 10f;
    public bool isGrounded;
    
    // Ground check settings
    public float groundCheckDistance = 0.3f;
    public LayerMask groundMask = -1; // Default to everything
    
    // Component reference
    private Rigidbody2D rb;
    
    void Start()
    {
        // Get the 2D rigidbody
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D found! Adding one...");
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.freezeRotation = true;
        }
        
        Debug.Log("2D Jump Script initialized!");
    }
    
    void Update()
    {
        // Check if we're on the ground
        CheckGrounded();
        
        // Handle jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }
    
    void CheckGrounded()
    {
        // Cast a ray straight down to see if we're grounded
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            groundCheckDistance,
            groundMask
        );
        
        isGrounded = hit.collider != null;
        
        // Visual debug
        Debug.DrawRay(
            transform.position,
            Vector2.down * groundCheckDistance,
            isGrounded ? Color.green : Color.red
        );
    }
    
    void Jump()
    {
        // Apply jump force to 2D rigidbody
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("Jump executed with 2D Rigidbody!");
    }
    
    // Alternative ground check method using OnCollisionStay2D
    void OnCollisionStay2D(Collision2D collision)
    {
        // Check if any of the contact points are below the player
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }
    
    // Reset grounded state when no longer in contact
    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}