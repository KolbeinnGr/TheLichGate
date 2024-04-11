using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscSettings : MonoBehaviour
{
    public GameObject settingsScreen;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!GameManager.Instance.isGamePaused)
            {
                GameManager.Instance.PauseGame();
                settingsScreen.SetActive(true);
            }
            else{
                closeSettings();
            }
        }
    }

    void closeSettings()
    {
        settingsScreen.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
}
