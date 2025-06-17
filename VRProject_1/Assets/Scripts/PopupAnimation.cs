using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
    }
    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
