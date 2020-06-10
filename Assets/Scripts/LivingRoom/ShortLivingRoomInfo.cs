using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortLivingRoomInfo : MonoBehaviour
{
    public Text Sort;
    public Text Live;
    public Text RoomName;
    public Text MasterName;
    public Image image;
    public int ID;

    public void Init(int id,string sort, string live, string roomName, string masterName)
    {
        if(id!=-1)
            ID=id;
        if (sort != null)
            Sort.text = sort;
        if (live != null)
            Live.text = live;
        if (roomName != null)
            RoomName.text = roomName;
        if (masterName != null)
            MasterName.text = masterName;
    }

    public void OnClick()
    {
        GameObject.Find("EventController").GetComponent<Controller>().EnterLivingRoom();
        (Controller.panelComeback.Peek() as GameObject).GetComponentInChildren<MsgManager>().CurrentId=ID;
    }
}
