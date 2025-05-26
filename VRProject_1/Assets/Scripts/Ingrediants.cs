using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingrediants : MonoBehaviour
{

    private bool isCombined = false;
    public enum BakeState
    {
        Raw,
        Baked,
        Burned
    }

    public string ingredientName;
    public BakeState bakeState = BakeState.Raw;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetBakeState(BakeState newState)
    {
        if (bakeState != newState)
        {
            bakeState = newState;
            Debug.Log($"{gameObject.name} ���� �����: {bakeState}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCombined) return; // �̹� ���յǾ����� ����

        if (other.CompareTag("Ingredient"))
        {
            Ingrediants otherIngredient = other.GetComponent<Ingrediants>();

            // ��뵵 �̹� ���յǾ����� ����
            if (otherIngredient == null || otherIngredient.isCombined) return;

            if (CanCombineWith(otherIngredient))
            {
                // ���� ó��
                isCombined = true;
                otherIngredient.isCombined = true;

                CombineIngredients(other.gameObject, otherIngredient);
            }
            else
            {
                Debug.Log("�ϳ� �̻��� �������� �ʾ� ���� �Ұ�");
            }
        }
    }

    private bool CanCombineWith(Ingrediants other)
    {
        return this.bakeState == BakeState.Baked && other.bakeState == BakeState.Baked;
    }

    private void CombineIngredients(GameObject otherObject, Ingrediants otherIngredient)
    {
        var combo = IngredientCombine.Instance.GetCombination(ingredientName, otherIngredient.ingredientName);

        if (combo != null)
        {
            Debug.Log($"���� ����! �����: {combo.result}");

            if (combo.resultPrefab != null)
            {
                Debug.Log("������ �ν��Ͻ�ȭ ����");
                Instantiate(combo.resultPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("�������� ��� �ֽ��ϴ�! ���� ��� �������� �ν����Ϳ��� �����ߴ��� Ȯ���ϼ���.");
            }
        }
        else
        {
            Debug.Log("���� ����: ��ϵ� ������ �����ϴ�.");
        }

        Destroy(gameObject);
        Destroy(otherObject);

    }
}
