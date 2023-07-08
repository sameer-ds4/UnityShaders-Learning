using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinClick : MonoBehaviour
{
    public delegate void StartSpin();
    public static event StartSpin OnSpin;

    private void OnMouseDown()
    {
        Debug.LogError("clickinbg");
        OnSpin?.Invoke();
    }
}
