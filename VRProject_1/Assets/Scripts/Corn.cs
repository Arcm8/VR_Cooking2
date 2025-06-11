using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Corn : MonoBehaviour
{
    public float breakVelocityThreshold = 5f; // �� �ӵ� �̻��� �� ����
    public GameObject brokenEggPrefab; // ���� �ް� ������

    public float maxHP = 100f;
    private float currentHP;
    private bool isBroken = false;

    public Slider healthSlider; // �����̴� UI ����

    private void OnCollisionEnter(Collision collision)
    {
        // �̹� �����ٸ� �� �̻� ó������ ����
        if (isBroken) return;

        float impactSpeed = collision.relativeVelocity.magnitude;

        if (impactSpeed >= breakVelocityThreshold)
        {
            TakeDamage(20f); // ���ϴ� ��ŭ ������ ����
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
