using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox : MonoBehaviour
{
    public Material skyboxMaterial;
    public float cycleDuration = 60f; // 전체 주기 시간
    public Color dayColor = new Color(0.5f, 0.7f, 1f);     // 밝은 하늘색
    public Color nightColor = new Color(0.05f, 0.05f, 0.1f); // 어두운 남색

    public Color ambientDayColor = new Color(0.8f, 0.8f, 0.8f);
    public Color ambientNightColor = new Color(0.05f, 0.05f, 0.1f);

    public Light sunLight;

    void Start()
    {
        RenderSettings.skybox = skyboxMaterial;
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time / cycleDuration * Mathf.PI * 2f) * 0.5f) + 0.5f;

        Color skyColor = Color.Lerp(nightColor, dayColor, t);

        // 하늘과 바닥 색을 같은 색으로 설정
        skyboxMaterial.SetColor("_SkyTint", skyColor);
        skyboxMaterial.SetColor("_GroundColor", skyColor);

        // 대기 두께, 노출
        skyboxMaterial.SetFloat("_AtmosphereThickness", Mathf.Lerp(1.2f, 0.4f, t));
        skyboxMaterial.SetFloat("_Exposure", Mathf.Lerp(0.3f, 1.3f, t));

        // 환경광 조정
        RenderSettings.ambientLight = Color.Lerp(ambientNightColor, ambientDayColor, t);

        // 태양 빛 (Directional Light) 조정 (선택)
        if (sunLight != null)
        {
            sunLight.intensity = Mathf.Lerp(0.1f, 1.2f, t);
            sunLight.color = Color.Lerp(new Color(0.2f, 0.2f, 0.3f), Color.white, t);
            sunLight.transform.rotation = Quaternion.Euler(Mathf.Lerp(170f, 50f, t), 30f, 0f);
        }
    }
}
