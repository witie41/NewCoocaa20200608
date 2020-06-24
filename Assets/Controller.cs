
using System.Net.Mime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using IVRCommon.Keyboard;

using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 此类这是为了方便入栈存储
/// 
/// </summary>
class Panels
{
    GameObject left;
    GameObject right;
    GameObject middle;
    GameObject menu;

    public Panels(GameObject left, GameObject middle)
    {
        this.left = left;
        this.middle = middle;
    }
    public Panels(GameObject left, GameObject middle, GameObject right)
    {
        this.left = left;
        this.middle = middle;
        this.right = right;
    }
    public Panels(GameObject left, GameObject middle, GameObject right, GameObject menu)
    {
        this.left = left;
        this.middle = middle;
        this.right = right;
        this.menu = menu;
    }
    public void Come()
    {
        if (left != null)
        {
            left.SetActive(true);
        }
        if (right != null)
        {
            right.SetActive(true);
        }
        if (middle != null)
        {
            middle.SetActive(true);
        }
        if (menu != null)
        {
            menu.SetActive(true);
        }
    }
    public void Hide()
    {
        if (left != null)
        {
            left.SetActive(false);
        }
        if (right != null)
        {
            right.SetActive(false);
        }
        if (middle != null)
        {
            middle.SetActive(false);
        }
        if (menu != null)
        {
            menu.SetActive(false);
        }
    }

}




public class Controller : MonoBehaviour
{
    [SerializeField]
    GameObject Middle_canvs;
    [SerializeField]
    GameObject Left_canvs;
    [SerializeField]
    GameObject Right_canvs;
    [SerializeField]
    GameObject Help_canvs;
    [SerializeField]
    GameObject Menu_canvs;
    [SerializeField]
    GameObject Icon_canvs;
    [SerializeField]
    public GameObject Big_middle_canvs;
    [SerializeField]
    public static Stack panelComeback = new Stack();

    public static bool isFlash = false;

    public  int MaxId ;

    static GameObject m_select_middle;
    static GameObject m_select_left;
    static GameObject m_select_right;
    static GameObject m_menu;

    static bool grand_select = false;
    static bool _360degree_select = false;
    static bool vr_select = false;
    static bool user_token_Get = false;
    static string user_info;
    static Panels select_panels;
    static Panels VR_recommend;
    static Panels select_panels_1;
    static Panels local_panels;
    static Panels video_panels;
    static Panels weekly_video_recommend_panels;
    static Panels monthly_video_recommend_panels;
    static Panels grand_select_panels;
    static Panels _360_select_panels;
    static Panels vr_select_panels;
    static Panels _3Dcinema_panels;
    static Panels _2Dcinema_panels;
    static Panels panorama_videos_panels;
    static Panels panorama_panels;
    static Panels vr_broast_panels;
    static Panels local_imgae_panels;
    static Panels local_installationPackages;
    static Panels local_delete_panels;
    static Panels tourist_panels;
    static Panels works_panels;
    static Panels my_panoramic_video_panels;
    static Panels my_panoramic_image_panels;
    static Panels my_livingback_panels;
    static Panels my_following_panels;
    static Panels my_following_anchorer_panels;
    static Panels my_following_creator_panels;
    static Panels my_staring_panels;
    static Panels my_funs_panels;
    static Panels my_history_panels;
    static Panels my_message_panels;
    static Panels my_phone_login_panels;
    static Panels my_common_login_panels;
    static Panels my_recommend_panels;
    public GameObject living_room ;
    public GameObject grand_living_room;
    public GameObject _360_living_room ;
    static public bool user_image;
    static public string mobile_number_email;
    static public string user_password;
    static  public bool getmobliecode = false;
    static public bool registerAndLogin = false;
    static public string ImageCode;
    static public string ImageCodeKey;
    static public string SMSCode;
    static public bool InPopularityList = false;
    static public bool InShowHome = false;
    static public bool InShowPerson = false;
    static public bool InOtherPerson = false;
    GameObject[] daylyRank;
    GameObject[] weeklyRank;
    GameObject[] monthlyRank;
    static public  Dictionary<int, GameObject> Handle_panel = new Dictionary<int, GameObject>();//储存面板

    public  GameObject MsgPanel;
    public Transform TipsRoot;

    private void Awake()
    { 
        //初始化所有幕布
        living_room  =  GameObject.Find("Living room");
       _360_living_room = GameObject.Find("Three hundred and sixty dergee living room");
       _360_living_room.SetActive(false);
        daylyRank = new GameObject[4];
        monthlyRank = new GameObject[4];
        weeklyRank = new GameObject[4];
       /* if (GameObject.Find("Middle canvs") == null)
        {
            Instantiate(Resources.Load<GameObject>("Canvs/Middle canvs"));
        }
        if (GameObject.Find("Left canvs") == null)
        {
            Instantiate(Resources.Load<GameObject>("Canvs/Left canvs"));
        }
        if (GameObject.Find("Right canvs") == null)
        {
            Instantiate(Resources.Load<GameObject>("Canvs/Right canvs"));
        }
        if (GameObject.Find("Icon canvs") == null)
        {
            Instantiate(Resources.Load<GameObject>("Canvs/Icon canvs"));
        }
        if (GameObject.Find("Menu canvs") == null)
        {
            Instantiate(Resources.Load<GameObject>("Canvs/Menu canvs"));
        }
        if (GameObject.Find("Help canvs") == null)
        {
            Instantiate(Resources.Load<GameObject>("Canvs/Help canvs"));
        }
        if (GameObject.Find("Big middle canvs") == null)
        {
            Instantiate(Resources.Load<GameObject>("Canvs/Big middle canvs"));
        }*/
        //GetAllCanvs();
        /*
        if (!Handle_panel.ContainsKey(0))
        {
            CreatPanel("Other panel/Icon", 0, Icon_canvs.transform);
            CreatPanel("Other panel/Panel", 1, Menu_canvs.transform);
        }*/

        //FindPanel();
        //select_panels = new Panels(m_select_left, m_select_middle, m_select_right, m_menu);
        //StartCoroutine(DataClassInterface.IEGetDate<LivingRoomData[]>(AllData.DataString+"/vr/getBroadcastList", new DataClassInterface.OnDataGet<LivingRoomData[]>(LivingDataUp), null));
        //panelComeback.Push(select_panels);
        //StartCoroutine(DataClassInterface.IEGetDate<FirstSelected[]>(AllData.DataString + "/vr/getVrRecommendList", new DataClassInterface.OnDataGet<FirstSelected[]>(GetFirstInfornation), null));
         StartCoroutine(DataClassInterface.IEGetDate<LivingRoomData[]>(AllData.DataString + "/vr/getBroadcastList?pageId=1" , new DataClassInterface.OnDataGet<LivingRoomData[]>(GetMaxId), null));
        panelComeback.Push(living_room);

    }


