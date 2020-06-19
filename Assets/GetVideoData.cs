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
        Right
    }
    class VideoPanel
    {
      public  GameObject panel;
      public pos mypos;
    }
    GameObject mid;
    GameObject left;
    GameObject right;
    GameObject [] rooms;
    Vector3 leftpanelVector;
    Vector3 midpanelVector;
    Vector3 rightpanelVector;
   // GameObject[] Rooms = new GameObject [1000];
    VideoPanel[] videopanels = new VideoPanel[3];
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
    RoomButtonControl[] roominfo = new RoomButtonControl[1000];
    // Start is called before the first frame update
    void Start()
    {
        /*
         物体赋值*/
        room = Resources.Load("LivingRoom/Living room folder/RoomsButton") as GameObject;
        
        indexvideo = 0;
        indexliving = 0;
        indexroom = 0;
        index = 0;//初始页面值；
        rooms = new GameObject[1000];
        mid = GameObject.FindWithTag("Mid");
        left = GameObject.FindWithTag("Left");
        right = GameObject.FindWithTag("Right");
     if(left!=null)
        {
            leftpanelVector = left.transform.position;
            
        }
     if(right!=null)
        {
            rightpanelVector = right.transform.position;
           
        }
        midpanelVector = mid.transform.position;
        /*
         获取数据
        */
        StartCoroutine(DataClassInterface.IEGetDate<VideoData[]>(AllData.DataString + "/vr/getVideoList" , new DataClassInterface.OnDataGet<VideoData[]>(GetVideo), null));
        StartCoroutine(DataClassInterface.IEGetDate<LivingRoomData[]>(AllData.DataString + "/vr/getBroadcastList" , new DataClassInterface.OnDataGet<LivingRoomData[]>(GetLiving), null));
        
        /*
         页面初始状态
         */
        videopanels[0] = new VideoPanel();
        videopanels[0].panel = left;
        videopanels[0].mypos = pos.Left;
        videopanels[1] = new VideoPanel();
        videopanels[1].panel = mid;
        videopanels[1].mypos = pos.Mid;
        videopanels[2] = new VideoPanel();
        videopanels[2].panel = right;
        videopanels[2].mypos = pos.Right;

       for(int i = 0;i<200;i++)
        {
            roominfo[i] = new RoomButtonControl(VideoType.Video2D, 0, "");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isFlash)
        {
            StartCoroutine(DataClassInterface.IEGetDate<VideoData[]>(AllData.DataString + "/vr/getVideoList", new DataClassInterface.OnDataGet<VideoData[]>(GetVideo), null));
            StartCoroutine(DataClassInterface.IEGetDate<LivingRoomData[]>(AllData.DataString + "/vr/getBroadcastList", new DataClassInterface.OnDataGet<LivingRoomData[]>(GetLiving), null));
            allroom = allivingroom + allvideoroom;
            if (allroom % 12 == 0)
            {
                allpage = allroom / 12;
            }
            else
            {
                allpage = allroom / 12 + 1;
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
                FlashMyRoom(index, allroom, 12, mid);
                FlashMyRoom(index + 1, allroom, 12, right);
                //Instantiate(Rooms[0], mid.transform);
                //Debug.Log("所有的数据" + Rooms.Length + "区别数据+" + Rooms[11] + "||" + Rooms[230]);
            }
           
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
        Debug.Log("点击事件");
    }
   public void ClickLeft()//确定点击的是左边
    {
        if(index == 1)
        {
            GameObject cor = GameObject.FindWithTag("GameController");
            cor.GetComponent<Controller>().DisplayMsg("已为第一页");
        }
        else
        {
            index--;
        }
    }
  public  void ClickRight()
    {
        index++;
        if(videopanels[0].mypos == pos.Mid)
        {
           
        }
        else if(videopanels[1].mypos == pos.Mid)
        {

        }
        else if(videopanels[2].mypos == pos.Mid)
        {

        }
    }
     


/*private void Creat(GameObject gameobject)
    {
        
        allroom = allivingroom + allvideoroom;
       
        if (allroom % 12 == 0)
        {
            allpage = allroom / 12;
        }
        else
        {
            allpage = allroom / 12 + 1;
        }
      for(int i =indexroom; i<=12*index&&indexroom<=allroom;indexroom++,i++)
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
    private void FlashMyRoom(int index, int real, int maxView,GameObject panel)//index:当前页数，real:实际数据 maxview：最大显示房间
    {
        Controller controller = new Controller();
        for (int i = 0; i < ((real - index * 12) > maxView ? maxView : (real - index * 12)); i++)
       //for(int i = 1;i<=1;i++)
        {
            
               // Destroy(GameObject.Find("RoomsButton(Clone)"));
               GameObject gbj = Instantiate(room, panel.transform);
                gbj.transform.Find("LeftTag").gameObject.SetActive(false);
                gbj.transform.Find("RightTag").gameObject.SetActive(false);
                gbj.transform.Find("Name").transform.GetComponent<Text>().text = roominfo[i+index*12].vname;
                StartCoroutine(DataClassInterface.IEGetSprite(roominfo[i + index * 12].photo, new DataClassInterface.OnDataGetSprite(GetSprite), gbj));
            gbj.name = roominfo[i + index * 12].ID + "";


            if (roominfo[i + index * 12].VType == VideoType.Live_Off || roominfo[i + index * 12].VType == VideoType.Live_On)
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

                    if (roominfo[i + index * 12].VType == VideoType.Live_On)
                    {
                        gbj.transform.Find("LeftTag").GetComponentInChildren<Text>().text = "直播中";
                        gbj.transform.Find("LeftTag").GetComponent<Image>().color = Color.green;
                    }
                    if (roominfo[i + index * 12].VType == VideoType.Live_Off)
                    {
                        gbj.transform.Find("LeftTag").GetComponentInChildren<Text>().text = "未开播";
                        gbj.transform.Find("LeftTag").GetComponent<Image>().color = Color.gray;
                    }
                }
                //视频
                else
                {
                    gbj.transform.Find("RightTag").gameObject.SetActive(true);
                Debug.Log("添加方法" + i + "画布名称" + panel.name + "名称·1" + roominfo[i + index * 12].vname + "视频：" + roominfo[i + index * 12].ID + "视频分类" + roominfo[i + index * 12].VType);

                gbj.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        Debug.Log("添加方法" + i + "画布名称" + panel.name + "名称·1" + roominfo[i + index * 12].vname + "视频：" + roominfo[i + index * 12].ID + "视频分类" + roominfo[i + index * 12].VType);

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
                    switch (roominfo[i + index * 12].VType)
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
        FlashMyRoom(index, real, maxView,game);
        return index;
    }
 
    private int BackPage(int index, int real, int maxView,GameObject game)
    {
   
        index--;
        FlashMyRoom(index, real, maxView,game);
        return index;
    }
  
    private void GetSprite(Sprite s, GameObject gbj, string nothing)
    {
        gbj.GetComponent<Image>().sprite = s;

    }


}
