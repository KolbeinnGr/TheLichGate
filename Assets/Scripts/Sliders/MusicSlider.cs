using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public Slider slider;
    void Start()
    {
        slider.value = AudioManager.Instance.musicVolume;
    }
    public void SetLevel(float sliderValue)
    {
        AudioManager.Instance.SetMusicVolume(sliderValue);
    }
}
