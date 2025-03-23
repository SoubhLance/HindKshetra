using UnityEngine;
using TMPro;  // Import TextMeshPro

public class WinGame : MonoBehaviour
{
    public GameObject winText;  // Assign "WinText" in Inspector

    void Start()
    {
        winText.SetActive(false);  // Hide text at start
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Check if Player enters
        {
            winText.SetActive(true);  // Show text
        }
    }
}
