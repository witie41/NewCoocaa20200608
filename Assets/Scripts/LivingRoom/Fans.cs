using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fans : MonoBehaviour
{
    public int ID;
    public string Name;
    public Sprite Photo;

    private void Init(int id,string name,string photourl)
    {
        this.ID = id;
        this.name = name;
        StartCoroutine(IEGetPhoto(photourl));
        GetComponentInChildren<Text>().text = name;

    }

    private IEnumerator IEGetPhoto(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        if(www.isDone)
        {
            this.Photo= Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.one * 0.5f);
            GetComponentInChildren<Image>().sprite = this.Photo;
        }
        yield break;
    }
}
