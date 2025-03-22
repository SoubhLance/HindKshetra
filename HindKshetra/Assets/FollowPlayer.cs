using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Drag the player GameObject here
    public float speed = 3f; // Movement speed

    void Update()
    {
        if (player != null)
        {
            // Move towards the player smoothly
            transform.position = Vector2.Lerp(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
