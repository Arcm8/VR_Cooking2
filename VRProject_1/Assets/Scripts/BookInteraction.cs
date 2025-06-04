using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class BookInteraction : MonoBehaviour
{
    public Text coinText;
    private Animator animator;
    private bool isOpen = false;
    private bool nextPage = true;

    public XRBaseInteractable bookInteractable;    // å ��ȣ�ۿ� ������Ʈ

    public Animator bookAnimator;

    private void Start()
    {
        bookInteractable.enabled = true;
        UpdateCoinText();

        animator = GetComponent<Animator>();

        // å Ŭ�� �̺�Ʈ ����
        if (bookInteractable != null)
            bookInteractable.selectEntered.AddListener(OnBookClicked);

        // �ݱ� ��ư Ŭ�� �̺�Ʈ ����
    }

    private void OnBookClicked(SelectEnterEventArgs args)
    {
        Debug.Log("Book_Cliked");
        if (isOpen) return;  // �̹� ���������� ����
        isOpen = true;

        animator.SetBool("isOpen", true);
        bookInteractable.enabled = false; // å Ŭ�� ��Ȱ��ȭ
    }

    public void CloseBook()
    {
        Debug.Log("Book_Close");
        if (!isOpen) return;

        if (bookAnimator != null)
            bookAnimator.SetBool("isOpen", false);
        isOpen = false;
        bookInteractable.enabled = true;
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateCoinText()
    {
        int currentCoins = gamaManager.Instance.CurrentData.coins;
        coinText.text = $"Coins: {currentCoins}";
    }

    public void NextPage()
    {
        nextPage = true;
        bookAnimator.SetBool("NextPage", true);
    }
    public void prevPage()
    {
        nextPage = false;
        bookAnimator.SetBool("NextPage", false);
    }
}
