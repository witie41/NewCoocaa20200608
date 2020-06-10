using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
public class CreateLiving : MonoBehaviour
{
    [SerializeField]
    private Button Forward_button;
    [SerializeField]
    private Button Backoff_button;
    [SerializeField]
    private GameObject Room;
    [SerializeField]
    private GameObject Content;
    [SerializeField]
    private GameObject Viewport;
    [SerializeField]
    private int Numbers;//在线的
    [SerializeField]
    Dictionary<int, LivingRoomData> RoomInformation;
    const int MaxInView = 12;
    [SerializeField]
    int Index = 1;//初始第0页
    [SerializeField]
    Dictionary<int, GameObject> RoomPrefab;
    Dictionary<int, VideoData> MyVideoData;
    public int Real_numbers;//随着数据库更新的数字，保持不变的
    [SerializeField]
    Controller controller;

    private GameObject[] LivingRooms = new GameObject[20];
    private GameObject[] Videos = new GameObject[20];

    private Sprite[] RoomSprite = new Sprite[20];
    private string[] ImageUrl = new string[20];

    bool isLivingCreat = true;



    bool isAppCreat = true;
    bool isVideoCreat = true;
    bool is3dCreat = true;
    bool is2dCreat = true;
    bool is360Creat = true;
    bool is180Creat = true;

    // Start is called before the first frame update
    void Start()
    {

        Regex name = new Regex("video");
        if (name.Match(this.name).Success)
        {
            MyVideoData = new Dictionary<int, VideoData>();
        }
        else
        {
            RoomInformation = new Dictionary<int, LivingRoomData>();
        }

        RoomPrefab = new Dictionary<int, GameObject>();

      /*
        if (Forward_button == null)
        {
            if (this.transform.GetChild(2) != null)
                Forward_button = this.transform.GetChild(2).GetComponent<Button>();
        }
        if (Backoff_button == null)
        {
            if (this.transform.GetChild(1) != null)
                Backoff_button = this.transform.GetChild(1).GetComponent<Button>();
        }*/
        if (Viewport == null)
        {
            Viewport = this.transform.GetChild(0).gameObject;
        }
        if (Content == null)
        {
            Content = Viewport.transform.GetChild(0).gameObject;
        }
        // Match();
        Numbers = Real_numbers;
        //InsideButton_Forword(Real_numbers, MaxInView, Index);
        //InsideButton_Backoff(Index);

        //  SaveInformation(Real_numbers);
        //CreatRoomGameobject(Real_numbers);
        //FlashMyRoom(0, Real_numbers, MaxInView);
        //Forward_button.onClick.AddListener(delegate () { this.ClickButtonForward(); });
       // Backoff_button.onClick.AddListener(delegate () { this.ClickButtonBackWard(); });
      
    }

    // Update is called once per frame
    void Update()
    {
        if(Controller.isFlash == true)
        {
            CreatRoom();

            Controller.isFlash = false;

        }
    }

    private void CreatRoomGameobject(int Numbers)
    {

        for (int i = 0; i < ((Numbers > MaxInView) ? MaxInView : Numbers); i++)
        {
            if (!RoomPrefab.ContainsKey(i))
            {
                RoomPrefab.Add(i, Instantiate(Room));
                RoomPrefab[i].transform.parent = Content.transform;
            }
        }


    }

    private void FlashMyRoom(int index, int real, int maxView)
    {
        for (int i = 0; i < ((real - index * 12) > maxView ? maxView : (real - index * 12)); i++)
        {
            //TransmitInformation(RoomPrefab[i], i + 12 * index);
        }

    }
    private void TransmitInformation(GameObject room, int key)
    {
        /**
         * 测试函数，更新信息时，用到此函数
         *  updata time 3.13
         **/


        if (room.activeInHierarchy == false)
        {
            room.SetActive(true);
        }


    }

    private int GetNumbersInNow()//返回当前直播间数量
    {
        return 0;
    }

    private void HideAllRoom()
    {
        foreach (GameObject room in RoomPrefab.Values)
        {
            room.SetActive(false);
        }
    }


    private int NextPage(int index, int real, int maxView)
    {
        HideAllRoom();
        index++;
        FlashMyRoom(index, real, maxView);
        return index;
    }


    private int BackPage(int index, int real, int maxView)
    {
        HideAllRoom();
        index--;
        FlashMyRoom(index, real, maxView);
        return index;
    }

