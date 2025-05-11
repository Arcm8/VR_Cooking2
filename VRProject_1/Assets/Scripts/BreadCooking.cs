using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BreadCooking : MonoBehaviour
{
    public Slider cookingSlider;
    public float cookTime = 5f;
    public float burnTime = 3f;
    public AudioSource sizzlingSound;
    public Image sliderFillImage;
    public Color cookedColor = Color.yellow;
    public Color burntColor = Color.red;

    public Material defaultMaterial;       // 처음 상태 메터리얼
    public Material cookedMaterial;        // 익은 메터리얼
    public Material burntMaterial;         // 탄 메터리얼

    private bool isCooking = false;
    private bool isCooked = false;
    private bool isBurnt = false;
    private float cookingProgress = 0f;

    private Renderer breadRenderer;

    void Start()
    {
        breadRenderer = GetComponent<Renderer>();
        if (breadRenderer != null && defaultMaterial != null)
        {
            breadRenderer.material = defaultMaterial;
        }
    }

    void Update()
    {
        if (isCooking && !isCooked)
        {
            cookingProgress += Time.deltaTime;
            cookingSlider.value = Mathf.Clamp01(cookingProgress / cookTime);

            if (!sizzlingSound.isPlaying)
                sizzlingSound.Play();

            if (cookingSlider.value >= 1f)
            {
                isCooked = true;
                cookingProgress = 0f;
                sliderFillImage.color = cookedColor;

                if (breadRenderer != null && cookedMaterial != null)
                {
                    breadRenderer.material = cookedMaterial;
                }
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
                sizzlingSound.Stop();

                if (breadRenderer != null && burntMaterial != null)
                {
                    breadRenderer.material = burntMaterial;
                }

                // 여기서 게임오버, 실패처리 등 추가 가능
            }
        }
        else
        {
            sizzlingSound.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FryingPan"))
        {
            isCooking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FryingPan"))
        {
            isCooking = false;
        }
    }
}
