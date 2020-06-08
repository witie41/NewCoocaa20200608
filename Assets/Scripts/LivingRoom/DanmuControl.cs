using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanmuControl : MonoBehaviour
{
    private GameObject TempDanmu;
    public GameObject Danmu;
    private Vector3 SpawnPot;
    public List<GameObject> ActiveDanmu=new List<GameObject>();
    public List<GameObject> InactiveDanmu = new List<GameObject>();

    private void Start()
    {
        SpawnPot = transform.position + Vector3.left * 30;
    }
    /// <summary>
    /// 发送弹幕
    /// </summary>
    /// <param name="text"></param>
    /// <param name="type">3对应弹幕,2对应礼物</param>
    public void Send(string text,string type="3")
    {
        if(AllData.userId==-1)
        {
            MsgManager manager= GameObject.FindGameObjectWithTag("LivingRoomManager").GetComponent<MsgManager>();
            manager.DisplayPanel(manager.TipToLogin,40);
            return;
        }
        //向服务器发送数据
        WWWForm form = new WWWForm();
        form.AddField("broadcastId",GameObject.FindGameObjectWithTag("LivingRoomManager").GetComponent<MsgManager>().CurrentId);
        form.AddField("masterId", AllData.userId);
        form.AddField("content", text);
        form.AddField("msgStatus", "2");
        form.AddField("msgType", type);
        form.AddField("title", "消息");
        form.AddField("broadcastRecordId", GameObject.FindGameObjectWithTag("LivingRoomManager").GetComponent<MsgManager>().broadcastRecordId);
        Debug.Log("broadcastId  masterId  content  msgStatus  msgType  title  broadcastRecordId");
        Debug.Log(GameObject.FindGameObjectWithTag("LivingRoomManager").GetComponent<MsgManager>().CurrentId);
        Debug.Log(AllData.userId);
        Debug.Log(text);
        Debug.Log("2");
        Debug.Log(type);
        Debug.Log("消息");
        Debug.Log(GameObject.FindGameObjectWithTag("LivingRoomManager").GetComponent<MsgManager>().broadcastRecordId);

        StartCoroutine(DataClassInterface.IEPostData<Info>(AllData.DataString+"/msg/sendBroadcastGroupChat", null, form, null));
    }

    /// <summary>
    /// 接收弹幕
    /// </summary>
    /// <param name="text"></param>
    /// <param name="type">0对应弹幕,1对应礼物</param>
    public void Recieve(string text,string type="3")
    {
        Debug.Log("接收到弹幕");
        //开启弹幕时接收数据
        if (transform.localScale == Vector3.zero)
            return;
        //创建
        if(InactiveDanmu.Count==0)
        {
            TempDanmu = Instantiate(Danmu, transform);
        }
        //复用
        else
        {
            TempDanmu = InactiveDanmu[0];
            TempDanmu.SetActive(true);
            Debug.Log(TempDanmu.activeSelf);
        }
        if (type == "4")
        {
            TempDanmu.GetComponentInChildren<Text>().color = Color.red;
        }
        else
        {
            TempDanmu.GetComponentInChildren<Text>().color = Color.black;
        }
        TempDanmu.GetComponentInChildren<Text>().text = text;
        TempDanmu.GetComponent<RectTransform>().sizeDelta = new Vector2(TempDanmu.GetComponentInChildren<Text>().text.Length * 16 + 20, 30);
        TempDanmu.transform.position = SpawnPot + Vector3.up * Random.Range(-6, 6);
        TempDanmu.transform.rotation = Quaternion.Euler((-90 - transform.parent.eulerAngles.y) * Vector3.up);
        TempDanmu.GetComponent<DanmuTip>().DestroySelf();
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * Time.fixedDeltaTime * 10);
    }
}
