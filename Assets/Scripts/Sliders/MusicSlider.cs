using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public Slider slider;
    void Start()
    {
        AudioManager.Instance.SetMusicVolume(0.5f);
        slider.value = AudioManager.Instance._musicSource.volume;
    }
    public void SetLevel(float sliderValue)
    {
        AudioManager.Instance.SetMusicVolume(sliderValue);
    }
}
