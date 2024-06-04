using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundActivation : MonoBehaviour
{
    public AudioSource soundPlayer;
    
    public void playButtonSoundEffect()
    {
        soundPlayer.Play();
    }
}
