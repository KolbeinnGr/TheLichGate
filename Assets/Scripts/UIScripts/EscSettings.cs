using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscSettings : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!GameManager.Instance.isGamePaused)
            {
                GameManager.Instance.PauseGame();
                GameManager.Instance.settingsScreen.SetActive(true);
            }
            else{
                closeSettings();
            }
        }
    }

    void closeSettings()
    {
        GameManager.Instance.settingsScreen.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
}
