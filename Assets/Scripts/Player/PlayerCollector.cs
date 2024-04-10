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
    }
}
