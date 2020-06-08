using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgManager: MonoBehaviour
{
    int DanmuNum = 0;

    private int currentId;
    public int CurrentId
    {
        get
        {
            return currentId;
        }
        set
        {
            currentId = value;
            DataClassInterface.OnDataGet<LivingRoomData> a = OnRoomIdGetFunction;
            StartCoroutine(DataClassInterface.IEGetDate(AllData.DataString+"/vr/getBroadCastById?id=" + value.ToString(), a, null));
        }
    }
    private void OnRoomIdGetFunction(LivingRoomData livingRoomData,GameObject[] gbj, string nothing)
    {
        ChangeModel changemodel = GameObject.Find("Models").GetComponent<ChangeModel>();
        CurrentURL = livingRoomData.playUrl;
        broadcastRecordId = livingRoomData.broadcastRecordId;
        MasterId = livingRoomData.userId;
        transform.parent.GetComponentInChildren<RankControl>().CurrentRank = 0;
        //更改直播地址
        foreach (Transform player in Players)
        {
            if (player.GetComponent<SvrVideoPlayerDemo>() != null)
                player.GetComponent<SvrVideoPlayerDemo>().VideoUrls[0] = CurrentURL;
        }
        changemodel.CurrentPlayer = changemodel.Player2D;
        Debug.Log(CurrentURL);

        //初始化弹幕socket
        GetComponent<DanmuSocket>().DanmuSocketUrl = AllData.DataString.Replace("http","ws")+"/webSocket/"+AllData.userId.ToString()+"/"+currentId.ToString();

        //初始化房间信息
        StartCoroutine(DataClassInterface.IEGetSprite(livingRoomData.coverImg1, (Sprite sprite, GameObject gos, string nothingstr) => { PhotoConp.sprite = sprite; }, null));
        MasterName = livingRoomData.nickName;
        if (livingRoomData.title == null)
            TitleConp.text = "暂无";
        else
            TitleConp.text = livingRoomData.title;

        if (livingRoomData.nickName == null)
        {
            StartCoroutine(DataClassInterface.IEGetDate<UserData>(AllData.DataString + "/vr/getUserById?id=" + livingRoomData.userId, (UserData data, GameObject[] gos, string str) =>
            {
                if (data.nickName != null)
                {
                    NameConp.text = data.nickName + "的直播间";
                    MasterName = data.nickName;
                }
                else
                {
                    NameConp.text = "暂无";
                }
            }
            ,null));
        }
        else
            NameConp.text = livingRoomData.nickName + "的直播间";

        if (livingRoomData.broadcastCategory == null)
            SortConp.text = "暂无";
        else
            SortConp.text = livingRoomData.broadcastCategory;

        if (livingRoomData.foreshow == null)
            ForeShowConp.text = "暂无";
        else
            ForeShowConp.text = livingRoomData.foreshow;

        if (livingRoomData.announcement == null)
            AnnouncementConp.text = "暂无";
        else
            AnnouncementConp.text = livingRoomData.announcement;

       

        //关注房间
        foreach (Transform Room in RecommenRoomContent)
        {
            Destroy(Room.gameObject);
        }
        DataClassInterface.OnDataGet<LivingRoomData[]> OnRoomListGet= OnRoomListGetFunction;
        StartCoroutine(DataClassInterface.IEGetDate<LivingRoomData[]>(AllData.DataString+"/vr/getBroadcastList", OnRoomListGet,null));

        DataClassInterface.OnDataGet<DanmuData[]> OnDanmuListGet = OnDanmuListGetFunction;
        StartCoroutine(DataClassInterface.IEGetDate<DanmuData[]>(AllData.DataString+"/msg/getBulletChatList", OnDanmuListGet, null));

        DataClassInterface.OnDataGet<SubscribeRoom[]> IfSubscribe = IfSubscribeFuncotion;
        StartCoroutine(DataClassInterface.IEGetDate<SubscribeRoom[]>(AllData.DataString+ "/broadcast/getAttentionBroadcastList?pageId=1&pageSize=100&userId=" + AllData.userId.ToString(), IfSubscribe, null));
    }

    //获取当前房间是否被订阅
    void IfSubscribeFuncotion(SubscribeRoom[] rooms,GameObject[] gos, string nothing)
    {
        Debug.Log("当前直播间的ID:" + currentId);
        foreach(SubscribeRoom room in rooms)
        {
            Debug.Log("直播间ID:" + room.dataId);
            if(room.dataId.Equals(currentId))
            {
                IsSubscribe = true;
                SubscribeButton.GetComponentInChildren<Text>().text = "取消订阅";
                return;
            }
        }
        IsSubscribe = false;
        SubscribeButton.GetComponentInChildren<Text>().text = "订阅";
        Debug.Log("当前房间状态: " + IsSubscribe);
    }

    //获取常用弹幕列表
    void OnDanmuListGetFunction(DanmuData[] danmuList,GameObject[] tbj, string nothing)
    {
        Debug.Log("生成新弹幕");
        foreach(Transform a in DanmuContent)
        {
            if (a.name =="Change")
            {
                continue;
            }
            DanmuOption temp = a.GetComponent<DanmuOption>();
            DanmuData Danmu = danmuList[DanmuNum % danmuList.Length];
            temp.id = Danmu.id;
            temp.message = Danmu.message;
            temp.GetComponentInChildren<Text>().text = Danmu.message;
            DanmuNum++;
        }
    }


    //获取房间列表
    void OnRoomListGetFunction(LivingRoomData[] Rooms,GameObject[] tbj, string nothing)
    {
        foreach (Transform a in RecommenRoomContent)
        {
            Destroy(a.gameObject);
        }
        for (int i=0;i<5;i++)
        {
            if (i == Rooms.Length)
                break;
            if (Rooms[i].id == currentId)
                continue;
            GameObject temp = Instantiate(RecommenedRoom,RecommenRoomContent);
            temp.GetComponent<RecommendRoom>().RoomName = Rooms[i].nickName;
            temp.GetComponent<RecommendRoom>().RoomID = Rooms[i].id;
            temp.GetComponent<RecommendRoom>().RoomSort = Rooms[i].broadcastCategory;
            temp.GetComponent<RecommendRoom>().RoomState = Rooms[i].roomStatus;
            temp.GetComponent<RecommendRoom>().Photo = Rooms[i].coverImg1;
            temp.GetComponent<RecommendRoom>().Init();
        }
    }

    [Header("初始化房间")]
    public string CurrentURL;
    public int broadcastRecordId;
    public Text TitleConp;
    public Text SortConp;
    public Text NameConp;
    public Text ForeShowConp;
    public Text AnnouncementConp;
    public Image PhotoConp;
    public Transform RecommenRoomContent;
    public Transform Players;
    public GameObject RecommenedRoom;
    public GameObject DanmuOption;
    public Transform DanmuContent;
    public Button SubscribeButton;
    public int MasterId;
    public string MasterName;
    private bool IsSubscribe;


    public GameObject MsgCanvas;

    public GameObject TipPanel;
     
    public GameObject ReportPanel;
    public GameObject TipToLogin;

    public void OnReportButtonClick()
    {
        if(AllData.userId==-1)
        {
            DisplayPanel(TipToLogin, 40);
        }
        else
        {
            DisplayPanel(ReportPanel, 40);
        }

    }

    public void DisplayPanel(GameObject Panel,float Distance)
    {
        if (Panel == null)
        {
            Debug.LogError("MsgCanvas Missed");
            return;
        }
        //确保页面被激活
        if (!Panel.activeSelf)
            Panel.SetActive(true);
        Panel.transform.position = Camera.main.transform.position+Camera.main.transform.forward * Distance;
        Panel.transform.LookAt(Panel.transform.position+ Panel.transform.position- Camera.main.transform.position);
    }

    public void MsgPanelDisplay(string msg)
    {
        if(TipPanel == null)
        {
            Debug.LogError("Tip Panel Missed");
            return;
        }
        GameObject TempMsgPanel=null;
        //检查是否有未激活的MsgPanel，有就激活，无就实例化
        for(int i=0;i<MsgCanvas.transform.childCount;i++)
        {
            if (MsgCanvas.transform.GetChild(i).CompareTag("TipPanel") && !MsgCanvas.transform.GetChild(i).gameObject.activeSelf)
            {
                TempMsgPanel = MsgCanvas.transform.GetChild(i).gameObject;
                break;
            }
        }

        if(TempMsgPanel==null)
            TempMsgPanel = Instantiate(TipPanel, MsgCanvas.transform);

        try
        {
            Text text = TempMsgPanel.transform.Find("Tip").GetComponent<Text>();
            text.text = msg;
        }
        catch
        {
            Debug.LogError("The Child GameObject \"Text\" Missed");
        }
        DisplayPanel(TempMsgPanel, 40);
    }

    //订阅
    public void Subscribe()
    {
        if(AllData.userId==-1)
        {
            DisplayPanel(TipToLogin,40);
            return;
        }
        if (!IsSubscribe)
        {
            WWWForm form = new WWWForm();
            form.AddField("userId", AllData.userId);
            form.AddField("broadcastId", CurrentId);
            DataClassInterface.OnDataGet<Info> OnSubscribe = OnSubscribeFunction;
            StartCoroutine(DataClassInterface.IEPostData(AllData.DataString+"/broadcast/attentionBroadcast", OnSubscribe, form, null));
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("userId", AllData.userId);
            form.AddField("broadcastId", CurrentId);
            DataClassInterface.OnDataGet<Info> OnSubscribe = OnSubscribeFunction;
            StartCoroutine(DataClassInterface.IEPostData(AllData.DataString+"/broadcast/unAttentionBroadcast", OnSubscribe, form, null));
        }
    }

    public void OnSubscribeFunction(Info info,GameObject[] gos, string nothing)
    {
         if(info.code==0)
        {
            IsSubscribe = !IsSubscribe;
            if (IsSubscribe)
                MsgPanelDisplay("关注成功");
            else
                MsgPanelDisplay("取消关注成功");
            SubscribeButton.GetComponentInChildren<Text>().text = IsSubscribe ? "取消订阅" : "订阅";
        }
         else
        {
            if (IsSubscribe)
                MsgPanelDisplay("取消关注失败，失败代码：" + info.code + "，失败原因：" + info.msg);
            else
                MsgPanelDisplay("关注失败，失败代码：" + info.code + "，失败原因：" + info.msg);
        }
    }

    public void FlashDanmu()
    {
        Debug.Log("获取新弹幕11");
        StartCoroutine(DataClassInterface.IEGetDate<DanmuData[]>(AllData.DataString+"/msg/getBulletChatList", new DataClassInterface.OnDataGet<DanmuData[]>(OnDanmuListGetFunction), null));
    }

    //缓冲gif测试
    GameObject a1;
    int time = 0;
    private void Start()
    {
        a1 = GameObject.FindGameObjectWithTag("Loading");
    }

    private void Update()
    {
        time++;
        if (time < 20)
            return;
        time = 0;
        if (Players.GetComponentInChildren<SvrVideoPlayer>() != null)
            if (Players.GetComponentInChildren<SvrVideoPlayer>().GetPlayerState().Equals(VideoPlayerState.Buffering))
            {
                a1.transform.localScale = Vector3.one;
            }
            else
            {
                a1.transform.localScale = Vector3.zero;
            }
    }
}
