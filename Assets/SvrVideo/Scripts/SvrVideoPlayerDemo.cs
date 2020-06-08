// Copyright 2018 Skyworth VR. All rights reserved.
//魔改
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public enum StereoType { None, LeftRight, TopBottom }

public class SvrVideoPlayerDemo : MonoBehaviour
{
    public SvrVideoPlayer SvrVideoPlayer;

    //魔改
    public bool Finish;

    [SerializeField]
    public string[] VideoUrls;
    [SerializeField]
    public SvrVideoControlPanel SvrVideoControlPanel;
    [SerializeField]
    private StereoType StereoType;

    public bool live = false;

    private int videoCount;
    private int currentPlayingVideoIndex;

    private void Start()
    {
        videoCount = VideoUrls.Length;
        currentPlayingVideoIndex = 0;

        if (SvrVideoPlayer == null)
            SvrVideoPlayer = GetComponent<SvrVideoPlayer>();

        SvrVideoPlayer.OnEnd += OnEnd;
        SvrVideoPlayer.OnReady += OnReady;
        SvrVideoPlayer.OnVolumeChange += OnVolumeChange;
        SvrVideoPlayer.OnProgressChange += OnProgressChange;
        SvrVideoPlayer.OnVideoError += OnVideoError;
        SvrVideoPlayer.OnVideoPlayerStatusChange += OnVideoPlayerStatusChange;
        //魔改
        //SvrVideoPlayer.OnBufferStart += OnBufferStart;
        //SvrVideoPlayer.OnBufferFinish += OnBufferFinish;
        Finish = true;

        if (GameObject.FindGameObjectWithTag("ChangeModel").GetComponent<ChangeModel>().Finish)
            PlayVideoByIndex(0,live);

    }

    private void OnEnable()
    {
        if (videoCount != 0)
        {
            PlayVideoByIndex(0,live);
            Debug.Log(222222222);
        }
    }

    private void OnDisable()
    {
        SvrVideoPlayer.Stop();
        SvrVideoPlayer.Release() ;
    }

    public void PlayVideoByIndex(int index,bool live)
    {
        if (videoCount < 0)
            return;

        if (currentPlayingVideoIndex + index > videoCount - 1)
            currentPlayingVideoIndex = 0;
        else if (currentPlayingVideoIndex + index < 0)
            currentPlayingVideoIndex = videoCount - 1;
        else
            currentPlayingVideoIndex += index;

        // Use CreatVideoPlayer before PreparedPlayVideo.
        SvrVideoPlayer.CreatVideoPlayer();
        // Set video data source.

        SvrVideoPlayer.PreparedPlayVideo(VideoUrls[currentPlayingVideoIndex], live);

        // Set video stereo mode.
        if (StereoType == StereoType.LeftRight)
            SvrVideoPlayer.SetPlayMode3DLeftRight();
        else if (StereoType == StereoType.TopBottom)
            SvrVideoPlayer.SetPlayMode3DTopBottom();
        else
            SvrVideoPlayer.SetPlayMode2D();

        string name = VideoUrls[currentPlayingVideoIndex].Substring(VideoUrls[currentPlayingVideoIndex].LastIndexOf('/') + 1);
        SvrVideoControlPanel.SetVideoName(name);
        SvrVideoControlPanel.SetVideoCurrentTime(0);

    }

    private void OnEnd()
    {
        SvrVideoPlayer.Release();
        // Play next video.
        PlayVideoByIndex(1,live);
    }

    private void OnReady()
    {
        long totalTime = SvrVideoPlayer.GetVideoDuration();
        SvrVideoControlPanel.SetVideoTotalTime(totalTime);
    }

    private void OnVolumeChange(float volumePercent)
    {
        SvrVideoControlPanel.ChangeVolumeByDevice(volumePercent);
    }

    private void OnProgressChange(int time)
    {
        SvrVideoControlPanel.SetVideoCurrentTime(time);
    }

    private void OnVideoError(ExceptionEvent errorCode, string errMessage)
    {
        Debug.LogErrorFormat("{0}:{1}", errorCode.ToString(), errMessage);
    }

    private void OnVideoPlayerStatusChange(bool status)
    {
        SvrVideoControlPanel.SetPlayControlButtonStatus(status);
    }

    private void OnApplicationQuit()
    {
        SvrVideoPlayer.OnEnd -= OnEnd;
        SvrVideoPlayer.OnReady -= OnReady;
        SvrVideoPlayer.OnVolumeChange -= OnVolumeChange;
        SvrVideoPlayer.OnProgressChange -= OnProgressChange;
        SvrVideoPlayer.OnVideoError -= OnVideoError;
        SvrVideoPlayer.OnVideoPlayerStatusChange -= OnVideoPlayerStatusChange;
    }


}
