using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsControl : MonoBehaviour
{
    public Transform VideoList;
    private Transform currentPanel=null;
    public Transform CurrentPanel
    {
        set
        {
            if (currentPanel == value)
                return;
            if (currentPanel != null)
            {
                StartCoroutine(IESmaller(currentPanel));
            }
            currentPanel = value;
            if (currentPanel != null)
                StartCoroutine(IEBigger(currentPanel));
            if(currentPanel==VideoList)
            {
                transform.parent.localScale=Vector3.zero;
            }
        }
        get
        {
            return currentPanel;
        }
    }


    float Speed = 0.05f;

    IEnumerator IESmaller(Transform target)
    {
        while (target.localScale.x > 0)
        {
            target.localScale -= Vector3.one * Speed;
            if (target.localScale.x <= Speed)
            {
                target.localScale = Vector3.zero;
                break;
            }
            yield return null;
        }
        yield break;
    }
    IEnumerator IEBigger(Transform target)
    {
        while (target.localScale.x < 1)
        {
            target.localScale += Vector3.one * Speed;
            yield return null;
        }
        yield break;
    }
}
