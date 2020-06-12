using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Panel : MonoBehaviour
{
    public Transform Patner;
    private float Speed=0.05f;

    private PanelsControl panelsControl;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        panelsControl=GetComponentInParent<PanelsControl>();
    }

    public void OnClick()
    {
        if(panelsControl.CurrentPanel==Patner)
        {
            panelsControl.CurrentPanel=null;
            return;
        }
        panelsControl.CurrentPanel = Patner;
    }


}
