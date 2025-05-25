using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Image orderImage;
    public GameObject successPanel;
    public GameObject failPanel;
    public TextMeshProUGUI orderNameText;
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI nextStageButtonText;
    public void UpdateTimer(float time)
    {
        int seconds = Mathf.CeilToInt(time);
        timerText.text = "���� �ð�: " + seconds + "��";
    }

    public void DisplayOrder(OrderData order)
    {
        if (orderNameText != null)
        {
            orderNameText.text = "�ֹ�: " + order.recipeName;
        }

        // (����) �̹����� ������ �̹����� ǥ��
        if (orderImage != null && order.recipeImage != null)
        {
            orderImage.sprite = order.recipeImage;
        }
    }


    public void ShowResult(bool isSuccess)
    {
        if (isSuccess)
        {
            nextStageButtonText.text = "���� �������� ����";
            successPanel.SetActive(true);
        }
        else
            failPanel.SetActive(true);
    }
    public void UpdateStageText(int stage)
    {
        if (stageText != null)
        {
            Debug.Log(" StageText ������Ʈ: Stage " + stage);
            stageText.text = "Stage " + stage;
        }
        else
        {
            Debug.LogWarning(" StageText�� ����Ǿ� ���� ����!");
        }
    }
    public void HidePanels()
    {
        successPanel.SetActive(false);
        failPanel.SetActive(false);
    }
    public void RetryStage()
    {
        FindObjectOfType<StageManager>().RetryCurrentStage();
    }
}