    private void Update()
    {


        if(InShowPerson == true)
        {
            if (!Handle_panel.ContainsKey(37))
            {
                Show(37, Big_middle_canvs, "Big middle canvs", "Big middle panel/Personal homepage");
            }
            ALLFalse();
            Handle_panel[37].transform.GetChild(0).gameObject.SetActive(true);
            Handle_panel[37].transform.GetChild(2).gameObject.SetActive(true);
            
            if(GameObject.Find("Living room(Clone)"))
            {
                Debug.Log("事件1");
                Big_middle_canvs.SetActive(true);
                GameObject.Find("Living room(Clone)").SetActive(false);
                StartCoroutine(DataClassInterface.IEGetDate<UserData>(AllData.DataString + "/vr/getUserById?id=" + GameObject.Find("Manager").transform.GetComponent<MsgManager>().MasterId, new DataClassInterface.OnDataGet<UserData>(ShowPersonInfo), null));
            }
            else
            {
                StartCoroutine(DataClassInterface.IEGetDate<UserData>(AllData.DataString + "/vr/getUserById?id=" + AllData.userId, new DataClassInterface.OnDataGet<UserData>(ShowPersonInfo), null));
            }
            // works_panels = new Panels(Handle_panel[37], Handle_panel[37].transform.GetChild(0).gameObject, Handle_panel[37].transform.GetChild(2).gameObject);
            panelComeback.Push(Handle_panel[37]);
            InShowPerson = false;
        }

        if(InOtherPerson == true)
        {
            if (!Handle_panel.ContainsKey(37))
            {
                Show(37, Big_middle_canvs, "Big middle canvs", "Big middle panel/Personal homepage");
            }
            ALLFalse();
            Handle_panel[37].transform.GetChild(0).gameObject.SetActive(true);
            Handle_panel[37].transform.GetChild(2).gameObject.SetActive(true);

            if (GameObject.Find("Info4-0"))
            {            
                Big_middle_canvs.SetActive(true);
                GameObject.Find("Living room(Clone)").SetActive(false);
                StartCoroutine(DataClassInterface.IEGetDate<UserData>(AllData.DataString + "/vr/getUserById?id=" + GameObject.Find("Manager").transform.GetComponent<MsgManager>().MasterId, new DataClassInterface.OnDataGet<UserData>(ShowPersonInfo), null));
            }
            else if(GameObject.Find("Info4-1"))
            {
                StartCoroutine(DataClassInterface.IEGetDate<UserData>(AllData.DataString + "/vr/getUserById?id=" + AllData.userId, new DataClassInterface.OnDataGet<UserData>(ShowPersonInfo), null));
            }
            else if(GameObject.Find("Info4-2"))
            {
                StartCoroutine(DataClassInterface.IEGetDate<UserData>(AllData.DataString + "/vr/getUserById?id=" + AllData.userId, new DataClassInterface.OnDataGet<UserData>(ShowPersonInfo), null));
            }
            // works_panels = new Panels(Handle_panel[37], Handle_panel[37].transform.GetChild(0).gameObject, Handle_panel[37].transform.GetChild(2).gameObject);
            panelComeback.Push(Handle_panel[37]);
            InOtherPerson = false;
        }

        if (user_token_Get == true)
        {
            WWWForm myForm = new WWWForm();

            Debug.Log("账号：" + mobile_number_email + "密码：" + user_password);
            StartCoroutine(DataClassInterface.IEPostData<UserToken>(AllData.DataString + "/coocaa/api/vrLogin?mobile_email=" + mobile_number_email + "&password=" + user_password, new DataClassInterface.OnDataGet<UserToken>(GetUserToken), myForm, null));          
            //Icon_canvs.transform.GetChild(0).GetChild(5).GetChild(1).GetComponent<Text>().text = "用户";
            user_token_Get = false;
        }

        if (user_image == true)
        {

            Debug.Log("图片！！");
            StartCoroutine(DataClassInterface.IEGetDate<VerificationImage>(AllData.DataString + "/coocaa/api/getImageCode", new DataClassInterface.OnDataGet<VerificationImage>(Get_Image), null));
            user_image = false;
        }

        if (registerAndLogin == true)
        {
            WWWForm myForm_1 = new WWWForm();
            myForm_1.AddField("mobile", mobile_number_email);
            myForm_1.AddField("password", user_password);
            myForm_1.AddField("captcha", SMSCode);

            Debug.Log("注册!");
            Pop();
            StartCoroutine(DataClassInterface.IEPostData<UserToken>(AllData.DataString + "/coocaa/api/vrRegister", new DataClassInterface.OnDataGet<UserToken>(RegisterGetToken), myForm_1, null));

            registerAndLogin = false;

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pop();
        }


        if (getmobliecode == true)
        {
            WWWForm myForm_2 = new WWWForm();
            Debug.Log("数据为：：：" + mobile_number_email + "!!!!!" + ImageCode + "!!!!" + ImageCodeKey);
            StartCoroutine(DataClassInterface.IEGetDate2(AllData.DataString + "/coocaa/api/getCaptcha?mobile_email=" + mobile_number_email + "&capcha=" + ImageCode + "&codeKey=" + ImageCodeKey));
            getmobliecode = false;
        }

        if (InPopularityList == true)
        {

            if (!Handle_panel.ContainsKey(11))
            {
                Show(11, Big_middle_canvs, "Big middle canvs", "Big middle panel/Rank");
                panelComeback.Push(Handle_panel[11]);
            }
            if (Handle_panel[11].activeInHierarchy == false)
            {
                Handle_panel[11].SetActive(true);
                panelComeback.Push(Handle_panel[11]);
            }
            for (int i = 0; i < 4; i++)
            {
                daylyRank[i] = Handle_panel[11].transform.GetChild(4 + i * 3).gameObject;
                weeklyRank[i] = Handle_panel[11].transform.GetChild(3 + i * 3).gameObject;
                monthlyRank[i] = Handle_panel[11].transform.GetChild(5 + i * 3).gameObject;
                Debug.Log(daylyRank[i].name);
                Debug.Log("周榜" + weeklyRank[i].name);
                Debug.Log(monthlyRank[i].name);
            }
            StartCoroutine(DataClassInterface.IEGetDate<PopularityList[]>(AllData.DataString + "/vr/getDailyPopularityList", new DataClassInterface.OnDataGet<PopularityList[]>(GetPopularityList), daylyRank));
            StartCoroutine(DataClassInterface.IEGetDate<PopularityList[]>(AllData.DataString + "/vr/getMonthlyPopularityList", new DataClassInterface.OnDataGet<PopularityList[]>(GetPopularityList), monthlyRank));
            StartCoroutine(DataClassInterface.IEGetDate<PopularityList[]>(AllData.DataString + "/vr/getWeeklyPopularityList", new DataClassInterface.OnDataGet<PopularityList[]>(GetPopularityList), weeklyRank));

            InPopularityList = false;
        }
        if (InShowHome == true)
        {
            DestoryHome();
            if (GameObject.Find("Rank(Clone)"))
            {
                GameObject.Find("Rank(Clone)").SetActive(false);
            }
            HideAll();
            if (!Handle_panel.ContainsKey(4) || !Handle_panel.ContainsKey(5) || !Handle_panel.ContainsKey(6) || !Handle_panel.ContainsKey(8))
            {
                Show(4, Help_canvs, "Help canvs", "Other panel/Home help menu");
                Show(5, Middle_canvs, "Middle canvs", "Middle panel/Select recommend");
                Show(6, Left_canvs, "Left canvs", "Left panel/Select left");
                Show(7, Right_canvs, "Right canvs", "Right panel/Game and App sort");
                if (Handle_panel[4].activeInHierarchy == false)
                {
                    Handle_panel[4].SetActive(true);
                }
                if (Handle_panel[5].activeInHierarchy == false)
                {
                    Handle_panel[5].SetActive(true);
                }
                if (Handle_panel[6].activeInHierarchy == false)
                {
                    Handle_panel[6].SetActive(true);
                }

            }
            else
            {
                HideAll();
                if (Handle_panel[4].activeInHierarchy == false)
                {
                    Handle_panel[4].SetActive(true);
                }
                if (Handle_panel[5].activeInHierarchy == false)
                {
                    Handle_panel[5].SetActive(true);
                }
                if (Handle_panel[6].activeInHierarchy == false)
                {
                    Handle_panel[6].SetActive(true);
                }
                if (Handle_panel[7].activeInHierarchy == false)
                {
                    Handle_panel[7].SetActive(true);
                }
            }
            select_panels_1 = new Panels(Handle_panel[5], Handle_panel[6], Handle_panel[7], Handle_panel[4]);
            panelComeback.Push(select_panels_1);
            Debug.Log(panelComeback.Peek());
            StartCoroutine(DataClassInterface.IEGetDate<FirstSelected[]>(AllData.DataString + "/vr/getVrRecommendList", new DataClassInterface.OnDataGet<FirstSelected[]>(GetFirstInfornation), null));
            InShowHome = false;

        }

        if (grand_select == true)
        {
            DestorySelect();
            Show(16, Middle_canvs, "Middle canvs", "Middle panel/Grand curtain cinema recommend");
            Show(17, Left_canvs, "Left canvs", "Left panel/Grand curtain cinema sort");
            if (Handle_panel[4].activeInHierarchy == false)
            {
                Handle_panel[4].SetActive(true);
            }
            if (Handle_panel[16].activeInHierarchy == false)
            {
                Handle_panel[16].SetActive(true);
            }
            grand_select_panels = new Panels(Handle_panel[4], Handle_panel[16], Handle_panel[17]);
            panelComeback.Push(grand_select_panels);


            StartCoroutine(DataClassInterface.IEGetDate<RecommendVideo[]>(AllData.DataString + "/vr/getVrRecommendVideoList", new DataClassInterface.OnDataGet<RecommendVideo[]>(GetGrandRecommend), null));
            grand_select = false;
        }

        if (vr_select == true)
        {
            DestorySelect();
            Show(20, Middle_canvs, "Middle canvs", "Middle panel/VR live broadcast recommend");
            Show(21, Left_canvs, "Left canvs", "Left panel/VR live broadcast sort");
            if (Handle_panel[4].activeInHierarchy == false)
            {
                Handle_panel[4].SetActive(true);
            }
            if (Handle_panel[20].activeInHierarchy == false)
            {
                Handle_panel[20].SetActive(true);
            }
            vr_select_panels = new Panels(Handle_panel[4], Handle_panel[20], Handle_panel[21]);
            panelComeback.Push(vr_select_panels);

            StartCoroutine(DataClassInterface.IEGetDate<RecommendLivng[]>(AllData.DataString + "/vr/getVrRecommendVideoList?pageId=1&pageSize=20&contentType=broadcast", new DataClassInterface.OnDataGet<RecommendLivng[]>(GetVRRecommend), null));


            vr_select = false;
        }

        if (_360degree_select == true)
        {
            DestorySelect();
            Show(18, Middle_canvs, "Middle canvs", "Middle panel/360 degree panorama recommend");
            Show(19, Left_canvs, "Left canvs", "Left panel/360 degree panorama sort");
            if (Handle_panel[4].activeInHierarchy == false)
            {
                Handle_panel[4].SetActive(true);
            }
            if (Handle_panel[18].activeInHierarchy == false)
            {
                Handle_panel[18].SetActive(true);
            }
            _360_select_panels = new Panels(Handle_panel[4], Handle_panel[18], Handle_panel[19]);
            panelComeback.Push(_360_select_panels);

            StartCoroutine(DataClassInterface.IEGetDate<RecommendVideo[]>(AllData.DataString + "/vr/getVrRecommendVideoList", new DataClassInterface.OnDataGet<RecommendVideo[]>(Get360Recommend), null));
            _360degree_select = false;
        }
    }

    public void ShowSearchPanel()
    {
        if (GameObject.Find("Rank(Clone)"))
        {
            GameObject.Find("Rank(Clone)").SetActive(false);
        }
        DestoryHome();
        Show(2, Middle_canvs, "Middle canvs", "Middle panel/Search panel");
        panelComeback.Push(Handle_panel[2]);
    }

    public void Show(int key, GameObject parent, string parent_name, string prefab_path)
    {

        if (!Handle_panel.ContainsKey(key))
        {
            if (parent == null)
            {
                parent = GameObject.Find(parent_name);
                CreatPanel(prefab_path, key, parent.transform);
            }
            else
                CreatPanel(prefab_path, key, parent.transform);
        }

        HideAll();

        if (Handle_panel[key].activeInHierarchy == false)
        {
            Handle_panel[key].SetActive(true);
        }



    }

    public void ShowGamePanel()
    {
        if (GameObject.Find("Rank(Clone)"))
        {
            GameObject.Find("Rank(Clone)").SetActive(false);
        }
        DestoryHome();
        Show(3, Middle_canvs, "Middle canvs", "Middle panel/Game panel");
        panelComeback.Push(Handle_panel[3]);
        Debug.Log(panelComeback.Count);
    }

