using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReceiptManager : MonoBehaviour
{
    public TextMeshProUGUI receiptText;

    // 이제 요리 이름 리스트도 같이 받습니다
    public void SetTexts(int successCount,
                         int failCount,
                         int totalScore,
                         List<string> successDishes,
                         List<string> failDishes)
    {
        // 이름들을 “, ”로 묶어서 문자열로 만듭니다
        string successLine = successDishes.Count > 0
            ? string.Join(", ", successDishes)
            : "없음";
        string failLine = failDishes.Count > 0
            ? string.Join(", ", failDishes)
            : "없음";

        receiptText.text =
            $"획득 점수: {totalScore}\n" +
            $"성공한 요리({successCount}개): " +
            $"{successLine}\n" +
            $"실패한 요리({failCount}개): " +
            $"{failLine}";
    }
}
