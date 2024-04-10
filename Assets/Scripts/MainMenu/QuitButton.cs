using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public AudioSource buttonClick;
    public void doExitGame() {
        AudioManager.Instance.PlaySound(buttonClick.clip);
        Debug.Log("I was here");
        Application.Quit();
    }
}
