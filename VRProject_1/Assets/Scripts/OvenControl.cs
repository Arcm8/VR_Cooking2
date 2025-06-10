using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Ingrediants;

public class OvenControl : MonoBehaviour
{
    private GameObject currentBread;

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

    public Material defaultMaterial;       // 처음 상태 메터리얼
    public Material cookedMaterial;        // 익은 메터리얼
    public Material burntMaterial;
    private float cookingProgress = 0f;

    private Ingrediants ingrediants;

    void OnTriggerEnter(Collider other)
    {
        var ingredientComponent = other.GetComponent<Ingrediants>();
        if (ingredientComponent != null && ingredientComponent.ingredientName == "Dough" && !isCooking)
        {
            ingrediants = other.GetComponent<Ingrediants>();
            currentBread = other.gameObject;
            isCooking = true;
            cookingProgress = 0f;

            // 빵의 Renderer를 미리 받아온 후 저장
            breadRenderer = currentBread.GetComponent<Renderer>();
            Debug.Log("빵 굽기 시작");
        }
    }

    void OnTriggerExit(Collider other)
    {
        var ingredientComponent = other.GetComponent<Ingrediants>();
        if (ingredientComponent != null && ingredientComponent.ingredientName == "Dough" && !isCooking)
        {
            ingrediants = null;
            isCooking = false;
            currentBread = null;
            cookingProgress = 0f;
            Debug.Log("빵이 빠져나갔어요");
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
                isCooked = true;
                cookingProgress = 0f;
                sliderFillImage.color = cookedColor;

                if (breadRenderer != null && cookedMaterial != null)
                {
                    breadRenderer.material = cookedMaterial;
                }
                ingrediants.SetBakeState(BakeState.Baked);
            }
        }
        else if (isCooking && isCooked && !isBurnt)
        {
            cookingProgress += Time.deltaTime;
            cookingSlider.value = Mathf.Clamp01(cookingProgress / burnTime);
            sliderFillImage.color = Color.Lerp(cookedColor, burntColor, cookingSlider.value);

            if (cookingSlider.value >= 1f)
            {
                isBurnt = true;
               // sizzlingSound.Stop();

                if (breadRenderer != null && burntMaterial != null)
                {
                    breadRenderer.material = burntMaterial;
                }
                ingrediants.SetBakeState(BakeState.Burned);


                // 여기서 게임오버, 실패처리 등 추가 가능
            }
        }
    }
        
}
