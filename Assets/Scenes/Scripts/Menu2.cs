
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour

{
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("The button was pressed");

    }
    public void QuitApp()
    {
        Application.Quit();
    }
}
