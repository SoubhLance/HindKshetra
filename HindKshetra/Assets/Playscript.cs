using UnityEngine;

public class Playscript : MonoBehaviour
{
    public float walkSpeed = 60f; 
    public float runSpeed = 60f;
    public float jumpForce = 10f;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ; //yea! techno 
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal"); 
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        Vector3 movement = new Vector3(moveX * speed * Time.deltaTime, 0, 0);
        transform.Translate(movement, Space.Self);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 5); 
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
