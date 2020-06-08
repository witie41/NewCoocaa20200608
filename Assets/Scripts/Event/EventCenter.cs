using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter
{
    private static Dictionary<EventEnum, Delegate> eventDictionary=new Dictionary<EventEnum, Delegate>();

    /// <summary>
    /// 添加监听
    /// </summary>
    private static bool OnAddListener(EventEnum eventEnum,Delegate callback)
    {
        //检测参数是否为空
        if(callback==null)
        {
            Debug.LogErrorFormat("添加监听失败：参数callback为空，不允许添加空键值");
            return false;
        }
        //字典中没有eventEnum就添加进去
        if (!eventDictionary.ContainsKey(eventEnum))
        {
            eventDictionary.Add(eventEnum, callback);
            Debug.Log("添加监听" + callback.Method.ToString() + "成功");
            return false;
        }
        //判断类型是否一致
        if (!eventDictionary[eventEnum].GetType().Equals(callback.GetType()))
        {
            Debug.LogErrorFormat("添加监听失败：事件对应的回调类型和已有方法类型不匹配");
            return false;
        }
        return true;
    }
    //无参数
    public static void AddListener(EventEnum eventEnum, Callback callback)
    {
        if(OnAddListener(eventEnum, callback))
        {
            eventDictionary[eventEnum] = (Callback)eventDictionary[eventEnum] + callback;
            Debug.Log("添加监听" + callback.Method.ToString() + "成功");
        }
    }
    //一个参数
    public static void AddListener<T>(EventEnum eventEnum, Callback<T> callback)
    {
        if (OnAddListener(eventEnum, callback))
        {
            eventDictionary[eventEnum] = (Callback<T>)eventDictionary[eventEnum] + callback;
            Debug.Log("添加监听" + callback.Method.ToString() + "成功");
        }
    }
    //两个参数
    public static void AddListener<T, W>(EventEnum eventEnum, Callback<T, W> callback)
    {
        if (OnAddListener(eventEnum, callback))
        {
            eventDictionary[eventEnum] = (Callback<T, W>)eventDictionary[eventEnum] + callback;
            Debug.Log("添加监听" + callback.Method.ToString() + "成功");
        }
    }
    //三个参数
    public static void AddListener<T, W,X>(EventEnum eventEnum, Callback<T, W,X> callback)
    {
        if (OnAddListener(eventEnum, callback))
        {
            eventDictionary[eventEnum] = (Callback<T, W,X>)eventDictionary[eventEnum] + callback;
            Debug.Log("添加监听" + callback.Method.ToString() + "成功");
        }
    }
    //四个参数
    public static void AddListener<T, W, X,Z>(EventEnum eventEnum, Callback<T, W, X,Z> callback)
    {
        if (OnAddListener(eventEnum, callback))
        {
            eventDictionary[eventEnum] = (Callback<T, W, X,Z>)eventDictionary[eventEnum] + callback;
            Debug.Log("添加监听" + callback.Method.ToString() + "成功");
        }
    }



    /// <summary>
    /// 移除监听
    /// </summary>
    private static bool OnRemoveListener(EventEnum eventEnum, Delegate callback)
    {
        if (!eventDictionary.ContainsKey(eventEnum))
        {
            Debug.LogErrorFormat("移除监听失败：字典中不包含对应key");
            return false;
        }
        if (!eventDictionary[eventEnum].GetType().Equals(callback.GetType()))
        {
            Debug.LogErrorFormat("移除监听失败：事件对应的回调类型和方法类型不匹配");
            return false;
        }
        for (int i = eventDictionary[eventEnum].GetInvocationList().Length - 1; i >= 0; i--)
        {
            //找到对应委托
            if (eventDictionary[eventEnum].GetInvocationList()[i].Method == callback.Method)
            {
                return true;
            }
        }
        //没找到对应委托
        Debug.LogErrorFormat("移除监听失败：未找到对应委托");
        return false;
    }
    //无参数
    public static void RemoveListener(EventEnum eventEnum,Callback callback)
    {
        if (OnRemoveListener(eventEnum, callback))
        {
            eventDictionary[eventEnum] = (Callback)eventDictionary[eventEnum] - callback;
            Debug.Log("移除监听" + callback.Method.ToString() + "成功");
            //判断该键值对应的委托是否为空，是则在字典中删除该键值
            if (eventDictionary[eventEnum] == null)
            {
                eventDictionary.Remove(eventEnum);
            }
        }
    }
    //一个参数
    public static void RemoveListener<T>(EventEnum eventEnum, Callback<T> callback)
    {
        if (OnRemoveListener(eventEnum, callback))
        {
            eventDictionary[eventEnum] = (Callback<T>)eventDictionary[eventEnum] - callback;
            Debug.Log("移除监听" + callback.Method.ToString() + "成功");
            //判断该键值对应的委托是否为空，是则在字典中删除该键值
            if (eventDictionary[eventEnum] == null)
            {
                eventDictionary.Remove(eventEnum);
            }
        }
    }
    //两个参数
    public static void RemoveListener<T,W>(EventEnum eventEnum, Callback<T,W> callback)
    {
        if (OnRemoveListener(eventEnum, callback))
        {
            eventDictionary[eventEnum] = (Callback<T,W>)eventDictionary[eventEnum] - callback;
            Debug.Log("移除监听" + callback.Method.ToString() + "成功");
            //判断该键值对应的委托是否为空，是则在字典中删除该键值
            if (eventDictionary[eventEnum] == null)
            {
                eventDictionary.Remove(eventEnum);
            }
        }
    }
    //三个参数
    public static void RemoveListener<T,W,X>(EventEnum eventEnum, Callback<T,W,X> callback)
    {
        if (OnRemoveListener(eventEnum, callback))
        {
            eventDictionary[eventEnum] = (Callback<T,W,X>)eventDictionary[eventEnum] - callback;
            Debug.Log("移除监听" + callback.Method.ToString() + "成功");
            //判断该键值对应的委托是否为空，是则在字典中删除该键值
            if (eventDictionary[eventEnum] == null)
            {
                eventDictionary.Remove(eventEnum);
            }
        }
    }
    //四个参数
    public static void RemoveListener<T,W,X,Z>(EventEnum eventEnum, Callback<T,W,X,Z> callback)
    {
        if (OnRemoveListener(eventEnum, callback))
        {
            eventDictionary[eventEnum] = (Callback<T,W,X,Z>)eventDictionary[eventEnum] - callback;
            Debug.Log("移除监听" + callback.Method.ToString() + "成功");
            //判断该键值对应的委托是否为空，是则在字典中删除该键值
            if (eventDictionary[eventEnum] == null)
            {
                eventDictionary.Remove(eventEnum);
            }
        }
    }



    /// <summary>
    /// 广播监听
    /// </summary>
    private static bool OnBroadcast(EventEnum eventEnum)
    {
        if (!eventDictionary.ContainsKey(eventEnum))
        {
            Debug.LogErrorFormat("广播监听失败：无该键值");
            return false;
        }
        if (eventDictionary[eventEnum]==null)
        {
            Debug.LogErrorFormat("广播监听失败：无对应监听");
            return false;
        }
        return true;
    }
    //无参数
    public static void Broadcast(EventEnum eventEnum)
    {
        if(OnBroadcast(eventEnum))
        {
            //调用监听
            Callback callback = eventDictionary[eventEnum] as Callback;
            callback();
        }
    }
    //一个参数
    public static void Broadcast<T>(EventEnum eventEnum,T arg1)
    {
        if (OnBroadcast(eventEnum))
        {
            //调用监听
            Callback<T> callback = eventDictionary[eventEnum] as Callback<T>;
            callback(arg1);
        }
    }
    //两个参数
    public static void Broadcast<T,W>(EventEnum eventEnum, T arg1, W arg2)
    {
        if (OnBroadcast(eventEnum))
        {
            //调用监听
            Callback<T,W> callback = eventDictionary[eventEnum] as Callback<T,W>;
            callback(arg1,arg2);
        }
    }
    //三个参数
    public static void Broadcast<T, W,X>(EventEnum eventEnum, T arg1, W arg2,X arg3)
    {
        if (OnBroadcast(eventEnum))
        {
            //调用监听
            Callback<T, W,X> callback = eventDictionary[eventEnum] as Callback<T, W,X>;
            callback(arg1, arg2,arg3);
        }
    }
    //四个参数
    public static void Broadcast<T, W,X,Z>(EventEnum eventEnum, T arg1, W arg2, X arg3, Z arg4)
    {
        if (OnBroadcast(eventEnum))
        {
            //调用监听
            Callback<T, W,X ,Z> callback = eventDictionary[eventEnum] as Callback<T, W,X,Z>;
            callback(arg1, arg2,arg3,arg4);
        }
    }
}
