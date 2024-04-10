using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonMenu : MonoBehaviour
{
    public AudioSource buttonClick;

    public void StartGame()
    {
        AudioManager.Instance.PlaySound(buttonClick.clip);
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene("StoryScene");
    }

}
