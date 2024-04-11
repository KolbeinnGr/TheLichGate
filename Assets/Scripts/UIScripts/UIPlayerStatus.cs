using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayerStatus : MonoBehaviour
{
    [Header("Body Stats Fields")]
    public TextMeshProUGUI bodyAttackDamage;
    public TextMeshProUGUI bodyAttackSpeed;
    public TextMeshProUGUI bodyAttackSize;
    public TextMeshProUGUI bodyAttacks;
    
    [Header("Soul Stats Fields")]
    public TextMeshProUGUI soulAttackDamage;
    public TextMeshProUGUI soulAttackSpeed;
    public TextMeshProUGUI soulAttackProjectileSpeed;
    public TextMeshProUGUI soulAttackProjectiles;
    public TextMeshProUGUI soulAttackPierce;

    private PlayerStats playerStats;
    


    public void UpdatePlayerStats()
    {
        if (!playerStats)
        {
            playerStats = GameManager.Instance.GetPlayerStats();
        }

        if (playerStats)
        {
            bodyAttackDamage.text = playerStats.bodyAttackDamage.ToString();
            bodyAttackSpeed.text = playerStats.bodyAttackSpeed.ToString();
            bodyAttackSize.text = playerStats.bodyAttackSize.ToString();
            bodyAttacks.text = playerStats.bodyAttacks.ToString();
            
            soulAttackDamage.text = playerStats.soulAttackDamage.ToString();
            soulAttackSpeed.text = playerStats.soulAttackSpeed.ToString();
            soulAttackProjectileSpeed.text = playerStats.soulAttackProjectileSpeed.ToString();
            soulAttackProjectiles.text = playerStats.soulAttackProjectiles.ToString();
            soulAttackPierce.text = playerStats.soulAttackPierce.ToString();
        }
    }
}
