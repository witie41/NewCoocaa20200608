using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Gvr;


public class NormalPanelControl: MonoBehaviour
{
    private float activeTime = 0;

    [Header("控制面板")]
    public GameObject ControlPanel;

    public PanelsControl panelsControl;

    private Transform player;
    
    //当前面板状态
    private bool currentState = true;
    private bool CurrentState
    {
       get
        {
            return currentState;
        }
        set
        {
            if (value != currentState)
            {
                ChangePanelActive(value);
            }
            currentState = value;
        }
    }

    private void Start()
    {
        player = Camera.main.transform;
        activeTime=0;
    }

    void Update()
    {
        //点击屏幕唤醒/隐藏菜单
        if( GvrControllerInput.ClickButtonDown||Input.GetMouseButtonUp(0))
        {
            if(EventSystem.current.IsPointerOverGameObject() == false)
             CurrentState = !CurrentState;
        }
        if (currentState&&panelsControl.CurrentPanel==null)
            activeTime+=Time.deltaTime;
        else
            activeTime=0;
        if(activeTime>=15)
        {
            CurrentState=false;
        }
    }

    //改变面板状态
    private void ChangePanelActive(bool value)
    {
        //面板隐藏
        if(!value)
        {
            //ControlPanel.transform.localScale = Vector3.zero;
            ControlPanel.transform.localScale = Vector3.zero;
            ControlPanel.GetComponentInChildren<PanelsControl>().CurrentPanel = null;
            //ChangePlayModelButton.transform.localScale = Vector3.zero;
        }

        else
        {
            //ControlPanel.transform.localScale = Vector3.one;
            ControlPanel.transform.position = player.position + player.forward * 30 ;
            ControlPanel.GetComponentInChildren<PanelsControl>().CurrentPanel = null;
           // Debug.Log(player.position+""+ Panels.transform.position);
            ControlPanel.transform.LookAt(ControlPanel.transform.position+ ControlPanel.transform.position-player.position);
            ControlPanel.transform.localScale = Vector3.one;
            //ChangePlayModelButton.transform.localScale = Vector3.one;
        }
    }

}
