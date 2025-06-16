using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    [Header("Player Stats")]
    public int coins = 0;

    [Header("Unlocked Upgrades")]
    public List<string> unlockedUpgrades = new List<string>();

    private void Awake()
    {
        // ½Ì±ÛÅæ ¼³Á¤
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // ¾À º¯°æ ½Ã¿¡µµ À¯Áö
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public void UnlockUpgrade(string upgradeName)
    {
        if (!unlockedUpgrades.Contains(upgradeName))
        {
            unlockedUpgrades.Add(upgradeName);
        }
    }

    public bool IsUpgradeUnlocked(string upgradeName)
    {
        return unlockedUpgrades.Contains(upgradeName);
    }
}
