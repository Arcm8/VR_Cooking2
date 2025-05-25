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
            Debug.Log($"{gameObject.name} ���� �����: {bakeState}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collition");
        if (other.CompareTag("Ingredient"))
        {
            // ��� ��ü ��������
            Ingrediants otherIngredient = other.GetComponent<Ingrediants>();

            // �� �� �������� ��츸 ���� ����
            if (CanCombineWith(otherIngredient))
            {
                CombineIngredients(other.gameObject, otherIngredient);
            }
            else
            {
                Debug.Log("�� ��� �� �ϳ��� ����� �������� �ʾҽ��ϴ�.");
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
            Debug.Log("���� ����! �����: " + resultProduct);
            CreateResultProduct(resultProduct);
        }

        // ��� ����
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
