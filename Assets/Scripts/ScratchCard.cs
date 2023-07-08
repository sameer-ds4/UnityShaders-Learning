using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScratchCard : MonoBehaviour
{
    public Camera mainCam;

    public GameObject[] rewardObjects;
    public GameObject baseLayer;
    public GameObject coverLayer;

    public GameObject mask;
    public GameObject maskParent;

    public int rewardIndex;
    public int scratchPercent;

    private bool draw;
    [SerializeField]
    int progress = 0;
    bool scratchCardOpened;

    private void OnEnable()
    {
        UIManager.ClosePage += ResetScratchCard;
    }

    private void OnDisable()
    {
        UIManager.ClosePage -= ResetScratchCard;
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
            draw = true;
        else
            draw = false;

    }

    private void Update()
    {
        if (draw)
            DrawBrushes();
    }

    // Update Show details for the card
    public void OnOpenScratchCard()
    {
        if(SaveDataHandler.Instance.saveData.Opened[rewardIndex])
        {
            coverLayer.SetActive(false);
            draw = false;
        }
        else
        {
            coverLayer.SetActive(true);
            draw = true;
        }
    }

    private void DrawBrushes()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 15;
        mousePos = mainCam.ScreenToWorldPoint(mousePos);

        Instantiate(mask, mousePos, Quaternion.identity, maskParent.transform);

        if (!scratchCardOpened)
        {
            progress++; 
            if (progress > scratchPercent)
                OpenScratchcard();
        }
    }

    private void OpenScratchcard()
    {
        //Tweening.TweenOut_gameobject(coverLayer.gameObject, 0.5f);
        coverLayer.SetActive(false);
        Tweening.BubbleOut_gameobject(baseLayer, 0.3f, Vector3.one * 0.16f, Vector3.one * 0.14f);
        scratchCardOpened = true;
        SaveDataHandler.Instance.saveData.Opened[rewardIndex] = true;
        //Update text details about the reward earned
    }

    private void ResetScratchCard()
    {
        DOVirtual.DelayedCall(0.2f, () =>
        {
            coverLayer.SetActive(true);
            progress = 0;
            scratchCardOpened = false;

        });

        for (int i = 0; i < maskParent.transform.childCount; i++)
        {
            maskParent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
