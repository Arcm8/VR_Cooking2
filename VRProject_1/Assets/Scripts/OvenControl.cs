using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Ingrediants;

public class OvenControl : MonoBehaviour
{
    private GameObject currentBread;

    public GameObject CheesePizza;
    public GameObject CheesePepperoniPizza;

    public Slider cookingSlider;
    public float cookTime = 5f;
    public float burnTime = 3f;
    public Image sliderFillImage;
    public Color cookedColor = Color.yellow;
    public Color burntColor = Color.red;

    private Renderer breadRenderer;

    private bool isCooking = false;
    private bool isCooked = false;
    private bool isBurnt = false;

    private int pizzatype = 0;

    private float cookingProgress = 0f;

    private Ingrediants ingrediants;

    void OnTriggerEnter(Collider other)
    {
        var ingredientComponent = other.GetComponent<Ingrediants>();
        if (ingredientComponent != null && (ingredientComponent.ingredientName == "Dough_Cheese_Pepperoni_Corn" ^ ingredientComponent.ingredientName == "Dough_Cheese") && !isCooking)
        {
            if (ingredientComponent.ingredientName== "Dough_Cheese")
            {
                pizzatype = 1;
            }
            else if (ingredientComponent.ingredientName== "Dough_Cheese_Pepperoni_Corn")
            {
                pizzatype = 2;
            }
            ingrediants = other.GetComponent<Ingrediants>();
            currentBread = other.gameObject;
            isCooking = true;
            cookingProgress = 0f;

            // »§ÀÇ Renderer¸¦ ¹Ì¸® ¹Þ¾Æ¿Â ÈÄ ÀúÀå
            breadRenderer = currentBread.GetComponent<Renderer>();
            Debug.Log("»§ ±Á±â ½ÃÀÛ");
        }
    }

    void OnTriggerExit(Collider other)
    {
        var ingredientComponent = other.GetComponent<Ingrediants>();
        if (ingredientComponent != null && (ingredientComponent.ingredientName == "Dough_Cheese_Pepperoni_Corn" ^ ingredientComponent.ingredientName == "Dough_Cheese") && isCooking)
        {
            pizzatype = 0;
            ingrediants = null;
            isCooking = false;
            currentBread = null;
            cookingProgress = 0f;
            Debug.Log("»§ÀÌ ºüÁ®³ª°¬¾î¿ä");
        }
    }

    void Update()
    {
        if (isCooking && !isCooked)
        {
            cookingProgress += Time.deltaTime;
            cookingSlider.value = Mathf.Clamp01(cookingProgress / cookTime);

           // if (!sizzlingSound.isPlaying)
              //  sizzlingSound.Play();

            if (cookingSlider.value >= 1f)
            {
                //isCooked = true;
                cookingProgress = 0f;
                cookingSlider.value = Mathf.Clamp01(cookingProgress / cookTime);
                sliderFillImage.color = cookedColor;
                ingrediants.SetBakeState(BakeState.Baked);
                if (pizzatype == 1)
                {
                    Instantiate(CheesePizza, transform.position, Quaternion.identity);
                }
                else if (pizzatype == 2)
                {
                    Instantiate(CheesePepperoniPizza, transform.position, Quaternion.identity);
                }
            }
        }
    }
        
}