    private void GetAllCanvs()
    {
        if (GameObject.Find("Middle canvs") != null && GameObject.Find("Left canvs") != null && GameObject.Find("Right canvs"))
        {
            Middle_canvs = GameObject.Find("Middle canvs");
            Left_canvs = GameObject.Find("Left canvs");
            Right_canvs = GameObject.Find("Right canvs");
            Help_canvs = GameObject.Find("Help canvs");
            Icon_canvs = GameObject.Find("Icon canvs");
            Big_middle_canvs = GameObject.Find("Big middle canvs");
            Menu_canvs = GameObject.Find("Menu canvs");
        }
    }
    private void HideAll()
    {

        foreach (GameObject panel in Handle_panel.Values)
        {
            if (panel == null)
            {
                Debug.Log("物体消失！！！");
            }
            else
            {
                panel.SetActive(false);
            }


        }
        if (Handle_panel.ContainsKey(0))
        {
            if (Handle_panel[0].activeInHierarchy == false)
            {
                Handle_panel[0].SetActive(true);
                Handle_panel[1].SetActive(true);
            }
        }
        else
        {
            //CreatPanel("Other panel/Icon", 0, Icon_canvs.transform);
            //CreatPanel("Other panel/Panel", 1, Menu_canvs.transform);
        }
    }
    private void CreatPanel(string path, int key, Transform parent)//生成固定panel
    {
        GameObject panel = Resources.Load<GameObject>(path);
        panel = Instantiate(panel);
        panel.transform.SetParent(parent, false);
        Handle_panel.Add(key, panel);
    }

    public void ShowHomePanel()
    {
        HideAll();
        InShowHome = true;

    }

    public void ShowAppPanel()
    {
        if (GameObject.Find("Rank(Clone)"))
        {
            GameObject.Find("Rank(Clone)").SetActive(false);
        }
        DestoryHome();
        Show(8, Middle_canvs, "Middle canvs", "Middle panel/App panel");
        panelComeback.Push(Handle_panel[8]);

    }

    public void ShowLocalPanel()
    {
        if (GameObject.Find("Rank(Clone)"))
        {
            GameObject.Find("Rank(Clone)").SetActive(false);
        }
        DestoryHome();
        Show(9, Left_canvs, "Left canvs", "Left panel/Local sort");
        Show(10, Middle_canvs, "Middle canvs", "Middle panel/Local videos");
        if (Handle_panel[9].activeInHierarchy == false)
        {
            Handle_panel[9].SetActive(true);
        }
        local_panels = new Panels(Handle_panel[9], Handle_panel[10]);
        panelComeback.Push(local_panels);

    }
    public void ShowPopularityList()
    {

        HideAll();
        InPopularityList = true;
    }
    public void ShowVideoList()
    {
        Show(12, Left_canvs, "Left canvs", "Left panel/Panoramic video list");
        Show(13, Middle_canvs, "Middle canvs", "Middle panel/Daily video recommendation");
        if (Handle_panel[12].activeInHierarchy == false)
        {
            Handle_panel[12].SetActive(true);
        }
        video_panels = new Panels(Handle_panel[12], Handle_panel[13]);
        panelComeback.Push(video_panels);
    }
    public void ShowDailyRecommend()
    {
        HideAll();
        if (Handle_panel[13].activeInHierarchy == false)
        {
            Handle_panel[13].SetActive(true);
        }
        if (Handle_panel[12].activeInHierarchy == false)
        {
            Handle_panel[12].SetActive(true);
        }
        panelComeback.Push(video_panels);

    }
    public void ShowWeeklyRecommend()
    {

        Show(14, Middle_canvs, "Middle canvs", "Middle panel/Weekly video recommendation");
        if (Handle_panel[12].activeInHierarchy == false)
        {
            Handle_panel[12].SetActive(true);
        }
        weekly_video_recommend_panels = new Panels(Handle_panel[12], Handle_panel[14]);
        panelComeback.Push(weekly_video_recommend_panels);
    }
    public void ShowMonthlyRecommend()
    {

        Show(15, Middle_canvs, "Middle canvs", "Middle panel/Monthly video recommendation");
        if (Handle_panel[12].activeInHierarchy == false)
        {
            Handle_panel[12].SetActive(true);
        }
        monthly_video_recommend_panels = new Panels(Handle_panel[15], Handle_panel[12]);
        panelComeback.Push(monthly_video_recommend_panels);
    }

    private void FindPanel()
    {
        if (GameObject.Find("Select recommend") != null)
        {
            m_select_middle = GameObject.Find("Select recommend");
        }
        if (GameObject.Find("Select left") != null)
        {
            m_select_left = GameObject.Find("Select left");
        }
        if (GameObject.Find("Game and App sort") != null)
        {
            m_select_right = GameObject.Find("Game and App sort");
        }
        if (GameObject.Find("Home help menu") != null)
        {
            m_menu = GameObject.Find("Home help menu");
        }
    }

    private void DestoryHome()
    {
        FindPanel();
        if (m_select_middle.activeInHierarchy == true && m_select_left.activeInHierarchy == true && m_select_right.activeInHierarchy == true && m_menu.activeInHierarchy == true)
        {
            m_select_left.SetActive(false);
            m_select_middle.SetActive(false);
            m_select_right.SetActive(false);
            m_menu.SetActive(false);
        }

    }
    private void DestorySelect()
    {
        FindPanel();
        if (!Handle_panel.ContainsKey(4))
        {
            Handle_panel.Add(4, GameObject.Find("Home help menu"));
        }
        if (m_select_middle.activeInHierarchy == true && m_select_right.activeInHierarchy == true && m_select_left == true)
        {
            m_select_left.SetActive(false);
            m_select_middle.SetActive(false);
            m_select_right.SetActive(false);
        }

    }

    public void ShowSelect()
    {
        InShowHome = true;
    }

    public void ShowGrandSelected()
    {
        grand_select = true;
    }
    public void Show360Select()
    {
        _360degree_select = true;
    }
    public void ShowVRSelect()
    {
        vr_select = true;
    }
    public void ShowRecommend()
    {
        DestorySelect();
        Show(100, Middle_canvs, "Middle canvs", "Middle panel/recomend");
        Show(101, Left_canvs, "Left canvs", "Left panel/VideoRecommend");
        if (Handle_panel[100].activeInHierarchy == false)
        {
            Handle_panel[100].SetActive(true);
        }
        if (Handle_panel[4].activeInHierarchy == false)
        {
            Handle_panel[4].SetActive(true);
        }
        my_recommend_panels = new Panels(Handle_panel[100], Handle_panel[101], Handle_panel[4]);
        panelComeback.Push(my_recommend_panels);
    }
    public void ShowRecommendAgin()
    {
        HideAll();
        if (Handle_panel.ContainsKey(100))
        {
            if (Handle_panel[100].activeInHierarchy == false)
            {
                Handle_panel[100].SetActive(true);
            }
            if (Handle_panel[4].activeInHierarchy == false)
            {
                Handle_panel[4].SetActive(true);
            }
            if (Handle_panel[101].activeInHierarchy == false)
            {
                Handle_panel[101].SetActive(true);
            }
        }
        panelComeback.Push(my_recommend_panels);
    }

    public void ShowVR_Recommend()
    {
        DestorySelect();
        Show(102, Middle_canvs, "Middle canvs", "Middle panel/VR recomend");
        if (Handle_panel.ContainsKey(100))
        {
            if (Handle_panel[4].activeInHierarchy == false)
            {
                Handle_panel[4].SetActive(true);
            }
            if (Handle_panel[101].activeInHierarchy == false)
            {
                Handle_panel[101].SetActive(true);
            }
        }
        VR_recommend = new Panels(Handle_panel[101], Handle_panel[102], Handle_panel[4]);
        panelComeback.Push(VR_recommend);

    }

    public void ShowGrandRemmond()
    {
        HideAll();
        if (Handle_panel[4].activeInHierarchy == false)
        {
            Handle_panel[4].SetActive(true);
        }
        if (Handle_panel[16].activeInHierarchy == false)
        {
            Handle_panel[16].SetActive(true);
        }
        if (Handle_panel[17].activeInHierarchy == false)
        {
            Handle_panel[17].SetActive(true);
        }
        panelComeback.Push(grand_select_panels);
    }

    public void Show3DCinema()
    {
        isFlash = true;
        DestorySelect();
        if (Handle_panel.ContainsKey(17))
        {

        }
        else
        {
            Show(17, Left_canvs, "Left canvs", "Left panel/Grand curtain cinema sort");
        }
        Show(40, Middle_canvs, "Middle canvs", "Middle panel/3D cinema video area");
        if (Handle_panel[4].activeInHierarchy == false)
        {
            Handle_panel[4].SetActive(true);
        }

        if (Handle_panel[17].activeInHierarchy == false)
        {
            Handle_panel[17].SetActive(true);
        }
        _3Dcinema_panels = new Panels(Handle_panel[40], Handle_panel[4], Handle_panel[17]);
        panelComeback.Push(_3Dcinema_panels);
        isFlash = true;
    }
    public void Show2DCinema()
    {
        isFlash = true;
        Show(22, Middle_canvs, "Middle canvs", "Middle panel/2D cinema video area");
        if (Handle_panel[4].activeInHierarchy == false)
        {
            Handle_panel[4].SetActive(true);
        }
        if (Handle_panel[17].activeInHierarchy == false)
        {
            Handle_panel[17].SetActive(true);
        }
        _2Dcinema_panels = new Panels(Handle_panel[22], Handle_panel[4], Handle_panel[17]);
        panelComeback.Push(_2Dcinema_panels);
        isFlash = true;
    }

    public void Show360DegreeRecommend()
    {
        HideAll();
        if (Handle_panel[4].activeInHierarchy == false)
        {
            Handle_panel[4].SetActive(true);
        }
        if (Handle_panel[18].activeInHierarchy == false)
        {
            Handle_panel[18].SetActive(true);
        }
        if (Handle_panel[19].activeInHierarchy == false)
        {
            Handle_panel[19].SetActive(true);
        }
        panelComeback.Push(_360_select_panels);
    }

    public void ShowPanoramaVideos()
    {
        isFlash = true;
        DestorySelect();
        if (Handle_panel.ContainsKey(4))
        {

        }
        else
        {
            Show(4, Help_canvs, "Help canvs", "Other panel/Home help menu");
        }
        if (Handle_panel.ContainsKey(19))
        {

        }
        else
        {
            Show(19, Left_canvs, "Left canvs", "Left panel/360 degree panorama sort");
        }
        Show(23, Middle_canvs, "Middle canvs", "Middle panel/panorama 360 video area");


        if (Handle_panel[4].activeInHierarchy == false)
        {
            Handle_panel[4].SetActive(true);
        }
        if (Handle_panel[19].activeInHierarchy == false)
        {
            Handle_panel[19].SetActive(true);
        }
        panorama_videos_panels = new Panels(Handle_panel[23], Handle_panel[4], Handle_panel[19]);
        panelComeback.Push(panorama_videos_panels);
        isFlash = true;
    }

