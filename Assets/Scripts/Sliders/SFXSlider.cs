using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SFXSlider : MonoBehaviour
{
    public AudioClip slideSound;
    public Slider slider;
    void Start()
    {
        slider.value = AudioManager.Instance.musicVolume;
    }
    public void SetLevel(float sliderValue)
    {
        AudioManager.Instance.SetSfxVolume(sliderValue);
        AudioManager.Instance.PlaySound(slideSound);
    }
}
