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

    private string trashTag = "TrashBin";

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
        Debug.Log("�ݶ��̵�!");

        if (other.CompareTag(trashTag))
        {
            Destroy(gameObject);
            return;
        }

        if (isCombined) return;

        if (other.CompareTag("Ingredient"))
        {
            Ingrediants otherIngredient = other.GetComponent<Ingrediants>();
            if (otherIngredient == null) return;
            if (otherIngredient.isCombined) return;

            Debug.Log($"��� 1 : {ingredientName}, ����: {bakeState} | ��� 2 : {otherIngredient.ingredientName}, ����: {otherIngredient.bakeState}");

            if (CanCombineWith(otherIngredient))
            {
                Debug.Log($"[���� ����] this ID: {GetInstanceID()}, other ID: {other.GetInstanceID()}");

                // �׳� �� �� ���� ���յ��� �ʾ����� ���� ����
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
                Destroy(gameObject);
                Destroy(otherObject);
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


    }
}
