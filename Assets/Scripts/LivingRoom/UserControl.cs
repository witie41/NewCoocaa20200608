using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IVRCommon.Keyboard;
using UnityEngine.UI;
public class UserControl : MonoBehaviour
{ 
    private Transform UserPanel;
    private Transform loginPanel;
    bool ShowPersonPage = false;
    private void Awake()
    {
        UserPanel = transform.GetComponent<Button_Panel>().Patner.GetChild(0);
        loginPanel= transform.GetComponent<Button_Panel>().Patner.GetChild(1);
        Controller.Handle_panel[30]=loginPanel.gameObject;
        loginPanel.gameObject.transform.GetChild(0).GetChild(0).GetComponent<IVRInputField>().onValueChanged.AddListener(Get_Mobile_Email);
        loginPanel.gameObject.transform.GetChild(3).GetChild(0).GetComponent<IVRInputField>().onValueChanged.AddListener(Get_Mobile_Email);
        loginPanel.gameObject.transform.GetChild(0).GetChild(1). GetComponent<IVRInputField>().onValueChanged.AddListener(Get_passord);
        loginPanel.gameObject.transform.GetChild(3).GetChild(1). GetComponent<IVRInputField>().onValueChanged.AddListener(Get_passord);
        loginPanel.gameObject.transform.GetChild(4).GetChild(2).GetComponent<IVRInputField>().onValueChanged.AddListener(GetImageCode);
        loginPanel.gameObject.transform.GetChild(0).GetChild(4).GetComponent<IVRInputField>().onValueChanged.AddListener(GetSMSCode);

    }
     private void Update() {
        if(ShowPersonPage)
        {
            StartCoroutine(DataClassInterface.IEGetDate<UserData>(AllData.DataString + "/vr/getUserById?id=" + AllData.userId, new DataClassInterface.OnDataGet<UserData>(ShowPersonInfo), null));
            ShowPersonPage = false;
        }
         }
    public  void OnClick()
    {
        if(AllData.userId==-1)
        {
            loginPanel.gameObject.SetActive(true);
            UserPanel.gameObject.SetActive(false);
        }
        else
        {
            ShowPersonPage = true;
            loginPanel.gameObject.SetActive(false);
            UserPanel.gameObject.SetActive(true);
            
        }
    }
   public void Get_Mobile_Email(string value)
    {
        Controller.mobile_number_email = value;
    }
    public void Get_passord(string value)
    {
        Controller.user_password = value;
        Debug.Log("password 的值为:"+Controller.user_password);
    }

    public void GetSMSCode(string value)
    {
        Controller.SMSCode = value;
    }
    public void GetImageCode(string value)
    {
        Controller.ImageCode = value;
    }

    void ShowPersonInfo(UserData datas ,GameObject[] gb ,string no)
    {
        UserPanel.transform. GetChild(4).GetComponent<Text>().text = datas.nickName;
       UserPanel.transform. GetChild(5).GetChild(2).GetComponent<Text>().text = datas.playNum+"";
       UserPanel.transform. GetChild(6).GetChild(1). GetComponent<Text>().text = datas.likeNum+"";
       UserPanel.transform. GetChild(7).GetChild(1). GetComponent<Text>().text = datas.worksNum+"";
       UserPanel.transform. GetChild(8).GetChild(1). GetComponent<Text>().text = datas.attentionNum+"";
       UserPanel.transform. GetChild(9).GetChild(1). GetComponent<Text>().text = datas.collectNum+"";
       UserPanel.transform. GetChild(10).GetChild(1). GetComponent<Text>().text = datas.myAttentionNum+"";
       UserPanel.transform. GetChild(11).GetComponent<Text>().text = datas.signature+"";
        StartCoroutine(DataClassInterface.IEGetSprite(datas.headImage, new DataClassInterface.OnDataGetSprite(GetSprite),UserPanel.transform. GetChild(3).gameObject));

    }
    
    private void GetSprite(Sprite s, GameObject gbj, string nothing)
    {
        gbj.GetComponent<Image>().sprite = s;

    }

}
