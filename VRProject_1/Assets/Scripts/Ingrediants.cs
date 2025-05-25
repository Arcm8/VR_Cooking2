using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingrediants : MonoBehaviour
{
    public GameObject Bread_and_Bacon;

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
            Debug.Log($"{gameObject.name} 상태 변경됨: {bakeState}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collition");
        if (other.CompareTag("Ingredient"))
        {
            // 재료 객체 가져오기
            Ingrediants otherIngredient = other.GetComponent<Ingrediants>();

            // 둘 다 구워졌을 경우만 조합 가능
            if (CanCombineWith(otherIngredient))
            {
                CombineIngredients(other.gameObject, otherIngredient);
            }
            else
            {
                Debug.Log("두 재료 중 하나가 제대로 구워지지 않았습니다.");
            }
        }
    }

    private bool CanCombineWith(Ingrediants other)
    {
        return this.bakeState == BakeState.Baked && other.bakeState == BakeState.Baked;
    }

    private void CombineIngredients(GameObject otherObject, Ingrediants otherIngredient)
    {
        string otherName = otherIngredient.ingredientName;
        string resultProduct = "";

        if ((ingredientName == "Bacon" && otherName == "Bread") ||
            (ingredientName == "Bread" && otherName == "Bacon"))
        {
            resultProduct = "Bread_n_Bacon";
        }
        else if ((ingredientName == "Flour" && otherName == "Egg") ||
                 (ingredientName == "Egg" && otherName == "Flour"))
        {
            resultProduct = "Cake";
        }

        if (!string.IsNullOrEmpty(resultProduct))
        {
            Debug.Log("조합 성공! 결과물: " + resultProduct);
            CreateResultProduct(resultProduct);
        }

        // 재료 삭제
        Destroy(gameObject);
        Destroy(otherObject);
    }

    private void CreateResultProduct(string product)
    {
        if (product== "Bread_n_Bacon")
        {
            Vector3 spawnPosition = (transform.position);
            Instantiate(Bread_and_Bacon, spawnPosition, Quaternion.identity);
        }
    }

}
