// Example on how it can be used:
// using UnityEngine;
//
// public class GameEventController : MonoBehaviour
// {
//     [SerializeField] private AudioClip newMusicClip;
//     [SerializeField] private AudioClip soundEffectClip;
//     [SerializeField] private float newMusicVolume = 0.5f;
//     [SerializeField] private float soundEffectVolume = 1f;
//
//     // Example function to demonstrate AudioManager's capabilities
//     public void HandleGameEvent()
//     {
//         // Play a sound effect
//         AudioManager.Instance.PlaySound(soundEffectClip, soundEffectVolume);
//
//         // Transition to a new music track with a fade-out duration of 2 seconds
//         AudioManager.Instance.StartCoroutine(AudioManager.Instance.FadeOutMusic(2f, () =>
//         {
//             AudioManager.Instance.PlayMusic(newMusicClip, newMusicVolume);
//         }));
//
//         // Adjust the SFX volume
//         AudioManager.Instance.SetSfxVolume(0.75f);
//
//         // Adjust the ambient volume
//         AudioManager.Instance.SetAmbientVolume(0.3f);
//     }
// }

using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _musicSource, _sfxSource, _ambientSource;
    
    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void PlaySound(AudioClip clip, float volume = 1f) {
        if (clip == null) return;
        _sfxSource.volume = volume;
        _sfxSource.PlayOneShot(clip);
    }
    
    public void PlayMusic(AudioClip clip, float volume = -1f) {
        if (clip == null) return;
        if (volume >= 0) {
            _musicSource.volume = volume;
        }
        _musicSource.clip = clip;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void PlayAmbient(AudioClip clip, float volume = -1f) {
        if (clip == null) return;
        if (volume >= 0) {
            _ambientSource.volume = volume;
        }
        _ambientSource.clip = clip;
        _ambientSource.loop = true;
        _ambientSource.Play();
    }

    public void StopMusic() {
        _musicSource.Stop();
    }
    
    public IEnumerator FadeOutMusic(float fadeDuration, Action onComplete = null) {
        float startVolume = _musicSource.volume;
        while (_musicSource.volume > 0) {
            _musicSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
        _musicSource.Stop();
        _musicSource.volume = startVolume;
        onComplete?.Invoke();
    }

    public void StopAmbient() {
        _ambientSource.Stop();
    }

    public void SetMasterVolume(float volume) {
        AudioListener.volume = volume;
    }
    
    public void SetMusicVolume(float volume) {
        _musicSource.volume = volume;
    }
    
    public void SetSfxVolume(float volume) {
        _sfxSource.volume = volume;
    }

    public void SetAmbientVolume(float volume) {
        _ambientSource.volume = volume;
    }
    
    public void ToggleMute() {
        AudioListener.pause = !AudioListener.pause;
    }
    
    public void ToggleMusicMute() {
        _musicSource.mute = !_musicSource.mute;
    }
    
    public void ToggleSfxMute() {
        _sfxSource.mute = !_sfxSource.mute;
    }

    public void ToggleAmbientMute() {
        _ambientSource.mute = !_ambientSource.mute;
    }
}
