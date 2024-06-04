using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class PauseMenuScript : MonoBehaviour
{
    // referinta la elemtul UI al meniului de pauza
    [SerializeField] GameObject pauseMenuScreen;

    // referinta la butoanele din meniul de pauza
    public GameObject buttons;

    // referinta la componenta AudioSource pentru redarea sunetelor
    public AudioSource soundPlayer;

    // metoda pentru a pune pauza jocului
    public void PauseGame()
    {
        Time.timeScale = 0; // opreste timpul in joc
        pauseMenuScreen.SetActive(true); // afiseaza meniul de pauza
    }

    // metoda pentru reluarea jocului
    public void ResumeGame()
    {
        Time.timeScale = 1; // reia timpul in joc
        pauseMenuScreen.SetActive(false); // ascunde meniul de pauza
    }

    public void ReloadGame()
    {
        Time.timeScale = 1;
        // reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /*
    // metoda pentru reincarcarea jocului
    public void ReloadGame()
    {
        Time.timeScale = 1; // reia timpul in joc
        pauseMenuScreen.SetActive(false); // ascunde meniul de pauza
        // porneste coroutine-ul pentru a reda sunetul si a reincarca scena
        StartCoroutine(PlaySoundAndReload());
    }

    // coroutine pentru a reda sunetul si a reincarca scena
    public IEnumerator PlaySoundAndReload()
    {
        soundPlayer.Play(); // reda sunetul
        yield return new WaitForSecondsRealtime(soundPlayer.clip.length); // asteapta pana se termina audio-ul
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reincarca scena curenta
    }
    */

    // metoda pentru parasirea nivelului
    public void QuitLevel()
    {
        Time.timeScale = 1; // reia timpul in joc
        // incarca scena Level Selection
        SceneManager.LoadScene("Level Selection");
    }

    // Next: Master Volume, Music Volume, SFX Volume

}