    private void InsideButton_Forword(int real, int max, int index)
    {
        if (index < (real / max))
        {
            Forward_button.gameObject.SetActive(true);
        }
        else
        {
            Forward_button.gameObject.SetActive(false);
        }
    }
    private void InsideButton_Backoff(int index)
    {
        if (index == 1)
        {
            Backoff_button.gameObject.SetActive(false);
        }
        else
        {
            Backoff_button.gameObject.SetActive(true);
        }
    }
    private void SaveInformation(int numbers)
    {
        /**
         * 选用字典储存而不是数组
         * 因为字典可以动态开辟空间
         * 相对数组比较方便
         **/
        for (int i = 0; i < numbers; i++)
        {
            if (!RoomInformation.ContainsKey(i))
            {
                RoomInformation.Add(i, new LivingRoomData());
            }
        }
    }

    private void UpdataMyRoom(int old, int now)
    {
        /**
         * 根据直播特性，可以有新主播加入
         * 暂时不考虑删除某个主播
         **/
        for (int i = old; i < now; i++)
        {
            if (!RoomInformation.ContainsKey(i))
            {
                RoomInformation.Add(i, new LivingRoomData());
            }
        }
    }

    public void ClickButtonForward()
    {
        Index++;
        //Index = NextPage(Index, Real_numbers, MaxInView);
        //InsideButton_Forword(Real_numbers, MaxInView, Index);
        InsideButton_Backoff(Index);
    }

    public void ClickButtonBackWard()
    {
        Index--;
        //Index = BackPage(Index, Real_numbers, MaxInView);
        InsideButton_Backoff(Index);
        //InsideButton_Forword(Real_numbers, MaxInView, Index);
    }

    private void Match()
    {
        switch (this.name)
        {
            case "2D cinema area(Clone)":
                Room = Resources.Load<GameObject>("Prefabs/GrandScreenRoom");
                Real_numbers = 5;
                break;
            case "3D cinema area(Clone)":
                Room = Resources.Load<GameObject>("Prefabs/GrandScreenRoom");
                Real_numbers = 7;
                break;
            case "App panel(Clone)":

                Room = Resources.Load<GameObject>("Prefabs/Image");
                Real_numbers = 8;
                break;
            case "Game panel(Clone)":

                Room = Resources.Load<GameObject>("Prefabs/Image");
                Real_numbers = 9;
                break;
            case "Local images(Clone)":

                Room = Resources.Load<GameObject>("Prefabs/Image");
                Real_numbers = 13;
                break;
            case "Local Installation packages(Clone)":

                Room = Resources.Load<GameObject>("Prefabs/Image");
                Real_numbers = 12;
                break;
            case "Local videos(Clone)":

                Room = Resources.Load<GameObject>("Prefabs/LivingRoom");
                Real_numbers = 2;
                break;
            case "panorama 180 area(Clone)":

                Room = Resources.Load<GameObject>("Prefabs/Image");
                Real_numbers = 9;
                break;
            case "panorama 360 area(Clone)":
                Room = Resources.Load<GameObject>("Prefabs/360DegreeRoom");

                Real_numbers = 12;
                break;
            case "VR live broadcast area(Clone)":
                Room = Resources.Load<GameObject>("Prefabs/LivingRoom");

                Real_numbers = 10;
                break;
            case "Panoramic video works panel":
                Room = Resources.Load<GameObject>("Prefabs/360DegreeRoom");

                Real_numbers = 5;
                break;
            case "Works panel":
                Room = Resources.Load<GameObject>("Prefabs/LivingRoom");

                Real_numbers = 9;
                break;
            case "Panoramic works panel":
                Room = Resources.Load<GameObject>("Prefabs/Image");

                Real_numbers = 4;
                break;
            case "Livng back panel":
                Room = Resources.Load<GameObject>("Prefabs/LivingRoom");

                Real_numbers = 4;
                break;
            case "Following anchor panel":

                Room = Resources.Load<GameObject>("Prefabs/Head");
                Real_numbers = 4;
                break;
            case "Follow creators panel":
                Room = Resources.Load<GameObject>("Prefabs/Head");
                Real_numbers = 2;
                break;
            case "Collection panel":
                Room = Resources.Load<GameObject>("Prefabs/LivingRoom");

                Real_numbers = 4;
                break;
            case "Funs panel":
                Room = Resources.Load<GameObject>("Prefabs/Head");
                Real_numbers = 2;
                break;
            case "History playback record panel":
                Room = Resources.Load<GameObject>("Prefabs/LivingRoom");
                Real_numbers = 6;

                break;
            default:
                Debug.Log("错误");
                break;
        }

    }

