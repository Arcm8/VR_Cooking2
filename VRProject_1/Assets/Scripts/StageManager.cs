using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("Stage Settings")]
    public float stageTime = 60f;
    public int goalOrders = 3;
    public int currentStage = 1;
    public int maxStage = 6;

    private float currentTime;
    private int completedOrders = 0;
    private bool stageRunning = true;
    private bool warningPlayed = false;
    private int stageScore = 0;     // 이 스테이지에서만 쌓이는 점수
    private int totalScore = 0;     // 스테이지를 넘어가도 누적되는 총점수
    [Header("Dependencies")]
    public OrderManager orderManager;
    public UIManager uiManager;

    [Header("Receipt Animation")]
    public Animator receiptAnimator;   // Inspector에서 할당
    public ReceiptManager receiptManager;    // 텍스트 세팅용

    [Header("Audio Clips")]
    public AudioSource audioSource;
    public AudioClip warningClip;
    public AudioClip clearClip;
    public AudioClip failClip;

    
    void Start()
    {
        // 여기서만 게임 전체를 0으로 세팅
        totalScore = 0;
        InitStage(currentStage);
        //에디터로 실험
       //TestFirstSubmission();
    }

    void Update()
    {
        if (!stageRunning) return;

        currentTime -= Time.deltaTime;
        uiManager.UpdateTimer(currentTime);

        if (!warningPlayed && currentTime <= 10f)
        {
            audioSource?.PlayOneShot(warningClip);
            warningPlayed = true;
        }

       
           if (currentTime <= 0f)
               {
            currentTime = 0f;                 // 음수 방지
            uiManager.UpdateTimer(0f);        // 화면에 0초 보여주기
            EndStage(false);                  // 실패 처리만 하고
               }
    }


 
    // ContextMenu를 통해 인스펙터 우클릭으로 실행 가능.
    // 첫 번째 제출만 했을 때 completedOrders가 1로 오르는지,
    // EndStage가 호출되는지 로그로 확인합니다.

    [ContextMenu("Test First Submission")]
    void TestFirstSubmission()
    {
        Debug.Log("=== TestFirstSubmission 시작 ===");
        Debug.Log($"BEFORE → completedOrders={completedOrders}, goalOrders={goalOrders}");
        OnOrderCompleted();
        Debug.Log($"AFTER  → completedOrders={completedOrders}, goalOrders={goalOrders}");
        Debug.Log("=== TestFirstSubmission 끝 ===");
    }
    /// Retry 버튼 누른 것처럼 스테이지 재시작 테스트
    [ContextMenu("Debug: Retry Current Stage")]
    void DebugRetryCurrentStage()
    {
        Debug.Log(">>> DebugRetryCurrentStage 호출!");
        RetryCurrentStage();
    }

    /// Next Stage 버튼 누른 것처럼 다음 스테이지 진입 테스트
    [ContextMenu("Debug: Go To Next Stage")]
    void DebugGoToNextStage()
    {
        Debug.Log(">>> DebugGoToNextStage 호출!");
        OnClickNextStage();
    }

    public void OnOrderCompleted()
    {
        // ① 성공 기록
        orderManager.RecordSuccess();

        // ② 기존 점수·UI 처리
        int score = orderManager.GetCurrentOrder().score;

        // ② 스테이지 점수와 총점수에 모두 더함
        stageScore += score;
        totalScore += score;


        // ④ 기존 로직: 성공 기록, completedOrders 증가 등
        
        uiManager.UpdateScore(totalScore); // (선택) 기존 UpdateScore는 총점 표시용으로 남겨둡니다.


        //  UI 갱신Score);
        uiManager.UpdateScore(stageScore);
        // 디버그: 브랜치 전후 값 확인
        Debug.Log($"[Debug OnOrderCompleted BEFORE] completedOrders={completedOrders}, goalOrders={goalOrders}");


        completedOrders++;

        Debug.Log($"[Debug OnOrderCompleted AFTER] completedOrders={completedOrders}, goalOrders={goalOrders}");



        if (completedOrders >= goalOrders)
        {
            Debug.Log("[Debug OnOrderCompleted] 조건 충족! EndStage(true) 호출");
            EndStage(true);
        }
        else
        {
            Debug.Log("[Debug OnOrderCompleted] 아직 목표치 미달, SpawnNewOrder 호출");
            orderManager.SpawnNewOrder();
        }
    }

    public void OnClickNextStage()
    {
        if (currentStage < maxStage)
        {
            currentStage++;
            Time.timeScale = 1f;
            InitStage(currentStage);
        }
    }

    void InitStage(int stage)
    {
        // 0) 스테이지 진입할 때만 초기화
        completedOrders = 0;
        stageScore = 0;    // 스테이지 점수 리셋
        // ※ totalScore는 건드리지 않음

        orderManager.ResetRecords();
        // ────── ① 영수증 리셋: 항상 올라간 상태로 세팅 ──────
        if (receiptAnimator != null)
        {
            // timeScale이 1인 동안엔 정상 재생 모드로
            receiptAnimator.updateMode = AnimatorUpdateMode.Normal;
            // Drop 파라미터를 false로 해서 “올려진” 상태로
            receiptAnimator.SetBool("Drop", false);
        }
        stageRunning = true;
        warningPlayed = false;
        completedOrders = 0;
        

        switch (stage)
        {
            case 1: goalOrders = 1; stageTime = 30f; break;   //임시로 하나로 수정
            case 2: goalOrders = 1; stageTime = 90f; break;
            case 3: goalOrders = 1; stageTime = 90f; break;
            case 4: goalOrders = 1; stageTime = 120f; break;
            case 5: goalOrders = 1; stageTime = 100f; break;
            case 6: goalOrders = 1; stageTime = 120f; break;
        }
        currentTime = stageTime;
        Debug.Log($"[Debug InitStage] stage={stage}, goalOrders={goalOrders}, stageTime={stageTime}");

        orderManager.SetStage(stage);
        orderManager.SpawnNewOrder();
        uiManager.HidePanels();
        uiManager.UpdateStageText(stage);
        
        uiManager.UpdateScore(stageScore);
    }

    void EndStage(bool success)
    {
        stageRunning = false;


        // ─── 시간 초과(=success==false) 시점에도 실패를 기록 ───
        if (!success)
        {
            // 현재 주문이 해결되지 못한 채 남아 있으므로 한 번 실패 로깅
            string timedOutRecipe = orderManager.GetCurrentOrder().recipeName;
            orderManager.RecordFailure(timedOutRecipe);
        }

        // 1) 사운드 & UI 처리…
        if (audioSource != null)
            audioSource.PlayOneShot(success ? clearClip : failClip);
        uiManager.ShowResult(success, totalScore);



        // 2) 영수증 텍스트 세팅
        int failCount = goalOrders - completedOrders;
        var successList = orderManager.GetSuccessNames();
        var failList = orderManager.GetFailNames();
        receiptManager.SetTexts(
          completedOrders,
          goalOrders - completedOrders,
          totalScore,
          successList,
          failList
        );
        // 3) 영수증 애니메이터 트리거
        receiptAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        receiptAnimator.SetBool("Drop", true);

        // 4) 게임 일시정지
        //Time.timeScale = 0f;
    }


    public void RetryCurrentStage()
    {
        // ▶ 현재 스테이지에서 쌓인 점수를 총점수에서 차감
        totalScore -= stageScore;
        if (totalScore < 0) totalScore = 0;  // 안전장치
        Time.timeScale = 1f;
        InitStage(currentStage);
    }
}
