using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReceiptManager : MonoBehaviour
{
    public TextMeshProUGUI receiptText;

    // ���� �丮 �̸� ����Ʈ�� ���� �޽��ϴ�
    public void SetTexts(int successCount,
                         int failCount,
                         int totalScore,
                         List<string> successDishes,
                         List<string> failDishes)
    {
        // �̸����� ��, ���� ��� ���ڿ��� ����ϴ�
        string successLine = successDishes.Count > 0
            ? string.Join(", ", successDishes)
            : "����";
        string failLine = failDishes.Count > 0
            ? string.Join(", ", failDishes)
            : "����";

        receiptText.text =
            $"ȹ�� ����: {totalScore}\n" +
            $"������ �丮({successCount}��): " +
            $"{successLine}\n" +
            $"������ �丮({failCount}��): " +
            $"{failLine}";
    }
}
