using UnityEngine;
using System.Collections.Generic;


/// 게임 내 주문 생성 및 검증

public class OrderManager : MonoBehaviour
{
   
    /// 전체 레시피 정보를 담고 있는 레시피 DB
  
    public RecipeDatabase recipeDB;

   
    /// 현재 진행 중인 주문 데이터
  
    private OrderData currentOrder;

  
    /// UI 표시를 담당하는 UI 매니저
    
    public UIManager uiManager;

  
    /// 현재 진행 중인 스테이지 번호

    private int currentStage = 1;

    /// 현재 스테이지 번호를 설정

    /// <param name="stage">스테이지 번호</param>
    public void SetStage(int stage)
    {
        currentStage = stage;
    }

 
    /// 현재 스테이지에 맞는 난이도의 새로운 주문을 생성하고 UI에 표시

    public void SpawnNewOrder()
    {
        int recipeCount = Mathf.Min(recipeDB.recipes.Count, GetRecipeCountForStage(currentStage));
        currentOrder = recipeDB.recipes[Random.Range(0, recipeCount)];
        Debug.Log("주문 생성: " + currentOrder.recipeName);
        uiManager.DisplayOrder(currentOrder);
    }

    /// 스테이지별로 사용할 수 있는 레시피 개수를 반환
    /// <param name="stage">스테이지 번호</param>
    /// <returns>레시피 개수</returns>
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

 
    /// 제출된 요리 재료가 현재 주문과 정확히 일치하는지 확인 (순서 포함) ( 완성 요리 이름으로 할 예정 )

    /// <param name="submitted">제출된 재료 목록</param>
    /// <returns>일치 여부</returns>
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

    /// 현재 주문 데이터를 반환

    /// <returns>현재 주문 데이터</returns>
    public OrderData GetCurrentOrder() => currentOrder;

  
    /// 제출된 재료가 현재 주문과 일치하는지 확인 (순서 무시 옵션 지원)
 
    /// <param name="submitted">제출된 재료 목록</param>
    /// <param name="ignoreOrder">재료 순서 무시 여부</param>
    /// <returns>일치 여부</returns>
    public bool CheckOrder(List<string> submitted, bool ignoreOrder = false)
    {
        var expected = currentOrder.ingredients;

        if (submitted.Count != expected.Count) return false;

        if (ignoreOrder)
        {
            var set1 = new HashSet<string>(submitted);
            var set2 = new HashSet<string>(expected);
            return set1.SetEquals(set2);
        }
        else
        {
            for (int i = 0; i < submitted.Count; i++)
            {
                if (submitted[i] != expected[i])
                    return false;
            }
            return true;
        }
    }
}
