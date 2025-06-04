using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class gamaManager : MonoBehaviour
{
    public static gamaManager Instance;
    private string saveFilePath;

    public PlayerData CurrentData = new PlayerData();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = Application.persistentDataPath + "/save.json";
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(CurrentData, true);
        File.WriteAllText(saveFilePath, json);
    }

    public void Load()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            CurrentData = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            // 초기값 설정
            CurrentData = new PlayerData
            {
                coins = 100,
                unlockedUpgrades = new List<string>()
            };
        }
    }

    public void AddCoins(int amount)
    {
        CurrentData.coins += amount;
        Save();
    }

    public void UnlockUpgrade(string upgradeId)
    {
        if (!CurrentData.unlockedUpgrades.Contains(upgradeId))
        {
            CurrentData.unlockedUpgrades.Add(upgradeId);
            Save();
        }
    }

    public bool IsUpgradeUnlocked(string upgradeId)
    {
        return CurrentData.unlockedUpgrades.Contains(upgradeId);
    }
}
