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

    public XRBaseInteractable bookInteractable;    // 책 상호작용 오브젝트

    public Animator bookAnimator;

    private void Start()
    {
        bookInteractable.enabled = true;
        UpdateCoinText();

        animator = GetComponent<Animator>();

        // 책 클릭 이벤트 연결
        if (bookInteractable != null)
            bookInteractable.selectEntered.AddListener(OnBookClicked);

        // 닫기 버튼 클릭 이벤트 연결
    }

    private void OnBookClicked(SelectEnterEventArgs args)
    {
        Debug.Log("Book_Cliked");
        if (isOpen) return;  // 이미 열려있으면 무시
        isOpen = true;

        animator.SetBool("isOpen", true);
        bookInteractable.enabled = false; // 책 클릭 비활성화
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
