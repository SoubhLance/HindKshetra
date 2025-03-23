using UnityEngine;

namespace YkinikY
{
    public class EnemyMovement : MonoBehaviour
    {
        public float speed = 2f;
        public Transform leftBarrel;
        public Transform rightBarrel;
        
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private enum Direction { Left, Right }
        private Direction currentDirection = Direction.Left;
        private bool isMoving = true;
        
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            
            // Start with left direction
            SetDirection(Direction.Left);
        }
        
        void Update()
        {
            // Check boundaries in Update for more reliable detection
            CheckBoundaries();
        }
        
        void FixedUpdate()
        {
            if (isMoving)
            {
                // Apply movement in FixedUpdate
                float movementDirection = (currentDirection == Direction.Right) ? 1f : -1f;
                rb.linearVelocity = new Vector2(speed * movementDirection, rb.linearVelocity.y);
            }
        }
        
        void CheckBoundaries()
        {
            if (currentDirection == Direction.Right && transform.position.x >= rightBarrel.position.x - 0.5f)
            {
                SetDirection(Direction.Left);
                Debug.Log("Reached right bound, now moving left");
            }
            else if (currentDirection == Direction.Left && transform.position.x <= leftBarrel.position.x + 0.5f)
            {
                SetDirection(Direction.Right);
                Debug.Log("Reached left bound, now moving right");
            }
        }
        
        void SetDirection(Direction newDirection)
        {
            currentDirection = newDirection;
            spriteRenderer.flipX = (currentDirection == Direction.Left);
            
            // Reset velocity to prevent momentum issues
            if (rb != null)
            {
                float movementDirection = (currentDirection == Direction.Right) ? 1f : -1f;
                rb.linearVelocity = new Vector2(speed * movementDirection, rb.linearVelocity.y);
            }
        }
        
        // For debugging - draw the patrol area in the editor
        void OnDrawGizmos()
        {
            if (leftBarrel != null && rightBarrel != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(
                    new Vector3(leftBarrel.position.x, leftBarrel.position.y - 0.5f, 0),
                    new Vector3(rightBarrel.position.x, rightBarrel.position.y - 0.5f, 0)
                );
                
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(leftBarrel.position, 0.25f);
                Gizmos.DrawSphere(rightBarrel.position, 0.25f);
            }
        }
    }
}