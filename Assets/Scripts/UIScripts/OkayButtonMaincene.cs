using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkayButtonSettings : MonoBehaviour
{
    public GameObject settingsScreen;
    public AudioSource buttonClick;
    public void CloseSettings()
    {
        AudioManager.Instance._sfxSource = buttonClick;
        AudioManager.Instance.PlaySound(buttonClick.clip);
        settingsScreen.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
}
