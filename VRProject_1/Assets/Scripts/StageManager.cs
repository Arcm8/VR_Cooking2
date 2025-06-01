using UnityEngine;


/// �������� ������ �����ϴ� Ŭ����
/// - Ÿ�̸�, ����, �ֹ� �Ϸ� �� üũ
/// - ����/���� �Ǵ� �� ��� UI ȣ��

public class StageManager : MonoBehaviour
{
    public float stageTime = 60f;          // �������� ���� �ð�
    public int goalOrders = 3;             // �������� �� ��ǥ �ֹ� ��
    public int currentStage = 1;           // ���� �������� ��ȣ
    public int maxStage = 6;               // �ִ� �������� ��

    private float currentTime;             // ���� �ð�
    private int completedOrders = 0;       // �Ϸ�� �ֹ� ��
    private bool stageRunning = true;      // �������� ���� ����
    private bool warningPlayed = false;    // 10�� ����� ��� ����
    private int totalScore = 0;            // ���� ����

    public OrderManager orderManager;      // �ֹ� ���� ��ũ��Ʈ
    public UIManager uiManager;            // UI ������Ʈ ��ũ��Ʈ

    // ȿ���� ����
    public AudioSource audioSource;
    public AudioClip failClip;
    public AudioClip clearClip;
    public AudioClip warningClip;

    void Start()
    {
        InitStage(currentStage); // ���� �� �������� �ʱ�ȭ
    }

    void Update()
    {
        if (!stageRunning) return;

        currentTime -= Time.deltaTime;
        uiManager.UpdateTimer(currentTime); // Ÿ�̸� UI ����

        // 10�� ������ �� ����� 1ȸ ���
        if (!warningPlayed && currentTime <= 10f)
        {
            if (warningClip != null && audioSource != null)
            {
                Debug.Log(" �ð� �Ҹ� ��� �õ�");
                audioSource.PlayOneShot(warningClip);
                warningPlayed = true;
            }
        }

        // �ð� ���� �� ���� ó��
        if (currentTime <= 0f)
        {
            EndStage(false);
        }
    }

    /// �ֹ��� ���������� �Ϸ�Ǿ��� �� ȣ���
    /// ���� ����, �Ϸ� �� üũ, ���� �ֹ� ȣ��
  
    public void OnOrderCompleted()
    {
        int score = orderManager.GetCurrentOrder().score;
        totalScore += score;

        Debug.Log($"�ֹ� �Ϸ�! ����: +{score}, �� ����: {totalScore}");

        uiManager.UpdateScore(totalScore); // ���� UI ����

        completedOrders++;
        if (completedOrders >= goalOrders)
        {
            EndStage(true); // ��ǥ �ֹ� �� �޼� �� ���� ó��
        }
        else
        {
            orderManager.SpawnNewOrder(); // ���� �ֹ� ����
        }
    }

    /// "���� ��������" ��ư Ŭ�� �� ȣ��
   
    public void OnClickNextStage()
    {
        if (currentStage < maxStage)
        {
            currentStage++;
            Time.timeScale = 1f; // �ð� �簳
            InitStage(currentStage); // ���� �������� �ʱ�ȭ
        }
        else
        {
            Debug.Log(" ��� �������� �Ϸ�!");
        }
    }

    
    /// �������� �ʱ�ȭ - Ÿ�̸�, ����, �г� �� ����
   
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
        uiManager.UpdateScore(totalScore); // ���� �ʱ�ȭ
    }

    
    /// �������� ���� ó�� - ���� ���ο� ���� ��� UI �� ȿ����
   
    void EndStage(bool success)
    {
        stageRunning = false;

        if (audioSource != null)
        {
            if (success && clearClip != null)
            {
                Debug.Log(" ���� �Ҹ� ��� �õ�");
                audioSource.PlayOneShot(clearClip);
            }
            else if (!success && failClip != null)
            {
                Debug.Log(" ���� �Ҹ� ��� �õ�");
                audioSource.PlayOneShot(failClip);
            }
        }

        uiManager.ShowResult(success, totalScore); // ��� UI ȣ��
        Time.timeScale = 0f; // �Ͻ�����
    }

 
    /// ���� �������� �����
    
    public void RetryCurrentStage()
    {
        Debug.Log(" ���� �������� �����");
        Time.timeScale = 1f;
        InitStage(currentStage);
    }
}
