using UnityEngine;

public class StageManager : MonoBehaviour
{
    public float stageTime = 60f;
    public int goalOrders = 3;
    public int currentStage = 1;
    public int maxStage = 6;

    private float currentTime;
    private int completedOrders = 0;
    private bool stageRunning = true;

    public OrderManager orderManager;
    public UIManager uiManager;

    void Start()
    {
        InitStage(currentStage); //  �ʱ� �������� ����
    }

    void Update()
    {
        if (!stageRunning) return;

        currentTime -= Time.deltaTime;
        uiManager.UpdateTimer(currentTime);

        if (currentTime <= 0f)
        {
            EndStage(false);
        }
    }

    public void OnOrderCompleted()
    {
        completedOrders++;

        if (completedOrders >= goalOrders)
        {
            EndStage(true);
        }
        else
        {
            orderManager.SpawnNewOrder();
        }
    }

    public void OnClickNextStage()
    {
        if (currentStage < maxStage)
        {
            currentStage++;
            InitStage(currentStage); //  �� ���ε� ���� ���� ����������
        }
        else
        {
            Debug.Log(" ��� �������� �Ϸ�!");
        }
    }

    void InitStage(int stage)
    {
        Debug.Log(" Init Stage " + stage);

        uiManager.UpdateStageText(stage);

        // ���������� ����
        switch (stage)
        {
            case 1:
                goalOrders = 2;
                stageTime = 60f;
                break;
            case 2:
                goalOrders = 3;
                stageTime = 75f;
                break;
            case 3:
                goalOrders = 4;
                stageTime = 90f;
                break;
            case 4:
                goalOrders = 5;
                stageTime = 105f;
                break;
            case 5:
                goalOrders = 6;
                stageTime = 120f;
                break;
            case 6:
                goalOrders = 7;
                stageTime = 135f;
                break;
            default:
                goalOrders = 2;
                stageTime = 60f;
                break;
        }

        completedOrders = 0;
        currentTime = stageTime;
        stageRunning = true;

        orderManager.SetStage(stage);
        orderManager.SpawnNewOrder();

        uiManager.HidePanels(); //  Success/Fail �г� �����
    }

    void EndStage(bool success)
    {
        stageRunning = false;
        uiManager.ShowResult(success);
        Time.timeScale = 0f;
    }
    public void RetryCurrentStage()
    {
        Debug.Log(" ���� �������� �����");

        Time.timeScale = 1f;
        InitStage(currentStage);
    }

}
