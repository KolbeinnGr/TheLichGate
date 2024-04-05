using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0f;
    public float shakeMagnitude = 0.7f;
    private Vector3 initialPosition;

    void Awake()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}

// How to use this script:
// CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
// if (cameraShake){
//     cameraShake.TriggerShake(0.2f, 0.3f); // Shake duration 0.5 seconds, magnitude 0.7
// }