    public void ShowPanorama()
    {
        isFlash = true;
        Show(24, Middle_canvs, "Middle canvs", "Middle panel/panorama 180 video area");
        if (Handle_panel[4].activeInHierarchy == false)
        {
            Handle_panel[4].SetActive(true);
        }
        if (Handle_panel[19].activeInHierarchy == false)
        {
            Handle_panel[19].SetActive(true);
        }
        panorama_panels = new Panels(Handle_panel[24], Handle_panel[4], Handle_panel[19]);
        panelComeback.Push(panorama_panels);
        isFlash = true;
    }

    public void ShowVRRecommend()
    {
        HideAll();
        if (Handle_panel[4].activeInHierarchy == false)
        {
            Handle_panel[4].SetActive(true);
        }
        if (Handle_panel[20].activeInHierarchy == false)
        {
            Handle_panel[20].SetActive(true);
        }

        if (Handle_panel[21].activeInHierarchy == false)
        {
            Handle_panel[21].SetActive(true);
        }
        panelComeback.Push(vr_select_panels);
    }

    public void ShowVRBroadcast()
    {
        isFlash = true;
        if (!(Handle_panel.ContainsKey(4) && Handle_panel.ContainsKey(21)))
        {
            Show(4, Help_canvs, "Help canvs", "Other panel/Home help menu");
            Show(21, Left_canvs, "Left canvs", "Left panel/VR live broadcast sort");
        }

        DestorySelect();
        Show(25, Middle_canvs, "Middle canvs", "Middle panel/VR live broadcast area");

        if (Handle_panel[4].activeInHierarchy == false)
        {
            Handle_panel[4].SetActive(true);
        }
        if (Handle_panel[21].activeInHierarchy == false)
        {
            Handle_panel[21].SetActive(true);
        }
        vr_broast_panels = new Panels(Handle_panel[25], Handle_panel[4], Handle_panel[21]);
        panelComeback.Push(vr_broast_panels);
      
    }

    public void ShowLocalVideo()
    {
        isFlash = true;
        HideAll();
        if (Handle_panel[9].activeInHierarchy == false)
        {
            Handle_panel[9].SetActive(true);
        }
        if (Handle_panel[10].activeInHierarchy == false)
        {
            Handle_panel[10].SetActive(true);
        }
        panelComeback.Push(local_panels);
      
    }

    public void ShowImage()
    {
        isFlash = true;
        DestorySelect();
        Show(26, Middle_canvs, "Middle canvs", "Middle panel/Local images");
        if (Handle_panel[9].activeInHierarchy == false)
        {
            Handle_panel[9].SetActive(true);
        }
        local_imgae_panels = new Panels(Handle_panel[26], Handle_panel[9]);
        panelComeback.Push(local_imgae_panels);
       
    }

    public void ShowInstallationPackages()
    {
        isFlash = true;
        Show(27, Middle_canvs, "Middle canvs", "Middle panel/Local Installation packages");
        if (Handle_panel[9].activeInHierarchy == false)
        {
            Handle_panel[9].SetActive(true);
        }
        local_installationPackages = new Panels(Handle_panel[27], Handle_panel[9]);
        panelComeback.Push(local_installationPackages);
       
    }
    public void ShowDelete()
    {
    
        Show(28, Middle_canvs, "Middle canvs", "Middle panel/Deleteing");
        if (Handle_panel[9].activeInHierarchy == false)
        {
            Handle_panel[9].SetActive(true);
        }
        local_delete_panels = new Panels(Handle_panel[28], Handle_panel[9]);
        panelComeback.Push(local_delete_panels);
        
    }
    public void DeleteIsYes()
    {
        Handle_panel[28].SetActive(false);
        DeleteApp();
    }
    public void DeleteCanel()
    {
        Handle_panel[28].SetActive(false);
    }

    public void DeleteApp()
    {
        Debug.Log("删除");
    }

    public void ClickTourist()
    {
        if (GameObject.Find("Select recommend(Clone)"))
        {
            GameObject.Find("Select recommend(Clone)").SetActive(false);
        }
        if (GameObject.Find("Select left(Clone)"))
        {
            GameObject.Find("Select left(Clone)").SetActive(false);
        }
        if (GameObject.Find("Game and App sort(Clone)"))
        {
            GameObject.Find("Game and App sort(Clone)").SetActive(false);
        }
        GetAllCanvs();
        if (Icon_canvs.transform.GetChild(0).GetChild(5).GetChild(1).GetComponent<Text>().text == "游客")
        {
            Show(29, Big_middle_canvs, "Big middle canvs", "Big middle panel/Visitors log in");

            DestoryHome();
            panelComeback.Push(Handle_panel[29]);
        }
        else
        {
            InShowPerson=true;
        }
    }

    public void ShowLogin()
    {
        DestoryHome();
        if (GameObject.Find("Living room(Clone)"))
        {
          
            AllEnbaleCanvs();
            if (GameObject.Find("Select recommend"))
            {
                DestoryHome();
            }
        }
        if(GameObject.Find("Icon canvs"))
        {
            GameObject.Find("Icon canvs").SetActive(false);
        }
        if(GameObject.Find("Menu"))
        {
            GameObject.Find("Menu").SetActive(false);
        }
        Show(30, Big_middle_canvs, "Big middle canvs", "Big Middle panel/User log in");
        panelComeback.Push(Handle_panel[30]);
        Handle_panel[30].transform.GetChild(5).gameObject.SetActive(true);
        Handle_panel[30].transform.GetChild(0).GetChild(0).GetComponent<IVRInputField>().onValueChanged.AddListener(Get_Mobile_Email);
        Handle_panel[30].transform.GetChild(3).GetChild(0).GetComponent<IVRInputField>().onValueChanged.AddListener(Get_Mobile_Email);
        Handle_panel[30].transform.GetChild(0).GetChild(1).GetComponent<IVRInputField>().onValueChanged.AddListener(Get_passord);
        Handle_panel[30].transform.GetChild(3).GetChild(1).GetComponent<IVRInputField>().onValueChanged.AddListener(Get_passord);
        Handle_panel[30].transform.GetChild(4).GetChild(2).GetComponent<IVRInputField>().onValueChanged.AddListener(GetImageCode);
        Handle_panel[30].transform.GetChild(0).GetChild(4).GetComponent<IVRInputField>().onValueChanged.AddListener(GetSMSCode);

    }

    public void MobliePhoneLogin()
    {
         
        Handle_panel[30].transform.GetChild(0).gameObject.SetActive(true);
        Handle_panel[30].transform.GetChild(3).gameObject.SetActive(false);
        Handle_panel[30].transform.GetChild(4).gameObject.SetActive(false);

    }

    public void CommenLogin()
    {
       
        Handle_panel[30].transform.GetChild(0).gameObject.SetActive(false);
        Handle_panel[30].transform.GetChild(3).gameObject.SetActive(true);
        Handle_panel[30].transform.GetChild(4).gameObject.SetActive(false);
      
    }

    public void BlueConnection()
    {
        DestoryHome();
        if (GameObject.Find("Select recommend(Clone)"))
        {
            GameObject.Find("Select recommend(Clone)").SetActive(false);
        }
        if (GameObject.Find("Select left(Clone)"))
        {
            GameObject.Find("Select left(Clone)").SetActive(false);
        }

        if (GameObject.Find("Game and App sort(Clone)"))
        {
            GameObject.Find("Game and App sort(Clone)").SetActive(false);
        }
        HideAll();
        Show(31, Big_middle_canvs, "Big middle canvs", "Big middle panel/Bluetooth connection interface");
        panelComeback.Push(Handle_panel[31]);
    }
    public void WifiConnection()
    {
        DestoryHome();
        if (GameObject.Find("Select recommend(Clone)"))
        {
            GameObject.Find("Select recommend(Clone)").SetActive(false);
        }
        if (GameObject.Find("Select left(Clone)"))
        {
            GameObject.Find("Select left(Clone)").SetActive(false);
        }
        if (GameObject.Find("Game and App sort(Clone)"))
        {
            GameObject.Find("Game and App sort(Clone)").SetActive(false);
        }
        HideAll();
        Show(32, Big_middle_canvs, "Big middle canvs", "Big middle panel/Wireless connection interface");
        panelComeback.Push(Handle_panel[32]);
    }

    public void ShowSetting()
    {
        DestoryHome();
        if (GameObject.Find("Select recommend(Clone)"))
        {
            GameObject.Find("Select recommend(Clone)").SetActive(false);
        }
        if (GameObject.Find("Select left(Clone)"))
        {
            GameObject.Find("Select left(Clone)").SetActive(false);
        }
        if (GameObject.Find("Game and App sort(Clone)"))
        {
            GameObject.Find("Game and App sort(Clone)").SetActive(false);
        }
        Show(33, Big_middle_canvs, "Big middle canvs", "Big middle panel/Setting");
        panelComeback.Push(Handle_panel[33]);
    }

    public void ShowScene()
    {
        if (GameObject.Find("Select recommend(Clone)"))
        {
            GameObject.Find("Select recommend(Clone)").SetActive(false);
        }
        if (GameObject.Find("Select left(Clone)"))
        {
            GameObject.Find("Select left(Clone)").SetActive(false);
        }
        if (GameObject.Find("Game and App sort(Clone)"))
        {
            GameObject.Find("Game and App sort(Clone)").SetActive(false);
        }
        Show(34, Big_middle_canvs, "Big middle canvs", "Big middle panel/Scene");
        panelComeback.Push(Handle_panel[34]);
    }

