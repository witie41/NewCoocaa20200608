using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetVideoData : MonoBehaviour
{
    enum pos
    {
        Mid,
        Left,
        Right,
        Flash1,
        Flash2
    }
    class VideoPanel
    {
      public  GameObject panel;
      public pos mypos;
        public GameObject videopanel;
    }
    GameObject mid;
    GameObject left;
    GameObject right;
    GameObject [] rooms;
     Vector3 leftpanelVector;
     Vector3 midpanelVector;
     Vector3 rightpanelVector;
     Vector3 flash1;
     Vector3 flash2;
   // GameObject[] Rooms = new GameObject [1000];
    VideoPanel[] videopanels = new VideoPanel[5];
    Dictionary<int, VideoData> videoDatas = new Dictionary<int, VideoData>();
    Dictionary<int, LivingRoomData> livingDatas = new Dictionary<int, LivingRoomData>();
     int index;
     int allpage;
     int allroom;
    int indexvideo;
    int indexliving;
    int indexroom;
    bool isFlash = false;
    int allvideoroom = 0;
    int allivingroom = 0;
    bool isRun = true;
    GameObject room;
    GameObject flashroom1 ;
    GameObject flashroom2;
    GameObject posmid;
    GameObject posleft;
    GameObject posright;
    GameObject flash;
    GameObject flash_1;
    Vector3 speed;
    Vector3 speed1;
    Vector3 speed2;
    Vector3 speed3;
    bool needright = false;
    bool needleft = false;
    RoomButtonControl[] roominfo = new RoomButtonControl[1000];
    GameObject HidePanel1;
    GameObject HidePanel2;
    
    // Start is called before the first frame update
    void Start()
    {
        /*
         物体赋值*/
        HidePanel1 = GameObject.FindGameObjectWithTag("Hide1");
        HidePanel2 = GameObject.FindGameObjectWithTag("HIde2");
        room = Resources.Load("LivingRoom/Living room folder/RoomsButton") as GameObject;
        flashroom1 = GameObject.FindWithTag("flash1");
        flashroom2 = GameObject.FindWithTag("flash2");
        flash1 = flashroom1.GetComponent<RectTransform>().anchoredPosition3D;
        flash2 = flashroom2.GetComponent<RectTransform>().anchoredPosition3D;
        posmid = GameObject.FindWithTag("Posmid");
        posleft = GameObject.FindWithTag("Posleft");
        posright = GameObject.FindWithTag("Posright");
        indexvideo = 0;
        indexliving = 0;
        indexroom = 0;
        index = 0;//初始页面值；
        rooms = new GameObject[1000];
        mid = GameObject.FindWithTag("Mid");
        left = GameObject.FindWithTag("Left");
        right = GameObject.FindWithTag("Right");
        flash = GameObject.FindGameObjectWithTag("Flash");
        flash_1 = GameObject.FindGameObjectWithTag("Flash1");
        leftpanelVector = posleft.GetComponent<RectTransform>().anchoredPosition3D;
        rightpanelVector = posright.GetComponent<RectTransform>().anchoredPosition3D;
        midpanelVector = posmid.GetComponent<RectTransform>().anchoredPosition3D;
        /*
         获取数据
        */
        StartCoroutine(DataClassInterface.IEGetDate<VideoData[]>(AllData.DataString + "/vr/getVideoList" , new DataClassInterface.OnDataGet<VideoData[]>(GetVideo), null));
        StartCoroutine(DataClassInterface.IEGetDate<LivingRoomData[]>(AllData.DataString + "/vr/getBroadcastList" , new DataClassInterface.OnDataGet<LivingRoomData[]>(GetLiving), null));
        
        /*
         页面初始状态
         */
        videopanels[0] = new VideoPanel();
        videopanels[0].panel = posmid;
        videopanels[0].videopanel = mid;
        videopanels[0].mypos = pos.Mid;
        videopanels[2] = new VideoPanel();
        videopanels[2].panel = posleft;
        videopanels[2].mypos = pos.Left;
        videopanels[2].videopanel = left;
        videopanels[1] = new VideoPanel();
        videopanels[1].panel = posright;
        videopanels[1].videopanel = right;
        videopanels[1].mypos = pos.Right;
        videopanels[3] = new VideoPanel();
        videopanels[3].panel = flashroom1;
        videopanels[3].videopanel = flash;
        videopanels[3].mypos = pos.Flash1;
        videopanels[4] = new VideoPanel();
        videopanels[4].panel = flashroom2;
        videopanels[4].videopanel = flash_1;
        videopanels[4].mypos = pos.Flash2;
        for (int i = 0;i<200;i++)
        {
            roominfo[i] = new RoomButtonControl(VideoType.Video2D, 0, "");
        }
        //videopanels[0].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickMid);
        //videopanels[1].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickRight);
        //videopanels[2].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickLeft);
        //FlashTest(0,ref allroom,9,mid);
        //FlashTest(1,ref allroom,9,right);
        //FlashTest(2, ref allroom, 9, left);
        // FlashTest(3, ref allroom, 9, flash);
        //FlashTest(4, ref allroom, 9, flash_1);
        HidePanel1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("啊这！！"+index+"数据"+allroom+"+"+allpage);
        if (isFlash)
        {
            StartCoroutine(DataClassInterface.IEGetDate<VideoData[]>(AllData.DataString + "/vr/getVideoList", new DataClassInterface.OnDataGet<VideoData[]>(GetVideo), null));
            StartCoroutine(DataClassInterface.IEGetDate<LivingRoomData[]>(AllData.DataString + "/vr/getBroadcastList", new DataClassInterface.OnDataGet<LivingRoomData[]>(GetLiving), null));
            allroom = allivingroom + allvideoroom;
                if (allroom % 9 == 0)
            {
                allpage = allroom / 9;
            }
            else
            {
                allpage = allroom / 9 + 1;
            }
            isFlash = false;
        }

    
           if(isRun)
        {
            if (videoDatas.Count != 0 && livingDatas.Count != 0)
            {
                 
                allroom = videoDatas.Count + livingDatas.Count;
                // Debug.Log("数据数量video" +videoDatas.Count+ "直播间数量" + livingDatas.Count);
                for (int i = 0; i <= videoDatas.Count + livingDatas.Count; i++)
                {
                    //rooms[i] = Resources.Load("LivingRoom/Living room folder/RoomsButton") as GameObject;
                    //Rooms[i] = rooms[i];
                    VideoType videoType;

                    if (i % 2 == 0)
                    {
                        if (videoDatas.ContainsKey(indexvideo))
                        {
                            if (videoDatas[indexvideo].videoType == 6 && videoDatas[indexvideo].workType == 2)
                            {
                                videoType = VideoType.Video2D;
                                roominfo[i] = new RoomButtonControl(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title);
                                roominfo[i].Setphoto(videoDatas[indexvideo].cover);
                                // Rooms[i].GetComponent<RoomButtonControl>().Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                                //roominfo[indexvideo].Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                            }
                            else if (videoDatas[indexvideo].videoType == 5 && videoDatas[indexvideo].workType == 2)
                            {

                                videoType = VideoType.Video3D;
                                roominfo[i] = new RoomButtonControl(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title);
                                roominfo[i].Setphoto(videoDatas[indexvideo].cover);
                                //roominfo[indexvideo].Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                            }
                            else if (videoDatas[indexvideo].videoType == 2 && videoDatas[indexvideo].workType == 0)
                            {
                                //roominfo[i] = new RoomButtonControl();
                                videoType = VideoType.Video180;
                                //roominfo[indexvideo].Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                                roominfo[i] = new RoomButtonControl(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title);
                                roominfo[i].Setphoto(videoDatas[indexvideo].cover);
                            }
                            else if (videoDatas[indexvideo].videoType == 0 && videoDatas[indexvideo].workType == 0)
                            {
                                // roominfo[i] = new RoomButtonControl();
                                videoType = VideoType.Video360;
                                //  roominfo[indexvideo].Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                                roominfo[i] = new RoomButtonControl(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title);
                                roominfo[i].Setphoto(videoDatas[indexvideo].cover);
                            }
                            else
                            {
                                continue;
                            }
                            indexvideo++;
                        }
                        else
                        {
                            if (livingDatas.ContainsKey(indexliving))
                            {
                                if (livingDatas[indexliving].roomStatus == "1")
                                {
                                    // roominfo[i] = new RoomButtonControl();
                                    videoType = VideoType.Live_On;
                                    // roominfo[i].Init(videoType, livingDatas[i].id, livingDatas[i].title, livingDatas[i].coverImg1);
                                    roominfo[i] = new RoomButtonControl(videoType, livingDatas[i].id, livingDatas[i].title);
                                    roominfo[i].Setphoto(livingDatas[indexliving].coverImg1);
                                    //Rooms[i].GetComponent<RoomButtonControl>().Init(videoType, livingDatas[i].id, livingDatas[i].title, livingDatas[i].coverImg1);

                                }
                                else
                                {
                                    // roominfo[i] = new RoomButtonControl();
                                    videoType = VideoType.Live_Off;
                                    // roominfo[i].Init(videoType, livingDatas[i].id, livingDatas[i].title, livingDatas[i].coverImg1);
                                    roominfo[i] = new RoomButtonControl(videoType, livingDatas[i].id, livingDatas[i].title);
                                    roominfo[i].Setphoto(livingDatas[indexliving].coverImg1);

                                }
                                indexliving++;
                            }
                            else
                            {

                            }
                        }
                    }
                    else
                    {
                        if (livingDatas.ContainsKey(indexliving))
                        {
                            if (livingDatas[indexliving].roomStatus == "1")
                            {
                                // roominfo[i] = new RoomButtonControl();
                                videoType = VideoType.Live_On;
                                // roominfo[i].Init(videoType, livingDatas[i].id, livingDatas[i].title, livingDatas[i].coverImg1);
                                roominfo[i] = new RoomButtonControl(videoType, livingDatas[indexliving].id, livingDatas[indexliving].title);
                                roominfo[i].Setphoto(livingDatas[indexliving].coverImg1);


                            }
                            else
                            {
                                // roominfo[i] = new RoomButtonControl();
                                videoType = VideoType.Live_Off;
                                // roominfo[i].Init(videoType, livingDatas[i].id, livingDatas[i].title, livingDatas[i].coverImg1);
                                roominfo[i] = new RoomButtonControl(videoType, livingDatas[indexliving].id, livingDatas[indexliving].title);
                                roominfo[i].Setphoto(livingDatas[indexliving].coverImg1);


                            }
                            indexliving++;
                        }
                        else
                        {
                            if (videoDatas.ContainsKey(indexvideo))
                            {
                                if (videoDatas[indexvideo].videoType == 6 && videoDatas[indexvideo].workType == 2)
                                {
                                    videoType = VideoType.Video2D;
                                    roominfo[i] = new RoomButtonControl(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title);
                                    roominfo[i].Setphoto(videoDatas[indexvideo].cover);

                                    // Rooms[i].GetComponent<RoomButtonControl>().Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                                    //roominfo[indexvideo].Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                                }
                                else if (videoDatas[indexvideo].videoType == 5 && videoDatas[indexvideo].workType == 2)
                                {

                                    videoType = VideoType.Video3D;
                                    roominfo[i] = new RoomButtonControl(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title);
                                    roominfo[i].Setphoto(videoDatas[indexvideo].cover);

                                    //roominfo[indexvideo].Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                                }
                                else if (videoDatas[indexvideo].videoType == 2 && videoDatas[indexvideo].workType == 0)
                                {
                                    //roominfo[i] = new RoomButtonControl();
                                    videoType = VideoType.Video180;
                                    //roominfo[indexvideo].Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                                    roominfo[i] = new RoomButtonControl(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title);
                                    roominfo[i].Setphoto(videoDatas[indexvideo].cover);

                                }
                                else if (videoDatas[indexvideo].videoType == 0 && videoDatas[indexvideo].workType == 0)
                                {
                                    // roominfo[i] = new RoomButtonControl();
                                    videoType = VideoType.Video360;
                                    //  roominfo[indexvideo].Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                                    roominfo[i] = new RoomButtonControl(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title);
                                    roominfo[i].Setphoto(videoDatas[indexvideo].cover);

                                }
              
                                indexvideo++;
                            }
                            else
                            {

                            }
                        }
                    }
                    isRun = false; 
                }

                FlashMyRoom(index,ref allroom, 9, mid);
                FlashMyRoom(index + 1,ref allroom, 9, right);
                FlashMyRoom(index + 2,ref allroom, 9, flash_1);
                HidePanel1.SetActive(false);
                HideAll(videopanels[3].videopanel);
                HideAll(videopanels[4].videopanel);
                //Instantiate(Rooms[0], mid.transform);
                //Debug.Log("所有的数据" + Rooms.Length + "区别数据+" + Rooms[11] + "||" + Rooms[230]);
            }
           
        }

           if(needright)
        {
            if (videopanels[0].mypos == pos.Mid)
            {
                videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D, leftpanelVector, ref speed, 0.3f);
                videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D, midpanelVector, ref speed1, 0.3f);
                videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D, flash1, ref speed2, 0.3f);
                videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D = flash2;
                videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D, rightpanelVector, ref speed3, 0.3f);
                HideAll(videopanels[2].videopanel);
                GetAll(videopanels[4].videopanel);
                if (Mathf.Abs(posmid.GetComponent<RectTransform>().anchoredPosition3D.magnitude-leftpanelVector.magnitude)<0.3f)
                {
                    videopanels[0].mypos = pos.Left;
                    videopanels[1].mypos = pos.Mid;
                    videopanels[2].mypos = pos.Flash1;
                    videopanels[3].mypos = pos.Flash2;
                    videopanels[4].mypos = pos.Right;
                    Debug.Log("底部1");
                    FlashMyRoom(index + 2, ref allroom, 9, videopanels[3].videopanel);
                    Debug.Log("底部2");
                    FlashMyRoom(index - 2, ref allroom, 9, videopanels[2].videopanel);
                    Debug.Log("底部3");
                    //videopanels[1].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickMid);
                    //videopanels[0].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickLeft);
                    //videopanels[4].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickRight);
                    GetAll(videopanels[0].videopanel);
                    GetAll(videopanels[1].videopanel);
                    GetAll(videopanels[2].videopanel);
                    GetAll(videopanels[3].videopanel);
                    GetAll(videopanels[4].videopanel);
                    HideAll(videopanels[2].videopanel);
                    HideAll(videopanels[3].videopanel);
                    needright = false;
                }
            }
            else if (videopanels[1].mypos == pos.Mid)
            {

                videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D, flash1, ref speed, 0.3f);
                videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D, leftpanelVector, ref speed1, 0.3f);
                videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D = flash2;
                videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D, rightpanelVector, ref speed2, 0.3f);              
                videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D, midpanelVector, ref speed3, 0.3f);
                HideAll(videopanels[0].videopanel);
                GetAll(videopanels[3].videopanel);
                if (Mathf.Abs(videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D.magnitude - midpanelVector.magnitude) < 0.3f)
                {
                    videopanels[1].mypos = pos.Left;
                    videopanels[4].mypos = pos.Mid;
                    videopanels[0].mypos = pos.Flash1;
                    videopanels[2].mypos = pos.Flash2;
                    videopanels[3].mypos = pos.Right;
                    //videopanels[4].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickMid);
                    //videopanels[1].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickLeft);
                    //videopanels[3].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickRight);
                    FlashMyRoom(index + 2, ref allroom, 9, videopanels[2].videopanel);
                    FlashMyRoom(index - 2, ref allroom, 9, videopanels[0].videopanel);
                    GetAll(videopanels[0].videopanel);
                    GetAll(videopanels[1].videopanel);
                    GetAll(videopanels[2].videopanel);
                    GetAll(videopanels[3].videopanel);
                    GetAll(videopanels[4].videopanel);
                    HideAll(videopanels[2].videopanel);
                    HideAll(videopanels[0].videopanel);
                    needright = false;
                }
            }
            else if (videopanels[2].mypos == pos.Mid)
            {
                videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D, midpanelVector, ref speed, 0.3f);
                videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D, rightpanelVector, ref speed1, 0.3f);
                videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D = flash2;
                videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D, flash1, ref speed2, 0.3f);
                videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D, leftpanelVector, ref speed3, 0.3f);
                HideAll(videopanels[3].videopanel);
                GetAll(videopanels[1].videopanel);
                if (Mathf.Abs(videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D.magnitude - midpanelVector.magnitude) < 0.3f)
                {
                    videopanels[1].mypos = pos.Right;
                    videopanels[4].mypos = pos.Flash2;
                    videopanels[0].mypos = pos.Mid;
                    videopanels[2].mypos = pos.Left;
                    videopanels[3].mypos = pos.Flash1;
                    //videopanels[0].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickMid);
                    //videopanels[2].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickLeft);
                    //videopanels[1].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickRight);
                    FlashMyRoom(index + 2, ref allroom, 9, videopanels[4].videopanel);
                    FlashMyRoom(index - 2, ref allroom, 9, videopanels[3].videopanel);
                    GetAll(videopanels[0].videopanel);
                    GetAll(videopanels[1].videopanel);
                    GetAll(videopanels[2].videopanel);
                    GetAll(videopanels[3].videopanel);
                    GetAll(videopanels[4].videopanel);
                    HideAll(videopanels[3].videopanel);
                    HideAll(videopanels[4].videopanel);
                    needright = false;
                }
            }
            else if (videopanels[3].mypos == pos.Mid)
            {

                videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D, rightpanelVector, ref speed, 0.3f);
                videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D, flash1, ref speed1, 0.3f);
                videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D = flash2;
                videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D, leftpanelVector, ref speed2, 0.3f);
                videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D, midpanelVector, ref speed3, 0.3f);
                HideAll(videopanels[4].videopanel);
                GetAll(videopanels[0].videopanel);
                if (Mathf.Abs(videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D.magnitude - midpanelVector.magnitude) < 0.3f)
                {
                    videopanels[3].mypos = pos.Left;
                    videopanels[2].mypos = pos.Mid;
                    videopanels[4].mypos = pos.Flash1;
                    videopanels[1].mypos = pos.Flash2;
                    videopanels[0].mypos = pos.Right;
                    //videopanels[2].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickMid);
                    //videopanels[3].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickLeft);
                    //videopanels[0].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickRight);
                    FlashMyRoom(index + 2, ref allroom, 9, videopanels[1].videopanel);
                    FlashMyRoom(index - 2, ref allroom, 9, videopanels[4].videopanel);
                    GetAll(videopanels[0].videopanel);
                    GetAll(videopanels[1].videopanel);
                    GetAll(videopanels[2].videopanel);
                    GetAll(videopanels[3].videopanel);
                    GetAll(videopanels[4].videopanel);
                    HideAll(videopanels[1].videopanel);
                    HideAll(videopanels[4].videopanel);
                    needright = false;
                }
            }
            else if (videopanels[4].mypos == pos.Mid)
            {
                videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D, rightpanelVector, ref speed, 0.3f);
                videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D, flash1, ref speed1, 0.3f);
                videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D = flash2;
                videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D, midpanelVector, ref speed2, 0.3f);
                videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D, leftpanelVector, ref speed3, 0.3f);
                HideAll(videopanels[1].videopanel);
                GetAll(videopanels[2].videopanel);
                if (Mathf.Abs(videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D.magnitude - midpanelVector.magnitude) < 0.3f)
                {
                    videopanels[4].mypos = pos.Left;
                    videopanels[3].mypos = pos.Mid;
                    videopanels[1].mypos = pos.Flash1;
                    videopanels[0].mypos = pos.Flash2;
                    videopanels[2].mypos = pos.Right;
                   // videopanels[3].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickMid);
                   // videopanels[4].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickLeft);
                    //videopanels[2].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickRight);
                    FlashMyRoom(index + 2, ref allroom, 9, videopanels[0].videopanel);
                    FlashMyRoom(index - 2, ref allroom, 9, videopanels[1].videopanel);
                    GetAll(videopanels[0].videopanel);
                    GetAll(videopanels[1].videopanel);
                    GetAll(videopanels[2].videopanel);
                    GetAll(videopanels[3].videopanel);
                    GetAll(videopanels[4].videopanel);
                    HideAll(videopanels[0].videopanel);
                    HideAll(videopanels[1].videopanel);
                    needright = false;
                }
            }
     
        }
           if (needleft)
        {
            if (videopanels[0].mypos == pos.Mid)
            {
                videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D, rightpanelVector, ref speed, 0.3f);
                videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D, leftpanelVector, ref speed1, 0.3f);
                videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D, midpanelVector, ref speed2, 0.3f);
                videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D = flash1;
                videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D, flash2, ref speed3, 0.3f);
                HideAll(videopanels[1].videopanel);
                GetAll(videopanels[3].videopanel);
                if (Mathf.Abs(videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D.magnitude - rightpanelVector.magnitude) < 0.3f)
                {
                    videopanels[0].mypos = pos.Right;
                    videopanels[1].mypos = pos.Flash2;
                    videopanels[2].mypos = pos.Mid;
                    videopanels[3].mypos = pos.Left;
                    videopanels[4].mypos = pos.Flash1;
                   // videopanels[2].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickMid);
                   // videopanels[3].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickLeft);
                  //  videopanels[0].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickRight);
                    FlashMyRoom(index + 2, ref allroom, 9, videopanels[1].videopanel);
                    FlashMyRoom(index - 2, ref allroom, 9, videopanels[4].videopanel);
                    GetAll(videopanels[0].videopanel);
                    GetAll(videopanels[1].videopanel);
                    GetAll(videopanels[2].videopanel);
                    GetAll(videopanels[3].videopanel);
                    GetAll(videopanels[4].videopanel);
                    HideAll(videopanels[1].videopanel);
                    HideAll(videopanels[4].videopanel);
                    needleft = false;
                }
            }
            else if (videopanels[1].mypos == pos.Mid)
            {
                videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D, midpanelVector, ref speed, 0.3f);
                videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D, rightpanelVector, ref speed1, 0.3f);
                videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D = flash1 ;
                videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D, leftpanelVector, ref speed2, 0.3f);
                videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D, flash2, ref speed3, 0.3f);
                HideAll(videopanels[4].videopanel);
                GetAll(videopanels[2].videopanel);
                if (Mathf.Abs(videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D.magnitude - midpanelVector.magnitude) < 0.3f)
                {
                    videopanels[1].mypos = pos.Right;
                    videopanels[4].mypos = pos.Flash2;
                    videopanels[0].mypos = pos.Mid;
                    videopanels[2].mypos = pos.Left;
                    videopanels[3].mypos = pos.Flash1;
                   // videopanels[0].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickMid);
                   // videopanels[2].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickLeft);
//videopanels[1].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickRight);
                    FlashMyRoom(index + 2, ref allroom, 9, videopanels[4].videopanel);
                    FlashMyRoom(index - 2, ref allroom, 9, videopanels[3].videopanel);
                    GetAll(videopanels[0].videopanel);
                    GetAll(videopanels[1].videopanel);
                    GetAll(videopanels[2].videopanel);
                    GetAll(videopanels[3].videopanel);
                    GetAll(videopanels[4].videopanel);
                    HideAll(videopanels[3].videopanel);
                    HideAll(videopanels[4].videopanel);
                    needleft = false;
                }
            }
            else if (videopanels[2].mypos == pos.Mid)
            {
                videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D, flash2, ref speed, 0.3f);
                videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D, leftpanelVector, ref speed1, 0.3f);
                videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D = flash1;
                videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D, midpanelVector, ref speed2, 0.3f);
                videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D, rightpanelVector, ref speed3, 0.3f);
                HideAll(videopanels[0].videopanel);
                GetAll(videopanels[4].videopanel);
                if (Mathf.Abs(videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D.magnitude - midpanelVector.magnitude) < 0.3f)
                {
                    videopanels[2].mypos = pos.Right;
                    videopanels[0].mypos = pos.Flash2;
                    videopanels[3].mypos = pos.Mid;
                    videopanels[4].mypos = pos.Left;
                    videopanels[1].mypos = pos.Flash1;
                  //  videopanels[3].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickMid);
                   // videopanels[4].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickLeft);
                   // videopanels[2].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickRight);
                    FlashMyRoom(index + 2, ref allroom, 9, videopanels[0].videopanel);
                    FlashMyRoom(index - 2, ref allroom, 9, videopanels[1].videopanel);
                    GetAll(videopanels[0].videopanel);
                    GetAll(videopanels[1].videopanel);
                    GetAll(videopanels[2].videopanel);
                    GetAll(videopanels[3].videopanel);
                    GetAll(videopanels[4].videopanel);
                    HideAll(videopanels[0].videopanel);
                    HideAll(videopanels[1].videopanel);
                    needleft = false;
                }
            }
            else if (videopanels[3].mypos == pos.Mid)
            {
                videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D, leftpanelVector, ref speed, 0.3f);
                videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D, midpanelVector, ref speed1, 0.3f);
                videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D = flash1;
                videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D, rightpanelVector, ref speed2, 0.3f);
                videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D, flash2, ref speed3, 0.3f);
                HideAll(videopanels[2].videopanel);
                GetAll(videopanels[1].videopanel);
                if (Mathf.Abs(videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D.magnitude - midpanelVector.magnitude) < 0.3f)
                {
                    videopanels[3].mypos = pos.Right;
                    videopanels[4].mypos = pos.Mid;
                    videopanels[0].mypos = pos.Flash1;
                    videopanels[2].mypos = pos.Flash2;
                    videopanels[1].mypos = pos.Left;
                  //  videopanels[4].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickMid);
                   // videopanels[1].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickLeft);
                  //  videopanels[3].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickRight);
                    FlashMyRoom(index + 2, ref allroom, 9, videopanels[2].videopanel);
                    FlashMyRoom(index - 2, ref allroom, 9, videopanels[0].videopanel);
                    GetAll(videopanels[0].videopanel);
                    GetAll(videopanels[1].videopanel);
                    GetAll(videopanels[2].videopanel);
                    GetAll(videopanels[3].videopanel);
                    GetAll(videopanels[4].videopanel);
                    HideAll(videopanels[0].videopanel);
                    HideAll(videopanels[2].videopanel);
                    needleft = false;
                }
            }
            else if (videopanels[4].mypos == pos.Mid)
            {
                videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[0].panel.GetComponent<RectTransform>().anchoredPosition3D, leftpanelVector, ref speed, 0.3f);
                videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D, midpanelVector, ref speed1, 0.3f);
                videopanels[2].panel.GetComponent<RectTransform>().anchoredPosition3D = flash1;
                videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[3].panel.GetComponent<RectTransform>().anchoredPosition3D, flash2, ref speed2, 0.3f);
                videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D = Vector3.SmoothDamp(videopanels[4].panel.GetComponent<RectTransform>().anchoredPosition3D, rightpanelVector, ref speed3, 0.3f);
                HideAll(videopanels[3].videopanel);
                GetAll(videopanels[0].videopanel);
                if (Mathf.Abs(videopanels[1].panel.GetComponent<RectTransform>().anchoredPosition3D.magnitude - midpanelVector.magnitude) < 0.3f)
                {
                    videopanels[4].mypos = pos.Right;
                    videopanels[3].mypos = pos.Flash2;
                    videopanels[1].mypos = pos.Mid;
                    videopanels[0].mypos = pos.Left;
                    videopanels[2].mypos = pos.Flash1;
                   // videopanels[1].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickMid);
                    //videopanels[0].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickLeft);
                    //videopanels[4].panel.transform.Find("Panels").Find("mid").GetComponent<Button>().onClick.AddListener(ClickRight);
                    
                    FlashMyRoom(index + 2, ref allroom, 9, videopanels[3].videopanel);
                    
                    FlashMyRoom(index - 2, ref allroom, 9, videopanels[2].videopanel);
                    GetAll(videopanels[0].videopanel);
                    GetAll(videopanels[1].videopanel);
                    GetAll(videopanels[2].videopanel);
                    GetAll(videopanels[3].videopanel);
                    GetAll(videopanels[4].videopanel);
                    HideAll(videopanels[2].videopanel);
                    HideAll(videopanels[3].videopanel);
                    needleft = false;
                }
            }
        }
    }
    void FlashTest(int index, int real, int maxView,GameObject panel){
        Destoryall(panel);
        for (int i = 0; i < ((real - index * 9) > maxView ? maxView : (real - index * 9)); i++)
{
    Instantiate(room,panel.transform);
    room.name = i+"";
}
    }

    void GetAll(GameObject panel)
    {
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            panel.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    void HideAll(GameObject panel)
    {
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            panel.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    void Destoryall(GameObject panel)
    {
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            Destroy(panel.transform.GetChild(i).gameObject);
        }
    }
    void GetVideo(VideoData[]datas ,GameObject [] gbj,string str)//获取数据
    {
        int i = 0;
       foreach(VideoData data in datas)
        {
            videoDatas[i] = data;
            i++;
        }
        allvideoroom = videoDatas.Count;
    }
    void GetLiving(LivingRoomData[] datas, GameObject[] gbj, string str)//获取数据
    {
        int i = 0;
        foreach(LivingRoomData data in datas)
        {
            //livingDatas[i] = new LivingRoomData();
            livingDatas[i] = data;
            i++;
        }
        allivingroom = livingDatas.Count;

    }
  public  void ClickMid()//刷新
    {
        isFlash = true;
        Debug.Log("点中间击事件");
    }
   public void ClickLeft()//确定点击的是左边
    {
        Debug.Log("左移动" + index);
      
 
        
            if(HidePanel1.activeSelf == false) {
                HidePanel1.SetActive(true);
            }
            HidePanel2.SetActive(true);   
            index--;
            if(index==0)
            {
               HidePanel1.SetActive(false);
            }
            needleft = true;            
        
    }
  public  void ClickRight()
    {
        Debug.Log(index + "当前页数");
        allroom = livingDatas.Count + videoDatas.Count;
        if (allroom % 9 == 0)
        {
            allpage = allroom / 9;
        }
        else
        {
            allpage = allroom / 9 + 1;
        }
        if (index < allpage)
            {

                if (HidePanel2.activeInHierarchy == false)
                {
                    HidePanel2.SetActive(true);
                }
                HidePanel1.SetActive(true);
                index++;
            if(index == allpage-1)
            {
              if (HidePanel2.activeSelf == true)
                {
                    HidePanel2.SetActive(false);
                }
           }
                needright = true;
            }
          

        
    }



    /**private void Creat(GameObject gameobject)
        {
            
            allroom = allivingroom + allvideoroom;
           
            if (allroom % 9 == 0)
            {
                allpage = allroom / 9;
            }
            else
            {
                allpage = allroom / 9 + 1;
            }
          for(int i =indexroom; i<=9*index&&indexroom<=allroom;indexroom++,i++)
            {

                if(i%2==0&&indexvideo<=allvideoroom)
                {
                    VideoType videoType;
                    if (videoDatas[indexvideo].videoType == 6 )
                    {
                        Instantiate(room, mid.transform);
                        room.name = i + "";
                        videoType = VideoType.Video2D;
                        room.GetComponent<RoomButtonControl>().Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                    }
                    else if (videoDatas[indexvideo].videoType == 5 )
                    {
                        Instantiate(room, mid.transform);
                        room.name = i + "";
                        videoType = VideoType.Video3D;
                        room.GetComponent<RoomButtonControl>().Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                    }
                    else if (videoDatas[indexvideo].videoType == 2 )
                    {
                        Instantiate(room, mid.transform);
                        room.name = i + "";
                        videoType = VideoType.Video180;
                        room.GetComponent<RoomButtonControl>().Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                    }
                    else if (videoDatas[indexvideo].videoType == 0)
                    {
                        Instantiate(room, mid.transform);
                        room.name = i + "";
                        videoType = VideoType.Video360;
                        room.GetComponent<RoomButtonControl>().Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                        indexvideo++;
                    }

                    else
                    {
                        Instantiate(room, mid.transform);
                        room.name = i + "";
                        videoType = VideoType.Video180;
                        room.GetComponent<RoomButtonControl>().Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                        indexvideo++;
                    }
                    
                }
                else
                {

                    if(indexliving<=allivingroom)
                    {

                            VideoType videoType;
                        if (livingDatas[indexliving].roomStatus == "1")
                        {
                            Instantiate(room, gameObject.transform);
                            room.name = i + "";
                            videoType = VideoType.Live_On;
                            room.GetComponent<RoomButtonControl>().Init(videoType, livingDatas[indexliving].id, livingDatas[indexliving].title, livingDatas[indexliving].coverImg1);

                        }
                        else
                        {
                            Instantiate(room, gameobject.transform);
                            room.name = i + "";
                            videoType = VideoType.Live_Off;
                            room.GetComponent<RoomButtonControl>().Init(videoType, livingDatas[indexliving].id, livingDatas[indexliving].title, livingDatas[indexliving].coverImg1);

                        }
                        indexliving++;
                    }
                    else
                    {
                        if(indexvideo<=videoDatas.Count)
                        {
                            VideoType videoType;
                            if (videoDatas[indexvideo].videoType == 6)
                            {
                                Instantiate(room, gameobject.transform);
                                room.name = i + "";
                                videoType = VideoType.Video2D;
                                room.GetComponent<RoomButtonControl>().Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].classificationName, videoDatas[indexvideo].cover);
                            }
                            else if (videoDatas[indexvideo].videoType == 5 )
                            {
                                Instantiate(room, gameobject.transform);
                                room.name = i + "";
                                videoType = VideoType.Video2D;
                                room.GetComponent<RoomButtonControl>().Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                            }
                            else if (videoDatas[indexvideo].videoType == 2)
                            {
                                Instantiate(room, gameobject.transform);
                                room.name = i + "";
                                videoType = VideoType.Video180;
                                room.GetComponent<RoomButtonControl>().Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                            }
                            else if (videoDatas[indexvideo].videoType == 0 )
                            {
                                Instantiate(room, gameobject.transform);
                                room.name = i + "";
                                videoType = VideoType.Video360;
                                room.GetComponent<RoomButtonControl>().Init(videoType, videoDatas[indexvideo].workId, videoDatas[indexvideo].title, videoDatas[indexvideo].cover);
                            }
                            indexvideo++;
                        }
                        else
                        {
                         
                            break;
                        }
                    }
                }
                //Debug.Log("name:" + room.name + "数据" + allivingroom + "   " + allvideoroom + "   " + "索引" + indexliving + "  " + indexvideo);
                isRun = false;
            }//初始化

          

        }
*/
    private void FlashMyRoom(int index,ref int real, int maxView,GameObject panel)//index:当前页数，real:实际数据 maxview：最大显示房间
    {
 
   
        if(index <0)
        {
            Debug.Log("出界");
            return;
        }
        Destoryall(panel);
        for (int i = 0; i < ((real - index * 9) > maxView ? maxView : (real - index * 9)); i++)
       //for(int i = 1;i<=1;i++)
        {
            
               // Destroy(GameObject.Find("RoomsButton(Clone)"));
               GameObject gbj = Instantiate(room, panel.transform);
                gbj.transform.Find("LeftTag").gameObject.SetActive(false);
                gbj.transform.Find("RightTag").gameObject.SetActive(false);
                gbj.transform.Find("Name").transform.GetComponent<Text>().text = roominfo[i+index*9].vname;
                StartCoroutine(DataClassInterface.IEGetSprite(roominfo[i + index * 9].photo, new DataClassInterface.OnDataGetSprite(GetSprite), gbj));
                gbj.name = roominfo[i + index * 9].ID + "";


            if (roominfo[i + index * 9].VType == VideoType.Live_Off || roominfo[i + index * 9].VType == VideoType.Live_On)
            {
                           gbj.transform.Find("LeftTag").gameObject.SetActive(true);

                gbj.GetComponent<Button>().onClick.AddListener(() => {
                    GameObject.Find("EventController").GetComponent<Controller>().EnterLivingRoom();
                    if (GameObject.Find("Living room"))
                    {
                        GameObject.Find("Living room").GetComponentInChildren<MsgManager>().CurrentId = Int32.Parse(gbj.name);
                    }
                    else if (GameObject.Find("Living room(Clone)"))
                    {
                                     GameObject.Find("Living room(Clone)").GetComponentInChildren<MsgManager>().CurrentId = Int32.Parse(gbj.name);
                    }
                }) ;

                    if (roominfo[i + index * 9].VType == VideoType.Live_On)
                    {
                        gbj.transform.Find("LeftTag").GetComponentInChildren<Text>().text = "直播中";
                        gbj.transform.Find("LeftTag").GetComponent<Image>().color = Color.green;
                    }
                    if (roominfo[i + index * 9].VType == VideoType.Live_Off)
                    {
                        gbj.transform.Find("LeftTag").GetComponentInChildren<Text>().text = "未开播";
                        gbj.transform.Find("LeftTag").GetComponent<Image>().color = Color.gray;
                    }
                }
                //视频
                else
                {
                    gbj.transform.Find("RightTag").gameObject.SetActive(true);
               // Debug.Log("添加方法" + i + "画布名称" + panel.name + "名称·1" + roominfo[i + index * 9].vname + "视频：" + roominfo[i + index * 9].ID + "视频分类" + roominfo[i + index * 9].VType);

                gbj.GetComponent<Button>().onClick.AddListener(() =>
                    {
                       // Debug.Log("添加方法" + i + "画布名称" + panel.name + "名称·1" + roominfo[i + index * 9].vname + "视频：" + roominfo[i + index * 9].ID + "视频分类" + roominfo[i + index * 9].VType);

                        GameObject.Find("EventController").GetComponent<Controller>().Enter360DegreeVideos();
                        if(GameObject.Find("Three hundred and sixty dergee living room"))
                        {
                            GameObject.Find("Three hundred and sixty dergee living room").GetComponentInChildren<VideoManager>().Id = Int32.Parse(gbj.name);
                        }
                      else  if (GameObject.Find("Three hundred and sixty dergee living room(Clone)"))
                        {
                            Debug.Log("进入直播间");
                            GameObject.Find("Three hundred and sixty dergee living room(Clone)").GetComponentInChildren<VideoManager>().Id = Int32.Parse(gbj.name);
                        }
                    });
                    switch (roominfo[i + index * 9].VType)
                    {
                        case VideoType.Video2D:
                            gbj.transform.Find("RightTag").GetComponentInChildren<Text>().text = "2D";
                            gbj.transform.Find("RightTag").GetComponent<Image>().color = Color.blue;
                            break;
                        case VideoType.Video3D:
                            gbj.transform.Find("RightTag").GetComponentInChildren<Text>().text = "3D";
                            gbj.transform.Find("RightTag").GetComponent<Image>().color = Color.blue;
                            break;
                        case VideoType.Video180:
                            gbj.transform.Find("RightTag").GetComponentInChildren<Text>().text = "180";
                            gbj.transform.Find("RightTag").GetComponent<Image>().color = Color.red;
                            break;
                        case VideoType.Video360:
                            gbj.transform.Find("RightTag").GetComponentInChildren<Text>().text = "360";
                            gbj.transform.Find("RightTag").GetComponent<Image>().color = Color.red;
                            break;
                        default:
                            Debug.LogError("VType有错误");
                            break;

                    }
                }
                         
        
            

        }

    }
    private int NextPage(int index, int real, int maxView,GameObject game)
    {
  
        index++;
        FlashMyRoom(index, ref allroom, maxView,game);
        return index;
    }
 
    private int BackPage(int index, int real, int maxView,GameObject game)
    {
   
        index--;
        FlashMyRoom(index, ref allroom, maxView,game);
        return index;
    }
  
    private void GetSprite(Sprite s, GameObject gbj, string nothing)
    {
        gbj.GetComponent<Image>().sprite = s;

    }


}
