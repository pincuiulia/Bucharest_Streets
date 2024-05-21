using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void DeschideNivel(int idNivel)
    {
        string numeNivel;

        // verificam daca id-ul nivelului este 0, caz în care numele nivelului este "DEMO"
        if (idNivel == 0)
        {
            numeNivel = "DEMO";
        }
        else
        {
            numeNivel = "Level" + idNivel;
        }

        // incarc scena cu numele corespunzator
        SceneManager.LoadScene(numeNivel);
    }

}
