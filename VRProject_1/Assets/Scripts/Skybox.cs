using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox : MonoBehaviour
{
    public Material skyboxMaterial;
    public Light sunLight;

    public float transitionDuration = 5f;

    [Header("Sky and Ground Colors")]
    public Color dayColor = new Color(0.5f, 0.7f, 1f);
    public Color nightColor = new Color(0.05f, 0.05f, 0.1f);

    [Header("Ambient Lighting")]
    public Color ambientDayColor = new Color(0.8f, 0.8f, 0.8f);
    public Color ambientNightColor = new Color(0.05f, 0.05f, 0.1f);

    private Coroutine transitionRoutine;

    void Start()
    {
        RenderSettings.skybox = skyboxMaterial;
    }

    public void StartNightTransition()
    {
        if (transitionRoutine != null)
            StopCoroutine(transitionRoutine);
        transitionRoutine = StartCoroutine(TransitionToColor(dayColor, nightColor));
    }

    public void StartDayTransition()
    {
        if (transitionRoutine != null)
            StopCoroutine(transitionRoutine);
        transitionRoutine = StartCoroutine(TransitionToColor(nightColor, dayColor));
    }

    private IEnumerator TransitionToColor(Color fromColor, Color toColor)
    {
        float timer = 0f;

        float fromExposure = (fromColor == dayColor) ? 1.3f : 0.3f;
        float toExposure = (toColor == dayColor) ? 1.3f : 0.3f;

        float fromAtmosphere = (fromColor == dayColor) ? 0.4f : 1.2f;
        float toAtmosphere = (toColor == dayColor) ? 0.4f : 1.2f;

        Color ambientFrom = (fromColor == dayColor) ? ambientDayColor : ambientNightColor;
        Color ambientTo = (toColor == dayColor) ? ambientDayColor : ambientNightColor;

        float fromLightIntensity = (fromColor == dayColor) ? 1.2f : 0.1f;
        float toLightIntensity = (toColor == dayColor) ? 1.2f : 0.1f;

        while (timer < transitionDuration)
        {
            float t = timer / transitionDuration;

            Color currentColor = Color.Lerp(fromColor, toColor, t);
            skyboxMaterial.SetColor("_SkyTint", currentColor);
            skyboxMaterial.SetColor("_GroundColor", currentColor);
            skyboxMaterial.SetFloat("_Exposure", Mathf.Lerp(fromExposure, toExposure, t));
            skyboxMaterial.SetFloat("_AtmosphereThickness", Mathf.Lerp(fromAtmosphere, toAtmosphere, t));
            RenderSettings.ambientLight = Color.Lerp(ambientFrom, ambientTo, t);

            if (sunLight != null)
            {
                sunLight.intensity = Mathf.Lerp(fromLightIntensity, toLightIntensity, t);
                sunLight.color = Color.Lerp(nightColor, Color.white, t);
                sunLight.transform.rotation = Quaternion.Euler(Mathf.Lerp(170f, 50f, t), 30f, 0f);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // º¸Á¤
        skyboxMaterial.SetColor("_SkyTint", toColor);
        skyboxMaterial.SetColor("_GroundColor", toColor);
        skyboxMaterial.SetFloat("_Exposure", toExposure);
        skyboxMaterial.SetFloat("_AtmosphereThickness", toAtmosphere);
        RenderSettings.ambientLight = ambientTo;
        if (sunLight != null)
        {
            sunLight.intensity = toLightIntensity;
        }
    }
}
