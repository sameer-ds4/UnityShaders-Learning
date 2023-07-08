using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class GetDataAPI : MonoBehaviour
{
    public static GetDataAPI Instance;

    public delegate void APICall(Wheeldata wd);
    public static event APICall obtainedWheelData;

    [Header ("API calls")]
    public string Get_URL;
    public string Post_URL;
    public string api_key;

    public Wheeldata wheelInfo;
    public WheelReward reward;

    private void Awake()
    {
        Instance = this;
        GetData();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(PostData());
    }

    public void GetData()
    {
        StartCoroutine(FetchData());
    }

    IEnumerator FetchData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Get_URL))
        {
            webRequest.SetRequestHeader("Authorization", api_key);

            yield return webRequest.SendWebRequest();


            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                wheelInfo = JsonUtility.FromJson<Wheeldata>(webRequest.downloadHandler.text);
                //Debug.LogError(webRequest.downloadHandler.text);
            }
        }
        obtainedWheelData?.Invoke(wheelInfo);
    }

    int p = 5;

    IEnumerator PostData()
    {
        reward.selected_id = p;
        string jsonRaw = JsonUtility.ToJson(reward);
        Debug.LogError(jsonRaw);
        byte[] Body = Encoding.UTF8.GetBytes(jsonRaw);

        var webRequest = new UnityWebRequest(Post_URL, "POST");
        //using(UnityWebRequest webRequest = UnityWebRequest.Post(Post_URL, jsonRaw))
        //{
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(Body);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", api_key);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.LogError(webRequest.downloadHandler.text);
            }
        //}
    }

}

[System.Serializable]
public class WheelInfo
{
    public int id;
    public string offer;
    public string color;
}

[System.Serializable]
public class Wheeldata
{
    public WheelInfo[] data;
}

[System.Serializable]
public class WheelReward
{
    public int selected_id;
}