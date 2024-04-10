using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiEXPbar : MonoBehaviour
{
    public Slider slider;
    public void ResetExpBar()
    {
        slider.value = 0;
    }

    public void SetMaxValue(float exp)
    {
        slider.maxValue = exp;
    }
    
    public void SetExp(float health)
    {
        slider.value = health;
    }
}
