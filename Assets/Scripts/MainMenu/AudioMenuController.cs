using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenuController : MonoBehaviour
{
    public AudioSource music;
    void Start()
    {
        AudioManager.Instance._musicSource = music;
        AudioManager.Instance.PlayMusic(music.clip);
    }
}
