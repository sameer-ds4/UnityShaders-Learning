using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Menu Pages")]
    public GameObject mainMenu;
    public GameObject scratchCardMenu;
    public GameObject spinWheel;
    public GameObject restrictPanel;

    [Header("ScratchCard Page Components")]
    public Image[] scratchCards;
    public Sprite hiddenCard, revealedCard;

    [Header("Spinwheel Page Components")]
    public GameObject popupReward;
    public Text popupContent;


    public delegate void OpenPageObject(int index);
    public static event OpenPageObject OpenScratchPage;

    public delegate void SpinWheel();
    public static event SpinWheel StartSpin;
    public static event SpinWheel OpenSpinWheel;

    public static event SpinWheel ClosePage;


    public delegate void UpdateData();
    public static event UpdateData UpdateCards;


    private void OnEnable()
    {
        UpdateCards += UpdateCardData;
        WheelSpin.SpinOver += SpinWheelPopup;
    }

    private void OnDisable()
    {
        UpdateCards -= UpdateCardData;
        WheelSpin.SpinOver -= SpinWheelPopup;
    }
    // Start is called before the first frame update
    void Start()
    {
        ReturntoMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReturntoMenu()
    {
        mainMenu.SetActive(true);

        scratchCardMenu.SetActive(false);
        spinWheel.SetActive(false);
    }

    public void OpenPage(string name)
    {
        //mainMenu.SetActive(false);

        switch(name)
        {
            case "ScratchCard":
                scratchCardMenu.SetActive(true);
                UpdateCardData();
                break;

            case "SpinWheel":
                spinWheel.SetActive(true);
                OpenSpinWheel?.Invoke();
                break;
        }
    }

    public void BackButton()
    {
        //Tweening.TweenOut(scratchCardMenu, 0.4f);
        //Tweening.TweenOut(spinWheel, 0.4f);


        //Tweening.TweenIn(mainMenu, 0.3f);
        ReturntoMenu();
        ClosePage?.Invoke();
    }

    public void OpenScratchCard_Popup(int index)
    {
        OpenScratchPage?.Invoke(index);
        restrictPanel.SetActive(true);
    }

    public void StartSpinwheel()
    {
        StartSpin?.Invoke();
    }

    public void CloseCard()
    {
        restrictPanel.SetActive(false);
        ClosePage?.Invoke();
        UpdateCards?.Invoke();
    }

    private void UpdateCardData()
    {
        for (int i = 0; i < scratchCards.Length; i++)
        {
            if (SaveDataHandler.Instance.saveData.Opened[i])
                scratchCards[i].sprite = revealedCard;
            else
                scratchCards[i].sprite = hiddenCard;
        }
    }

    private void SpinWheelPopup(int x)
    {
        popupReward.SetActive(true);
        popupContent.text = "You've earned a " + x + "% Discount Voucher \n Use before it expires";
    }
}
