using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class DanmuTip : MonoBehaviour
{
    private Vector3 Position;
    private bool IsStart = false;

    void SetStart()
    {
        IsStart = true;
    }

    private void Update()
    {
        if(IsStart)
            if(Vector3.Distance(transform.position,Position)<=10)
            {
                DestroySelf2();
            }
    }


    private DanmuControl danmuControl;

    private void OnEnable()
    {
        Position = transform.position;
        IsStart = false;
        Invoke("SetStart", 3);
        
        if (danmuControl==null)
            danmuControl = GetComponentInParent<DanmuControl>();
        //复用
        if (danmuControl.InactiveDanmu.Contains(gameObject))
            danmuControl.InactiveDanmu.Remove(gameObject);
        if (!danmuControl.ActiveDanmu.Contains(gameObject)) 
        danmuControl.ActiveDanmu.Add(gameObject);
    }

    private void OnDisable()
    {
        if (danmuControl == null)
            danmuControl = GetComponentInParent<DanmuControl>();
        if (danmuControl.ActiveDanmu.Contains(gameObject))
            danmuControl.ActiveDanmu.Remove(gameObject);
        if(danmuControl.InactiveDanmu.Count>=20)
            Destroy(gameObject);
        else
            danmuControl.InactiveDanmu.Add(gameObject);
    }

    private void OnDestroy()
    {
        if (danmuControl.InactiveDanmu.Contains(gameObject))
            danmuControl.InactiveDanmu.Remove(gameObject);
        if (danmuControl.ActiveDanmu.Contains(gameObject))
            danmuControl.ActiveDanmu.Remove(gameObject);
    }


    public void DestroySelf()
    {
        Invoke("DestroySelf2",40);
    }
    private void DestroySelf2()
    {
        gameObject.SetActive(false);
    }
}
