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
            Debug.Log($"{gameObject.name} 상태 변경됨: {bakeState}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("콜라이드!");

        if (other.CompareTag(trashTag))
        {
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Ingredient"))
        {
            Ingrediants otherIngredient = other.GetComponent<Ingrediants>();
            if (otherIngredient == null) return;

            Debug.Log($"재료 1 : {ingredientName} ({bakeState}) | 재료 2 : {otherIngredient.ingredientName} ({otherIngredient.bakeState})");

            if (CanCombineWith(otherIngredient))
            {
                // 조합 주도권: Y값이 더 낮은 오브젝트가 조합 수행
                if (transform.position.y <= other.transform.position.y)
                {
                    Debug.Log("내가 아래에 있으니 조합 시작!");

                    CombineIngredients(other.gameObject, otherIngredient);
                }
                else
                {
                    Debug.Log("상대가 아래에 있어, 조합을 맡김");
                }
            }
            else
            {
                Debug.Log("하나 이상이 구워지지 않아 조합 불가");
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
            Debug.Log($"조합 성공! 결과물: {combo.result}");

            if (combo.resultPrefab != null)
            {
                Debug.Log("프리팹 인스턴스화 시작");
                Instantiate(combo.resultPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(otherObject);
            }
            else
            {
                Debug.LogError("프리팹이 비어 있습니다! 조합 결과 프리팹을 인스펙터에서 지정했는지 확인하세요.");
            }
        }
        else
        {
            Debug.Log("조합 실패: 등록된 조합이 없습니다.");
        }


    }
}
