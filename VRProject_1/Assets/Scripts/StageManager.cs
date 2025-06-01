using UnityEngine;


/// 스테이지 진행을 관리하는 클래스
/// - 타이머, 점수, 주문 완료 수 체크
/// - 성공/실패 판단 및 결과 UI 호출

public class StageManager : MonoBehaviour
{
    public float stageTime = 60f;          // 스테이지 제한 시간
    public int goalOrders = 3;             // 스테이지 당 목표 주문 수
    public int currentStage = 1;           // 현재 스테이지 번호
    public int maxStage = 6;               // 최대 스테이지 수

    private float currentTime;             // 남은 시간
    private int completedOrders = 0;       // 완료된 주문 수
    private bool stageRunning = true;      // 스테이지 진행 여부
    private bool warningPlayed = false;    // 10초 경고음 재생 여부
    private int totalScore = 0;            // 누적 점수

    public OrderManager orderManager;      // 주문 관리 스크립트
    public UIManager uiManager;            // UI 업데이트 스크립트

    // 효과음 관련
    public AudioSource audioSource;
    public AudioClip failClip;
    public AudioClip clearClip;
    public AudioClip warningClip;

    void Start()
    {
        InitStage(currentStage); // 시작 시 스테이지 초기화
    }

    void Update()
    {
        if (!stageRunning) return;

        currentTime -= Time.deltaTime;
        uiManager.UpdateTimer(currentTime); // 타이머 UI 갱신

        // 10초 남았을 때 경고음 1회 재생
        if (!warningPlayed && currentTime <= 10f)
        {
            if (warningClip != null && audioSource != null)
            {
                Debug.Log(" 시간 소리 재생 시도");
                audioSource.PlayOneShot(warningClip);
                warningPlayed = true;
            }
        }

        // 시간 종료 시 실패 처리
        if (currentTime <= 0f)
        {
            EndStage(false);
        }
    }

    /// 주문이 성공적으로 완료되었을 때 호출됨
    /// 점수 누적, 완료 수 체크, 다음 주문 호출
  
    public void OnOrderCompleted()
    {
        int score = orderManager.GetCurrentOrder().score;
        totalScore += score;

        Debug.Log($"주문 완료! 점수: +{score}, 총 점수: {totalScore}");

        uiManager.UpdateScore(totalScore); // 점수 UI 갱신

        completedOrders++;
        if (completedOrders >= goalOrders)
        {
            EndStage(true); // 목표 주문 수 달성 시 성공 처리
        }
        else
        {
            orderManager.SpawnNewOrder(); // 다음 주문 생성
        }
    }

    /// "다음 스테이지" 버튼 클릭 시 호출
   
    public void OnClickNextStage()
    {
        if (currentStage < maxStage)
        {
            currentStage++;
            Time.timeScale = 1f; // 시간 재개
            InitStage(currentStage); // 다음 스테이지 초기화
        }
        else
        {
            Debug.Log(" 모든 스테이지 완료!");
        }
    }

    
    /// 스테이지 초기화 - 타이머, 점수, 패널 등 리셋
   
    void InitStage(int stage)
    {
        currentTime = stageTime;
        stageRunning = true;
        completedOrders = 0;
        totalScore = 0;
        warningPlayed = false;
        switch (stage)
        {
            case 1: goalOrders = 2; break;
            case 2: goalOrders = 3; break;
            case 3: goalOrders = 3; break;
            case 4: goalOrders = 4; break;
            case 5: goalOrders = 3; break;
            case 6: goalOrders = 3; break;
            default: goalOrders = 3; break;
        }
        switch (stage)
        {
            case 1: stageTime = 60f; break;
            case 2: stageTime = 90f; break;
            case 3: stageTime = 90f; break;
            case 4: stageTime = 120f; break;
            case 5: stageTime = 100f; break;
            case 6: stageTime = 120f; break;
        }
        currentTime = stageTime;
        orderManager.SetStage(stage);
        orderManager.SpawnNewOrder();

        uiManager.HidePanels();
        uiManager.UpdateStageText(stage);
        uiManager.UpdateScore(totalScore); // 점수 초기화
    }

    
    /// 스테이지 종료 처리 - 성공 여부에 따라 결과 UI 및 효과음
   
    void EndStage(bool success)
    {
        stageRunning = false;

        if (audioSource != null)
        {
            if (success && clearClip != null)
            {
                Debug.Log(" 성공 소리 재생 시도");
                audioSource.PlayOneShot(clearClip);
            }
            else if (!success && failClip != null)
            {
                Debug.Log(" 실패 소리 재생 시도");
                audioSource.PlayOneShot(failClip);
            }
        }

        uiManager.ShowResult(success, totalScore); // 결과 UI 호출
        Time.timeScale = 0f; // 일시정지
    }

 
    /// 현재 스테이지 재시작
    
    public void RetryCurrentStage()
    {
        Debug.Log(" 현재 스테이지 재시작");
        Time.timeScale = 1f;
        InitStage(currentStage);
    }
}
