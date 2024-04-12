using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
    // --- Body stats ---
    public float bodyAttackSpeed = 1f;
    public float bodyAttackDamage = 30f;
    public float bodyAttackSize = 1f;
    public int bodyAttacks = 2;
    public int bodyArmor = 0;
    
    // --- Soul stats ---
    public float soulAttackDamage = 5f;
    public float soulAttackSpeed = 1f;
    public float soulAttackProjectileSpeed = 5f;
    public int soulAttackProjectiles = 1;
    public int soulAttackPierce = 0;

    public int experience = 0;
    public int level = 1;
    public int experienceCap;
    
    // --- Leveling ---
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }
    
      // --- Exp UI ---
    public UiEXPbar eXPbar;

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            
            // Increase experience cap for the next levels
            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }

            experienceCap += experienceCapIncrease;
            eXPbar.SetMaxValue(experienceCap);
            eXPbar.SetExp(experience);
            GameManager.Instance.StartLevelUp();
        }
    }

    public List<LevelRange> levelRanges;

    void Start()
    {
        // Initialize experience cap
        experienceCap = levelRanges[0].experienceCapIncrease;
        eXPbar.SetMaxValue(experienceCap);
        eXPbar.SetExp(0);
    }
    
    public void UpgradeStat(UpgradeType type, float amount)
    {
        switch (type)
        {
            case UpgradeType.bodyAttackSpeed:
                bodyAttackSpeed /= 1 + amount/100f;
                break;
            case UpgradeType.bodyAttackDamage:
                bodyAttackDamage *= 1 + amount/100f;
                break;
            case UpgradeType.bodyAttackSize:
                bodyAttackSize *= 1 + amount/100f;
                break;
            case UpgradeType.bodyArmor:
                bodyArmor += (int)amount;
                break;
            case UpgradeType.bodyAttacks:
                bodyAttacks += (int)amount;
                break;
            case UpgradeType.soulAttackDamage:
                soulAttackDamage *= 1 + amount/100f;
                break;
            case UpgradeType.soulAttackSpeed:
                soulAttackSpeed /= 1 + amount/100f;
                break;
            case UpgradeType.soulAttackProjectileSpeed:
                soulAttackProjectileSpeed += 1 + amount/100f;
                break;
            case UpgradeType.soulAttackProjectiles:
                soulAttackProjectiles += (int)amount;
                break;
            case UpgradeType.soulAttackPierce:
                soulAttackPierce += (int)amount;
                break;
            case UpgradeType.increaseExperience:
                experience += (int)amount;
                eXPbar.SetExp(experience);
                LevelUpChecker();
                break;
        }
    }
    
    
    public enum UpgradeType
    {
        bodyAttackSpeed,
        bodyAttackDamage,
        bodyAttackSize,
        bodyArmor,
        bodyAttacks,
        soulAttackDamage,
        soulAttackSpeed,
        soulAttackProjectileSpeed,
        soulAttackProjectiles,
        soulAttackPierce,
        increaseExperience
    }
}
