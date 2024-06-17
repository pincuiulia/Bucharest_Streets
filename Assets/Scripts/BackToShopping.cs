using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToPreviousScene : MonoBehaviour
{
    // numele scenei catre care vreau sa ma intorc
    public string previousSceneName;

    void Update()
    {
        // verificam daca Esc a fost apasar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // incarcare scena
            SceneManager.LoadScene(previousSceneName);
            Debug.Log("Returned to the previous scene: " + previousSceneName);
        }
    }
}
