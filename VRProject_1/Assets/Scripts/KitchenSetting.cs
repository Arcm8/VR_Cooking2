using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenSetting : MonoBehaviour
{
    public Animator targetAnimator;   // 대상 오브젝트의 Animator
    public string parameterName;      // 조작할 bool 파라미터 이름

    public void SetBoolTrue()
    {
        if (targetAnimator != null)
            targetAnimator.SetBool(parameterName, true);
    }

    public void SetBoolFalse()
    {
        if (targetAnimator != null)
            targetAnimator.SetBool(parameterName, false);
    }

    public void ToggleBool()
    {
        if (targetAnimator != null)
        {
            bool current = targetAnimator.GetBool(parameterName);
            targetAnimator.SetBool(parameterName, !current);
        }
    }
}
