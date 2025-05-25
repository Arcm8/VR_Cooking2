using UnityEngine;
using System.Collections.Generic;

public class CookingSlot : MonoBehaviour
{
    public OrderManager orderManager;
    public StageManager stageManager;

    public List<string> selectedIngredients = new List<string>();

    public void AddIngredient(string name)
    {
        selectedIngredients.Add(name);
    }

    public void SubmitDish()
    {
        bool correct = orderManager.CheckOrder(selectedIngredients);
        if (correct)
        {
            stageManager.OnOrderCompleted();
        }

        selectedIngredients.Clear(); // √ ±‚»≠
    }
}
