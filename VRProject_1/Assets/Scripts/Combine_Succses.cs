using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject starEffectPrefab;
    public Transform spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(starEffectPrefab, spawnPoint.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
