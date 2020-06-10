using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class RecommendRoomsControl : MonoBehaviour
{
    public GameObject Rooms360;
    public GameObject RoomsGrand;
    public GameObject RoomsLiving;
    private Transform Content360;
    private Transform ContentGrand;
    private Transform ContentLiving;
    DataClassInterface.OnDataGet<VideoData[]> Rooms360Spawn;
    DataClassInterface.OnDataGet<VideoData[]> GrandRoomsSpawn;
    DataClassInterface.OnDataGet<LivingRoomData[]> LivingRoomsSpawn;
    private void Awake() {
        Content360 = GameObject.FindWithTag("360Content").transform;
        ContentGrand=GameObject.FindWithTag("GrandContent").transform;
        ContentLiving=GameObject.FindWithTag("LivingContent").transform;
        Rooms360Spawn=Create360Rooms;
        GrandRoomsSpawn=CreateGrandRooms;
        LivingRoomsSpawn=CreateLivingRooms;
        FlashRooms();
    }

    void FlashRooms()
    {
        //全景视频
        StartCoroutine(DataClassInterface.IEGetDate(AllData.DataString+"/vr/getVideoList?workType=0",Rooms360Spawn,null));
        StartCoroutine(DataClassInterface.IEGetDate(AllData.DataString+"/vr/getVideoList?workType=2",GrandRoomsSpawn,null));
        StartCoroutine(DataClassInterface.IEGetDate(AllData.DataString+"/vr/getBroadcastList",LivingRoomsSpawn,null));
    }

    void Create360Rooms(VideoData[] data,GameObject[] gos,string str)
    {
        VideoShortData temp;
        foreach(VideoData a in data)
        {
            temp=Instantiate(Rooms360,Content360).GetComponent<VideoShortData>();
            temp.Init(a.workId,-1,-1,-1,DataClassInterface.SecondsToTime(a.duration),a.title,a.nickName);
            StartCoroutine(DataClassInterface.IEGetSprite(a.cover,(Sprite s,GameObject go,string str1)=>{temp.image.sprite=s;},null));
        }
    } 
    void CreateGrandRooms(VideoData[] data,GameObject[] gos,string str)
    {
        VideoShortData temp;
        foreach(VideoData a in data)
        {
            temp=Instantiate(RoomsGrand,ContentGrand).GetComponent<VideoShortData>();
            temp.Init(a.workId,-1,-1,-1,DataClassInterface.SecondsToTime(a.duration),a.title,a.nickName);
            StartCoroutine(DataClassInterface.IEGetSprite(a.cover,(Sprite s,GameObject go,string str1)=>{temp.image.sprite=s;},null));
        }
    } 
    void CreateLivingRooms(LivingRoomData[] data,GameObject[] gos,string str)
    {
        ShortLivingRoomInfo temp;
        foreach(LivingRoomData a in data)
        {   
            temp=Instantiate(RoomsLiving,ContentLiving).GetComponent<ShortLivingRoomInfo>();
            StartCoroutine(DataClassInterface.IEGetSprite(a.coverImg1,(Sprite s,GameObject go,string str1)=>{temp.image.sprite=s;},null));
            temp.Init(a.id,"分区"+a.broadcastCategory,a.roomStatus=="1"?"正在直播":"已下播",a.title,a.nickName);
        }
    } 
}
