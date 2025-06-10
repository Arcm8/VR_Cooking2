using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Oven_Interaction : MonoBehaviour
{
    public Animator ovenAnimator;              // Animator 컴포넌트
    public XRSimpleInteractable interactable; // XR Simple Interactable

    private bool isAnimating = false;
    private bool isOpen = false; // 문 상태 (열림/닫힘)

    void OnEnable()
    {
        interactable.selectEntered.AddListener(OnSelectEntered);
    }

    void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (isAnimating) return; // 애니메이션 재생 중이면 무시

        StartCoroutine(ToggleDoor());
    }

    private IEnumerator ToggleDoor()
    {
        isAnimating = true;
        interactable.enabled = false;

        if (!isOpen)
        {
            ovenAnimator.SetTrigger("Open");
        }
        else
        {
            ovenAnimator.SetTrigger("Close");
        }

        // 애니메이션 재생 시간 (애니메이션 길이에 맞춰 수정)
        yield return new WaitForSeconds(2f);

        isOpen = !isOpen; // 상태 변경
        interactable.enabled = true;
        isAnimating = false;
    }
}
