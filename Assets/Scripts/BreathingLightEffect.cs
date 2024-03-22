using UnityEngine;
using UnityEngine.Rendering.Universal; // Required for Light2D

[RequireComponent(typeof(Light2D))]
public class BreathingLightEffect : MonoBehaviour
{
    [Header("Light Settings")]
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.0f;
    public float pulseSpeed = 1.0f;

    private Light2D light2D;
    private float targetIntensity;

    void Start()
    {
        light2D = GetComponent<Light2D>();
        targetIntensity = maxIntensity;
    }

    void Update()
    {
        // Smoothly transition between intensities
        light2D.intensity = Mathf.MoveTowards(light2D.intensity, targetIntensity, Time.deltaTime * pulseSpeed);

        // Switch target intensity when the current one is reached
        if (Mathf.Approximately(light2D.intensity, targetIntensity))
        {
            targetIntensity = (targetIntensity == maxIntensity) ? minIntensity : maxIntensity;
        }
    }
}