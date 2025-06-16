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
            if (!hit.CompareTag("Completed"))
                continue;

            string resultName = CleanName(hit.name);
            string expected = stageManager.orderManager.GetCurrentOrder().recipeName;

            Debug.Log($"제출된 요리: {resultName} / 기대한 요리: {expected}");

            //  성공 여부 판단
            if (ignoreOrder || resultName == expected)
            {
                // ? 성공 처리 ?
                stageManager.OnOrderCompleted();
                Destroy(hit.gameObject);
            }
            else
            {
                // ? 실패 처리 ?
                Debug.Log($"제출 실패 - 잘못된 요리: {resultName}");
                stageManager.orderManager.RecordFailure(resultName);  // 실패 기록
                Destroy(hit.gameObject);                             // (선택) 잘못된 요리 제거
                stageManager.orderManager.SpawnNewOrder();           // 다음 주문 생성
            }

            return; // 첫 감지된 하나만 처리
        }

        Debug.Log("제출된 요리가 없습니다.");
    }



    /// 오브젝트 이름에서 "_Completed" 접미사를 제거

    string CleanName(string objName)
    {
        return objName.Replace("_Completed", "");
    }
}
