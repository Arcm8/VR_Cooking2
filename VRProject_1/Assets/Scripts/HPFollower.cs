using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPFollower : MonoBehaviour
{

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        // 카메라를 향하도록 회전
        transform.forward = cam.transform.forward;
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}
