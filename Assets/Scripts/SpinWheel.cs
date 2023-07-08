using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpinWheel : MonoBehaviour
{
    public GameObject spinWheel;
    public GameObject buttonSpin;

    public float spinSlowdown;



    [SerializeField]
    private float speed;
    private float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;      
    }

    // Update is called once per frame
    void Update()
    {
        DoRotate();

        timeElapsed += Time.deltaTime;

        if (timeElapsed > spinSlowdown)
            ReduceSpeed();
    }

    private void ReduceSpeed()
    {
        speed = Mathf.Lerp(speed, 0, .005f);
    }

    private void DoRotate()
    {
        //spinWheel.transform.DORotate(new Vector3(0, 0, 360), speed, RotateMode.FastBeyond360)
        //    .SetLoops(-1, LoopType.Restart)
        //    .SetRelative()
        //    .SetEase(Ease.Linear);

        spinWheel.transform.Rotate(new Vector3(0, 0, 1) * speed);
    }

    
}
