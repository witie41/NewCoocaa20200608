using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlPanelManager : MonoBehaviour
{
    private Text VolumeValueText;
    private float activeTime = 0;
    private GameObject DanmuCanvas;
    private Transform player;
    private ChangeModel changeModel;
    private bool DanmuIsON = false;

    [Header("控制面板")]
    public GameObject ControlPanel;

    [Header("面板")]
    public GameObject Panels;

    [Header("弹幕状态")]
    public Text DanmuState;

    [Header("切换播放模式按钮")]
    public GameObject ChangePlayModelButton;

    [Header("面板控制器")]
    public PanelsControl panelsControl;

    //当前面板状态
    private bool currentState = true;
    public bool CurrentState
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
        DanmuCanvas = GameObject.FindGameObjectWithTag("DanmuCanvas").gameObject;
        player = Camera.main.transform;

        DanmuSwitch();
    }

    void Update()
    {
        //点击屏幕唤醒/隐藏菜单
        if (GvrControllerInput.ClickButtonDown || Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
                CurrentState = !CurrentState;
        }
        if (currentState&&panelsControl.CurrentPanel==null)
            activeTime += Time.deltaTime;
        else
            activeTime = 0;
        if (activeTime >= 15)
        {
            CurrentState=false;
        }
    }

    //改变面板状态
    private void ChangePanelActive(bool value)
    {
        //面板隐藏
        if (!value)
        {
            //ControlPanel.transform.localScale = Vector3.zero;
            Panels.transform.localScale = Vector3.zero;
            Panels.GetComponentInChildren<PanelsControl>().CurrentPanel = null;
            //ChangePlayModelButton.transform.localScale = Vector3.zero;
        }

        else
        {
            //ControlPanel.transform.localScale = Vector3.one;
            Panels.transform.parent.position = player.position + player.forward * 30;
            Panels.GetComponentInChildren<PanelsControl>().CurrentPanel = null;
            // Debug.Log(player.position+""+ Panels.transform.position);
            Panels.transform.parent.LookAt(Panels.transform.position + Panels.transform.position - player.position);
            Panels.transform.localScale = Vector3.one;
            //ChangePlayModelButton.transform.localScale = Vector3.one;
        }
    }

    //按钮切换弹幕开关图标
    public void DanmuSwitch()
    {
        DanmuIsON = !DanmuIsON;

        DanmuState.text = DanmuIsON ? "关闭弹幕" : "开启弹幕";

        //清除旧弹幕
        DanmuCanvas.transform.localScale = DanmuIsON ? Vector3.one * 0.04f : Vector3.zero;
    }

    //退出时清除弹幕
    private void OnDisable()
    {
        DanmuIsON = false;

        DanmuState.text = DanmuIsON ? "关闭弹幕" : "开启弹幕";

        //清除旧弹幕
        DanmuCanvas.transform.localScale = DanmuIsON ? Vector3.one * 0.04f : Vector3.zero;

        foreach (Transform danmu in DanmuCanvas.transform)
        {
            danmu.gameObject.SetActive(false);
        }
        Debug.Log(DanmuCanvas);
    }

}
