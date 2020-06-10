using System.Net.Mime;
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
    public Text AuthorName;
    public Text VideoName;


    public void Init(int videoId, int playTime, int likeTime, int collectTime, string duration, string videoName, string authorName)
    {
        VideoId = videoId;
        if (playTime != -1)
            PlayTime.text = playTime.ToString();
        if (likeTime != -1)
            LikeTime.text = likeTime.ToString();
        if (collectTime != -1)
            CollectTime.text = collectTime.ToString();
        if (duration != null)
            Duration.text = duration;
        if (videoName != null)
            VideoName.text = videoName;
        if (authorName != null)
            AuthorName.text = authorName;
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
