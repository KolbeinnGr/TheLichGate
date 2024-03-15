using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // --- Body stats ---
    public int maxHealth = 100;
    
    private int currentHealth;
    private float bodyAttackSpeed = 1f;
    private float bodyAttackDamage = 10f;
    private float bodyAttackRange = 1f;
    private int bodyArmor = 0;
    
    // --- Soul stats ---
    private float soulAttackDamage = 5f;
    private float soulAttackSpeed = 1f;
    private float soulAttackProjectileSpeed = 5f;
    private int soulAttackProjectiles = 0;
    private int soulAttackPierce = 0;
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
    
    void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    
    void UpgradeStat(UpgradeType type, float amount)
    {
        switch (type)
        {
            case UpgradeType.bodyAttackSpeed:
                bodyAttackSpeed += amount;
                break;
            case UpgradeType.bodyAttackDamage:
                bodyAttackDamage += amount;
                break;
            case UpgradeType.bodyAttackRange:
                bodyAttackRange += amount;
                break;
            case UpgradeType.bodyArmor:
                bodyArmor += (int)amount;
                break;
            case UpgradeType.soulAttackDamage:
                soulAttackDamage += amount;
                break;
            case UpgradeType.soulAttackSpeed:
                soulAttackSpeed += amount;
                break;
            case UpgradeType.soulAttackProjectileSpeed:
                soulAttackProjectileSpeed += amount;
                break;
            case UpgradeType.soulAttackProjectiles:
                soulAttackProjectiles += (int)amount;
                break;
            case UpgradeType.soulAttackPierce:
                soulAttackPierce += (int)amount;
                break;
        }
    }
    
    enum  UpgradeType 
    {
        bodyAttackSpeed,
        bodyAttackDamage,
        bodyAttackRange,
        bodyArmor,
        soulAttackDamage,
        soulAttackSpeed,
        soulAttackProjectileSpeed,
        soulAttackProjectiles,
        soulAttackPierce
        
    }
}
