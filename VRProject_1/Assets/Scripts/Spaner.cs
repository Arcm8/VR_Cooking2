using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaner : MonoBehaviour
{
    public GameObject prefab;
    public int desiredCount = 3;
    public float spawnRadius = 1f;
    public float spawnDelay = 0.2f;

    private List<GameObject> spawned = new List<GameObject>();
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    void Update()
    {
        // 리스트에서 null 제거 (삭제된 오브젝트)
        spawned.RemoveAll(obj => obj == null);
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // 필요한 개수만큼 소환
            while (spawned.Count < desiredCount)
            {
                Vector3 offset = Random.insideUnitSphere * spawnRadius;
                offset.y = 0f;

                Vector3 spawnPos = transform.position + offset;
                GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
                spawned.Add(obj);

                yield return new WaitForSeconds(spawnDelay);
            }

            // 충분히 생성되었으면 다음 프레임까지 대기
            yield return null;
        }
    }
}
