using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GiftButton : MonoBehaviour
{
    private GiftsControl control;
    private Text MyAccount;
    private GvrDropdown NumDropdown;
    public GameObject BG;
    public int id;
    public int price;
    DanmuControl DanmuSpawn;
    MsgManager CurrentRoom;
    private void Start()
    {
        control = GetComponentInParent<GiftsControl>();
        BG = transform.Find("BG").gameObject;
        MyAccount = GameObject.Find("MyAccount").GetComponent<Text>();
        NumDropdown = MyAccount.transform.parent.GetComponentInChildren<GvrDropdown>();
        DanmuSpawn = GameObject.FindGameObjectWithTag("Player").transform.Find("DanmuCanvas").GetComponent<DanmuControl>();
        CurrentRoom = GameObject.FindGameObjectWithTag("LivingRoomManager").GetComponent<MsgManager>();
    }

    public void OnSelected()
    {
        control.CurrentButton = this;
    }

    public void SendGift()
    {
        //判断酷币是否足够
        if (!ChargeAccount())
        {
            CurrentRoom.MsgPanelDisplay("余额不足，您可登录酷开VR官网-酷币钱包 进行充值！官网地址：vr.coocaa.com");
            return;
        }

        CurrentRoom.GetComponent<ControlPanelManager>().CurrentState = false;
        if (AllData.userId == -1)
        {
            CurrentRoom.DisplayPanel(CurrentRoom.TipToLogin, 40);
            return;
        }
        WWWForm form = new WWWForm();
        form.AddField("acceptUserId", CurrentRoom.MasterId);
        form.AddField("presentedUserId", AllData.userId);
        form.AddField("broadcastId", CurrentRoom.CurrentId);
        form.AddField("broadcastRecordId", CurrentRoom.broadcastRecordId);
        form.AddField("giftId", id);
        form.AddField("giftNum", 1);

        //扣除酷币余额
        MyAccount.text = (int.Parse(MyAccount.text.Substring(MyAccount.text.IndexOf(":") + 1)) - price * int.Parse(NumDropdown.options[NumDropdown.value].text)).ToString();
        StartCoroutine(DataClassInterface.IEPostData<Info>(AllData.DataString + "/broadcast/giveGift", null, form, null));

        DanmuSpawn.Send("用户 " + AllData.UserName + " 赠送礼物: " + GetComponentInChildren<Text>().text + " x"+(int.Parse(NumDropdown.options[NumDropdown.value].text)).ToString(), "4");
    }
    bool ChargeAccount()
    {
        int CoinNum = int.Parse(MyAccount.text.Substring(MyAccount.text.IndexOf(":") + 1));
        if (CoinNum <= price * int.Parse(NumDropdown.options[NumDropdown.value].text))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
