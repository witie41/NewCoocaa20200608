using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportCnotrol : MonoBehaviour
{
    public GameObject Content;
    private Text TitleText;
    public GameObject ReportButton;
    private GameObject TempReportButton;
    private ReportButton currentButton = null;
    private MsgManager msgManager;
    public ReportButton CurrentButton
    {
        get
        {
            return currentButton;
        }
        set
        {
            if(currentButton==null)
            {
                currentButton = value;
                currentButton.ButtonState = true;
                return;
            }
            if(currentButton!=value)
            {
                currentButton.ButtonState = false;
                currentButton = value;
                currentButton.ButtonState = true;
            }
        }
    }




    void Start()
    {
        msgManager = GameObject.FindGameObjectWithTag("LivingRoomManager").GetComponent<MsgManager>();
        TitleText = transform.Find("TitleText").GetComponent<Text>();
        TitleText.text="举报\""+msgManager.MasterName+'('+msgManager.MasterId+')'+"\"主播";
        if (AllData.reportDatas==null)
        {
            DataClassInterface.OnDataGet<ReportData[]> a = GetReportSorts;
            StartCoroutine(DataClassInterface.IEGetDate(AllData.DataString+"/broadcast/getReportTypeList ", a, null));
        }
        else
        {
            foreach (ReportData temp in AllData.reportDatas)
            {
                TempReportButton = Instantiate(ReportButton, Content.transform);
                TempReportButton.GetComponent<ReportButton>().Init(temp.id, temp.name, temp.seq);
            }
        }
    }

    private void GetReportSorts(ReportData[] data,GameObject[] go,string nothing)
    {
        AllData.reportDatas = new ReportData[data.Length];
        int i = 0;
        foreach (ReportData temp in data)
        {
            TempReportButton = Instantiate(ReportButton, Content.transform);
            TempReportButton.GetComponent<ReportButton>().Init(temp.id, temp.name, temp.seq);
            AllData.reportDatas[i++] = temp;
        }
    }

    public void OnReport()
    {
        WWWForm form = new WWWForm();
        form.AddField("userId",AllData.userId);
        form.AddField("BroadcastId", GameObject.FindGameObjectWithTag("LivingRoomManager").GetComponent<MsgManager>().CurrentId);
        form.AddField("reportTypeId",currentButton.GetComponent<ReportButton>().id);
        DataClassInterface.OnDataGet<Info> a = OnReportFunction;
        StartCoroutine(DataClassInterface.IEPostData<Info>(AllData.DataString+"/broadcast/reportBroadcast", a, form, null));
    }
    
   void OnReportFunction(Info info,GameObject[] gos, string nothing)
    {
        //成功
        if(info.code==0)
        {
            msgManager.MsgPanelDisplay("举报成功，如有反馈将第一时间通知您");
        }
        //失败
        else
        {
            msgManager.MsgPanelDisplay("举报失败，错误代码："+info.code+"，错误原因："+info.msg);
        }
        gameObject.SetActive(false);
    }
}
