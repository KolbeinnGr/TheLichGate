using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkayButton : MonoBehaviour
{
    public AudioSource buttonClick;
    public GameObject PlayButton;
    public GameObject SettingButton;
    public GameObject QuitButton;
    public GameObject Background;
    public GameObject Settings;

    public void Okay()
    {
        AudioManager.Instance.PlaySound(buttonClick.clip);
        Settings.SetActive(false);
        PlayButton.SetActive(true);
        SettingButton.SetActive(true);
        QuitButton.SetActive(true);
        Background.SetActive(true);
    }
}
