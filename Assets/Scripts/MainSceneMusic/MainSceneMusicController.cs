using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneMusicController : MonoBehaviour
{
    public AudioSource startMusic;
    public AudioSource loopMusic;
    int counter = 1;
    void Awake()
    {
        if (AudioManager.Instance)
        {
            Debug.Log(startMusic.clip.length);
            AudioManager.Instance._musicSource = startMusic;
            AudioManager.Instance.PlayMusic(startMusic.clip);
            startMusic.loop = false;
        }
        
    }

    void Start()
    {
        if(!startMusic.isPlaying)
        {
        AudioManager.Instance._musicSource = startMusic;
        AudioManager.Instance.PlayMusic(startMusic.clip);
        startMusic.loop = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(counter == 1)
        {   
            counter -= 1;
            AudioManager.Instance._musicSource = loopMusic;
            AudioManager.Instance.PlayMusicDelayed(loopMusic.clip, startMusic.clip.length);
            AudioManager.Instance._musicSource = startMusic;
        } 

        if(counter == 0 && startMusic.isPlaying == false)
        {
            AudioManager.Instance._musicSource = loopMusic;
            AudioManager.Instance._musicSource.volume = AudioManager.Instance.musicVolume;
        }  
    }
}
