using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoShortData : MonoBehaviour
{
    public int VideoId;
    public Text PlayTime;
    public Text LikeTime;
    public Text CollectTime;
    public Text Duration;
    public Sprite BlueEdge;
    public Sprite YellowEdge;
    public Image image;


    public void Init(int videoId,int playTime,int likeTime,int collectTime,string duration)
    {
        VideoId = videoId;
        PlayTime.text = playTime.ToString();
        LikeTime.text = likeTime.ToString();
        CollectTime.text = collectTime.ToString();
        Duration.text = duration;
    }

    public void OnClick()
    {
        GameObject.Find("EventController").GetComponent<Controller>().Enter360DegreeVideos();
        (Controller.panelComeback.Peek() as GameObject).GetComponentInChildren<VideoManager>().Id = VideoId;
    }

    public void Enter()
    {
        image.sprite = YellowEdge;
    }

    public void Exit()
    {
        image.sprite = BlueEdge;
    }
}
