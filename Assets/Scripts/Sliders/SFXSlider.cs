using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    public Slider slider;
    void Start()
    {
        AudioManager.Instance.SetSfxVolume(0.5f);
        slider.value = AudioManager.Instance._sfxSource.volume;
    }
    public void SetLevel(float sliderValue)
    {
        AudioManager.Instance.SetSfxVolume(sliderValue);
    }
}
