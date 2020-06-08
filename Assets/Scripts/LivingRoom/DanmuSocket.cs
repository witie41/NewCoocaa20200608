using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP.WebSocket;
using System;
using LitJson;

public class DanmuSocket : MonoBehaviour
{
    private DanmuControl danmuControl;
    private WebSocket webSocket;
    private string danmuSocketUrl;
    public string DanmuSocketUrl
    {
        set
        {
            danmuSocketUrl = value;
            init(value);
        }
        get
        {
            return danmuSocketUrl;
        }
    }

    private void Start()
    {
        danmuControl = GameObject.FindGameObjectWithTag("Player").transform.Find("DanmuCanvas").GetComponent<DanmuControl>();
    }

    private void OnEnable()
    {
        if (danmuSocketUrl == null)
            return;
        if(webSocket==null)
            init(danmuSocketUrl);
    }

    private void OnDisable()
    {
        OnClosed(webSocket,0,"退出直播间时关闭ws");
    }

    private void init(string url)
    {
        webSocket = new WebSocket(new Uri(url));
        Debug.Log(new Uri(url));
        webSocket.OnOpen += OnOpen;
        webSocket.OnMessage += OnMessageReceived;
        webSocket.OnError += OnError;
        webSocket.OnBinary += OnBinary;
        webSocket.OnClosed += OnClosed;
        webSocket.Open();
        Debug.Log("websocket初始化成功");
    }
    void OnOpen(WebSocket ws)
    {
        Debug.Log("连接成功");
    }
    void OnMessageReceived(WebSocket ws, string msg)
    {
        MsgRecieve temp= JsonMapper.ToObject<MsgRecieve>(msg);
        danmuControl.Recieve(temp.content, temp.msgType);
    }
    void OnBinary(WebSocket ws, byte[] msg)
    {
        Debug.Log(msg);
    }
    void OnClosed(WebSocket ws, UInt16 code, string msg)
    { 
        Debug.Log("连接关闭"+msg);
        webSocket.OnOpen = null;
        webSocket.OnMessage = null;
        webSocket.OnError = null;
        webSocket.OnClosed = null;
        webSocket = null;
    }
    void OnError(WebSocket ws, Exception ex)
    {
        Debug.LogError("连接出错"+ex);
        OnClosed(ws,0,null);
    }

    void  OnApplicationQuit()
    {
        Debug.Log("连接关闭");
        webSocket.OnOpen = null;
        webSocket.OnMessage = null;
        webSocket.OnError = null;
        webSocket.OnClosed = null;
        webSocket = null;
    }
}
