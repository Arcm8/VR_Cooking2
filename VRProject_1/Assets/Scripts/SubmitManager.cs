using UnityEngine;
using System.Collections.Generic;


/// �丮 ������ ó���ϴ� Ŭ����
/// ���� ��ư�� ������ �� ������ ������ �ִ� �ϼ��� �丮�� ����
// ���� �ֹ��� ��ġ�ϴ� ��� �ֹ��� �Ϸ� ó���Ѵ�.

public class SubmitManager : MonoBehaviour
{
    public Transform submitZoneCenter;        // ���� ���� �߽� ��ġ
    public float detectionRadius = 0.5f;       // ���� �ݰ�
    public StageManager stageManager;         // StageManager ����

    public bool ignoreOrder = true;           // ���� ���� ����


    /// ���� ��ư�� ������ �� ����Ǵ� �Լ�
    /// ���� ������ �ִ� 'Completed' �±� ������Ʈ�� �˻��ϰ� TAG �ɼ�
    /// �ֹ��� ��ġ�ϸ� �ֹ� �Ϸ� ó��
  
    public void OnSubmitButtonPressed()
    {
        Collider[] hits = Physics.OverlapSphere(submitZoneCenter.position, detectionRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Completed"))
            {
                string resultName = CleanName(hit.name); // ������Ʈ �̸����� "_Completed" ����
                string expected = stageManager.orderManager.GetCurrentOrder().recipeName;

                Debug.Log("����� �丮: " + resultName + " / ����� �丮: " + expected);

                // ���� �����̰ų� �̸��� ��ġ�ϸ� ���� ó��
                if (ignoreOrder || resultName == expected)
                {
                    stageManager.OnOrderCompleted(); // �ֹ� �Ϸ� ó��
                    Destroy(hit.gameObject);         // ����� �丮 ����
                }
                else
                {
                    Debug.Log("���� ���� - �߸��� �丮");
                    stageManager.orderManager.SpawnNewOrder();
                }

                return; // ù ��° ������ �丮�� ó��
            }
        }

        Debug.Log("����� �丮�� �����ϴ�.");
    }


    /// ������Ʈ �̸����� "_Completed" ���̻縦 ����
    
    string CleanName(string objName)
    {
        return objName.Replace("_Completed", "");
    }
}
