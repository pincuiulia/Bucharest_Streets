using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    void Update()
    {
        // vedem daca Esc e apasat
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // load la scena
            LoadLevelSelectionMenu();
        }
    }

    void LoadLevelSelectionMenu()
    {
        // load la Level Selection
        SceneManager.LoadScene("Level Selection");
        Debug.Log("Scene Level Selection has been loaded successfully!");
    }
}
