
using UnityEngine;
using UnityEngine.UI;
public enum VideoType
{
    Video2D,
    Video3D,
    Video180,
    Video360,
    Live_On,
    Live_Off
}
public class RoomButtonControl : MonoBehaviour
{
    public Color OnLive;
    public Color OffLive;
    public Color V2D;
    public Color V3D;
    public Color V180;
    public Color V360;
    public Image Photo;
    public VideoType VType;
    public Transform LeftTag;
    public Transform RightTag;
    public Text VName;
    public string vname;
    public int ID;
    public string photo;
    public void Init(VideoType VType, int id, string vName, string photo)
    {

       // Controller controller = new Controller();
        //LeftTag.gameObject.SetActive(false);
        //RightTag.gameObject.SetActive(false);
        vname = vName;
        ID = id;
        this.VType = VType;
        //StartCoroutine(DataClassInterface.IEGetSprite(photo, (Sprite sprite, GameObject gtb, string nothing) => { Photo.sprite = sprite; }, null));
        //直播间
        /*if (VType == VideoType.Live_Off || VType == VideoType.Live_On)
        {
          
            LeftTag.gameObject.SetActive(true);
            GetComponent<Button>().onClick.AddListener(() =>
            {
                GameObject.Find("EventController").GetComponent<Controller>().EnterLivingRoom();
               controller.living_room .GetComponentInChildren<MsgManager>().CurrentId = ID;
            });
            if (VType == VideoType.Live_On)
            {
                LeftTag.GetComponentInChildren<Text>().text = "直播中";
                LeftTag.GetComponent<Image>().color = OnLive;
            }
            if (VType == VideoType.Live_Off)
            {
                LeftTag.GetComponentInChildren<Text>().text = "未开播";
                LeftTag.GetComponent<Image>().color = OffLive;
            }
        }
        //视频
        else
        {
            RightTag.gameObject.SetActive(true);
            GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("点击了");
                GameObject.Find("EventController").GetComponent<Controller>().Enter360DegreeVideos();
                controller._360_living_room.GetComponentInChildren<VideoManager>().Id = ID;
            });
            switch (VType)
            {
                case VideoType.Video2D:
                RightTag.GetComponentInChildren<Text>().text = "2D";
                RightTag.GetComponent<Image>().color = V2D;
                    break;
                case VideoType.Video3D:
                RightTag.GetComponentInChildren<Text>().text = "3D";
                RightTag.GetComponent<Image>().color = V3D;
                    break;
                case VideoType.Video180:
                RightTag.GetComponentInChildren<Text>().text = "180";
                RightTag.GetComponent<Image>().color = V180;
                    break;
                case VideoType.Video360:
                RightTag.GetComponentInChildren<Text>().text = "360";
                RightTag.GetComponent<Image>().color = V360;
                    break;
                default:
                Debug.LogError("VType有错误");
                    break;

            }
        }*/
    }

    public RoomButtonControl(VideoType VType, int id, string vName)
    {
        vname = vName;
        ID = id;
        this.VType = VType;
    }

    public void Setphoto(string photo)
    {
        this.photo = photo;
    }
}
