using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// 此脚本是对于每个直播间类的抽象表示。方便对房间精准的控制；
/// </summary>
public class MyLivingRoom :MonoBehaviour
{
    private string theRoomName;
    private int theRoomHot;//房间热度，观看人数；
    private int theRoomNumber;//房间号,房间号应该是不可修改，查看后期数据
    private int key;//索引值,方便在 字典中查找
    private Sprite theRoomSprite;
    public bool isLiving;//是否在播放,public方便调控，正式时需要封装
    private enum RoomType
    {
        _360DegreeRoom,
        GrandScreenRoom,
        LivingRoom
    }
    RoomType roomType;
    public MyLivingRoom (string name,int hot,int number,int key,Sprite sprite)
    {
        theRoomName = name;
        isLiving = true;//是否直播
        theRoomHot = hot;//热度
        theRoomNumber = number;//房间号
        this.name = key + "";//房间索引值
        theRoomSprite = sprite;
    }
    public MyLivingRoom (string name,int hot,int number,int key)
    {
        isLiving = true;
        theRoomHot = hot;
        theRoomNumber = number;
        this.name = key + "";
    }
    public MyLivingRoom(int key)
    {
        /**
          此代码只为测试的构造函数
         */
        this.key = key;
    }


    public string GetRoomName()
    {
        return theRoomName;
    }

    public Sprite GetRoomSpirte()
    {
        return theRoomSprite;
    }

    public int GetRoomKey()
    {
        return key;
    }

    public int GetRoomNumber()
    {
        return theRoomNumber;
    }
    public int GetRoomHot()
    {
        return theRoomHot;
    }
    
    

}
