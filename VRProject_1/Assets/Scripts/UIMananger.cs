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
        timerText.text = "남은 시간: " + seconds + "초";
    }

    public void DisplayOrder(OrderData order)
    {
        if (orderNameText != null)
        {
            orderNameText.text = "주문: " + order.recipeName;
        }

        // (선택) 이미지가 있으면 이미지도 표시
        if (orderImage != null && order.recipeImage != null)
        {
            orderImage.sprite = order.recipeImage;
        }
    }


    public void ShowResult(bool isSuccess)
    {
        if (isSuccess)
        {
            nextStageButtonText.text = "다음 스테이지 시작";
            successPanel.SetActive(true);
        }
        else
            failPanel.SetActive(true);
    }
    public void UpdateStageText(int stage)
    {
        if (stageText != null)
        {
            Debug.Log(" StageText 업데이트: Stage " + stage);
            stageText.text = "Stage " + stage;
        }
        else
        {
            Debug.LogWarning(" StageText가 연결되어 있지 않음!");
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
