using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanmuOption : MonoBehaviour
{
    public int id;
    public string message;
    private Transform DanmuSpawn;
    private void Start()
    {
        DanmuSpawn = GameObject.FindGameObjectWithTag("Player").transform.Find("DanmuCanvas");
    }

    public void Send()
    {
        Debug.Log("Send");
        DanmuSpawn.GetComponent<DanmuControl>().Send(message);
    }


}
