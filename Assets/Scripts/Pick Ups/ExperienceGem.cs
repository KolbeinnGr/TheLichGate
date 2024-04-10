using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : MonoBehaviour
{
    public int experienceGranted;

    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.UpgradeStat(PlayerStats.UpgradeType.increaseExperience, experienceGranted);
        Destroy(gameObject);
    }
}
