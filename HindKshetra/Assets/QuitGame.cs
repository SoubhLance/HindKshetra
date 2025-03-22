using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Quit button pressed!"); // Shows in console for testing

        #if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Stops Play Mode in Unity Editor
        #else
        Application.Quit(); // Closes the game in a built version
        #endif
    }
}
