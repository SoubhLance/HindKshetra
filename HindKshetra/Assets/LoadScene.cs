using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("H"); // Loads the Level1 scene
    }
}
