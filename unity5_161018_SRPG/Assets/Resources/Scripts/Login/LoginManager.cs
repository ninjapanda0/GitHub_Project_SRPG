﻿using UnityEngine;
using System; // DateTime
using System.Collections.Generic; // Dictionary
using JsonFx.Json; // JsonReader
using UnityEngine.SceneManagement; // SceneManager
using UnityEngine.UI; // InputField -> NGUI 안써보려고..

public class LoginManager : MonoBehaviour
{
    public UIInput id;
    public UIInput pw;

    public InputField uid;
    public InputField upw;

    public void RequestLogin()
    {
        //if(id.value.Length < 4 || pw.value.Length < 4)
        //{

        //}

        Dictionary<string, object> sendData = new Dictionary<string, object>();
        sendData.Add("contents", "login");
        //sendData.Add("id", id.value);
        //sendData.Add("pw", pw.value);
        sendData.Add("id", uid.text);
        sendData.Add("pw", upw.text);

        StartCoroutine(NetworkManagerEX.Instance.ProcessNetwork(sendData, ReplyLogin));
    }

    private class RecvLoginData
    {
        public int account;
        public string acc_name;
        public int timestamp;
        public string message;
    }
    
    public void ReplyLogin(string json)
    {
        RecvLoginData data = JsonReader.Deserialize<RecvLoginData>(json);

        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(data.timestamp);

        Debug.Log(data.message);
        Debug.Log((origin.ToLocalTime()).ToString("yyyy년 MM월 dd일의 tt HH시 mm분 s초에 로그인 했습니다."));

        SceneManager.LoadScene(0);
    }
}