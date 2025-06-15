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
    private int totalScore = 0;

    [Header("Dependencies")]
    public OrderManager orderManager;
    public UIManager uiManager;

    [Header("Receipt Animation")]
    public Animator receiptAnimator;   // Inspector���� �Ҵ�
    public ReceiptManager receiptManager;    // �ؽ�Ʈ ���ÿ�

    [Header("Audio Clips")]
    public AudioSource audioSource;
    public AudioClip warningClip;
    public AudioClip clearClip;
    public AudioClip failClip;

    
    void Start()
    {
        InitStage(currentStage);
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
            currentTime = 0f;                 // ���� ����
            uiManager.UpdateTimer(0f);        // ȭ�鿡 0�� �����ֱ�
            EndStage(false);                  // ���� ó���� �ϰ�
               }
    }

    public void OnOrderCompleted()
    {
        // �� ���� ���
        orderManager.RecordSuccess();

        // �� ���� ������UI ó��
        int score = orderManager.GetCurrentOrder().score;
        totalScore += score;
        uiManager.UpdateScore(totalScore);

        completedOrders++;
        if (completedOrders >= goalOrders)
            EndStage(true);
        else
            orderManager.SpawnNewOrder();
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
        stageRunning = true;
        warningPlayed = false;
        completedOrders = 0;
        totalScore = 0;

        switch (stage)
        {
            case 1: goalOrders = 2; stageTime = 5f; break;
            case 2: goalOrders = 3; stageTime = 90f; break;
            case 3: goalOrders = 3; stageTime = 90f; break;
            case 4: goalOrders = 4; stageTime = 120f; break;
            case 5: goalOrders = 3; stageTime = 100f; break;
            case 6: goalOrders = 3; stageTime = 120f; break;
        }
        currentTime = stageTime;

        orderManager.SetStage(stage);
        orderManager.SpawnNewOrder();
        uiManager.HidePanels();
        uiManager.UpdateStageText(stage);
        uiManager.UpdateScore(totalScore);
    }

    void EndStage(bool success)
    {
        stageRunning = false;

        // 1) ���� & UI ó����
        if (audioSource != null)
            audioSource.PlayOneShot(success ? clearClip : failClip);
        uiManager.ShowResult(success, totalScore);



        // 2) ������ �ؽ�Ʈ ����
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
        // 3) ������ �ִϸ����� Ʈ����
        receiptAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        receiptAnimator.SetBool("Drop", true);

        // 4) ���� �Ͻ�����
        Time.timeScale = 0f;
    }


    public void RetryCurrentStage()
    {
        Time.timeScale = 1f;
        InitStage(currentStage);
    }
}
