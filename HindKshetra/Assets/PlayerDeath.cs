using UnityEngine;

namespace YkinikY
{
    public class PlayerDeath : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Player Died!");
                FindFirstObjectByType<PlayerMovement>().Respawn();
            }
        }
    }
}
