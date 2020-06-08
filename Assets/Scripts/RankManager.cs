using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour
{
    public Transform MonthRank;
    public Transform WeekRank;
    public Transform DayRank;
    public GameObject RankGameObject;

    private void OnEnable()
    {
        StartCoroutine(DataClassInterface.IEGetDate(AllData.DataString+"/vr/getMonthlyPopularityList", new DataClassInterface.OnDataGet<PopularityList[]>(OnMonthDataGet), null));
        StartCoroutine(DataClassInterface.IEGetDate(AllData.DataString+"/vr/getWeeklyPopularityList", new DataClassInterface.OnDataGet<PopularityList[]>(OnWeekDataGet), null));
        StartCoroutine(DataClassInterface.IEGetDate(AllData.DataString+"/vr/getDailyPopularityList", new DataClassInterface.OnDataGet<PopularityList[]>(OnDayDataGet), null));
    }
    private void OnMonthDataGet(PopularityList[] datas,GameObject[] gos,string str)
    {
        foreach(PopularityList data in datas)
        {
            GameObject temp = Instantiate(RankGameObject, MonthRank);
            temp.GetComponent<EdgeChange>().Init(data.nickName, data.likeNum, null, data.userId);
            StartCoroutine(DataClassInterface.IEGetSprite(data.headImage, (Sprite sprite, GameObject go, string nos) => { temp.GetComponent<EdgeChange>().Photo.sprite = sprite; }, null));
        }
    }

    private void OnWeekDataGet(PopularityList[] datas, GameObject[] gos, string str)
    {
        foreach (PopularityList data in datas)
        {
            GameObject temp = Instantiate(RankGameObject, WeekRank);
            temp.GetComponent<EdgeChange>().Init(data.nickName, data.likeNum, null, data.userId);
            StartCoroutine(DataClassInterface.IEGetSprite(data.headImage, (Sprite sprite, GameObject go, string nos) => { temp.GetComponent<EdgeChange>().Photo.sprite = sprite; }, null));
        }
    }

    private void OnDayDataGet(PopularityList[] datas, GameObject[] gos, string str)
    {
        foreach (PopularityList data in datas)
        {
            GameObject temp = Instantiate(RankGameObject, DayRank);
            temp.GetComponent<EdgeChange>().Init(data.nickName, data.likeNum, null, data.userId);
            StartCoroutine(DataClassInterface.IEGetSprite(data.headImage, (Sprite sprite, GameObject go, string nos) => { temp.GetComponent<EdgeChange>().Photo.sprite = sprite; }, null));
        }
    }
}
