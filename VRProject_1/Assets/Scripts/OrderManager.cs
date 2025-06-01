using UnityEngine;
using System.Collections.Generic;


/// ���� �� �ֹ� ���� �� ����

public class OrderManager : MonoBehaviour
{
   
    /// ��ü ������ ������ ��� �ִ� ������ DB
  
    public RecipeDatabase recipeDB;

   
    /// ���� ���� ���� �ֹ� ������
  
    private OrderData currentOrder;

  
    /// UI ǥ�ø� ����ϴ� UI �Ŵ���
    
    public UIManager uiManager;

  
    /// ���� ���� ���� �������� ��ȣ

    private int currentStage = 1;

    /// ���� �������� ��ȣ�� ����

    /// <param name="stage">�������� ��ȣ</param>
    public void SetStage(int stage)
    {
        currentStage = stage;
    }

 
    /// ���� ���������� �´� ���̵��� ���ο� �ֹ��� �����ϰ� UI�� ǥ��

    public void SpawnNewOrder()
    {
        int recipeCount = Mathf.Min(recipeDB.recipes.Count, GetRecipeCountForStage(currentStage));
        currentOrder = recipeDB.recipes[Random.Range(0, recipeCount)];
        Debug.Log("�ֹ� ����: " + currentOrder.recipeName);
        uiManager.DisplayOrder(currentOrder);
    }

    /// ������������ ����� �� �ִ� ������ ������ ��ȯ
    /// <param name="stage">�������� ��ȣ</param>
    /// <returns>������ ����</returns>
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

 
    /// ����� �丮 ��ᰡ ���� �ֹ��� ��Ȯ�� ��ġ�ϴ��� Ȯ�� (���� ����) ( �ϼ� �丮 �̸����� �� ���� )

    /// <param name="submitted">����� ��� ���</param>
    /// <returns>��ġ ����</returns>
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

    /// ���� �ֹ� �����͸� ��ȯ

    /// <returns>���� �ֹ� ������</returns>
    public OrderData GetCurrentOrder() => currentOrder;

  
    /// ����� ��ᰡ ���� �ֹ��� ��ġ�ϴ��� Ȯ�� (���� ���� �ɼ� ����)
 
    /// <param name="submitted">����� ��� ���</param>
    /// <param name="ignoreOrder">��� ���� ���� ����</param>
    /// <returns>��ġ ����</returns>
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
