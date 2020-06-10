using System;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Diagnostics.Contracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum VideoType
{
    Video2D,
    Video3D,
    Video180,
    Video360,
    Live_On,
    Live_Off
}
public class RoomButtonControl : MonoBehaviour
{
    public Color OnLive;
    public Color OffLive;
    public Color V2D;
    public Color V3D;
    public Color V180;
    public Color V360;
    public Image Photo;
    public VideoType VType;
    public Transform LeftTag;
    public Transform RightTag;
    public Text VName;
    public int ID;
    public void Init(VideoType VType,int id,string vName,string photo)
    {
        LeftTag.gameObject.SetActive(false);
        RightTag.gameObject.SetActive(false);
        VName.text=vName;
        ID=id;
        StartCoroutine(DataClassInterface.IEGetSprite(photo,(Sprite sprite,GameObject gtb, string nothing) => { Photo.sprite = sprite; },null));
        if(VType==VideoType.Live_Off||VType==VideoType.Live_On)
        {
            LeftTag.gameObject.SetActive(true);
            if(VType==VideoType.Live_On)
            {
                LeftTag.GetComponentInChildren<Text>().text="直播中";
                LeftTag.GetComponent<Image>().color=OnLive;
            }
            if(VType==VideoType.Live_Off)
            {
                LeftTag.GetComponentInChildren<Text>().text="未开播";
                LeftTag.GetComponent<Image>().color=OffLive;
            }
        }
    }
}
