using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkayButtonSettings : MonoBehaviour
{
    public AudioSource buttonClick;
    public void closeSettings()
    {
        AudioManager.Instance.PlaySound(buttonClick.clip);
        GameManager.Instance.settingsScreen.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
}
