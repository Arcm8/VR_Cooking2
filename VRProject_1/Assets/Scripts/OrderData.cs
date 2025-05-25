using UnityEngine;
using System.Collections.Generic;

// ScriptableObject로 만들기 위한 속성

[CreateAssetMenu(fileName = "OrderData", menuName = "Cooking/Order")]

public class OrderData : ScriptableObject
{
    public string recipeName;
    public List<string> ingredients; // 순서 중요
    public Sprite recipeImage; // UI에 띄울 이미지 (삭제??)
}
