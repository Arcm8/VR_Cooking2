using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{

    public GameObject NextSTGEffect;
    public GameObject UpgradeEffect;
    public GameObject NextSTGText;



    public void StageStart()
    {
        NextSTGEffect.SetActive(false);
        UpgradeEffect.SetActive(false);
        NextSTGText.SetActive(false);
    }

    public void NextSTG()
    {
        NextSTGEffect.SetActive(true);
        UpgradeEffect.SetActive(true);
        NextSTGText.SetActive(true);
    }
}
