using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    //作者id
    private int userId;

    //视频id
    private int id;
    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
            StartCoroutine(DataClassInterface.IEGetDate(AllData.DataString+"/vr/getVideoById?workId="+Id.ToString(), new DataClassInterface.OnDataGet<VideoData>(OnVideoDataGetFunction), null));
        }
    }

    //测试
    GameObject a1;
    int time = 0;
    private void Start()
    {
        a1 = GameObject.FindGameObjectWithTag("Loading");
    }

    private void Update()
    {
        time++;
        if (time < 20)
            return;
        time = 0;
        if(players.GetComponentInChildren<SvrVideoPlayer>()!=null)
        if(players.GetComponentInChildren<SvrVideoPlayer>().GetPlayerState().Equals(VideoPlayerState.Buffering))
        {
            a1.transform.localScale = Vector3.one;
        }
        else
        {
            a1.transform.localScale = Vector3.zero;
        }
    }

    void OnVideoDataGetFunction(VideoData data,GameObject[] gos,string nos)
    {
        //视频详情
        userId = data.userId;
        if(data.title==null)
            title.text = "未知";
        else
            title.text = data.title;

        if (data.releaseTime == 0)
            pubishTime.text = "未知";
        else
            pubishTime.text = DataClassInterface.TimeNumToTime(data.releaseTime);

        if (data.classificationName == null)
            sort.text = "未知";
        else
            sort.text = data.classificationName;

        if (data.description == null)
            Introduction.text = "未知";
        else
            Introduction.text = data.description;

        watchTime.text = data.playNum.ToString();
        LikeNum.text = data.likeNum.ToString();
        FavoriteNum.text = data.collectNum.ToString();

        //更改视频地址
        foreach (Transform player in players.transform)
        {
            if (player.GetComponent<SvrVideoPlayerDemo>() != null)
                player.GetComponent<SvrVideoPlayerDemo>().VideoUrls[0] = data.path;
        }
        ChangeModel changemodel = GameObject.FindGameObjectWithTag("ChangeModel").GetComponent<ChangeModel>();
        //全景
        if (data.workType == 0)
        {
            //360
            if(data.videoType==0)
            {
                changemodel.RecommenedPlayer = changemodel.Player360;
            }
            //180
            else if(data.videoType==3)
            {
                changemodel.RecommenedPlayer = changemodel.Player180;
            }
        }
        //巨幕影院
        else if (data.workType == 2)
        {
            //2D
            if(data.videoType==6)
            {
                changemodel.RecommenedPlayer = changemodel.Player2D;
            }
            //3D
            else if(data.videoType==5)
            {
                changemodel.RecommenedPlayer = changemodel.Player3DLR;
            }
        }
        //switch (data.workType)
        //{
        //    case 0:
        //        changemodel.RecommenedPlayer = changemodel.Player360;
        //        break;
        //    case 3:
        //        changemodel.RecommenedPlayer = changemodel.Player180;
        //        break;
        //    case 6:
        //        changemodel.RecommenedPlayer = changemodel.Player2D;
        //        break;
        //    default：
        //        changemodel.RecommenedPlayer = changemodel.Player2D;
        //        break;
        //}
        changemodel.CurrentPlayer = changemodel.RecommenedPlayer;
        //changemodel.CurrentPlayer.GetComponent<SvrVideoPlayerDemo>().PlayVideoByIndex(0);
        //GameObject.FindGameObjectWithTag("Loading").GetComponentInChildren<Text>().text += " VideoManagerOver";
        //作者信息
        StartCoroutine(DataClassInterface.IEGetDate(AllData.DataString+"/vr/getUserById?id="+userId.ToString(), new DataClassInterface.OnDataGet<UserData>(OnAuthorDataGet), null));
    }

    //作者信息
    void OnAuthorDataGet(UserData data,GameObject[] gos,string nos)
    {
        if (data.nickName == null)
            AuthorName.text = "未知";
        else
            AuthorName.text = data.nickName;

        StartCoroutine(DataClassInterface.IEGetSprite(data.headImage, (Sprite sprite, GameObject go, string str) => { AuthorPhoto.GetComponent<Image>().sprite = sprite; }, null));
    }

    public Text title;
    public Text pubishTime;
    public Text watchTime;
    public Text sort;
    public Text Introduction;
    public Text LikeNum;
    public Text FavoriteNum;

    public Text AuthorName;
    public Image AuthorPhoto;

    public Transform players;
    public Transform Videos;

    public Transform Content;

}
