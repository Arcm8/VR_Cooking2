using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiggingManager : MonoBehaviour
{
    public Transform leftHandIK; //�޼�IK
    public Transform rightHandIK; //������ IK
    public Transform headIK; //�Ӹ� IK

    public Transform leftHandController; //�޼� ��Ʈ�ѷ�
    public Transform rightHandContoller; //������ ��Ʈ�ѷ�
    public Transform hmd; //����

    public Vector3[] leftOffset; //�޼� ������
    public Vector3[] rightOffset; //������ ������
    public Vector3[] headOffset; //�Ӹ� ������

    public float smootValue = 0.1f; //������ ��
    public float modelHeight = 1.6f; //�� Ű


    private void LateUpdate()
    {
        MappingHandTransform(leftHandIK, leftHandController, true);
        MappingHandTransform(rightHandIK, rightHandContoller, false);
        MappingBodyTransform(headIK, hmd);
        MappingHeadTransform(headIK, hmd);
    }

    //��ġ�� �����ϴ� �Լ�(IK����, ��Ʈ�ѷ�, ���� ����)
    void MappingHandTransform(Transform ik, Transform controller, bool isLeft)
    {
        //offset�� �޼�����? ��->leftOffset, ����->rightOffset
        var offset = isLeft ? leftOffset : rightOffset;

        ik.position = controller.TransformPoint(offset[0]);
        ik.rotation = controller.rotation * Quaternion.Euler(offset[1]);
    }

    //���� ���� �Լ�(IK����, ����)
    void MappingBodyTransform(Transform ik, Transform hmd)
    {
        transform.position = new Vector3(hmd.position.x, hmd.position.y - modelHeight, hmd.position.z);
        float yaw = hmd.eulerAngles.y;
        var targetRotation = new Vector3(transform.eulerAngles.x, yaw, transform.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), smootValue);
    }

    //�Ӹ� ���� �Լ�(IK����, ����)
    void MappingHeadTransform(Transform ik, Transform hmd)
    {
        ik.position = hmd.TransformPoint(headOffset[0]);
        ik.rotation = hmd.rotation * Quaternion.Euler(headOffset[1]);
    }
}
