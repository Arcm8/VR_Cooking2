using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Bar : MonoBehaviour
{
    public float maxHP = 100f;
    private float currentHP;

    public Slider healthSlider; // 슬라이더 UI 연결

    public GameObject objectToSpawnOnDeath;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        UpdateHealthUI();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword")) // 칼의 태그를 "Sword"로 설정
        {
            Debug.Log("collision");
            TakeDamage(10f); // 원하는 만큼 데미지 조정
        }
    }

    public void TakeDamage(float damage)
    {
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

        if (objectToSpawnOnDeath != null)
        {
            Instantiate(objectToSpawnOnDeath, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
