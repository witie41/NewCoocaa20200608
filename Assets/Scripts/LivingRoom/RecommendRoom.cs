using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecommendRoom : MonoBehaviour
{
    public string RoomName;
    public int RoomID;
    public string RoomState;
    public string RoomSort;
    public string Photo;
    private Controller controller;

    public void OnClick()
    {
        if(controller == null)
        {
            controller = GameObject.Find("EventController").GetComponent<Controller>();
        }
        controller.EnterLivingRoom();
        GameObject tempRoom= Controller.panelComeback.Peek() as GameObject;
        tempRoom.GetComponentInChildren<MsgManager>().CurrentId = RoomID;
     }


    public void Init()
    {
        if (RoomName == null)
            transform.Find("Name").GetComponent<Text>().text = "未知";
        else
            transform.Find("Name").GetComponent<Text>().text = RoomName;

        if (RoomSort == null)
            transform.Find("Sort").GetComponent<Text>().text = "未知";
        else
            transform.Find("Sort").GetComponent<Text>().text = "分区-"+ RoomSort;
        StartCoroutine(DataClassInterface.IEGetSprite(Photo, (Sprite sprite,GameObject goj, string nothing) => { transform.Find("PhotoImage").Find("Photo").GetComponent<Image>().sprite = sprite; }, null));
        switch (RoomState)
        {
            case "0":
                transform.Find("On_Or_Off").GetComponent<Text>().color = Color.black;
                transform.Find("On_Or_Off").GetComponent<Text>().text = "禁播";
                break;
            case "1":
                transform.Find("On_Or_Off").GetComponent<Text>().color = Color.red;
                transform.Find("On_Or_Off").GetComponent<Text>().text = "直播中";
                break;
            case "2":
                transform.Find("On_Or_Off").GetComponent<Text>().color = Color.grey;
                transform.Find("On_Or_Off").GetComponent<Text>().text = "休息中";
                break;
            default:
                transform.Find("On_Or_Off").GetComponent<Text>().color = Color.grey;
                transform.Find("On_Or_Off").GetComponent<Text>().text = "未知";
                break;
        }

    }
}
