using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public Image backgroundImage;

    // 팝업 설정 함수
    public void Setup(string message, Color bgColor)
    {
        if (messageText != null) messageText.text = message;
        if (backgroundImage != null) backgroundImage.color = bgColor;
    }

    // 애니메이션 이벤트에서 호출됨
    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