    /// <summary>
    /// 此方法来实现获取服务器数据，修改时直接修改即可。
    /// </summary>
    /// <param name="data"></param>
    /// <param name="gbj"></param>
    private void LivingDataUp(LivingRoomData[] data, GameObject[] gbj, string nothing)
    {



   
        if (isLivingCreat == true)
        {


            int i = 0;
            foreach (LivingRoomData lm in data)
            {
                Room = Resources.Load<GameObject>("Prefabs/LivingRoom");
                gbj[i] = Instantiate(Room, Content.transform);
                //gbj[i].transform.parent = Content.transform;
                gbj[i].name = lm.id + " ";
                gbj[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = lm.title;
                gbj[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = lm.nickName;

                StartCoroutine(DataClassInterface.IEGetSprite(lm.coverImg1, new DataClassInterface.OnDataGetSprite(GetSprite), gbj[i]));
                i++;
            }
            isLivingCreat = false;
        }
      else
        {
           
            foreach(GameObject gb in LivingRooms)
            {

                Destroy(gb);
            }
            int i = 0;
            foreach (LivingRoomData lm in data)
            {
                Room = Resources.Load<GameObject>("Prefabs/LivingRoom");
                gbj[i] = Instantiate(Room, Content.transform);
                //gbj[i].transform.parent = Content.transform;
                gbj[i].name = lm.id + " ";
                gbj[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = lm.title;
                gbj[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = lm.nickName;



                StartCoroutine(DataClassInterface.IEGetSprite(lm.coverImg1, new DataClassInterface.OnDataGetSprite(GetSprite), gbj[i]));
                i++;
            }

        }


    }

    private void AppDataUp(App[] data, GameObject[] gbj, string nothing)
    {

        
      if(isAppCreat == true)

        {
            int i = 0;
            Room = Resources.Load<GameObject>("Prefabs/Image");
            foreach (App app in data)
            {

                gbj[i] = Instantiate(Room, Content.transform);
                //gbj[i].transform.parent = Content.transform;
                gbj[i].name = app.apkId + " ";
                gbj[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = app.apkName;
                gbj[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = app.apkPackage;

                StartCoroutine(DataClassInterface.IEGetSprite(app.apkIcon, new DataClassInterface.OnDataGetSprite(GetSprite), gbj[i]));
                i++;
            }

            isAppCreat = false;
        }
        else
        {
            foreach (GameObject gb in LivingRooms)
            {
                Destroy(gb);
            }
            int i = 0;
            Room = Resources.Load<GameObject>("Prefabs/Image");
            foreach (App app in data)
            {

                gbj[i] = Instantiate(Room, Content.transform);
                //gbj[i].transform.parent = Content.transform;
                gbj[i].name = app.apkId + " ";
                gbj[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = app.apkName;
                gbj[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = app.apkPackage;

                StartCoroutine(DataClassInterface.IEGetSprite(app.apkIcon, new DataClassInterface.OnDataGetSprite(GetSprite), gbj[i]));
                i++;
            }

        }

    }

    private void VideoDataUp(VideoData[] data, GameObject[] gbj, string nothing)
    {

        int i = 0;



        if (this.name == "2D cinema video area(Clone)")
        {

           if(is2dCreat == true)

            {
                Room = Resources.Load<GameObject>("Prefabs/GrandScreenRoom");
                foreach (VideoData lm in data)
                {
                    if (lm.videoType == 6 && lm.workType == 2)
                    {
                        gbj[i] = Instantiate(Room, Content.transform);
                        //gbj[i].transform.parent = Content.transform;
                        gbj[i].name = lm.workId + " ";
                        if (gbj[i].transform.GetChild(0) != null)

                        {
                            gbj[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = lm.title;
                            gbj[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = lm.nickName;
                            StartCoroutine(DataClassInterface.IEGetSprite(lm.cover, new DataClassInterface.OnDataGetSprite(GetSprite), gbj[i]));
                            i++;
                        }
                    }
                }
                is2dCreat = false;
            }

           else
            {
                foreach (GameObject gb in Videos)
                {
                    Destroy(gb);
                }
                Room = Resources.Load<GameObject>("Prefabs/GrandScreenRoom");
                foreach (VideoData lm in data)
                {
                    if (lm.videoType == 6 && lm.workType == 2)
                    {
                        gbj[i] = Instantiate(Room, Content.transform);
                        //gbj[i].transform.parent = Content.transform;
                        gbj[i].name = lm.workId + " ";
                        if (gbj[i].transform.GetChild(0) != null)

                        {
                            gbj[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = lm.title;
                            gbj[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = lm.nickName;
                            StartCoroutine(DataClassInterface.IEGetSprite(lm.cover, new DataClassInterface.OnDataGetSprite(GetSprite), gbj[i]));
                            i++;
                        }
                    }
                }
               
            }
        }
      else  if (this.name == "3D cinema video area(Clone)")
        {
           if(is3dCreat == true)
            {
                Room = Resources.Load<GameObject>("Prefabs/GrandScreenRoom");
                foreach (VideoData lm in data)
                {
                    if (lm.videoType == 5 && lm.workType == 2)
                    {
                        gbj[i] = Instantiate(Room, Content.transform);
                        //gbj[i].transform.parent = Content.transform;
                        gbj[i].name = lm.workId + " ";
                        if (gbj[i].transform.GetChild(0) != null)

                        {
                            gbj[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = lm.title;
                            gbj[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = lm.nickName;
                            StartCoroutine(DataClassInterface.IEGetSprite(lm.cover, new DataClassInterface.OnDataGetSprite(GetSprite), gbj[i]));
                            i++;
                        }
                    }
                }
                is3dCreat = false;
            }
           else
            {
                foreach (GameObject gb in Videos)
                {
                    Destroy(gb);
                }
                Room = Resources.Load<GameObject>("Prefabs/GrandScreenRoom");
                foreach (VideoData lm in data)
                {
                    if (lm.videoType == 5 && lm.workType == 2)
                    {
                        gbj[i] = Instantiate(Room, Content.transform);
                        //gbj[i].transform.parent = Content.transform;
                        gbj[i].name = lm.workId + " ";
                        if (gbj[i].transform.GetChild(0) != null)

                        {
                            gbj[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = lm.title;
                            gbj[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = lm.nickName;
                            StartCoroutine(DataClassInterface.IEGetSprite(lm.cover, new DataClassInterface.OnDataGetSprite(GetSprite), gbj[i]));
                            i++;
                        }

                    }
                }
        
            }
        }
      else if (this.name == "panorama 180 video area(Clone)")
        {
            if(is180Creat == true)
            {


                Room = Resources.Load<GameObject>("Prefabs/180DegreeRoom");
                foreach (VideoData lm in data)
                {
                    if (lm.videoType == 2 && lm.workType == 0)
                    {
                        gbj[i] = Instantiate(Room, Content.transform);
                        //gbj[i].transform.parent = Content.transform;
                        gbj[i].name = lm.workId + " ";
                        if (gbj[i].transform.GetChild(0) != null)

                        {
                            gbj[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = lm.title;
                            gbj[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = lm.nickName;
                            StartCoroutine(DataClassInterface.IEGetSprite(lm.cover, new DataClassInterface.OnDataGetSprite(GetSprite), gbj[i]));
                            i++;
                        }
                    }
                }
                is180Creat = false;
            }
            else
            {
                foreach (GameObject gb in Videos)
                {
                    Destroy(gb);
                }
                Room = Resources.Load<GameObject>("Prefabs/180DegreeRoom");
                foreach (VideoData lm in data)
                {
                    if (lm.videoType == 2 && lm.workType == 0)
                    {
                        gbj[i] = Instantiate(Room, Content.transform);
                        //gbj[i].transform.parent = Content.transform;
                        gbj[i].name = lm.workId + " ";
                        if (gbj[i].transform.GetChild(0) != null)

                        {
                            gbj[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = lm.title;
                            gbj[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = lm.nickName;
                            StartCoroutine(DataClassInterface.IEGetSprite(lm.cover, new DataClassInterface.OnDataGetSprite(GetSprite), gbj[i]));
                            i++;
                        }
                    }
                }
            }
        }
      else if (this.name == "panorama 360 video area(Clone)")
        {

            if(is360Creat == true)
            {
                Room = Resources.Load<GameObject>("Prefabs/360DegreeRoom");
                foreach (VideoData lm in data)
                {
                    if (lm.videoType == 0 && lm.workType == 0)
                    {
                        gbj[i] = Instantiate(Room, Content.transform);
                        //gbj[i].transform.parent = Content.transform;
                        gbj[i].name = lm.workId + " ";
                        if (gbj[i].transform.GetChild(0) != null)

                        {
                            gbj[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = lm.title;
                            gbj[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = lm.nickName;
                            StartCoroutine(DataClassInterface.IEGetSprite(lm.cover, new DataClassInterface.OnDataGetSprite(GetSprite), gbj[i]));
                            i++;
                        }
                    }
                }
                is360Creat = false;
            }
            else
            {
                foreach (GameObject gb in Videos)
                {
                    Destroy(gb);

                }
                Room = Resources.Load<GameObject>("Prefabs/360DegreeRoom");
                foreach (VideoData lm in data)
                {
                    if (lm.videoType == 0 && lm.workType == 0)
                    {
                        gbj[i] = Instantiate(Room, Content.transform);
                        //gbj[i].transform.parent = Content.transform;
                        gbj[i].name = lm.workId + " ";
                        if (gbj[i].transform.GetChild(0) != null)

                        {
                            gbj[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = lm.title;
                            gbj[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = lm.nickName;
                            StartCoroutine(DataClassInterface.IEGetSprite(lm.cover, new DataClassInterface.OnDataGetSprite(GetSprite), gbj[i]));
                            i++;
                        }

                    }
                }
             
            }
        }



    }

    private void CreatRoom()
    {

        Regex name = new Regex("video");
        if (!name.Match(this.name).Success)
        {
            if (this.name == "Game panel(Clone)")
            {
                StartCoroutine(DataClassInterface.IEGetDate<App[]>(AllData.DataString + "/app/getRecommendGameList?pageId=" + Index, new DataClassInterface.OnDataGet<App[]>(AppDataUp), LivingRooms));
            }
            else if (this.name == "App panel(Clone)")
            {
                StartCoroutine(DataClassInterface.IEGetDate<App[]>(AllData.DataString + "/app/getRecommendAppList?pageId=" + Index, new DataClassInterface.OnDataGet<App[]>(AppDataUp), LivingRooms));

            }
            else if (this.name == "VR recomend(Clone)")
            {
                StartCoroutine(DataClassInterface.IEGetDate<RecommendVideo[]>(AllData.DataString + "/vr/getVrRecommendVideoList?pageId=" + Index, new DataClassInterface.OnDataGet<RecommendVideo[]>(GetRecommend), LivingRooms));

            }
            else if (this.name == "recomend(Clone)")
            {

            }
            else
                StartCoroutine(DataClassInterface.IEGetDate<LivingRoomData[]>(AllData.DataString + "/vr/getBroadcastList?pageId=" + Index, new DataClassInterface.OnDataGet<LivingRoomData[]>(LivingDataUp), LivingRooms));
        }
        else
        {
     
            StartCoroutine(DataClassInterface.IEGetDate<VideoData[]>(AllData.DataString + "/vr/getVideoList?pageId=" + Index, new DataClassInterface.OnDataGet<VideoData[]>(VideoDataUp), Videos));
        }
    
    }

    private void GetSprite(Sprite s, GameObject gbj, string nothing)
    {
        gbj.GetComponent<Image>().sprite = s;

    }

    public void GetRecommend(RecommendVideo[] videos, GameObject[] gbj, string no)
    {
        int i = 0;
        if (this.name == "recomend(Clone)")
        {
            Room = Resources.Load<GameObject>("Prefabs/RecommendGrandScreenRoom");
            foreach (RecommendVideo data in videos)
            {
                if (data.contentType == "video")
                {
                    gbj[i] = Instantiate(Room);
                    gbj[i].transform.parent = Content.transform;
                    gbj[i].name = data.contentId + " ";
                    if (gbj[i].transform.GetChild(0) != null)

                    {
                        gbj[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = data.sourceName;
                        gbj[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = data.source;
                        //StartCoroutine(DataClassInterface.IEGetSprite(" ", (Sprite sprite, GameObject go, string str) => { gbj[i].GetComponent<Image>().sprite = sprite; }, null));

                        i++;
                    }
                }
            }


        }
        else if (this.name == "VR recomend(Clone)")
        {
            Room = Resources.Load<GameObject>("Prefabs/360RecommendDegreeRoom");
            foreach (RecommendVideo data in videos)
            {
                if (data.contentType == "video_vr")
                {
                    gbj[i] = Instantiate(Room);
                    gbj[i].transform.parent = Content.transform;
                    gbj[i].name = data.contentId + " ";
                    if (gbj[i].transform.GetChild(0) != null)

                    {
                        gbj[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = data.sourceName;
                        gbj[i].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = data.source;
                        // StartCoroutine(DataClassInterface.IEGetSprite(, new DataClassInterface.OnDataGetSprite(GetSprite), gbj[i]));
                        i++;
                    }
                }
            }
        }
    }
}
