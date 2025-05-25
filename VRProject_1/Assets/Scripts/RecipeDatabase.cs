using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RecipeDatabase", menuName = "Cooking/RecipeDatabase")]
public class RecipeDatabase : ScriptableObject
{
    public List<OrderData> recipes;
}
