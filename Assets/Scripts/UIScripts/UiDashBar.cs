using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiDashBar : MonoBehaviour
{
    public Slider slider;
    public void resetDashbar()
    {
        slider.value = 0;
    }

    public void setDashBar(float fill)
    {   
        slider.value = fill;
    }
}
