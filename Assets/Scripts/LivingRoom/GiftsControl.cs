using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GiftsControl : MonoBehaviour
{
    public GameObject GiftButton;

    // Start is called before the first frame update
    void Start()
    {
        DataClassInterface.OnDataGet<GiftData[]> a = ResetGifts;
        StartCoroutine(DataClassInterface.IEGetDate<GiftData[]>(AllData.DataString+"/vr/getGiftSettingList?pageId=1&pageSize=20&giftType=1", a, null));
    }

    private void ResetGifts(GiftData[] giftData, GameObject[] gos, string nos)
    {
        foreach (GiftData giftdata in giftData)
        {
            GameObject TempGift= Instantiate(GiftButton, transform);
            TempGift.GetComponent<GiftButton>().id = giftdata.id;
            TempGift.GetComponentInChildren<Text>().text = giftdata.name;
            StartCoroutine(DataClassInterface.IEGetSprite(giftdata.imgPath, (Sprite sprite, GameObject go, string str) => { go.GetComponent<Image>().sprite = sprite; }, TempGift));
        }
    }

    private IEnumerator IECreateGiftImage(string GiftPath,Image image)
    {
        WWW www = new WWW(@"file:\\"+GiftPath);
        Debug.Log(@"file:\\" + GiftPath);
        yield return www;
        if (www.isDone)
        {
            image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.one*0.5f);
            yield break;
        }
    }
}
