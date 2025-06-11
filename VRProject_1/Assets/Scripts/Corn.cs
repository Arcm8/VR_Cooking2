using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Corn : MonoBehaviour
{
    public float breakVelocityThreshold = 5f; // 이 속도 이상일 때 깨짐
    public GameObject brokenEggPrefab; // 깨진 달걀 프리팹

    public float maxHP = 100f;
    private float currentHP;
    private bool isBroken = false;

    public Slider healthSlider; // 슬라이더 UI 연결

    private void OnCollisionEnter(Collision collision)
    {
        // 이미 깨졌다면 더 이상 처리하지 않음
        if (isBroken) return;

        float impactSpeed = collision.relativeVelocity.magnitude;

        if (impactSpeed >= breakVelocityThreshold)
        {
            TakeDamage(20f); // 원하는 만큼 데미지 조정
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        Instantiate(brokenEggPrefab, transform.position, Quaternion.identity);
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHealthUI();

        if (currentHP <= 0)
        {
            Die();
        }
    }
    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHP / maxHP;
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} died!");

        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
