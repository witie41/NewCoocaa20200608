using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftButton : MonoBehaviour
{
    public int id;
    DanmuControl DanmuSpawn;
    MsgManager CurrentRoom;
    private void Start()
    {
        DanmuSpawn = GameObject.FindGameObjectWithTag("Player").transform.Find("DanmuCanvas").GetComponent<DanmuControl>();
        CurrentRoom = GameObject.FindGameObjectWithTag("LivingRoomManager").GetComponent<MsgManager>();
    }

    public void SendGift()
    {
        if (AllData.userId == -1)
        {
            MsgManager manager = GameObject.FindGameObjectWithTag("LivingRoomManager").GetComponent<MsgManager>();
            manager.DisplayPanel(manager.TipToLogin, 40);
            return;
        }
        WWWForm form = new WWWForm();   
        form.AddField("acceptUserId", CurrentRoom.MasterId);
        form.AddField("presentedUserId", AllData.userId);
        form.AddField("broadcastId", CurrentRoom.CurrentId);
        form.AddField("broadcastRecordId", CurrentRoom.broadcastRecordId);
        form.AddField("giftId", id);
        form.AddField("giftNum", 1);

        StartCoroutine(DataClassInterface.IEPostData<Info>(AllData.DataString+"/broadcast/giveGift", null, form, null));

        DanmuSpawn.Send("用户 " + AllData.UserName+" 赠送礼物: "+GetComponentInChildren<Text>().text,"4");
    }

}
