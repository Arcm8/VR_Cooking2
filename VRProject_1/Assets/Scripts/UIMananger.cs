using UnityEngine;
using TMPro;
using UnityEngine.UI;


/// 게임 UI 전반을 관리하는 클래스. 타이머, 점수, 스테이지 정보, 주문 정보, 결과 패널 등을 제어한다.

public class UIManager : MonoBehaviour
{
    // --- UI 요소들 (TextMeshPro 및 Unity UI 연결) ---
    public TextMeshProUGUI timerText;               // 남은 시간을 표시할 텍스트
    public Image orderImage;                        // 현재 주문된 요리의 이미지
    public GameObject successPanel;                 // 스테이지 클리어 시 표시되는 패널
    public GameObject failPanel;                    // 스테이지 실패 시 표시되는 패널
    public TextMeshProUGUI orderNameText;           // 주문 요리 이름을 표시할 텍스트
    public TextMeshProUGUI stageText;               // 현재 스테이지 번호를 표시할 텍스트
    public TextMeshProUGUI nextStageButtonText;     // 성공 시 버튼에 표시될 텍스트 ("다음 스테이지 시작")
    public TextMeshProUGUI scoreText;               // 실시간 점수 표시 텍스트
    public TextMeshProUGUI scoreResultText;         // 스테이지 종료 후 결과로 표시되는 총 점수 텍스트

   
    /// 남은 시간을 초 단위로 표시한다.
 
    public void UpdateTimer(float time)
    {
        int seconds = Mathf.CeilToInt(time);
        timerText.text = "남은 시간: " + seconds + "초";
    }

    
    /// 주문 정보를 UI에 표시한다 (요리 이름 및 이미지). ( 이미지 안씀)
    
    public void DisplayOrder(OrderData order)
    {
        if (orderNameText != null)
        {
            orderNameText.text = "주문: " + order.recipeName;
        }

        if (orderImage != null && order.recipeImage != null)
        {
            orderImage.sprite = order.recipeImage;
        }
    }

    
    /// 스테이지 결과를 UI에 표시한다.
    /// 성공/실패에 따라 패널을 다르게 표시하며, 성공 시 총 점수를 함께 표시한다.
   
    public void ShowResult(bool isSuccess, int totalScore)
    {
        if (isSuccess)
        {
            nextStageButtonText.text = "다음 스테이지 시작";
            successPanel.SetActive(true);
        }
        else
        {
            failPanel.SetActive(true);
        }

        if (scoreResultText != null)
        {
            scoreResultText.text = "총 점수: " + totalScore.ToString();
        }
    }

    /// 현재 스테이지 번호를 UI에 갱신한다.

    public void UpdateStageText(int stage)
    {
        if (stageText != null)
        {
            Debug.Log("StageText 업데이트: Stage " + stage);
            stageText.text = "Stage " + stage;
        }
        else
        {
            Debug.LogWarning("StageText가 연결되어 있지 않음!");
        }
    }

   
    /// 성공/실패 패널을 모두 숨긴다. 스테이지 초기화 시 호출됨.
   
    public void HidePanels()
    {
        successPanel.SetActive(false);
        failPanel.SetActive(false);
    }

    
    /// 현재 스테이지를 다시 시작한다.
    /// Retry 버튼 클릭 시 호출됨.
   
    public void RetryStage()
    {
       // FindObjectOfType<StageManager>().RetryCurrentStage();
    }

   
    /// 실시간 점수를 UI에 표시한다.
   
    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = "점수: " + score.ToString();
        else
            Debug.LogWarning("scoreText가 연결되어 있지 않습니다!");
    }
}
