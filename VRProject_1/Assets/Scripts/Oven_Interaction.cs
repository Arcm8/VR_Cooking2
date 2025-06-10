using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Oven_Interaction : MonoBehaviour
{
    public Animator ovenAnimator;              // Animator ������Ʈ
    public XRSimpleInteractable interactable; // XR Simple Interactable

    private bool isAnimating = false;
    private bool isOpen = false; // �� ���� (����/����)

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
        if (isAnimating) return; // �ִϸ��̼� ��� ���̸� ����

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

        // �ִϸ��̼� ��� �ð� (�ִϸ��̼� ���̿� ���� ����)
        yield return new WaitForSeconds(2f);

        isOpen = !isOpen; // ���� ����
        interactable.enabled = true;
        isAnimating = false;
    }
}
