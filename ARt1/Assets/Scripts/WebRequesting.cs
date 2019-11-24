using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequesting : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartRegistration(string url, byte[] byteData)
    {
        StartCoroutine(AuthRequest(url, byteData));
    }
    public void StartLogin(string url, byte[] byteData)
    {
        StartCoroutine(AuthRequest(url, byteData));
    }
    public void StartSendTest(string url, byte[] byteData)
    {
        StartCoroutine(SendTestRequest(url, byteData));
    }

    IEnumerator AuthRequest(string url, byte[] byteData)
    {
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        www.uploadHandler = new UploadHandlerRaw(byteData);
        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();
        www.downloadHandler = downloadHandlerBuffer;
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete! Status Code: " + www.responseCode);
            string token = www.downloadHandler.text;
            Debug.Log(token);
            ToolBox.Get<AuthorisationManager>().PutToken(token);
        }

        www.Dispose();
    }
    IEnumerator SendTestRequest(string url, byte[] byteData)
    {
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        www.uploadHandler = new UploadHandlerRaw(byteData);
        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();
        www.downloadHandler = downloadHandlerBuffer;
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", "Bearer " + ToolBox.Get<DataGameSession>().Token);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete! Status Code: " + www.responseCode);
            string answer = www.downloadHandler.text;
            Debug.Log("Answer: " + answer);
        }

        www.Dispose();
    }
}