using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeData
{
    public string upgradeId;          // 업그레이드 식별용 ID
    public int cost;                  // 코인 비용
    public GameObject prefabToSpawn;  // 업그레이드 완료 후 생성할 오브젝트
}
