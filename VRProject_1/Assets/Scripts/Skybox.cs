using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox : MonoBehaviour
{
    public Material skyboxMaterial;
    public float cycleDuration = 60f; // ��ü �ֱ� �ð�
    public Color dayColor = new Color(0.5f, 0.7f, 1f);     // ���� �ϴû�
    public Color nightColor = new Color(0.05f, 0.05f, 0.1f); // ��ο� ����

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

        // �ϴð� �ٴ� ���� ���� ������ ����
        skyboxMaterial.SetColor("_SkyTint", skyColor);
        skyboxMaterial.SetColor("_GroundColor", skyColor);

        // ��� �β�, ����
        skyboxMaterial.SetFloat("_AtmosphereThickness", Mathf.Lerp(1.2f, 0.4f, t));
        skyboxMaterial.SetFloat("_Exposure", Mathf.Lerp(0.3f, 1.3f, t));

        // ȯ�汤 ����
        RenderSettings.ambientLight = Color.Lerp(ambientNightColor, ambientDayColor, t);

        // �¾� �� (Directional Light) ���� (����)
        if (sunLight != null)
        {
            sunLight.intensity = Mathf.Lerp(0.1f, 1.2f, t);
            sunLight.color = Color.Lerp(new Color(0.2f, 0.2f, 0.3f), Color.white, t);
            sunLight.transform.rotation = Quaternion.Euler(Mathf.Lerp(170f, 50f, t), 30f, 0f);
        }
    }
}
