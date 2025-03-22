using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YkinikY
{
    public class PlayerController_ykiniky : MonoBehaviour
    {
        [Header("(c) Ykiniky")]
        [Header("Movement")]
        public bool canMove = true;
        public bool canJump = true;
        public float velocity = 5; // Extreme speed
        [Header("Camera")]
        public PlayerCameraFollow_ykiniky playerCameraFollow;
        public Vector2 lastCheckpoint;

        void Start()
        {

        }

        void Update()
        {
            if (canMove) MovimentUpdate();
            if (playerCameraFollow != null)
            {
                if (transform.position.x > 0)
                {
                    playerCameraFollow.FollowX();
                }
                else
                {
                    playerCameraFollow.DontFollowX();
                }
                if (transform.position.y > 0)
                {
                    playerCameraFollow.FollowY();
                }
                else
                {
                    playerCameraFollow.DontFollowY();
                }
            }
        }
        public void GameOver()
        {
            canMove = false;
        }
        void MovimentUpdate()
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.position += 25 * Time.deltaTime * velocity * Vector3.left; // Super fast
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.position += 25 * Time.deltaTime * velocity * Vector3.right; // Super fast
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if ((Input.GetKey(KeyCode.Space) & canJump) || (Input.GetKey(KeyCode.W) & canJump))
            {
                canJump = false;
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(GetComponent<Rigidbody2D>().linearVelocity.x, 20); // Super high jump
            }
            if (Input.GetButton("Jump") && canJump)
            {
                canJump = false;
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(GetComponent<Rigidbody2D>().linearVelocity.x, 10); // Super high jump
            }
            transform.position += 15 * Time.deltaTime * velocity * Vector3.right * Input.GetAxis("Horizontal");
            if (Input.GetAxis("Horizontal") == 1)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (Input.GetAxis("Horizontal") == -1)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
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
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(GetComponent<Rigidbody2D>().linearVelocity.x, 30); // Extreme elevator boost
            }
            if (collision.name == "Down_elevator")
            {
                GetComponent<Rigidbody2D>().linearVelocity = new Vector2(GetComponent<Rigidbody2D>().linearVelocity.x, -20); // Extreme downward boost
            }
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Nastro trasportatore s")
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector3(-200, GetComponent<Rigidbody2D>().linearVelocity.y)); // Extreme force
            }
            if (collision.gameObject.name == "Nastro trasportatore d")
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector3(200, GetComponent<Rigidbody2D>().linearVelocity.y)); // Extreme force
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
            transform.position = new Vector2(playerX, transform.position.y);
        }
        public void TeleportPlayerY(float playerY)
        {
            transform.position = new Vector2(transform.position.x, playerY);
        }
        public void BecomeSlow()
        {
            velocity = 2f; // Slow but still fast
        }
        public void BecomeNormal()
        {
            velocity = 5f; // Super fast normal speed
        }
    }
}
