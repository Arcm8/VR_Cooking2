using UnityEngine;
using TMPro;
using UnityEngine.UI;


/// ���� UI ������ �����ϴ� Ŭ����. Ÿ�̸�, ����, �������� ����, �ֹ� ����, ��� �г� ���� �����Ѵ�.

public class UIManager : MonoBehaviour
{
    // --- UI ��ҵ� (TextMeshPro �� Unity UI ����) ---
    public TextMeshProUGUI timerText;               // ���� �ð��� ǥ���� �ؽ�Ʈ
    public Image orderImage;                        // ���� �ֹ��� �丮�� �̹���
    public GameObject successPanel;                 // �������� Ŭ���� �� ǥ�õǴ� �г�
    public GameObject failPanel;                    // �������� ���� �� ǥ�õǴ� �г�
    public TextMeshProUGUI orderNameText;           // �ֹ� �丮 �̸��� ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI stageText;               // ���� �������� ��ȣ�� ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI nextStageButtonText;     // ���� �� ��ư�� ǥ�õ� �ؽ�Ʈ ("���� �������� ����")
    public TextMeshProUGUI scoreText;               // �ǽð� ���� ǥ�� �ؽ�Ʈ
    public TextMeshProUGUI scoreResultText;         // �������� ���� �� ����� ǥ�õǴ� �� ���� �ؽ�Ʈ

   
    /// ���� �ð��� �� ������ ǥ���Ѵ�.
 
    public void UpdateTimer(float time)
    {
        int seconds = Mathf.CeilToInt(time);
        timerText.text = "���� �ð�: " + seconds + "��";
    }

    
    /// �ֹ� ������ UI�� ǥ���Ѵ� (�丮 �̸� �� �̹���). ( �̹��� �Ⱦ�)
    
    public void DisplayOrder(OrderData order)
    {
        if (orderNameText != null)
        {
            orderNameText.text = "�ֹ�: " + order.recipeName;
        }

        if (orderImage != null && order.recipeImage != null)
        {
            orderImage.sprite = order.recipeImage;
        }
    }

    
    /// �������� ����� UI�� ǥ���Ѵ�.
    /// ����/���п� ���� �г��� �ٸ��� ǥ���ϸ�, ���� �� �� ������ �Բ� ǥ���Ѵ�.
   
    public void ShowResult(bool isSuccess, int totalScore)
    {
        if (isSuccess)
        {
            nextStageButtonText.text = "���� �������� ����";
            successPanel.SetActive(true);
        }
        else
        {
            failPanel.SetActive(true);
        }

        if (scoreResultText != null)
        {
            scoreResultText.text = "�� ����: " + totalScore.ToString();
        }
    }

    /// ���� �������� ��ȣ�� UI�� �����Ѵ�.

    public void UpdateStageText(int stage)
    {
        if (stageText != null)
        {
            Debug.Log("StageText ������Ʈ: Stage " + stage);
            stageText.text = "Stage " + stage;
        }
        else
        {
            Debug.LogWarning("StageText�� ����Ǿ� ���� ����!");
        }
    }

   
    /// ����/���� �г��� ��� �����. �������� �ʱ�ȭ �� ȣ���.
   
    public void HidePanels()
    {
        successPanel.SetActive(false);
        failPanel.SetActive(false);
    }

    
    /// ���� ���������� �ٽ� �����Ѵ�.
    /// Retry ��ư Ŭ�� �� ȣ���.
   
    public void RetryStage()
    {
       // FindObjectOfType<StageManager>().RetryCurrentStage();
    }

   
    /// �ǽð� ������ UI�� ǥ���Ѵ�.
   
    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = "����: " + score.ToString();
        else
            Debug.LogWarning("scoreText�� ����Ǿ� ���� �ʽ��ϴ�!");
    }
}
