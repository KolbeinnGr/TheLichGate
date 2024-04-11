using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }
    
    public List<UpgradeOption> possibleUpgrades;
    public List<RarityWeight> rarityWeights;
    
    public enum UpgradeRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic
    }

    [System.Serializable]
    public class UpgradeOption
    {
        public PlayerStats.UpgradeType type;
        public UpgradeRarity rarity;
        public float effectValue;
        public string name;
        public string description;
    }

    [System.Serializable]
    public class RarityWeight
    {
        public UpgradeRarity rarity;
        public int weight;
        public int rank; // Higher rank means rarer
    }
    
    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    
    void Start()
    {
        InitializeRarityWeights();
        InitializeUpgrades();
    }
    
    public List<UpgradeOption> GetRandomUpgrades(int numberOfOptions)
    {
        List<UpgradeOption> selectedUpgrades = new List<UpgradeOption>();
        HashSet<PlayerStats.UpgradeType> selectedTypes = new HashSet<PlayerStats.UpgradeType>();

        while (selectedUpgrades.Count < numberOfOptions)
        {
            UpgradeOption potentialUpgrade = GetRandomUpgradeByRarity();

            // Check if this upgrade type has already been selected
            if (!selectedTypes.Contains(potentialUpgrade.type))
            {
                selectedUpgrades.Add(potentialUpgrade);
                selectedTypes.Add(potentialUpgrade.type);
            }
        }
        return selectedUpgrades;
    }

    
    
    UpgradeRarity GetRandomRarity()
    {
        int totalWeight = 0;
        foreach (var rarityWeight in rarityWeights)
        {
            totalWeight += rarityWeight.weight;
        }

        int randomValue = UnityEngine.Random.Range(0, totalWeight);
        foreach (var rarityWeight in rarityWeights)
        {
            if (randomValue < rarityWeight.weight)
                return rarityWeight.rarity;
            randomValue -= rarityWeight.weight;
        }

        return UpgradeRarity.Common; // Fallback
    }
    
    int GetRandomRarityRank()
    {
        int totalWeight = 0;
        foreach (var rarityWeight in rarityWeights)
            totalWeight += rarityWeight.weight;

        int randomValue = UnityEngine.Random.Range(0, totalWeight);
        foreach (var rarityWeight in rarityWeights)
        {
            if (randomValue < rarityWeight.weight)
                return rarityWeight.rank;
            randomValue -= rarityWeight.weight;
        }

        return 0; // Fallback to the lowest rank
    }

    
    UpgradeOption GetRandomUpgradeByRarity()
    {
        int targetRank = GetRandomRarityRank();
        UpgradeOption upgrade = null;

        while (upgrade == null && targetRank >= 0)
        {
            UpgradeRarity targetRarity = rarityWeights.Find(r => r.rank == targetRank).rarity;
            List<UpgradeOption> filteredUpgrades = possibleUpgrades.FindAll(u => u.rarity == targetRarity);

            if (filteredUpgrades.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, filteredUpgrades.Count);
                upgrade = filteredUpgrades[randomIndex];
            }
            else
            {
                // Decrement rank to check the next lower rarity
                targetRank--;
            }
        }

        return upgrade; // May return null if no upgrades are available at all
    }

    
    public void ApplyUpgrade(UpgradeOption selectedUpgrade)
    {
        // Access the player's stats and apply the upgrade
        var playerStats = FindObjectOfType<PlayerStats>();
        playerStats.UpgradeStat(selectedUpgrade.type, selectedUpgrade.effectValue);
    }

    void InitializeRarityWeights()
    {
        rarityWeights = new List<RarityWeight>
        {
            new RarityWeight { rarity = UpgradeRarity.Common, weight = 45, rank = 0 },
            new RarityWeight { rarity = UpgradeRarity.Uncommon, weight = 30, rank = 1 },
            new RarityWeight { rarity = UpgradeRarity.Rare, weight = 15, rank = 2 },
            new RarityWeight { rarity = UpgradeRarity.Epic, weight = 10, rank = 3 }
        };
    }

    void InitializeUpgrades()
    {
        
        // Body upgrades
        // Attack speed
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyAttackSpeed, rarity = UpgradeRarity.Common, effectValue = 10f, name = "Melee Weapon Attack Speed (Common)", description = "Increases the speed of your Melee attacks by 10%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyAttackSpeed, rarity = UpgradeRarity.Uncommon, effectValue = 15f, name = "Melee Weapon Attack Speed (Uncommon)", description = "Increases the speed of your Melee attacks by 15%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyAttackSpeed, rarity = UpgradeRarity.Rare, effectValue = 20f, name = "Melee Weapon Attack Speed (Rare)", description = "Increases the speed of your Melee attacks by 20%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyAttackSpeed, rarity = UpgradeRarity.Epic, effectValue = 25f, name = "Melee Weapon Attack Speed (Epic)", description = "Increases the speed of your Melee attacks by 25%."});
        
        // Attack damage
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyAttackDamage, rarity = UpgradeRarity.Common, effectValue = 10f, name = "Melee Weapon Attack Damage (Common)", description = "Increases the damage of your Melee attacks by 10%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyAttackDamage, rarity = UpgradeRarity.Uncommon, effectValue = 15f, name = "Melee Weapon Attack Damage (Uncommon)", description = "Increases the damage of your Melee attacks by 15%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyAttackDamage, rarity = UpgradeRarity.Rare, effectValue = 20f, name = "Melee Weapon Attack Damage (Rare)", description = "Increases the damage of your Melee attacks by 20%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyAttackDamage, rarity = UpgradeRarity.Epic, effectValue = 25f, name = "Melee Weapon Attack Damage (Epic)", description = "Increases the damage of your Melee attacks by 25%."});;
        
        // Attack size
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyAttackSize, rarity = UpgradeRarity.Common, effectValue = 10f, name = "Melee Weapon Attack Size (Common)", description = "Increases the size of your Melee attacks by 10%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyAttackSize, rarity = UpgradeRarity.Uncommon, effectValue = 15f, name = "Melee Weapon Attack Size (Uncommon)", description = "Increases the size of your Melee attacks by 15%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyAttackSize, rarity = UpgradeRarity.Rare, effectValue = 20f, name = "Melee Weapon Attack Size (Rare)", description = "Increases the size of your Melee attacks by 20%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyAttackSize, rarity = UpgradeRarity.Epic, effectValue = 25f, name = "Melee Weapon Attack Size (Epic)", description = "Increases the size of your Melee attacks by 25%."});
        
        // Attacks
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyAttacks, rarity = UpgradeRarity.Rare, effectValue = 1f, name = "Melee Weapon Attacks (Rare)", description = "Increases the number of Melee attacks you can perform by 1."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyAttacks, rarity = UpgradeRarity.Epic, effectValue = 2f, name = "Melee Weapon Attacks (Epic)", description = "Increases the number of Melee attacks you can perform by 2."});
        
        // Armor
        // possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyArmor, rarity = UpgradeRarity.Common, effectValue = 1f, name = "Armor", description = "Increases your armor by 1."});
        // possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyArmor, rarity = UpgradeRarity.Uncommon, effectValue = 2f, name = "Armor", description = "Increases your armor by 2."});
        // possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyArmor, rarity = UpgradeRarity.Rare, effectValue = 3f, name = "Armor", description = "Increases your armor by 3."});
        // possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.bodyArmor, rarity = UpgradeRarity.Epic, effectValue = 5f, name = "Armor", description = "Increases your armor by 5."});
        
        // Soul upgrades
        // Attack Speed
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackSpeed, rarity = UpgradeRarity.Common, effectValue = 10f, name = "Soul Weapon Attack Speed (Common)", description = "Increases the speed of your Soul attacks by 10%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackSpeed, rarity = UpgradeRarity.Uncommon, effectValue = 15f, name = "Soul Weapon Attack Speed (Uncommon)", description = "Increases the speed of your Soul attacks by 15%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackSpeed, rarity = UpgradeRarity.Rare, effectValue = 20f, name = "Soul Weapon Attack Speed (Rare)", description = "Increases the speed of your Soul attacks by 20%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackSpeed, rarity = UpgradeRarity.Epic, effectValue = 25f, name = "Soul Weapon Attack Speed (Epic)", description = "Increases the speed of your Soul attacks by 25%."});
        
        // Attack Damage
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackDamage, rarity = UpgradeRarity.Common, effectValue = 10f, name = "Soul Weapon Attack Damage (Common)", description = "Increases the damage of your Soul attacks by 10%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackDamage, rarity = UpgradeRarity.Uncommon, effectValue = 15f, name = "Soul Weapon Attack Damage (Uncommon)", description = "Increases the damage of your Soul attacks by 15%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackDamage, rarity = UpgradeRarity.Rare, effectValue = 20f, name = "Soul Weapon Attack Damage (Rare)", description = "Increases the damage of your Soul attacks by 20%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackDamage, rarity = UpgradeRarity.Epic, effectValue = 25f, name = "Soul Weapon Attack Damage (Epic)", description = "Increases the damage of your Soul attacks by 25%."});
        
        // Projectiles
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackProjectiles, rarity = UpgradeRarity.Rare, effectValue = 1f, name = "Soul Weapon Projectiles (Rare)", description = "Increases the number of Soul projectiles you can shoot by 1."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackProjectiles, rarity = UpgradeRarity.Epic, effectValue = 2f, name = "Soul Weapon Projectiles (Epic)", description = "Increases the number of Soul projectiles you can shoot by 2."});
        
        // Projectile Speed
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackProjectileSpeed, rarity = UpgradeRarity.Common, effectValue = 10f, name = "Soul Weapon Projectile Speed (Common)", description = "Increases the speed of your Soul projectiles by 10%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackProjectileSpeed, rarity = UpgradeRarity.Uncommon, effectValue = 15f, name = "Soul Weapon Projectile Speed (Uncommon)", description = "Increases the speed of your Soul projectiles by 15%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackProjectileSpeed, rarity = UpgradeRarity.Rare, effectValue = 20f, name = "Soul Weapon Projectile Speed (Rare)", description = "Increases the speed of your Soul projectiles by 20%."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackProjectileSpeed, rarity = UpgradeRarity.Epic, effectValue = 25f, name = "Soul Weapon Projectile Speed (Epic)", description = "Increases the speed of your Soul projectiles by 25%."});
        
        // Pierce
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackPierce, rarity = UpgradeRarity.Rare, effectValue = 1f, name = "Soul Weapon Pierce (Rare)", description = "Increases the number of enemies your Soul projectiles can pierce by 1."});
        possibleUpgrades.Add(new UpgradeOption { type = PlayerStats.UpgradeType.soulAttackPierce, rarity = UpgradeRarity.Epic, effectValue = 2f, name = "Soul Weapon Pierce (Epic)", description = "Increases the number of enemies your Soul projectiles can pierce by 2."});
        
    }
}
