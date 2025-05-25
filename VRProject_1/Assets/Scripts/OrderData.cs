using UnityEngine;
using System.Collections.Generic;

// ScriptableObject�� ����� ���� �Ӽ�

[CreateAssetMenu(fileName = "OrderData", menuName = "Cooking/Order")]

public class OrderData : ScriptableObject
{
    public string recipeName;
    public List<string> ingredients; // ���� �߿�
    public Sprite recipeImage; // UI�� ��� �̹��� (����??)
}
