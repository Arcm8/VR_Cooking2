using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public float breakVelocityThreshold = 5f; // ÀÌ ¼Óµµ ÀÌ»óÀÏ ¶§ ±úÁü
    public GameObject brokenEggPrefab; // ±úÁø ´Þ°¿ ÇÁ¸®ÆÕ

    private bool isBroken = false;

    private void OnCollisionEnter(Collision collision)
    {
        // ÀÌ¹Ì ±úÁ³´Ù¸é ´õ ÀÌ»ó Ã³¸®ÇÏÁö ¾ÊÀ½
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

        // ±úÁø ´Þ°¿ ÇÁ¸®ÆÕ »ý¼º
        if (brokenEggPrefab != null)
        {
            Instantiate(brokenEggPrefab, transform.position, Quaternion.identity);
        }

        // ±âÁ¸ ´Þ°¿ Á¦°Å
        Destroy(gameObject);
    }
}
