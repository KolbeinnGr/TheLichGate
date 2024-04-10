using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public AudioSource buttonClick;
    public GameObject PlayButton;
    public GameObject SettingButton;
    public GameObject QuitButton;
    public GameObject Background;
    public GameObject Settings;

    public void settings()
    {
        AudioManager.Instance.PlaySound(buttonClick.clip);
        Settings.SetActive(true);
        PlayButton.SetActive(false);
        SettingButton.SetActive(false);
        QuitButton.SetActive(false);
        Background.SetActive(false);
    }
}
