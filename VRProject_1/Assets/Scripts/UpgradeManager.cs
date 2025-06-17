using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public UpgradeData[] upgrades;          // 인스펙터에서 설정
    public Transform popupSpawnPoint;

    [Header("UI Elements")]
    public Text coinText;
    public GameObject popupPrefab;

    private void Start()
    {
        UpdateCoinUI();
    }

    public void TryUpgrade(string upgradeId)
    {
        var upgrade = System.Array.Find(upgrades, u => u.upgradeId == upgradeId);

        // 2. 코인 부족 시
        if (gamaManager.Instance.GetCoins() < upgrade.cost)
        {
            Debug.Log("코인 부족");
            ShowPopup("notEnough");
            return;
        }

        // 3. 이미 업그레이드 됐는지 확인
        if (gamaManager.Instance.IsUpgradeUnlocked(upgradeId))
        {
            Debug.Log("이미 업그래이드");
            ShowPopup("already");
            return;
        }


        // ? 4. 코인 차감
        gamaManager.Instance.AddCoins(-upgrade.cost);

        // ? 5. 업그레이드 상태 저장
        gamaManager.Instance.UnlockUpgrade(upgradeId);


        // ? 6. 업그레이드 효과 오브젝트 생성
        if (upgrade.prefabToSpawn != null)
        {
            Debug.Log("프리팹 생성");
            Instantiate(upgrade.prefabToSpawn, popupSpawnPoint.transform.position, Quaternion.identity);
        }

        UpdateCoinUI();
        ShowPopup("success");
    }

    private void ShowPopup(string type)
    {
        if (popupPrefab == null || popupSpawnPoint == null)
            return;

        GameObject popup = Instantiate(popupPrefab, popupSpawnPoint.position, popupSpawnPoint.rotation);

        var controller = popup.GetComponent<PopUpController>();
        if (controller == null)
            return;

        string message = "";
        Color color = Color.white;

        switch (type)
        {
            case "notEnough":
                message = "Not Enough Coins!";
                color = Color.red;
                break;
            case "already":
                message = "Already Upgraded!";
                color = Color.yellow;
                break;
            case "success":
                message = "Upgrade Complete!";
                color = Color.green;
                break;
        }

        controller.Setup(message, color);
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + gamaManager.Instance.GetCoins();
        }
    }
}
