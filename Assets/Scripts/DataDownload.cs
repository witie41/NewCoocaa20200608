using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataDownload : MonoBehaviour
{
    private string EmojiName;
    private string Emojiurl;
    private string Emojipath;
    private FileInfo EmojiFile;
    private string EmojiDirPath;
    private string GiftDirPath;
    private string GiftName;
    private string Gifturl;
    private string Giftpath;
    private FileInfo GiftFile;

    class Image1
    {
        public string codeKey;
        public string codeImage;
    }
    //private void Start()
    //{
    //    //TEST
    //    DataClassInterface.OnDataGet<Image1> aa = (Image1 image,GameObject[] go, string nothing) => { Debug.Log(image.codeImage); };
    //    StartCoroutine(DataClassInterface.IEGetDate <Image1>(@AllData.DataString+"/coocaa/api/getImageCode", aa,null));

    //    EmojiDirPath = Application.persistentDataPath + "Resouce/gif";
    //    GiftDirPath = Application.persistentDataPath + "Resouce/gift";
    //    DataClassInterface.OnDataGet<EmojiData[]> a = CheckEmoji;
    //    StartCoroutine(DataClassInterface.IEGetDate(AllData.DataString+"/vr/getSysExpressionsList?pageId=1&pageSize=20", a, null));
    //    DataClassInterface.OnDataGet<GiftData[]> b = CheckGift;
    //    StartCoroutine(DataClassInterface.IEGetDate(AllData.DataString+"/vr/getGiftSettingList?pageId=1%pageSize=20&giftType=1", b, null));
    //}

    //表情
    public void CheckEmoji(EmojiData[] data, GameObject[] go, string nothing)
    {
        DirectoryInfo mydir = new DirectoryInfo(EmojiDirPath);
        //目录不存在则新建
        if (!mydir.Exists)
        {
            Directory.CreateDirectory(mydir.ToString());
            Debug.Log("创建目录");
        }
        foreach(EmojiData emoji in data)
        {
            EmojiName = emoji.title;
            Emojiurl = emoji.path;

            Emojipath = EmojiDirPath +"/"+ EmojiName + ".gif";
            EmojiFile = new FileInfo(Emojipath);
            if (!File.Exists(Emojipath))
            {
                StartCoroutine(IEDownload(Emojiurl,EmojiFile));
            }
        }
    }
    //下载
    IEnumerator IEDownload(string url,FileInfo file)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.isDone)
        {
            byte[] bytes = www.bytes;
            Stream stream;
            stream = file.Create();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            stream.Dispose();
        }
        Debug.Log("下载完成");
        yield break;
    }

    //礼物
    public void CheckGift(GiftData[] data, GameObject[] go, string nothing)
    {
        DirectoryInfo mydir = new DirectoryInfo(GiftDirPath);
        //目录不存在则新建
        if (!mydir.Exists)
        {
            Directory.CreateDirectory(mydir.ToString());
            Debug.Log("创建目录");
        }
        foreach (GiftData gift in data)
        {
            GiftName = gift.name;
            Gifturl = gift.imgPath;

            Giftpath = GiftDirPath + "/" + GiftName + @".jpg";
            GiftFile = new FileInfo(Giftpath);
            if (!File.Exists(Giftpath))
            {
                StartCoroutine(IEDownload(Gifturl, GiftFile));
            }
        }
    }
}
