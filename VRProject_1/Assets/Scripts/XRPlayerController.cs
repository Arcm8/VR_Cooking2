using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRPlayerController : MonoBehaviour
{
    public enum XRHandState
    {
        LeftHand, RightHand
    }
    public XRHandState xrHandState = XRHandState.LeftHand; //���� ����

    public XRController controller; //��Ʈ�ѷ�
    public XRBinding[] bindings;

    public GameObject hitObject; //�浹 ���

    private bool isTrigger; //Ʈ���� ���� ��ȣ
    private bool isGrip; //�׸� ���� ��ȣ
    private bool isXButton; //X ��ư ���� ��ȣ
    private bool isYButton; //Y ��ư ���� ��ȣ
    private bool isAButton; //A ��ư ���� ��ȣ
    private bool isBButton; //B ��ư ���� ��ȣ

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       foreach (var binding in bindings)
        {
            binding.Update(controller.inputDevice);
        }
    }

    //XR��Ʈ�ѷ� �Է� �Լ� (Device-based)
    void XRControllerInput()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool trigger))
        {
            //Ʈ���Ÿ� ������ ���
            if (trigger)
            {
                //Ʈ���� ��ȣ�� true�� ���
                if (isTrigger)
                {
                    Debug.Log("Ʈ���� ��ư ������");
                    isTrigger = false;
                }
            }
            //Ʈ���Ÿ� ������ ���
            else
            {
                //Ʈ���� ��ȣ�� true�� ���
                if (!isTrigger)
                {
                    Debug.Log("Ʈ���� ��ư ����");
                    isTrigger = true;
                }
            }
        }
    }
}

public enum XRButton
{
    Trigger, Grip, Primary, PrimaryTouch, Secondary,
    SecondaryTouch, Primary2DAxisClick, Primary2DAxisTouch
}
public enum PressType
{
    Begin, End, Continuous
}

[Serializable]
public class XRBinding
{
    private XRButton button;
    private PressType pressType;
    private UnityEvent onActive;

    bool isPressed;
    bool wasPressed;

    public void Update(InputDevice device)
    {
        device.TryGetFeatureValue(XRStatics.GetFeature(button), out isPressed);
        bool active = false;

        switch(pressType)
        {
            case PressType.Continuous: active = isPressed; break;
            case PressType.Begin: active = isPressed && !wasPressed; break;
            case PressType.End: active = !isPressed && !wasPressed; break;
        }

        if (active) onActive.Invoke();
        wasPressed = isPressed;
    }
}

public static class XRStatics
{
    public static InputFeatureUsage<bool> GetFeature(XRButton button)
    {
        switch (button)
        {
            case XRButton.Trigger: return CommonUsages.triggerButton;
            case XRButton.Grip: return CommonUsages.gripButton;
            case XRButton.Primary: return CommonUsages.primaryButton;
            case XRButton.PrimaryTouch: return CommonUsages.primaryTouch;
            case XRButton.Secondary: return CommonUsages.secondaryButton;
            case XRButton.SecondaryTouch: return CommonUsages.secondaryTouch;
            case XRButton.Primary2DAxisClick: return CommonUsages.primary2DAxisClick;
            case XRButton.Primary2DAxisTouch: return CommonUsages.primary2DAxisTouch;
            default:Debug.LogError("button" + button + "not found");
                return CommonUsages.triggerButton;
        }
    }
}
