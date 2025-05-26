using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class IngredientCombination
{
    public string ingredientA;
    public string ingredientB;
    public string result;
    public GameObject resultPrefab;
}


public class IngredientCombine : MonoBehaviour
{
    public static IngredientCombine Instance { get; private set; }

    public List<IngredientCombination> combinations;
    private Dictionary<(string, string), IngredientCombination> combinationDict;

    private void Awake()
    {
        // ΩÃ±€≈Ê º≥¡§
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // ¡∂«’ µÒº≈≥ ∏Æ √ ±‚»≠
        combinationDict = new Dictionary<(string, string), IngredientCombination>();

        foreach (var combo in combinations)
        {
            combinationDict[(combo.ingredientA, combo.ingredientB)] = combo;
            combinationDict[(combo.ingredientB, combo.ingredientA)] = combo;
        }
    }

    public IngredientCombination GetCombination(string a, string b)
    {
        combinationDict.TryGetValue((a, b), out var combo);
        return combo;
    }
}
