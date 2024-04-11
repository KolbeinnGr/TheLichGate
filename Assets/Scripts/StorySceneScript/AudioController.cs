using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource newMusicClip;
    void Awake()
    {
        //AudioManager.Instance._musicSource = newMusicClip;
    }

    void Start()
    {
        //AudioManager.Instance.PlayMusic(newMusicClip.clip);
    }

}
