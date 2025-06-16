using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public UpgradeData[] upgrades;          // 인스펙터에서 설정
    public GameObject upgradeParent;        // 새로 생성할 오브젝트 위치 지정용 (없으면 월드 중앙 등)

    [Header("UI Elements")]
    public Text coinText;
    public GameObject notEnoughCoinsPopup;
    public GameObject alreadyUpgradedPopup;
    public GameObject upgradeSuccessPopup;

    private void Start()
    {
        UpdateCoinUI();
    }

    public void TryUpgrade(string upgradeId)
    {
        var upgrade = System.Array.Find(upgrades, u => u.upgradeId == upgradeId);
        if (upgrade == null)
        {
            Debug.LogError("해당 업그레이드 ID가 없습니다: " + upgradeId);
            return;
        }

        // 코인, 업그레이드 상태 체크
        if (gamaManager.Instance.GetCoins() < upgrade.cost)
        {
            ShowPopup(notEnoughCoinsPopup);
            return;
        }

        if (gamaManager.Instance.IsUpgradeUnlocked(upgradeId))
        {
            ShowPopup(alreadyUpgradedPopup);
            return;
        }

        // 코인 차감
        gamaManager.Instance.AddCoins(-upgrade.cost);

        // 업그레이드 상태 저장
        gamaManager.Instance.UnlockUpgrade(upgradeId);

        // 새 오브젝트 생성
        if (upgrade.prefabToSpawn != null)
        {
            Vector3 spawnPos = upgradeParent != null ? upgradeParent.transform.position : Vector3.zero;
            Instantiate(upgrade.prefabToSpawn, spawnPos, Quaternion.identity);
        }

        UpdateCoinUI();
        ShowPopup(upgradeSuccessPopup);
    }

    private void ShowPopup(GameObject popup)
    {
        if (popup != null)
        {
            popup.SetActive(true);
            // 필요하면 자동으로 일정 시간 후 닫기 코루틴도 추가 가능
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + gamaManager.Instance.GetCoins();
        }
    }
}
