using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankControl: MonoBehaviour
{
    public enum Rank
    {
        ContributionRank,
        GuestRank,
        FunsRank
    };
    private Image[] Buttons;
    public ScrollRect[] Infos=null;
    public GameObject Info4_0;
    public GameObject Info4_1;
    public GameObject Info4_2;

    private Color NormalColor=new Color(0.7f,0.7f,0.7f,1);
    private Color SelectedColor = new Color(0.6f,0.67f,1,1);

    private DataClassInterface.OnDataGet<GiftRank[]> OnGiftsRank;
    private DataClassInterface.OnDataGet<GuestData[]> OnGuestRank;
    private DataClassInterface.OnDataGet<FanData[]> OnFansRank;

    private Rank currentRank=0;
    public Rank CurrentRank
    {
        get
        {
            return currentRank;
        }
        set
        {
            //改变按钮颜色
            Buttons[(int)currentRank].color = NormalColor;
            //改变内容
            Infos[(int)currentRank].gameObject.SetActive(false);
            currentRank = value;
            Buttons[(int)currentRank].color = SelectedColor;
            Infos[(int)currentRank].gameObject.SetActive(true);

            //调用数据
            switch (currentRank)
            {
                case Rank.ContributionRank:
                    StartCoroutine(DataClassInterface.IEGetDate(AllData.DataString+"/vr/getBroadcastGiftRankings?broadcastId="+GameObject.FindGameObjectWithTag("LivingRoomManager").GetComponent<MsgManager>().CurrentId.ToString(), OnGiftsRank, null));
                    break;
                case Rank.GuestRank:
                    StartCoroutine(DataClassInterface.IEGetDate(AllData.DataString+"/vr/getOnLineMember?pageId=0&pageSize=20&broadcastId=" + GameObject.FindGameObjectWithTag("LivingRoomManager").GetComponent<MsgManager>().CurrentId.ToString(), OnGuestRank, null));
                    break;
                case Rank.FunsRank:
                    StartCoroutine(DataClassInterface.IEGetDate(AllData.DataString+"/broadcast/getBroadcastFanList?pageId=1&pageSize=20&broadcastId=" + GameObject.FindGameObjectWithTag("LivingRoomManager").GetComponent<MsgManager>().CurrentId.ToString(), OnFansRank, null));
                    break;
            }
        }
    }

    
    void OnGiftsRankFunction(GiftRank[] giftrank, GameObject[] tbj, string nothing)
    {
        //清空原有的榜单
        foreach(Transform user in Infos[0].content.transform)
        {
            Destroy(user.gameObject);
        }

        foreach(GiftRank user in giftrank)
        {
            UserShortInfo temp = Instantiate(Info4_0, Infos[0].content.transform).GetComponent<UserShortInfo>();
            temp.Name = user.presentedUserName;
            temp.Photo = user.headImage;
            temp.UserId = user.presentedUserId;
            temp.Contribution = user.giftNum;
        }
    }

    void OnGuestRankFunction(GuestData[] guestData, GameObject[] tbj, string nothing)
    {
        //清空原有的榜单
        foreach (Transform user in Infos[1].content.transform)
        {
            Destroy(user.gameObject);
        }

        foreach (GuestData user in guestData)
        {
            UserShortInfo temp = Instantiate(Info4_1, Infos[1].content.transform).GetComponent<UserShortInfo>();
            temp.Name = user.nickName;
            temp.Photo = user.headImage;
            temp.UserId = user.userId;
            //temp.Contribution = user.giftNum;
        }
    }

    void OnfanrankFunction(FanData[] fanrank, GameObject[] tbj, string nothing)
    {
        //清空原有的榜单
        foreach (Transform user in Infos[2].content.transform)
        {
            Destroy(user.gameObject);
        }

        foreach (FanData user in fanrank)
        {
            UserShortInfo temp = Instantiate(Info4_2, Infos[2].content.transform).GetComponent<UserShortInfo>();
            temp.Name = user.userName;
            temp.Photo = user.headImage;
            temp.UserId = user.userId;
            //temp.Contribution = user.giftNum;
        }
    }


    private void Awake()
    {
        if (Infos == null)
            Infos = new ScrollRect[3];
        Buttons = transform.Find("Buttons").GetComponentsInChildren<Image>();
        Infos = transform.GetComponentsInChildren<ScrollRect>();
        Infos[1].gameObject.SetActive(false);
        Infos[2].gameObject.SetActive(false);

        OnGiftsRank = OnGiftsRankFunction;
        OnGuestRank = OnGuestRankFunction;
        OnFansRank = OnfanrankFunction;
    }

    public void ChangeRank(int RankOrder)
    {
        CurrentRank = (Rank)RankOrder;
    }
}
