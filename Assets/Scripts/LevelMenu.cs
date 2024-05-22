using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void DeschideNivel(int idNivel)
    {
        string numeNivel;
         //Debug.Log("TEST");

        // verificam daca id-ul nivelului este 0, caz în care numele nivelului este "DEMO"
        if (idNivel == 0)
        {
            numeNivel = "DEMO";
        }
        else
        {
            numeNivel = "Level " + idNivel;
        }

        // verific daca scena exista in Build Settings inainte de a incerca incarcarea ei
        if (Application.CanStreamedLevelBeLoaded(numeNivel))
        {
            SceneManager.LoadScene(numeNivel);
            // afisare mesaj in debugger
            Debug.Log("Scene " + numeNivel + " has been loaded successfully!");
        }
        else
        {
            Debug.LogError("Scene " + numeNivel + " doesn't exist in Build Settings!");
        }
    }


    // metoda care va fi apelata pentru a schimba scena
    public void SwitchScene(string sceneName)
    {
        // incarca scena cu numele specificat
        SceneManager.LoadScene(sceneName);
    }

}

