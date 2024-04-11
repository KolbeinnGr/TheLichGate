using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; // If you're using TextMeshPro

public class LevelUpScreen : MonoBehaviour
{
    [System.Serializable]
    public class UpgradeOptionUI
    {
        public Image icon;
        public TextMeshProUGUI upgradeName;
        public TextMeshProUGUI upgradeDescription;
        public Image background;
        public Button selectButton;
    }
    
    [Header("Attack Icons")]
    public Image SoulAttackIcon;
    public Image BodyAttackIcon;

    [Header("Upgrade Option 1")]
    public UpgradeOptionUI upgradeOption1;

    [Header("Upgrade Option 2")]
    public UpgradeOptionUI upgradeOption2;

    [Header("Upgrade Option 3")]
    public UpgradeOptionUI upgradeOption3;

    [Header("Upgrade Option 4")]
    public UpgradeOptionUI upgradeOption4;

    private UpgradeOptionUI[] upgradeOptions;

    [Header("Rarity Frames")]
    public Sprite[] rarityFrames; // Common, Uncommon, Rare, Epic
    
    
    [Header("Upgrade Icon")]
    public List<UpgradeIconMapping> upgradeIcon;
    
    [System.Serializable]
    public class UpgradeIconMapping
    {
        public PlayerStats.UpgradeType type;
        public Sprite icon;
    }

    
    public void InitializeIfNeeded()
    {
        if (upgradeOptions == null || upgradeOptions.Length == 0)
        {
            upgradeOptions = new UpgradeOptionUI[] { upgradeOption1, upgradeOption2, upgradeOption3, upgradeOption4 };
        }
    }

    public void SetupUpgradeOptions(List<UpgradeManager.UpgradeOption> options)
    {
        InitializeIfNeeded();
        
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("Checking option " + i);
            if (i < options.Count)
            {
                UpgradeOptionUI ui = upgradeOptions[i];
                UpgradeManager.UpgradeOption option = options[i];

                ui.upgradeName.text = option.name;
                ui.upgradeDescription.text = option.description;
                
                // Find the icon for the upgrade
                foreach (UpgradeIconMapping iconMapping in upgradeIcon)
                {
                    if (iconMapping.type == option.type)
                    {
                        ui.icon.sprite = iconMapping.icon;
                        break;
                    }
                }
                
                if (option.rarity >= 0 && (int)option.rarity < rarityFrames.Length)
                {
                    ui.background.sprite = rarityFrames[(int)option.rarity];
                }
                else
                {
                    Debug.LogError("Rarity index out of range for upgrade option.");
                }

                ui.icon.gameObject.SetActive(true);
                
                
                ui.selectButton.onClick.RemoveAllListeners();
                ui.selectButton.onClick.AddListener(() => SelectUpgradeOption(option));
                ui.selectButton.gameObject.SetActive(true);
            }
            else
            {
                upgradeOptions[i].icon.gameObject.SetActive(false); // Hide if no option
            }
        }
    }
    
    private void SelectUpgradeOption(UpgradeManager.UpgradeOption option)
    {
        UpgradeManager.Instance.ApplyUpgrade(option);
        GameManager.Instance.EndLevelUp();
    }
}