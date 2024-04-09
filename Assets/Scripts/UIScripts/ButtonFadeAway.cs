using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFadeAway : MonoBehaviour
{
    public SpriteRenderer yourSpriteRenderer;
    void Start()
    {
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        float alphaVal = yourSpriteRenderer.color.a;
        Color tmp = yourSpriteRenderer.color;

        while (yourSpriteRenderer.color.a > 0)
        {
            alphaVal -= 0.01f;
            tmp.a = alphaVal;
            yourSpriteRenderer.color = tmp;

            yield return new WaitForSeconds(0.05f); // update interval
        }
    }
}