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
        if(GameManager.Instance.isGamePaused)
        {
            GameManager.Instance.ResumeGame();
        }
        
        SceneManager.LoadScene("StoryScene");
    }

}
