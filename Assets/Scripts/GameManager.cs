using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ScratchCard scratchCard;

    public GameObject spinWheelPage;
    public WheelSpin spinWheel;

    private void OnEnable()
    {
        UIManager.OpenScratchPage += ShowScratchCard;
        UIManager.OpenSpinWheel += ShowSpinWheel;

        UIManager.ClosePage += HideSpinWheel;
        UIManager.ClosePage += HideScratchCard;
    }

    private void OnDisable()
    {
        UIManager.OpenScratchPage -= ShowScratchCard;
        UIManager.OpenSpinWheel -= ShowSpinWheel;

        UIManager.ClosePage -= HideSpinWheel;
        UIManager.ClosePage -= HideScratchCard;
    }
    // Start is called before the first frame update
    void Start()
    {
        scratchCard.gameObject.SetActive(false);
        spinWheelPage.SetActive(false);
    }



    private void ShowScratchCard(int i)
    {
        //scratchCard.gameObject.SetActive(true);
        Tweening.TweenIn_gameObject(scratchCard.gameObject, 0.25f, Vector3.one * 10, Vector3.one * 25);
        scratchCard.rewardIndex = i;
        scratchCard.OnOpenScratchCard();
    }

    private void HideScratchCard()
    {
        //scratchCard.gameObject.SetActive(false);
        Tweening.TweenOut_gameobject(scratchCard.gameObject, 0.2f, Vector3.one * 25, Vector3.one * 10);
    }

    private void ShowSpinWheel()
    {
        //Tween into the page
        Tweening.TweenMove_gameobject(spinWheelPage, 0.25f, Vector3.zero, new Vector3(10, 0f, 0));
        spinWheelPage.SetActive(true);
    }

    private void HideSpinWheel()
    {
        spinWheelPage.SetActive(false);
    }
}
