using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



/// <summary>
/// @author 郭夏同
/// 控制直播间的显示房间显示，包括主页和热门视频
/// 
/// </summary>
public class LivingRoom : UIBehaviour, IPointerDownHandler, IPointerUpHandler
{

    // Start is called before the first frame update
    public int livingRoomCount = 6;//直播房间总数
    public MyLivingRoom[] m_livingRooms;//方便数据观察而增加的类
    public Font theFont;//字体
    private Font m_Font { get { return theFont; } set { theFont = value; } }
    private int livingRoom { get { return livingRoomCount; } set { livingRoomCount = value; } }
    [SerializeField]
    private RectTransform m_content;//显示内容框
    public RectTransform Content { get { return m_content; } set { m_content = value; } }
    [SerializeField]
    private GameObject Item;//挂载物体
    [SerializeField]
    private GridLayoutGroup m_GirdLayoutGroup;//布局
    [SerializeField]
    RectTransform[] m_rectTransform;//获取子物体的
    [SerializeField]
    Image [] m_image;//图片集
    [SerializeField]
    Sprite[] hot_sprites;//热门贴图集
    [SerializeField]
    Sprite[] first_sprites;//首页的贴图
    [SerializeField]
    GameObject[] m_Text;//文本显示区域
    [SerializeField]
    Sprite[] hot_people_sprite;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(gameObject.name);
       
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        
    }

    protected  override void Awake()
    {

        
        
        base.Awake();
        m_image= new Image[livingRoom];
        first_sprites = Resources.LoadAll<Sprite>("Firstsprite");//读取当前所有图片
        hot_sprites = Resources.LoadAll<Sprite>("Hotsprite");//读取当然热门文件夹中所有图片
        hot_people_sprite = Resources.LoadAll<Sprite>("HotPeopletSprite");
        if (this.name == "首页")
        {
          
            m_Text = new GameObject[livingRoom];//字体
            Item = GameObject.FindGameObjectWithTag("Content2");//显示区
            m_GirdLayoutGroup = GameObject.FindGameObjectWithTag("Content2").GetComponent<GridLayoutGroup>();
            GameObject[] theLivingRoom = new GameObject[livingRoom];//实例化的房间
            m_rectTransform = new RectTransform[livingRoom];//矩形组件
            m_livingRooms = new MyLivingRoom[livingRoom];//抽象的房间

            for (int i = 0; i < livingRoom; i++)
            {

         
                m_Text[i] = new GameObject();//文本
                theLivingRoom[i] = new GameObject();
                theLivingRoom[i].AddComponent<MyLivingRoom>();
                m_Text[i].transform.parent = theLivingRoom[i].transform;
                theLivingRoom[i].transform.parent = Item.transform;

                m_Text[i].AddComponent<Text>();
                m_Text[i].GetComponent<Text>().text = ""+i;
                m_Text[i].GetComponent<Text>().font = m_Font;
                m_Text[i].GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -70, 0);
                m_Text[i].GetComponent<RectTransform>().sizeDelta = new Vector2(295, 20);
                m_Text[i].name = "Text" + i;
                m_Text[i].transform.parent = theLivingRoom[i].transform;
                theLivingRoom[i].AddComponent<Image>();
                theLivingRoom[i].AddComponent<Button>();
                m_rectTransform[i] = theLivingRoom[i].GetComponent<RectTransform>();
                m_rectTransform[i].anchoredPosition3D = new Vector3(m_content.anchoredPosition3D.x, m_content.anchoredPosition3D.y, 0);
                m_rectTransform[i].localScale = new Vector3(1, 1, 1);
                //m_livingRooms[i] = new MyLivingRoom();
            //    theLivingRoom[i].GetComponent<Image>().sprite = first_sprites[i];
                //theLivingRoom[i].GetComponent<Button>().onClick.AddListener(delegate () { this.OnClick(); });
                //m_livingRooms[i].livingRoomName = "测试专用名称";
                //m_livingRooms[i].theRoomHot = 1000;
                //m_Text[i].GetComponent<Text>().text = m_livingRooms[i].livingRoomName + "\t\t\t" + "热度:" + m_livingRooms[i].theRoomHot;


            }


        }
        if(this.name == "热门")
        {
            this.transform.rotation = Quaternion.Euler(0, -60, 0);
            Item = GameObject.FindGameObjectWithTag("Content");
            m_GirdLayoutGroup = GameObject.FindGameObjectWithTag("Content").GetComponent<GridLayoutGroup>();
            GameObject[] theLivingRoom = new GameObject[livingRoom];
            m_rectTransform = new RectTransform[livingRoom];
            m_Text = new GameObject[livingRoom];//字体
            m_livingRooms = new MyLivingRoom[livingRoom];//抽象的房间
            for (int i = 0; i < livingRoom; i++)
            {
                theLivingRoom[i] = new GameObject();
                theLivingRoom[i].transform.parent = Item.transform;
                theLivingRoom[i].AddComponent<Image>();
                theLivingRoom[i].AddComponent<Button>();

                m_Text[i] = new GameObject();//文本
                m_Text[i].transform.parent = theLivingRoom[i].transform;
                m_Text[i].AddComponent<Text>();
                m_Text[i].GetComponent<Text>().text = "" + i;
                m_Text[i].GetComponent<Text>().font = m_Font;
                m_Text[i].GetComponent<Text>().fontSize = 10;
                m_Text[i].GetComponent<Text>().color = Color.black;
                m_Text[i].GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -65, 0);
                m_Text[i].GetComponent<RectTransform>().sizeDelta = new Vector2(295, 20);


               // m_livingRooms[i] = new MyLivingRoom();
               // m_livingRooms[i].livingRoomName = "测试专用名称";
               // m_livingRooms[i].theRoomNumber = i;//房间号
               // m_livingRooms[i].theRoomHot = 10000;//房间人数
               // m_Text[i].GetComponent<Text>().text = m_livingRooms[i].livingRoomName + "\t\t\t\t\t\t\t\t\t\t\t\t" + "热度:" + m_livingRooms[i].theRoomHot;


                m_rectTransform[i] = theLivingRoom[i].GetComponent<RectTransform>();
                m_image[i] = theLivingRoom[i].GetComponent<Image>();
                m_rectTransform[i].anchoredPosition3D = new Vector3(m_content.anchoredPosition3D.x, m_content.anchoredPosition3D.y, 0);
                m_rectTransform[i].localScale = new Vector3(1, 1, 1);
                theLivingRoom[i].GetComponent<Image>().sprite = hot_sprites[i];
                theLivingRoom[i].transform.rotation = Quaternion.Euler(0,-60,0);
                theLivingRoom[i].GetComponent<Button>().onClick.AddListener(delegate () { this.OnClick(); });
            
            }




        }

        if (name == "热门主播")
        {
            Item = GameObject.FindGameObjectWithTag("Content3");//显示区
            m_GirdLayoutGroup = GameObject.FindGameObjectWithTag("Content3").GetComponent<GridLayoutGroup>();
            GameObject[] theLivingRoom = new GameObject[livingRoom];//实例化的房间
            m_rectTransform = new RectTransform[livingRoom];//矩形组件
            m_livingRooms = new MyLivingRoom[livingRoom];//抽象的房间


            for(int i = 0;i<livingRoom;i++)
            {
                theLivingRoom[i] = new GameObject();
                theLivingRoom[i].transform.parent = Item.transform;
                theLivingRoom[i].AddComponent<Image>();
                theLivingRoom[i].AddComponent<Button>();


                m_rectTransform[i] = theLivingRoom[i].GetComponent<RectTransform>();
                m_rectTransform[i].localScale = new Vector3(1, 1, 1);
                m_GirdLayoutGroup.cellSize = new Vector2(400, 50);
                m_GirdLayoutGroup.spacing = new Vector2(10, 10);

                theLivingRoom[i].transform.rotation = Quaternion.Euler(0, 60, 0);
                theLivingRoom[i].GetComponent<Button>().onClick.AddListener(delegate () { this.OnClick(); });

                theLivingRoom[i].GetComponent<Image>().sprite = hot_people_sprite[i];

            }
        }
    }
    public void OnClick()//对接函数
    {
        Debug.Log("进入房间");
    }

    public void Refash(RectTransform m_content)
    
    {
         
    }
    
    
    // Update is called once per frame

}
