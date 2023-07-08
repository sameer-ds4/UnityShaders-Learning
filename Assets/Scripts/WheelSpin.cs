using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    public List<int> prizes;
    public List<AnimationCurve> animationCurves;

    public bool spinning;
    public float angleItem;
    public int randTime;
    public int itemNumber;

    private bool startSpin;


    public delegate void UpdateData(int i);
    public static event UpdateData SpinOver;

    private void OnEnable()
    {
        //UIManager.StartSpin += StartWheelSpin;
        SpinClick.OnSpin += StartWheelSpin;
        UIManager.ClosePage += ResetWheel;

        SetSpin();
    }

    private void OnDisable()
    {
        //UIManager.StartSpin -= StartWheelSpin;
        SpinClick.OnSpin -= StartWheelSpin;
        UIManager.ClosePage -= ResetWheel;
    }


    // Update is called once per frame
    void Update()
    {
        if(startSpin && !spinning)
        {
            randTime = Random.Range(7, 12);
            itemNumber = Random.Range(0, prizes.Count);

            float maxAngle = (360 * randTime) + (itemNumber * angleItem);
            Debug.LogError(maxAngle);

            StartCoroutine(SpinTheWheel(randTime, maxAngle));
        }
    }

    void SetSpin()
    {
        spinning = false;
        startSpin = false;
        angleItem = 360 / prizes.Count;

        //Tweening.TweenIn_gameObject(gameObject, 0.4f);
    }

    public void StartWheelSpin()
    {
        startSpin = true;
    }

    IEnumerator SpinTheWheel(float time, float maxAngle)
    {
        startSpin = false;
        spinning = true;

        float timer = 0.0f;
        float startAngle = transform.eulerAngles.z;
        maxAngle -= startAngle;

        int animationCurveNumber = Random.Range(0, animationCurves.Count);
        Debug.Log("Animation Curve No. : " + animationCurveNumber);

        while (timer < time)
        {
            //to calculate rotation
            float angle = maxAngle * animationCurves[animationCurveNumber].Evaluate(timer / time);
            //Debug.LogError(angle);
            transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            yield return 0;
        }

        transform.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle + startAngle);
        spinning = false;

        SaveReward();
        Debug.Log("Prize: " + prizes[itemNumber]);//use prize[itemNumnber] as per requirement
    }   

    private void ResetWheel()
    {
        transform.eulerAngles = Vector3.zero;
    }

    private void SaveReward()
    {
        SaveDataHandler.Instance.saveData.rewardIndex = itemNumber;
        SpinOver?.Invoke(prizes[itemNumber]);
    }    
}
