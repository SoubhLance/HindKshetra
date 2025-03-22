using UnityEngine;

namespace YkinikY
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("(c) Ykiniky")]
        
        [Header("Movement")]
        public bool canMove = true;
        public float moveSpeed = 5f;
        
        [Header("Jumping")]
        public float jumpForce = 12f;
        public bool canJump = true;
        private bool hasDoubleJumped = false; // Track if double jump has been used
        
        [Header("Ground Check")]
        public float groundCheckDistance = 0.3f;
        public LayerMask groundMask = -1; // Default to everything
        private bool isGrounded;
        
        [Header("Components")]
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        
        void Awake()
        {
            // Get references to components
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            
            // Make sure rigidbody is set up correctly
            rb.gravityScale = 2.5f;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        
        void Update()
        {
            CheckGrounded();
            
            if (canMove)
            {
                HandleMovement();
                HandleJumping();
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
            
            bool wasGrounded = isGrounded;
            isGrounded = hit.collider != null;
            
            // Reset double jump ability when landing
            if (isGrounded && !wasGrounded)
            {
                hasDoubleJumped = false;
            }
            
            // Visual debug
            Debug.DrawRay(
                transform.position,
                Vector2.down * groundCheckDistance,
                isGrounded ? Color.green : Color.red
            );
        }
        
        void HandleMovement()
        {
            // Get horizontal input (keyboard arrows, WASD, or controller)
            float horizontalInput = Input.GetAxis("Horizontal");
            
            // Calculate move direction with improved responsiveness
            float moveFactor = 8f;
            Vector2 movement = new Vector2(horizontalInput * moveSpeed * moveFactor, rb.linearVelocity.y);
            
            // Apply movement
            rb.linearVelocity = new Vector2(movement.x * Time.deltaTime, rb.linearVelocity.y);
            
            // Flip sprite based on movement direction
            if (horizontalInput > 0.1f)
                spriteRenderer.flipX = false;
            else if (horizontalInput < -0.1f)
                spriteRenderer.flipX = true;
        }
        
        void HandleJumping()
        {
            // Jump handling - use GetKeyDown for one-time press
            bool jumpPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Jump");
            
            if (jumpPressed && canJump)
            {
                if (isGrounded)
                {
                    // First jump when grounded
                    PerformJump();
                }
                else if (!hasDoubleJumped)
                {
                    // Double jump in air (only once)
                    PerformJump();
                    hasDoubleJumped = true;
                }
            }
        }
        
        void PerformJump()
        {
            // Reset vertical velocity for consistent jump height
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
            // Play jump animation or sound here if needed
        }
        
        // Alternative ground check method using collision detection
        void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if any of the contact points are below the player
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    hasDoubleJumped = false; // Reset double jump ability
                    return;
                }
            }
        }
        
        // Used by other scripts to disable movement
        public void DisableMovement()
        {
            canMove = false;
        }
        
        // Used by other scripts to enable movement
        public void EnableMovement()
        {
            canMove = true;
        }
    }
}