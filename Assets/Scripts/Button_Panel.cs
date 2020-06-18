using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Panel : MonoBehaviour
{
    public Transform Patner;
    private Text Tag;
    private float Speed=0.05f;
    private bool IsBigger=true;

    private PanelsControl panelsControl;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        panelsControl=GetComponentInParent<PanelsControl>();
        Tag=transform.parent.GetComponentInChildren<Text>();
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

    public void OnEnter()
    {
        IsBigger=true;
        StartCoroutine(IEBigger(Tag.transform));
    }

    
    public void OnExit()
    {
        IsBigger=false;
        StartCoroutine(IESmaller(Tag.transform));
    }

    IEnumerator IEBigger(Transform target)
    {
        while(target.localScale.x<=1.1f&&IsBigger)
        {
            target.localScale += Vector3.one * 0.05f;
            yield return null;
        }
        
    }

    IEnumerator IESmaller(Transform target)
    {
        while (target.localScale.x >=0.1f&&!IsBigger)
        {
            target.localScale -= Vector3.one * 0.05f;
            if(target.localScale.x<=0.11f)
            {
                target.localScale=Vector3.zero;
            }
            yield return null;
        }
       
    }
}
