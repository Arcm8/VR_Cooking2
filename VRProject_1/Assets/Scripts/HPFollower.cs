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
        // ī�޶� ���ϵ��� ȸ��
        transform.forward = cam.transform.forward;
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}