    public void ShowProjectionScreen()
    {
        if (GameObject.Find("Select recommend(Clone)"))
        {
            GameObject.Find("Select recommend(Clone)").SetActive(false);
        }
        if (GameObject.Find("Select left(Clone)"))
        {
            GameObject.Find("Select left(Clone)").SetActive(false);
        }
        if (GameObject.Find("Game and App sort(Clone)"))
        {
            GameObject.Find("Game and App sort(Clone)").SetActive(false);
        }
        Show(35, Big_middle_canvs, "Big middle canvs", "Big middle panel/Projection screen");
        panelComeback.Push(Handle_panel[35]);
    }
    public void ShowSystemUpdata()
    {
        if (GameObject.Find("Select recommend(Clone)"))
        {
            GameObject.Find("Select recommend(Clone)").SetActive(false);
        }
        if (GameObject.Find("Select left(Clone)"))
        {
            GameObject.Find("Select left(Clone)").SetActive(false);
        }
        if (GameObject.Find("Game and App sort(Clone)"))
        {
            GameObject.Find("Game and App sort(Clone)").SetActive(false);
        }
        Show(36, Big_middle_canvs, "Big middle canvs", "Big middle panel/System update interface");
        panelComeback.Push(Handle_panel[36]);
    }
    public void AccountLogin()
    {
        user_token_Get = true;

    }

    private void ALLFalse()
    {
        if (Handle_panel.ContainsKey(37))
        {
            Handle_panel[37].transform.GetChild(0).gameObject.SetActive(false);
            Handle_panel[37].transform.GetChild(12).gameObject.SetActive(false);
            Handle_panel[37].transform.GetChild(2).gameObject.SetActive(false);
            Handle_panel[37].transform.GetChild(4).gameObject.SetActive(false);
            Handle_panel[37].transform.GetChild(5).gameObject.SetActive(false);
            Handle_panel[37].transform.GetChild(6).gameObject.SetActive(false);
            Handle_panel[37].transform.GetChild(12).gameObject.SetActive(false);
            Handle_panel[37].transform.GetChild(13).gameObject.SetActive(false);
            Handle_panel[37].transform.GetChild(14).gameObject.SetActive(false);
            Handle_panel[37].transform.GetChild(15).gameObject.SetActive(false);
            Handle_panel[37].transform.GetChild(16).gameObject.SetActive(false);
            Handle_panel[37].transform.GetChild(17).gameObject.SetActive(false);
            Handle_panel[37].transform.GetChild(18).gameObject.SetActive(false);
        }
    }
  
    public void ShowAllWork()
    {
        if (!Handle_panel.ContainsKey(37))
        {
            Show(37, Big_middle_canvs, "Big middle canvs", "Big middle panel/Personal homepage");
        }
        ALLFalse();
        Handle_panel[37].transform.GetChild(0).gameObject.SetActive(true);
        Handle_panel[37].transform.GetChild(2).gameObject.SetActive(true);
      

    }
    public void ShowMyPanoramicVideo()
    {
        if (!Handle_panel.ContainsKey(37))
        {
            Show(37, Big_middle_canvs, "Big middle canvs", "Big middle panel/Personal homepage");
        }
        ALLFalse();
        Handle_panel[37].transform.GetChild(0).gameObject.SetActive(true);
        Handle_panel[37].transform.GetChild(4).gameObject.SetActive(true);
      //  my_panoramic_video_panels = new Panels(Handle_panel[37], Handle_panel[37].transform.GetChild(0).gameObject, Handle_panel[37].transform.GetChild(4).gameObject);
    }
    public void ShowMyPanoramicImage()
    {
        if (!Handle_panel.ContainsKey(37))
        {
            Show(37, Big_middle_canvs, "Big middle canvs", "Big middle panel/Personal homepage");
        }
        ALLFalse();
        Handle_panel[37].transform.GetChild(0).gameObject.SetActive(true);
        Handle_panel[37].transform.GetChild(5).gameObject.SetActive(true);
        // my_panoramic_image_panels = new Panels(Handle_panel[37], Handle_panel[37].transform.GetChild(5).gameObject, Handle_panel[37].transform.GetChild(0).gameObject);
        //  panelComeback.Push(my_panoramic_image_panels);
    }
    public void ShowMyLvingBack()
    {
        if (!Handle_panel.ContainsKey(37))
        {
            Show(37, Big_middle_canvs, "Big middle canvs", "Big middle panel/Personal homepage");
        }
        ALLFalse();
        Handle_panel[37].transform.GetChild(0).gameObject.SetActive(true);
        Handle_panel[37].transform.GetChild(6).gameObject.SetActive(true);
        // my_livingback_panels = new Panels(Handle_panel[37], Handle_panel[37].transform.GetChild(0).gameObject, Handle_panel[37].transform.GetChild(6).gameObject);
        //panelComeback.Push(my_livingback_panels);
    }

    public void ShowFollowingPanel()
    {
        if (!Handle_panel.ContainsKey(37))
        {
            Show(37, Big_middle_canvs, "Big middle canvs", "Big middle panel/Personal homepage");
        }
        ALLFalse();
        Handle_panel[37].transform.GetChild(12).gameObject.SetActive(true);
        Handle_panel[37].transform.GetChild(13).gameObject.SetActive(true);
        // my_following_panels = new Panels(Handle_panel[37], Handle_panel[37].transform.GetChild(12).gameObject, Handle_panel[37].transform.GetChild(13).gameObject);
        // panelComeback.Push(my_following_panels);
    }

    public void ShowFollowing_AnchorPanel()
    {
        if (!Handle_panel.ContainsKey(37))
        {
            Show(37, Big_middle_canvs, "Big middle canvs", "Big middle panel/Personal homepage");
        }
        ALLFalse();
        Handle_panel[37].transform.GetChild(12).gameObject.SetActive(true);
        Handle_panel[37].transform.GetChild(13).gameObject.SetActive(true);
        // my_following_anchorer_panels = new Panels(Handle_panel[37], Handle_panel[37].transform.GetChild(12).gameObject, Handle_panel[37].transform.GetChild(13).gameObject);
        //panelComeback.Push(my_following_anchorer_panels);
    }

    public void ShowFollowingCreatorPanel()
    {
        if (!Handle_panel.ContainsKey(37))
        {
            Show(37, Big_middle_canvs, "Big middle canvs", "Big middle panel/Personal homepage");
        }
        ALLFalse();
        Handle_panel[37].transform.GetChild(12).gameObject.SetActive(true);
        Handle_panel[37].transform.GetChild(14).gameObject.SetActive(true);
        //my_following_creator_panels = new Panels(Handle_panel[37], Handle_panel[37].transform.GetChild(14).gameObject, Handle_panel[37].transform.GetChild(12).gameObject);
        //panelComeback.Push(my_following_creator_panels);
    }

    public void ShowStarPanel()
    {
        if (!Handle_panel.ContainsKey(37))
        {
            Show(37, Big_middle_canvs, "Big middle canvs", "Big middle panel/Personal homepage");
        }
        ALLFalse();
        Handle_panel[37].transform.GetChild(15).gameObject.SetActive(true);
        //my_staring_panels = new Panels(Handle_panel[37], Handle_panel[37].transform.GetChild(15).gameObject);
        //panelComeback.Push(my_staring_panels);
    }
    public void ShowFunsPanel()
    {
        if (!Handle_panel.ContainsKey(37))
        {
            Show(37, Big_middle_canvs, "Big middle canvs", "Big middle panel/Personal homepage");
        }
        ALLFalse();
        Handle_panel[37].transform.GetChild(16).gameObject.SetActive(true);
        //my_funs_panels = new Panels(Handle_panel[37], Handle_panel[37].transform.GetChild(16).gameObject);
        // panelComeback.Push(my_funs_panels);
    }
    public void ShowHistoryPanel()
    {
        if (!Handle_panel.ContainsKey(37))
        {
            Show(37, Big_middle_canvs, "Big middle canvs", "Big middle panel/Personal homepage");
        }
        ALLFalse();
        Handle_panel[37].transform.GetChild(17).gameObject.SetActive(true);
        //    my_history_panels = new Panels(Handle_panel[37], Handle_panel[37].transform.GetChild(17).gameObject);
        //    panelComeback.Push(my_history_panels);
    }
    public void ShowMessagePanel()
    {
        if (!Handle_panel.ContainsKey(37))
        {
            Show(37, Big_middle_canvs, "Big middle canvs", "Big middle panel/Personal homepage");
        }
        ALLFalse();
        Handle_panel[37].transform.GetChild(18).gameObject.SetActive(true);
        //my_message_panels = new Panels(Handle_panel[37], Handle_panel[37].transform.GetChild(18).gameObject);
        //panelComeback.Push(my_message_panels);
    }

    private void DisableHomeCanvs()
    {
        GetAllCanvs();
        if (Middle_canvs != null && Left_canvs != null)
        {
            Middle_canvs.SetActive(false);
            Left_canvs.SetActive(false);
            Right_canvs.SetActive(false);
            Big_middle_canvs.SetActive(false);
            Icon_canvs.SetActive(false);
            Help_canvs.SetActive(false);
            Menu_canvs.SetActive(false);
        }

    }

    public void EnterLivingRoom()
    {
        Debug.Log("进入直播间");
        if (GameObject.Find("Living room") != null)
            GameObject.Find("Living room").SetActive(false);
        if (GameObject.Find("Living room(Clone)") != null)
            GameObject.Find("Living room(Clone)").SetActive(false);
        if (GameObject.Find("Three hundred and sixty dergee living room") != null)
            GameObject.Find("Three hundred and sixty dergee living room").SetActive(false);

        if (GameObject.Find("Three hundred and sixty dergee living room(Clone)") != null)
            GameObject.Find("Three hundred and sixty dergee living room(Clone)").SetActive(false);

        living_room = CreatLivingRoom("LivingRoom/Living room folder/Living room");
 


        panelComeback.Push(living_room);
    }
    public void Enter360DegreeVideos()
    {
        Debug.Log("点击360");
        if (GameObject.Find("Living room") != null)
            GameObject.Find("Living room").SetActive(false);
        if (GameObject.Find("Living room(Clone)") != null)
            GameObject.Find("Living room(Clone)").SetActive(false);
        if (GameObject.Find("Three hundred and sixty dergee living room") != null)
            GameObject.Find("Three hundred and sixty dergee living room ").SetActive(false);
        if (GameObject.Find("Three hundred and sixty dergee living room(Clone)") != null)
            GameObject.Find("Three hundred and sixty dergee living room(Clone)").SetActive(false);
        _360_living_room = CreatLivingRoom("LivingRoom/360 degree video folder/Three hundred and sixty dergee living room");
        panelComeback.Push(_360_living_room);
    }
    public void EnterGrandScreenCinema()
    {
        Debug.Log("事件响应");
        DisableHomeCanvs();
        if (GameObject.Find("Grand curtain cinema room") != null)
            GameObject.Find("Grand curtain cinema room").SetActive(false);
        if (GameObject.Find("Grand curtain cinema room(Clone)") != null)
            GameObject.Find("Grand curtain cinema room(Clone)").SetActive(false);
        grand_living_room = CreatLivingRoom("LivingRoom/Grand curtain cinema folder/Grand curtain cinema room");
        panelComeback.Push(grand_living_room);
    }

