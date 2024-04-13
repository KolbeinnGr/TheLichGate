using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFlask : MonoBehaviour
{
    public int healthGranted;
    public AudioClip drinkSound;

    public void Collect()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Health health = player.GetComponent<Health>();
        health.Heal(healthGranted);

        if (drinkSound)
        {
            AudioManager.Instance.PlaySound(drinkSound);
        }
        
        Destroy(gameObject);
    }
}
