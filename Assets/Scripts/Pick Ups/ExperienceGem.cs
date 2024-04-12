using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    public int experienceGranted;
    public AudioClip collectSound;

    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.UpgradeStat(PlayerStats.UpgradeType.increaseExperience, experienceGranted);
        if (collectSound)
        {
            AudioManager.Instance.PlaySound(collectSound, 0.15f);
        }
        Destroy(gameObject);
    }
}
