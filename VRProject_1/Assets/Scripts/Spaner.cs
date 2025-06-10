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
        // ����Ʈ���� null ���� (������ ������Ʈ)
        spawned.RemoveAll(obj => obj == null);
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // �ʿ��� ������ŭ ��ȯ
            while (spawned.Count < desiredCount)
            {
                Vector3 offset = Random.insideUnitSphere * spawnRadius;
                offset.y = 0f;

                Vector3 spawnPos = transform.position + offset;
                GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
                spawned.Add(obj);

                yield return new WaitForSeconds(spawnDelay);
            }

            // ����� �����Ǿ����� ���� �����ӱ��� ���
            yield return null;
        }
    }
}
