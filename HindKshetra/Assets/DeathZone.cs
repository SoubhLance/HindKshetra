using UnityEngine;
using YkinikY; // Ensure this matches PlayerMovement namespace

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object entered DeathBarrier: " + other.gameObject.name); // Debugging

        if (other.CompareTag("Player"))  
        {
            Debug.Log("Player fell! Respawning...");

            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null) // Check if the script is found
            {
                playerMovement.Respawn();
            }
            else
            {
                Debug.LogError("Error: PlayerMovement script not found on Player GameObject!");
            }
        }
    }
}
