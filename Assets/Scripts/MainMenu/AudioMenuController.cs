using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenuController : MonoBehaviour
{
    public AudioSource music;
    void Start()
    {
        AudioManager.Instance._musicSource.volume = 0.5f;
        AudioManager.Instance._sfxSource.volume = 0.5f;
        AudioManager.Instance.PlayMusic(music.clip);
    }
}
