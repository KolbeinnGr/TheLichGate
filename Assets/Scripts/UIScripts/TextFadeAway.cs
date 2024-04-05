using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFadeAway : MonoBehaviour
{
    public float fadeTime;
    public TextMeshProUGUI fadeAwayText;

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float alphaVal = fadeAwayText.color.a;
        Color tmp = fadeAwayText.color;

        while (fadeAwayText.color.a > 0)
        {
            alphaVal -= 0.01f;
            tmp.a = alphaVal;
            fadeAwayText.color = tmp;

            yield return new WaitForSeconds(0.05f); // update interval
        }
    }
}
