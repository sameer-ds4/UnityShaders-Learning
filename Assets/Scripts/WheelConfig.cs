using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelConfig : MonoBehaviour
{
    public Image[] slices;
    public List<Text> textContent;

    public float maxSlices;
    public List<WheelInfo> wheelInfos;

    float x; // Using for Angle offset of circles Arcs

    private void OnEnable()
    {
        GetDataAPI.obtainedWheelData += GetParams;
    }

    private void OnDisable()
    {
        GetDataAPI.obtainedWheelData -= GetParams;
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    private void GetParams(Wheeldata wheeldata)
    {
        maxSlices = wheeldata.data.Length;
        for (int i = 0; i < maxSlices; i++)
        {
            wheelInfos.Add(wheeldata.data[i]);
        }
        WheelForming();

    }

    private void WheelForming()
    {
        float angle = 360 / maxSlices;
        float fillAmount =  1 / maxSlices;

        RearrangeArray(slices, maxSlices / 2, (int)maxSlices);

        for (int i = 0; i < maxSlices; i++)
        {
            slices[i].gameObject.SetActive(true);
            slices[i].fillAmount = fillAmount - .01f;
            slices[i].color = AssignColor(wheelInfos[i].color);
            textContent[i].gameObject.GetComponent<Text>().text = wheelInfos[i].offer;
            slices[i].transform.eulerAngles = new Vector3(0, 0, x + 20);
            x += angle;
        }
    }

    private void RearrangeArray(Image[] arr, float d, int n)
    {
        var p = 1;
        while (p <= d)
        {
            var last = arr[0];
            for (int i = 0; i < n - 1; i++)
            {
                arr[i] = arr[i + 1];
            }
            arr[n - 1] = last;
            p++;
        }
    }

    private Color AssignColor(string hexCode)
    {
        Color newcol;

        if (ColorUtility.TryParseHtmlString(hexCode, out newcol))
            return newcol;

        return Color.black;
    }
}
