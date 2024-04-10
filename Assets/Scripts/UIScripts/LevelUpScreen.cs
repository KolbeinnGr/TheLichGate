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

    private static readonly Color[] RarityColors = { Color.gray, Color.green, Color.blue, Color.magenta }; // Common, Uncommon, Rare, Epic
    
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

                // Assign the values from options to the UI elements
                // ui.icon.sprite = option.icon;
                ui.upgradeName.text = option.name;
                ui.upgradeDescription.text = option.description;
                ui.background.color = RarityColors[(int)option.rarity];

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