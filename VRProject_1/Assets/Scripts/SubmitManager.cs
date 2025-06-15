using UnityEngine;
using System.Collections.Generic;


/// 요리 제출을 처리하는 클래스
/// 제출 버튼이 눌렸을 때 지정된 영역에 있는 완성된 요리를 감지
// 현재 주문과 일치하는 경우 주문을 완료 처리한다.

public class SubmitManager : MonoBehaviour
{
    public Transform submitZoneCenter;        // 제출 영역 중심 위치
    public float detectionRadius = 0.5f;       // 감지 반경
    public StageManager stageManager;         // StageManager 참조

    public bool ignoreOrder = true;           // 순서 무시 여부


    /// 제출 버튼이 눌렸을 때 실행되는 함수
    /// 제출 영역에 있는 'Completed' 태그 오브젝트를 검사하고 TAG 옵션
    /// 주문과 일치하면 주문 완료 처리
  
    public void OnSubmitButtonPressed()
    {
        Collider[] hits = Physics.OverlapSphere(submitZoneCenter.position, detectionRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Completed"))
            {
                string resultName = CleanName(hit.name); // 오브젝트 이름에서 "_Completed" 제거
                string expected = stageManager.orderManager.GetCurrentOrder().recipeName;

                Debug.Log("제출된 요리: " + resultName + " / 기대한 요리: " + expected);

                // 순서 무시이거나 이름이 일치하면 성공 처리
                if (ignoreOrder || resultName == expected)
                {
                    stageManager.OnOrderCompleted(); // 주문 완료 처리
                    Destroy(hit.gameObject);         // 제출된 요리 제거
                }
                else
                {
                    Debug.Log("제출 실패 - 잘못된 요리");
                    stageManager.orderManager.SpawnNewOrder();
                }

                return; // 첫 번째 감지된 요리만 처리
            }
        }

        Debug.Log("제출된 요리가 없습니다.");
    }


    /// 오브젝트 이름에서 "_Completed" 접미사를 제거
    
    string CleanName(string objName)
    {
        return objName.Replace("_Completed", "");
    }
}
