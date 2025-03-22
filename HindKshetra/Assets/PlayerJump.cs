using UnityEngine;

public class PlayerJump2D : MonoBehaviour
{
    public float jumpForce = 5f;  // Adjust the jump height
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;  // Prevents double jumping
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f) // Checks if touching the ground
        {
            isGrounded = true;
        }
    }
}
