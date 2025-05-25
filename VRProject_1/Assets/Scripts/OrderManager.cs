using UnityEngine;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public RecipeDatabase recipeDB;
    private OrderData currentOrder;
    public UIManager uiManager;

    private int currentStage = 1;

    public void SetStage(int stage)
    {
        currentStage = stage;
    }

    public void SpawnNewOrder()
    {
        int recipeCount = Mathf.Min(recipeDB.recipes.Count, GetRecipeCountForStage(currentStage));
        currentOrder = recipeDB.recipes[Random.Range(0, recipeCount)];
        Debug.Log(" 주문 생성: " + currentOrder.recipeName);
        uiManager.DisplayOrder(currentOrder);
    }

    private int GetRecipeCountForStage(int stage)
    {
        switch (stage)
        {
            case 1: return 3;
            case 2: return 5;
            case 3: return 7;
            case 4: return 10;
            case 5: return 14;
            case 6: return 18;
            default: return 3;
        }
    }

    public bool CheckOrder(List<string> submitted)
    {
        if (submitted.Count != currentOrder.ingredients.Count) return false;

        for (int i = 0; i < submitted.Count; i++)
        {
            if (submitted[i] != currentOrder.ingredients[i])
                return false;
        }
        return true;
    }

    public OrderData GetCurrentOrder() => currentOrder;
}
