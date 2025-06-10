using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public float breakVelocityThreshold = 5f; // �� �ӵ� �̻��� �� ����
    public GameObject brokenEggPrefab; // ���� �ް� ������

    private bool isBroken = false;

    private void OnCollisionEnter(Collision collision)
    {
        // �̹� �����ٸ� �� �̻� ó������ ����
        if (isBroken) return;

        float impactSpeed = collision.relativeVelocity.magnitude;

        if (impactSpeed >= breakVelocityThreshold)
        {
            BreakEgg();
        }
    }

    private void BreakEgg()
    {
        isBroken = true;

        // ���� �ް� ������ ����
        if (brokenEggPrefab != null)
        {
            Instantiate(brokenEggPrefab, transform.position, Quaternion.identity);
        }

        // ���� �ް� ����
        Destroy(gameObject);
    }
}
