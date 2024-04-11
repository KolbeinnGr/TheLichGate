using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioMenuController : MonoBehaviour
{
    public AudioSource music;
    public AudioSource sfx;
    void Start()
    {
        GameObject sFXSource = AudioManager.Instance.transform.Find("SFXSource").gameObject;
        GameObject musicSource = AudioManager.Instance.transform.Find("MusicSource").gameObject;
        sfx = sFXSource.GetComponent<AudioSource>();
        music = musicSource.GetComponent<AudioSource>();
        AudioManager.Instance._sfxSource = sfx;
        AudioManager.Instance._musicSource = music;
        AudioManager.Instance.PlayMusic(music.clip);
        AudioManager.Instance.SetMusicVolume(0.1f);
    }
}
