using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public string upgradeId;
    private UpgradeManager upgradeManager;

    private void Start()
    {
        upgradeManager = FindObjectOfType<UpgradeManager>();
    }

    public void OnClickUpgrade()
    {
        Debug.Log($"[UpgradeButton] 버튼 클릭됨 - ID: {upgradeId}");

        if (upgradeManager != null)
        {
            upgradeManager.TryUpgrade(upgradeId);
        }
        else
        {
            Debug.LogError("[UpgradeButton] UpgradeManager가 null입니다!");
        }
    }
}