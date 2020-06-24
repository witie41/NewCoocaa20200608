
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class GiftsControl : MonoBehaviour
{
    public GameObject GiftButton;
    private RectTransform rectTransform;
    private bool FinishSlide = true;
    private float speed;

    private GiftButton currentButton;
    public GiftButton CurrentButton
    {
        get
        {
            return currentButton;
        }
        set
        {
            if (currentButton != null)
                currentButton.BG.SetActive(false);
            currentButton = value;
            currentButton.BG.SetActive(true);
        }
    }

    private MsgManager msgManager;

    // Start is called before the first frame update
    void Start()
    {
        msgManager = GameObject.FindGameObjectWithTag("LivingRoomManager").GetComponent<MsgManager>();
        rectTransform = GetComponent<RectTransform>();
        DataClassInterface.OnDataGet<GiftData[]> a = ResetGifts;
        StartCoroutine(DataClassInterface.IEGetDate<GiftData[]>(AllData.DataString + "/vr/getGiftSettingList?pageId=1&pageSize=10&giftType=1", a, null));
    }

    private void ResetGifts(GiftData[] giftData, GameObject[] gos, string nos)
    {
        foreach (GiftData giftdata in giftData)
        {
            GameObject TempGift = Instantiate(GiftButton, transform);
            TempGift.GetComponent<GiftButton>().id = giftdata.id;
            TempGift.GetComponent<GiftButton>().price = giftdata.coocaaCoin;
            TempGift.transform.Find("Name").GetComponent<Text>().text = giftdata.name;
            TempGift.transform.Find("Price").GetComponent<Text>().text = giftdata.coocaaCoin + "";
            StartCoroutine(DataClassInterface.IEGetSprite(giftdata.imgPath, (Sprite sprite, GameObject go, string str) => { go.GetComponent<Image>().sprite = sprite; }, TempGift));
        }
    }

    public void SendGift()
    {
        if (CurrentButton == null)
            msgManager.MsgPanelDisplay("请选择赠送的礼物!");
        else
            CurrentButton.SendGift();
    }

    public void ReCharge()
    {
        msgManager.MsgPanelDisplay("功能暂未开放，您可登录酷开VR官网-酷币钱包 进行充值！官网地址：vr.coocaa.com");
    }

    public void MoveContent(float value)
    {
        if (FinishSlide)
            StartCoroutine(IEMoveContentNect(value));
    }

    IEnumerator IEMoveContentNect(float value)
    {
        FinishSlide = false;
        float TargetValue = rectTransform.anchoredPosition.x + value;
        int time = 150;
        while (time >= 0)
        {
            rectTransform.anchoredPosition = new Vector2(Mathf.SmoothDamp(rectTransform.anchoredPosition.x, TargetValue, ref speed, 0.05f, 1000), rectTransform.anchoredPosition.y);
            if (Mathf.Abs(TargetValue - rectTransform.position.x) <= 1)
            {
                rectTransform.anchoredPosition = new Vector2(TargetValue, rectTransform.anchoredPosition.y);
                break;
            }
            time--;
            yield return null;
        }
        FinishSlide = true;
        yield break;
    }
}
