using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUIManager : MonoBehaviour
{

    public GameObject UpgradeUI1;
    public GameObject UpgradeUI2;

    // Start is called before the first frame update
    void Start()
    {
        UpgradeUI1.SetActive(false);
        UpgradeUI2.SetActive(false);
    }

    public void UpOn()
    {
        UpgradeUI1.SetActive(true);
        UpgradeUI2.SetActive(true);
    }
       public void UpOff()
    {
        UpgradeUI1.SetActive(false);
        UpgradeUI2.SetActive(false);
    }




}