    private GameObject CreatLivingRoom(string path)
    {
        GameObject livngroom = Resources.Load<GameObject>(path);
        livngroom = Instantiate(livngroom);
        return livngroom;
    }

    private void ExitRoom(string name)
    {
        if (GameObject.Find(name) == null)
            Debug.Log("没找到物体");
        Destroy(GameObject.Find(name));
        AllEnbaleCanvs();
    }

    public void ExitLivingRoom()
    {
        ExitRoom("Living room(Clone)");
    }

    public void ExitGrandScreenCinema()
    {
        ExitRoom("Grand curtain cinema room(Clone)");
    }

    public void Exit360DegreeVideos()
    {

        ExitRoom("Three hundred and sixty dergee living room(Clone)");

    }

    private void AllEnbaleCanvs()
    {

        Middle_canvs.SetActive(true);
        Left_canvs.SetActive(true);
        Right_canvs.SetActive(true);
        Big_middle_canvs.SetActive(true);
        Icon_canvs.SetActive(true);
        Help_canvs.SetActive(true);
        Menu_canvs.SetActive(true);
    }

    private void Pop()
    {
        Debug.Log("出栈!");
        //ALLFalse();
        //AllEnbaleCanvs();
        if (panelComeback.Count > 1)
        {
            if (panelComeback.Peek().GetType() == gameObject.GetType())
            {

                GameObject panel = (GameObject)panelComeback.Peek();
                panel.SetActive(false);
            }
            else
            {
                Panels panels = (Panels)panelComeback.Peek();
                panels.Hide();
            }
            panelComeback.Pop();

            if (panelComeback.Peek().GetType() == gameObject.GetType())
            {

                GameObject panel = (GameObject)panelComeback.Peek();
                Regex name = new Regex("room");
                if (name.Match(panel.name).Success)
                {
                    DisableHomeCanvs();
                }
                panel.SetActive(true);
            }
            else
            {
                Panels panels = (Panels)panelComeback.Peek();
                panels.Come();
            }

        }
        else
        {
            ExitApp();
        }

        Debug.Log(panelComeback.Peek());
    }

    private void ExitApp()
    {
        if (GameObject.Find("Quiting(Clone)") == false)
        {
            Big_middle_canvs = GameObject.Find("Big middle canvs");
            CreatPanel("Middle panel/Quiting", 50, Big_middle_canvs.transform);
        }
    }
    public void ExitTheVRApp()
    {
        Application.Quit();
    }
    public void NotExit()
    {
        //Show(50, Middle_canvs, "Middle_Canvs", "Middle panel/Quiting");
        Destroy(GameObject.Find("Quiting(Clone)"));
        Handle_panel.Remove(50);
    }

    private void GetUserToken(UserToken user, GameObject[] nothing, string nothing_s)
    {
        user_info = user.token;
        AllData.token = user.token;

        StartCoroutine(DataClassInterface.IEGetDate<UserDataFind>(AllData.DataString+"/coocaa/api/getUserByToken?token=" + user.token, new DataClassInterface.OnDataGet<UserDataFind>(GetUserFromToken), null));
        
    }
    private void RegisterGetToken(UserToken user, GameObject[] nothing, string no)
    {
        user_info = user.token;
        AllData.token = user.token;

        StartCoroutine(DataClassInterface.IEGetDate<UserDataFind>(AllData.DataString + "/coocaa/api/getUserByToken?token=" + user.token, new DataClassInterface.OnDataGet<UserDataFind>(GetUserFromToken), null));

    }

    public void  Get_Mobile_Email(string value)
    {
        
        mobile_number_email = value;
    
    }

    public void Get_passord(string value)
    {
        
        user_password = value;
       
    }
    public void GetImageCode(string value)
    {
        ImageCode = value;
     
    }
    private void Get_Image(VerificationImage myImage, GameObject[] go, string base64)
    {
        GameObject userLogin = GameObject.Find("User log in");

        if (userLogin.transform.GetChild(4).GetChild(1).gameObject.activeInHierarchy == false)
        {
            userLogin.transform.GetChild(4).GetChild(1).gameObject.SetActive(true);
        }

        Debug.Log("原版：" + base64);
        Debug.Log("数据" + base64.Substring(base64.LastIndexOf(",") + 1, base64.LastIndexOf("}") - 2 - base64.LastIndexOf(",")));
        ImageCodeKey = base64.Substring(base64.IndexOf(":") + 2, base64.IndexOf(",") - 13);
        Debug.Log("图片码：" + ImageCodeKey);
        Base64Tolmg(userLogin.transform.GetChild(4).GetChild(1).GetComponent<Image>(), base64.Substring(base64.LastIndexOf(",") + 1, base64.LastIndexOf("}") - 2 - base64.LastIndexOf(",")));
    }

    public void Get_Image_button()
    {
        Debug.Log("点击成功");
        user_image = true;
    }

    private void Base64Tolmg(Image imgComponent, string base64)
    {

        byte[] bytes = Convert.FromBase64String(base64);
        Texture2D tex2D = new Texture2D(100, 100);
        tex2D.LoadImage(bytes);
        Sprite s = Sprite.Create(tex2D, new Rect(0, 0, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f));
        imgComponent.sprite = s;
        Resources.UnloadUnusedAssets();
    }

    public void RegisterAndLogin()
    {
        registerAndLogin = true;

    }

    public void Get_Moblie_Verification()
    {
        if (mobile_number_email == null)
        {
            DisplayMsg("请输入手机号F");
            Debug.LogError("请输入手机号");
        }
        else
        {

            if (mobile_number_email.Length == 11)
            {
                if (!Handle_panel.ContainsKey(30))
                {
                    Show(30, Big_middle_canvs, "Big middle canvs", "Big Middle panel/User log in");
                }
                Handle_panel[30].transform.GetChild(4).gameObject.SetActive(true);
                Handle_panel[30].transform.GetChild(0).gameObject.SetActive(false);
                

            }
            else
            {
                DisplayMsg("请输入正确的手机号");
            }
        }
    }

    private void GetMoblieFromwww(VerificationImage data, GameObject[] go, string nothig)
    {
        DisplayMsg("获取手机验证码");
    }

    public void GetCodeF()
    {
        getmobliecode = true;
    }

    public void GetSMSCode(string value)
    {
        SMSCode = value;
      
    }

    public void GetBackResgin()
    {
        Handle_panel[30].transform.GetChild(0).gameObject.SetActive(true);
        Handle_panel[30].transform.GetChild(3).gameObject.SetActive(false);
        Handle_panel[30].transform.GetChild(4).gameObject.SetActive(false);
    }

    private void GetPopularityList(PopularityList[] datas, GameObject[] room, string nothing)//日榜的数据选择
    {
        int i = 0;
        foreach (PopularityList data in datas)
        {
            Debug.Log("榜单信息" + data.ToString());
            room[i].transform.GetChild(3).GetComponent<Text>().text = data.nickName;
            room[i].transform.GetChild(5).GetComponent<Text>().text = data.likeNum + "";
            StartCoroutine(DataClassInterface.IEGetSprite(data.headImage, new DataClassInterface.OnDataGetSprite(GetSprite), room[i].transform.GetChild(2).gameObject));
            i++;
        }
        for (; i < 4; i++)
        {
            room[i].SetActive(false);
        }
    }

    private void GetSprite(Sprite s, GameObject gbj, string nothing)
    {
        gbj.GetComponent<Image>().sprite = s;

    }

    private void LivingDataUp(LivingRoomData[] datas, GameObject[] gbj, string nothing)
    {//直播页面推荐位显示
        GameObject selected;
        if (GameObject.Find("Select recommend") != null)
        {
            selected = GameObject.Find("Select recommend");
        }
        else if (GameObject.Find("Select recommend(Clone)") != null)
        {
            selected = GameObject.Find("Select recommend(Clone)");
        }

    }

    public void ShowMyHome()
    {

        Debug.Log("点击事件0");
        InShowPerson = true;
    }


    //模拟键盘触发事件

    public void GetMobile_1()
    {
        if (GameObject.Find("User log in(Clone)").transform.GetChild(5).gameObject.activeInHierarchy == true)
        {
            mobile_number_email += "1";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            }
        }

