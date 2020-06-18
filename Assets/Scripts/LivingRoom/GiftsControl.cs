
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class GiftsControl : MonoBehaviour
{
    public GameObject GiftButton;
    private RectTransform rectTransform;
    private bool FinishSlide = true;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
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
            TempGift.transform.Find("Name").GetComponent<Text>().text = giftdata.name;
            TempGift.transform.Find("Price").GetComponent<Text>().text = giftdata.coocaaCoin+"";
            StartCoroutine(DataClassInterface.IEGetSprite(giftdata.imgPath, (Sprite sprite, GameObject go, string str) => { go.GetComponent<Image>().sprite = sprite; }, TempGift));
        }
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
        int time=150;
        while (time>=0)
        {   
            rectTransform.anchoredPosition=new Vector2(Mathf.SmoothDamp(rectTransform.anchoredPosition.x, TargetValue, ref speed,0.05f,1000), rectTransform.anchoredPosition.y);
            if (Mathf.Abs(TargetValue - rectTransform.position.x) <= 1)
            {
                rectTransform.anchoredPosition=new Vector2(TargetValue, rectTransform.anchoredPosition.y);
                break;
            }
            time--;
            yield return null;
        }
        FinishSlide = true;
        yield break;
    }
}
