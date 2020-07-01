using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0246 // 未能找到类型或命名空间名“LitJson”(是否缺少 using 指令或程序集引用?)
using LitJson;
#pragma warning restore CS0246 // 未能找到类型或命名空间名“LitJson”(是否缺少 using 指令或程序集引用?)
using System;

public class DataClassInterface : MonoBehaviour
{
    public delegate void OnDataGet<T>(T t, GameObject[] tbj, string orgin_data);
    public delegate void OnDataGetSprite(Sprite s, GameObject room, string data);

    /// <summary>
    /// POST请求
    /// </summary>
    /// <typeparam name="T">返回数据的类型</typeparam>
    /// <param name="url">网址（不带参数）</param>
    /// <param name="OnDataGet">后续操作的委托</param>
    /// <param name="form">自定义的参数表格</param>
    /// <param name="tbj"></param>
    /// <returns></returns>
    public static IEnumerator IEPostData<T>(string url, OnDataGet<T> OnDataGet, WWWForm form, GameObject[] tbj)
    {
        WWW www = new WWW(url, form);
        yield return www;
        if (www.error != null)
        {
            Debug.LogError("POST失败: " + www.error);
        }
        Info tempInfo = JsonMapper.ToObject<Info>(www.text);
        Debug.Log("!!!!!!!!" + www.text);
        if (OnDataGet == null)
            yield break;
        if (tempInfo.data == null)
        {
            OnDataGet(JsonMapper.ToObject<T>(www.text), null,null);
        }
        else
        {
            OnDataGet(JsonMapper.ToObject<T>(tempInfo.data), tbj, tempInfo.data);
        }
    }
    public static IEnumerator IEPostData2<T>(string url, OnDataGet<string> OnDataGet, WWWForm form, GameObject[] tbj)
    {
        WWW www = new WWW(url, form);
        yield return www;
        if (www.error != null)
        {
            Debug.LogError("POST失败: " + www.error);
        }
        Info tempInfo = JsonMapper.ToObject<Info>(www.text);
        Debug.Log("!!!!!!!!" + www.text);

      
            OnDataGet(tempInfo.msg, tbj, tempInfo.msg);

    }


    //从服务器获取Sprite的接口，用法同普通数据的接口
    public static IEnumerator IEGetSprite(string url, OnDataGetSprite OnDataSprite, GameObject tbj)
    {

        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
        {
            Sprite sprite= Resources.Load<Sprite>("Firstsprite/暂无封面");
            OnDataSprite(sprite,  tbj, "success");
            yield break;
        }
        else
        {
            OnDataSprite(Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.one * 0.5f), tbj, "success");
        }

    }

    /// <summary>
    /// 数据返回接口
    /// </summary>
    /// <typeparam name="T">接收的数据对象类型</typeparam>
    /// <param name="url">数据网址：网址后+“?参数名=参数值&参数名=参数值”，如：http://47.92.145.187:8083/vr/getVideoList?pageId=1&pageSize=10&videoType=0</param>
    /// <param name="OnDataGet">委托，需要自己写一个参数类型为T的方法来处理数据，其中参数即为该接口传回的数据对象</param>
    public static IEnumerator IEGetDate<T>(string url, OnDataGet<T> OnDataGet, GameObject[] tbj)
    {
        WWW www = new WWW(url);
        yield return www;
        yield return null;
        if (www.error != null)
        {
            Debug.LogError("数据获取失败：" + www.error);
            yield break;
        }
        else
        {
            Info t = JsonMapper.ToObject<Info>(www.text);
            if (t.msg.Equals("success"))
            {
                if (t.data.Equals(null))
                {
                    Debug.LogError("Data为空");
                }
                else
                {
                    OnDataGet(JsonMapper.ToObject<T>(t.data), tbj, t.data.ToString());
                }
            }
            else
            {
                Debug.LogError("Data读取失败,msg为：" + t.msg);
            }
            yield break;
        }
    }

    public static IEnumerator IEGetDate2<T>(string url, OnDataGet<string> OnDataGet, GameObject[] tbj)
    {


        WWW www = new WWW(url);
        yield return www;
        yield return null;
        if (www.error != null)
        {
            Debug.LogError("数据获取失败：" + www.error);
            yield break;
        }
        else
        {
            Info t = JsonMapper.ToObject<Info>(www.text);

            OnDataGet(t.msg, tbj, t.msg.ToString());
            yield break;
        }
    }

    //时间戳转化为标准时间
    public static string TimeNumToTime(long timeNum)
    {
        string time = timeNum.ToString();
        time = time.Substring(0, 10);
        timeNum = long.Parse(time);
        System.DateTime datetime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        datetime = datetime.AddSeconds(timeNum).ToLocalTime();
        return datetime.ToString(); ;
    }

    //将时间秒转化为标准时间格式
    public static string SecondsToTime(int Seconds)
    {
        int seconds = Seconds % 60;
        int minutes = Seconds/60 % 60;
        int hours = Seconds / 3600;

        string time = "";
        if (hours != 0)
            time += hours.ToString() + ":";
        if (minutes < 10)
            time += "0";
        time += minutes.ToString() + ":";
        if (seconds < 10)
            time += "0";
        time += seconds.ToString();
        return time;
    }
}
