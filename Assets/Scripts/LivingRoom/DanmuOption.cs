using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanmuOption : MonoBehaviour
{
    public int id;
    public string message;
    private DanmuControl DanmuSpawn;
    private ControlPanelManager panelManager;
    private void Start()
    {
        DanmuSpawn = GameObject.FindGameObjectWithTag("Player").transform.Find("DanmuCanvas").GetComponent<DanmuControl>();
        panelManager=GameObject.Find("Manager").GetComponent<ControlPanelManager>();
    }

    public void Send()
    {
        panelManager.CurrentState=false;
        DanmuSpawn.Send(message);
    }


}
