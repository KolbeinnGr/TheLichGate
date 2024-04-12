using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ExperienceGem"))
        {
            ExperienceGem experienceGem = other.GetComponent<ExperienceGem>();
            experienceGem.Collect();
        }
        
        if (other.CompareTag("HealthFlask"))
        {
            HealthFlask healthFlask = other.GetComponent<HealthFlask>();
            healthFlask.Collect();
        }

        if (other.CompareTag("Chest"))
        {
            ChestOpenScript chest = other.GetComponent<ChestOpenScript>();
            chest.Collect();
        }
    }
}
