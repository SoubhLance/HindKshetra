using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YkinikY
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Playscript : MonoBehaviour
    {
        [Header("(c) Ykiniky")]
        
        [Header("Movement")]
        public bool canMove = true;
        public bool canJump = true;
        [Range(1f, 10f)]
        public float velocity = 5f; // Movement speed
        public float jumpForce = 20f; // Jump height
        
        [Header("Physics")]
        public float gravity = 9.8f; // Standard gravity
        
        [Header("Camera")]
        public Transform cameraToFollow;
        private Vector3 cameraOffset = new Vector3(0, 0, -10);
        public bool followCameraX = true;
        public bool followCameraY = true;
        
        [Header("Game State")]
        public Vector2 lastCheckpoint;
        
        // Component references
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rb;
        private float originalVelocity;
        
        void Awake()
        {
            // Cache component references
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            originalVelocity = velocity;
            
            // Configure the 2D rigidbody
            rb.gravityScale = gravity / Physics2D.gravity.magnitude;
            rb.freezeRotation = true;
        }

        void Start()
        {
            if (lastCheckpoint == Vector2.zero)
            {
                lastCheckpoint = transform.position;
            }
        }

        void Update()
        {
            if (canMove) 
            {
                MovementUpdate();
            }
            
            UpdateCameraFollow();
        }
        
        void UpdateCameraFollow()
        {
            if (cameraToFollow == null) return;
            
            Vector3 targetPosition = transform.position + cameraOffset;
            Vector3 currentCameraPosition = cameraToFollow.position;
            
            float newX = followCameraX && transform.position.x > 0 ? targetPosition.x : currentCameraPosition.x;
            float newY = followCameraY && transform.position.y > 0 ? targetPosition.y : currentCameraPosition.y;
            
            cameraToFollow.position = new Vector3(newX, newY, currentCameraPosition.z);
        }
        
        // Camera follow methods
        public void FollowX()
        {
            followCameraX = true;
        }
        
        public void DontFollowX()
        {
            followCameraX = false;
        }
        
        public void FollowY()
        {
            followCameraY = true;
        }
        
        public void DontFollowY()
        {
            followCameraY = false;
        }
        
        public void GameOver()
        {
            canMove = false;
            rb.linearVelocity = Vector2.zero;
        }
        
        void MovementUpdate()
        {
            // Keyboard movement
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.position += 25 * Time.deltaTime * velocity * Vector3.left;
                spriteRenderer.flipX = true;
            }
            
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.position += 25 * Time.deltaTime * velocity * Vector3.right;
                spriteRenderer.flipX = false;
            }
            
            // Controller movement
            float horizontalInput = Input.GetAxis("Horizontal");
            if (Mathf.Abs(horizontalInput) > 0.1f)
            {
                transform.position += 15 * Time.deltaTime * velocity * Vector3.right * horizontalInput;
                if (horizontalInput > 0)
                    spriteRenderer.flipX = false;
                else if (horizontalInput < 0)
                    spriteRenderer.flipX = true;
            }
            
            // Jump handling - fixed to use Input.GetKeyDown for one-time press
            bool jumpPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Jump");
            
            if (canJump && jumpPressed)
            {
                canJump = false;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Reset jump flag on collision with ground
            canJump = true;
            
            if (collision.gameObject.name == "PlayerSlower")
            {
                BecomeSlow();
            }
        }
        
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.name == "PlayerSlower")
            {
                BecomeNormal();
            }
        }
        
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.name == "Elevator")
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 30);
            }
            
            if (collision.name == "Down_elevator")
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -20);
            }
        }
        
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Nastro trasportatore s")
            {
                rb.AddForce(new Vector2(-200, 0));
            }
            
            if (collision.gameObject.name == "Nastro trasportatore d")
            {
                rb.AddForce(new Vector2(200, 0));
            }
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Checkpoint")
            {
                lastCheckpoint = transform.position;
            }
        }
        
        public void TeleportPlayerX(float playerX)
        {
            transform.position = new Vector3(playerX, transform.position.y, transform.position.z);
        }
        
        public void TeleportPlayerY(float playerY)
        {
            transform.position = new Vector3(transform.position.x, playerY, transform.position.z);
        }
        
        public void BecomeSlow()
        {
            velocity = 2f;
        }
        
        public void BecomeNormal()
        {
            velocity = 5f;
        }
    }
}