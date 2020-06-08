using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gvr;


public class NormalPanelControl: MonoBehaviour
{
    private Text VolumeValueText;
    private float acitiveTime = 0;

    [Header("控制面板")]
    public GameObject ControlPanel;
    [Header("左悬浮栏")]
    public GameObject LeftCanvas;
    [Header("右悬浮栏")]
    public GameObject RightCanvas;
    [Header("模式切换")]
    public GameObject ChangeModel;

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

    void Update()
    {
        //active时间累加
        if (CurrentState)
        {
            acitiveTime += Time.deltaTime;
        }

        //点击时时间清零
        if (GvrControllerInput.ClickButton || Input.GetMouseButton(0))
        {
            acitiveTime = 0;
            if (!CurrentState)
            {
                CurrentState = true;
            }
        }

        //累计时间大于3秒自动隐藏
        if (acitiveTime >= 3 && CurrentState)
        {
            CurrentState = false;
        }
    }

    //改变面板状态
    private void ChangePanelActive(bool value)
    {
        //面板隐藏
        if(!value)
        {
            if (ControlPanel)
                ControlPanel.transform.localScale = Vector3.zero;
            if (LeftCanvas)
                LeftCanvas.transform.localScale = Vector3.zero;
            if (RightCanvas)
                RightCanvas.transform.localScale = Vector3.zero;
            if (ChangeModel)
                ChangeModel.transform.localScale = Vector3.zero;
        }
        else
        {
            if (ControlPanel)
                ControlPanel.transform.localScale = Vector3.one;
            if (LeftCanvas)
                LeftCanvas.transform.localScale = Vector3.one;
            if (RightCanvas)
                RightCanvas.transform.localScale = Vector3.one;
            if (ChangeModel)
                ChangeModel.transform.localScale = Vector3.one;
        }
    }

}