        else if (GameObject.Find("User log in(Clone)").transform.GetChild(6).gameObject.activeInHierarchy == true)
        {
            user_password += "1";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(1).GetComponent<InputField>().text = user_password;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(1).GetComponent<InputField>().text = user_password;
            }

        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(7).gameObject.activeInHierarchy == true)
        {
            SMSCode += "1";
            GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(4).GetComponent<InputField>().text = SMSCode;
        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(8).gameObject.activeInHierarchy == true)
        {
            ImageCode += "1";
            GameObject.Find("User log in(Clone)").transform.GetChild(4).GetChild(2).GetComponent<InputField>().text = ImageCode;
        }

    }
    public void GetMobile_2()
    {

        if (GameObject.Find("User log in(Clone)").transform.GetChild(5).gameObject.activeInHierarchy == true)
        {
            mobile_number_email += "2";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            }
        }

        else if (GameObject.Find("User log in(Clone)").transform.GetChild(6).gameObject.activeInHierarchy == true)
        {
            user_password += "2";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(1).GetComponent<InputField>().text = user_password;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(1).GetComponent<InputField>().text = user_password;
            }

        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(7).gameObject.activeInHierarchy == true)
        {
            SMSCode += "2";
            GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(4).GetComponent<InputField>().text = SMSCode;
        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(8).gameObject.activeInHierarchy == true)
        {
            ImageCode += "2";
            GameObject.Find("User log in(Clone)").transform.GetChild(4).GetChild(2).GetComponent<InputField>().text = ImageCode;
        }
    }
    public void GetMobile_3()
    {
        if (GameObject.Find("User log in(Clone)").transform.GetChild(5).gameObject.activeInHierarchy == true)
        {
            mobile_number_email += "3";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            }
        }

        else if (GameObject.Find("User log in(Clone)").transform.GetChild(6).gameObject.activeInHierarchy == true)
        {
            user_password += "3";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(1).GetComponent<InputField>().text = user_password;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(1).GetComponent<InputField>().text = user_password;
            }

        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(7).gameObject.activeInHierarchy == true)
        {
            SMSCode += "3";
            GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(4).GetComponent<InputField>().text = SMSCode;
        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(8).gameObject.activeInHierarchy == true)
        {
            ImageCode += "3";
            GameObject.Find("User log in(Clone)").transform.GetChild(4).GetChild(2).GetComponent<InputField>().text = ImageCode;
        }
    }
    public void GetMobile_4()
    {

        if (GameObject.Find("User log in(Clone)").transform.GetChild(5).gameObject.activeInHierarchy == true)
        {
            mobile_number_email += "4";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            }
        }

        else if (GameObject.Find("User log in(Clone)").transform.GetChild(6).gameObject.activeInHierarchy == true)
        {
            user_password += "4";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(1).GetComponent<InputField>().text = user_password;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(1).GetComponent<InputField>().text = user_password;
            }

        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(7).gameObject.activeInHierarchy == true)
        {
            SMSCode += "4";
            GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(4).GetComponent<InputField>().text = SMSCode;
        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(8).gameObject.activeInHierarchy == true)
        {
            ImageCode += "4";
            GameObject.Find("User log in(Clone)").transform.GetChild(4).GetChild(2).GetComponent<InputField>().text = ImageCode;
        }
    }
    public void GetMobile_5()
    {
        if (GameObject.Find("User log in(Clone)").transform.GetChild(5).gameObject.activeInHierarchy == true)
        {
            mobile_number_email += "5";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            }
        }

        else if (GameObject.Find("User log in(Clone)").transform.GetChild(6).gameObject.activeInHierarchy == true)
        {
            user_password += "5";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(1).GetComponent<InputField>().text = user_password;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(1).GetComponent<InputField>().text = user_password;
            }

        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(7).gameObject.activeInHierarchy == true)
        {
            SMSCode += "5";
            GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(4).GetComponent<InputField>().text = SMSCode;
        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(8).gameObject.activeInHierarchy == true)
        {
            ImageCode += "5";
            GameObject.Find("User log in(Clone)").transform.GetChild(4).GetChild(2).GetComponent<InputField>().text = ImageCode;
        }
    }
    public void GetMobile_6()
    {

        if (GameObject.Find("User log in(Clone)").transform.GetChild(5).gameObject.activeInHierarchy == true)
        {
            mobile_number_email += "6";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            }
        }

        else if (GameObject.Find("User log in(Clone)").transform.GetChild(6).gameObject.activeInHierarchy == true)
        {
            user_password += "6";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(1).GetComponent<InputField>().text = user_password;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(1).GetComponent<InputField>().text = user_password;
            }

        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(7).gameObject.activeInHierarchy == true)
        {
            SMSCode += "6";
            GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(4).GetComponent<InputField>().text = SMSCode;
        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(8).gameObject.activeInHierarchy == true)
        {
            ImageCode += "6";
            GameObject.Find("User log in(Clone)").transform.GetChild(4).GetChild(2).GetComponent<InputField>().text = ImageCode;
        }
    }
    public void GetMobile_7()
    {

        if (GameObject.Find("User log in(Clone)").transform.GetChild(5).gameObject.activeInHierarchy == true)
        {
            mobile_number_email += "7";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            }
        }

        else if (GameObject.Find("User log in(Clone)").transform.GetChild(6).gameObject.activeInHierarchy == true)
        {
            user_password += "7";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(1).GetComponent<InputField>().text = user_password;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(1).GetComponent<InputField>().text = user_password;
            }

        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(7).gameObject.activeInHierarchy == true)
        {
            SMSCode += "7";
            GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(4).GetComponent<InputField>().text = SMSCode;
        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(8).gameObject.activeInHierarchy == true)
        {
            ImageCode += "7";
            GameObject.Find("User log in(Clone)").transform.GetChild(4).GetChild(2).GetComponent<InputField>().text = ImageCode;
        }
    }
    public void GetMobile_8()
    {

        if (GameObject.Find("User log in(Clone)").transform.GetChild(5).gameObject.activeInHierarchy == true)
        {
            mobile_number_email += "8";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            }
        }

        else if (GameObject.Find("User log in(Clone)").transform.GetChild(6).gameObject.activeInHierarchy == true)
        {
            user_password += "8";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(1).GetComponent<InputField>().text = user_password;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(1).GetComponent<InputField>().text = user_password;
            }

        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(7).gameObject.activeInHierarchy == true)
        {
            SMSCode += "8";
            GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(4).GetComponent<InputField>().text = SMSCode;
        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(8).gameObject.activeInHierarchy == true)
        {
            ImageCode += "8";
            GameObject.Find("User log in(Clone)").transform.GetChild(4).GetChild(2).GetComponent<InputField>().text = ImageCode;
        }
    }
    public void GetMobile_9()
    {
        if (GameObject.Find("User log in(Clone)").transform.GetChild(5).gameObject.activeInHierarchy == true)
        {
            mobile_number_email += "9";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            }
        }

        else if (GameObject.Find("User log in(Clone)").transform.GetChild(6).gameObject.activeInHierarchy == true)
        {
            user_password += "9";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(1).GetComponent<InputField>().text = user_password;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(1).GetComponent<InputField>().text = user_password;
            }

        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(7).gameObject.activeInHierarchy == true)
        {
            SMSCode += "9";
            GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(4).GetComponent<InputField>().text = SMSCode;
        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(8).gameObject.activeInHierarchy == true)
        {
            ImageCode += "9";
            GameObject.Find("User log in(Clone)").transform.GetChild(4).GetChild(2).GetComponent<InputField>().text = ImageCode;
        }
    }
    public void GetMobile_0()
    {
        if (GameObject.Find("User log in(Clone)").transform.GetChild(5).gameObject.activeInHierarchy == true)
        {
            mobile_number_email += "0";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
            }
        }

        else if (GameObject.Find("User log in(Clone)").transform.GetChild(6).gameObject.activeInHierarchy == true)
        {
            user_password += "0";
            if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(1).GetComponent<InputField>().text = user_password;
            else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(1).GetComponent<InputField>().text = user_password;
            }

        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(7).gameObject.activeInHierarchy == true)
        {
            SMSCode += "0";
            GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(4).GetComponent<InputField>().text = SMSCode;
        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(8).gameObject.activeInHierarchy == true)
        {
            ImageCode += "0";
            GameObject.Find("User log in(Clone)").transform.GetChild(4).GetChild(2).GetComponent<InputField>().text = ImageCode;
        }
    }
    public void GetMobile_Back()
    {
        if (GameObject.Find("User log in(Clone)").transform.GetChild(5).gameObject.activeInHierarchy == true)
        {
            if (mobile_number_email.Length != 0)
            {
                mobile_number_email = mobile_number_email.Substring(0, mobile_number_email.Length - 1);

                if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                    GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
                else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
                {
                    GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(0).GetComponent<InputField>().text = mobile_number_email;
                }

            }
        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(6).gameObject.activeInHierarchy == true)
        {
            if (user_password.Length != 0)
            {
                user_password = user_password.Substring(0, user_password.Length - 1);

                if (GameObject.Find("User log in(Clone)").transform.GetChild(0).gameObject.activeInHierarchy == true)
                    GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(1).GetComponent<InputField>().text = user_password;
                else if (GameObject.Find("User log in(Clone)").transform.GetChild(3).gameObject.activeInHierarchy == true)
                {
                    GameObject.Find("User log in(Clone)").transform.GetChild(3).GetChild(1).GetComponent<InputField>().text = user_password;
                }

            }
        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(7).gameObject.activeInHierarchy == true)
        {
            if (SMSCode.Length != 0)
            {
                SMSCode = SMSCode.Substring(0, SMSCode.Length - 1);

                GameObject.Find("User log in(Clone)").transform.GetChild(0).GetChild(4).GetComponent<InputField>().text = SMSCode;

            }
        }
        else if (GameObject.Find("User log in(Clone)").transform.GetChild(8).gameObject.activeInHierarchy == true)
        {
            if (ImageCode.Length != 0)
            {
                ImageCode = ImageCode.Substring(0, ImageCode.Length - 1);

                GameObject.Find("User log in(Clone)").transform.GetChild(4).GetChild(2).GetComponent<InputField>().text = ImageCode;
                Debug.Log("返回键！！！" + ImageCode);
            }
        }

    }

    public void GetUserFromToken(UserDataFind data, GameObject[] gbj, string no)
    {
        //用户的所有信息
        GameObject target =  GameObject.FindWithTag("ButtonControl") ;  
        AllData.userId = data.id;
        AllData.UserName = data.name;
        //target.GetComponent<UserControl>().OnClick();   //这句是跳转到个人信息页面的方法,但是物体丢失了,你一会找找看
    }
    public void GetFirstInfornation(FirstSelected[] datas, GameObject[] nothing, string no)//获取首页信息
    {

        if (GameObject.Find("Select recommend") == true)
        {
            GameObject gbj = GameObject.Find("Select recommend");

            foreach (FirstSelected data in datas)
            {
                gbj.transform.GetChild(data.index - 1).name = data.contentId + "";
                if (data.contentType == "apk")
                {
                    gbj.transform.GetChild(data.index - 1).GetComponent<Button>().onClick.AddListener(OpenApp);
                }
                else if (data.contentType == "video" || data.contentType == "video_vr")
                {

                    gbj.transform.GetChild(data.index - 1).GetComponent<Button>().onClick.AddListener(Enter360DegreeVideos);
                    gbj.transform.GetChild(data.index - 1).GetComponent<Button>().onClick.AddListener(gbj.transform.GetChild(data.index - 1).GetComponent<AfterEnter>().OnEnterRecommendVideo);
                }
                else if (data.contentType == "game")
                {
                    gbj.transform.GetChild(data.index - 1).GetComponent<Button>().onClick.AddListener(OpenGame);
                }
                else if (data.contentType == "web")
                {
                    gbj.transform.GetChild(data.index - 1).GetComponent<Button>().onClick.AddListener(OpenWeb);
                }

                else if (data.contentType == "broadcast")
                {
                    gbj.transform.GetChild(data.index - 1).name = data.recommendAction;
                    gbj.transform.GetChild(data.index - 1).GetComponent<Button>().onClick.AddListener(EnterLivingRoom);
                    gbj.transform.GetChild(data.index - 1).GetComponent<Button>().onClick.AddListener(gbj.transform.GetChild(data.index - 1).GetComponent<AfterEnter>().OnEnterLivingRoom);
                }
                StartCoroutine(DataClassInterface.IEGetSprite(data.thumbUrl, new DataClassInterface.OnDataGetSprite(GetSprite), gbj.transform.GetChild(data.index - 1).gameObject));

            }
        }
        else if (GameObject.Find("Select recommend(Clone)") == true)
        {

            GameObject gbj = GameObject.Find("Select recommend(Clone)");
            foreach (FirstSelected data in datas)
            {
                gbj.transform.GetChild(data.index - 1).name = data.contentId + "";
                if (data.contentType == "apk")
                {
                    gbj.transform.GetChild(data.index - 1).GetComponent<Button>().onClick.AddListener(OpenApp);
                }
                else if (data.contentType == "video" || data.contentType == "video_vr")
                {
                    gbj.transform.GetChild(data.index - 1).GetComponent<Button>().onClick.AddListener(Enter360DegreeVideos);
                    gbj.transform.GetChild(data.index - 1).GetComponent<Button>().onClick.AddListener(gbj.transform.GetChild(data.index - 1).GetComponent<AfterEnter>().OnEnterRecommendVideo);

                }
                else if (data.contentType == "game")
                {
                    gbj.transform.GetChild(data.index - 1).GetComponent<Button>().onClick.AddListener(OpenGame);
                }
                else if (data.contentType == "web")
                {
                    gbj.transform.GetChild(data.index - 1).GetComponent<Button>().onClick.AddListener(OpenWeb);
                }
                else if (data.contentType == "broadcast")
                {
                    gbj.transform.GetChild(data.index - 1).name = data.recommendAction;
                    gbj.transform.GetChild(data.index - 1).GetComponent<Button>().onClick.AddListener(EnterLivingRoom);
                    gbj.transform.GetChild(data.index - 1).GetComponent<Button>().onClick.AddListener(gbj.transform.GetChild(data.index - 1).GetComponent<AfterEnter>().OnEnterLivingRoom);
                }
                StartCoroutine(DataClassInterface.IEGetSprite(data.thumbUrl, new DataClassInterface.OnDataGetSprite(GetSprite), gbj.transform.GetChild(data.index - 1).gameObject));
            }
        }
    }

    private void GetGrandRecommend(RecommendVideo[] datas, GameObject[] nothing, string no)
    {
        GameObject grand = GameObject.Find("Grand curtain cinema recommend(Clone)");
        // GameObject vr = GameObject.Find("VR live broadcast recommend(Clone)");
        // GameObject _360 = GameObject.Find("360 degree panorama recommend(Clone)");
        if (grand != null)
        {
            int i = 0;
            foreach (RecommendVideo data in datas)
            {

                if (data.contentType == "video")
                {

                    grand.transform.GetChild(i).gameObject.name = data.contentId + "";
                    grand.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = data.sourceName;
                    grand.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = data.source;

                    StartCoroutine(DataClassInterface.IEGetSprite(data.url, new DataClassInterface.OnDataGetSprite(GetSprite), grand.transform.GetChild(i).gameObject));
                    i++;
                }

            }

        }

    }

    private void GetVR_Zhubo(RecommendZhubo[] datas, GameObject[] no, string nothing)
    {
        GameObject vr = GameObject.Find("VR live broadcast recommend(Clone)");
        if (vr != null)
        {
            int i = 0;
            foreach (RecommendZhubo data in datas)
            {

            }
        }
    }

    private void GetVRRecommend(RecommendLivng[] datas, GameObject[] no, string nothing)
    {
        GameObject vr = GameObject.Find("VR live broadcast recommend(Clone)");
        if (vr != null)
        {
            int i = 0;
            foreach (RecommendLivng data in datas)
            {

                if (data.contentType == "broadcast" && data.videoType == "video")
                {

                    if (i > 5)
                    {
                        break;
                    }

                    vr.transform.GetChild(i).gameObject.name = data.resourceId + "";
                    vr.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = data.source;
                    vr.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = data.sourceName;
                    StartCoroutine(DataClassInterface.IEGetSprite(data.cover, new DataClassInterface.OnDataGetSprite(GetSprite), vr.transform.GetChild(i).gameObject));
                    i++;
                }
                else
                {
                    Debug.Log("没有数据！ ");
                }


            }
        }

    }

    private void Get360Recommend(RecommendVideo[] datas, GameObject[] nothing, string no)
    {
        //GameObject grand = GameObject.Find("Grand curtain cinema recommend(Clone)");
        // GameObject vr = GameObject.Find("VR live broadcast recommend(Clone)");
        GameObject _360 = GameObject.Find("360 degree panorama recommend(Clone)");
        if (_360 != null)
        {
            int i = 0;
            foreach (RecommendVideo data in datas)
            {
                if (data.contentType == "video_vr")
                {

                    _360.transform.GetChild(i).gameObject.name = data.contentId + "";
                    _360.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = data.sourceName;
                    _360.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = data.source;

                    StartCoroutine(DataClassInterface.IEGetSprite(data.url, new DataClassInterface.OnDataGetSprite(GetSprite), _360.transform.GetChild(i).gameObject));
                    i++;
                }

            }
        }


    }

    private void OpenApp()
    {

        DisplayMsg("APP下载功能正在开发");
    }
    private void OpenGame()
    {
        Debug.Log("打开游戏");
        DisplayMsg("游戏下载功能正在开发");
    }
    private void OpenWeb()
    {
        Debug.Log("打开web");
        DisplayMsg("WEB功能正在开发");
    }

    public void DisplayMsg(string msg)
    {
        if(TipsRoot==null)
        {
            TipsRoot = GameObject.Find("TipsRoot").transform;
        }
        GameObject TempMsgPanel = null;
        for (int i = 0; i < TipsRoot.childCount; i++)
        {
            if (TipsRoot.GetChild(i).CompareTag("TipPanel") && !TipsRoot.GetChild(i).gameObject.activeSelf)
            {
                TempMsgPanel = TipsRoot.GetChild(i).gameObject;
                break;
            }
        }

        if (TempMsgPanel == null)
        {
            TempMsgPanel = Instantiate(MsgPanel, TipsRoot, false);
        }
        Text text = TempMsgPanel.transform.Find("Tip").GetComponent<Text>();
        text.text = msg;
        if (!TempMsgPanel.activeSelf)
            TempMsgPanel.SetActive(true);
        TempMsgPanel.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 400;
        TempMsgPanel.transform.LookAt(TempMsgPanel.transform.position + TempMsgPanel.transform.position - Camera.main.transform.position);
        Destroy(TempMsgPanel, 2f);
    }

    private void ShowPersonInfo(UserData datas ,GameObject[] gb ,string no)
    {
        Handle_panel[37].transform.GetChild(1).GetChild(6).GetComponent<Text>().text = datas.nickName;
        Handle_panel[37].transform.GetChild(1).GetChild(7).GetChild(2).GetComponent<Text>().text = datas.playNum+"";
        Handle_panel[37].transform.GetChild(1).GetChild(8).GetChild(1).GetComponent<Text>().text = datas.likeNum+"";
        Handle_panel[37].transform.GetChild(1).GetChild(9).GetChild(1).GetComponent<Text>().text = datas.worksNum+"";
        Handle_panel[37].transform.GetChild(1).GetChild(10).GetChild(1).GetComponent<Text>().text = datas.attentionNum+"";
        Handle_panel[37].transform.GetChild(1).GetChild(11).GetChild(1).GetComponent<Text>().text = datas.collectNum+"";
        Handle_panel[37].transform.GetChild(1).GetChild(12).GetChild(1).GetComponent<Text>().text = datas.myAttentionNum+"";
        Handle_panel[37].transform.GetChild(1).GetChild(13).GetComponent<Text>().text = datas.signature+"";
        StartCoroutine(DataClassInterface.IEGetSprite(datas.headImage, new DataClassInterface.OnDataGetSprite(GetSprite), Handle_panel[37].transform.GetChild(1).GetChild(3).gameObject));



    }

    public void ShowOtherPeople()
    {
        InOtherPerson = true;
    }

    private void GetMaxId(LivingRoomData[]datas,GameObject[]gbj,string str)
    {
        
        foreach(LivingRoomData data in datas)
        {
            int max = 0;
           
            if(Int32.Parse(data.score)>=max)
            {
                max = Int32.Parse(data.score);
                MaxId = data.id;
            
            }
        }
        
         if(MaxId == 0)//等于0说明没有直播了直接跳转到巨幕影院.
         {
       
             living_room.SetActive(false);
             _360_living_room.SetActive(true);       
             StartCoroutine(DataClassInterface.IEGetDate<VideoData[]>(AllData.DataString + "/vr/getVideoList?pageId=1" , new DataClassInterface.OnDataGet<VideoData[]>(GotoVideo), null));
             
         }
         else
        living_room.GetComponentInChildren<MsgManager>().CurrentId = MaxId;
    }

    void GotoVideo(VideoData[] datas ,GameObject[] room,string str)
    {
        foreach(VideoData data in datas)
        {
            _360_living_room.transform.GetComponentInChildren<VideoManager>().Id = data.workId;
            break;
        }
    }

    public void ButtonTest()
    {
        Debug.Log("点击了");
    }
}





