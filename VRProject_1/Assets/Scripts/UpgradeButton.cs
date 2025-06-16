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
        if (upgradeManager != null)
        {
            upgradeManager.TryUpgrade(upgradeId);
        }
    }
}