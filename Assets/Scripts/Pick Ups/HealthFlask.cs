using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFlask : MonoBehaviour
{
    public int healthGranted;
    public AudioClip drinkSound;

    public void Collect()
    {
        Health health = FindObjectOfType<Health>();
        health.Heal(healthGranted);

        if (drinkSound)
        {
            AudioManager.Instance.PlaySound(drinkSound, 0.3f);
        }
        
        Destroy(gameObject);
    }
}
