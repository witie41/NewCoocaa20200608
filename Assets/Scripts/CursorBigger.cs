using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorBigger : MonoBehaviour
{
    public void OnEnter()
    {
        StartCoroutine(IEBigger());
    }

    IEnumerator IEBigger()
    {
        while(transform.parent.localScale.x<=1.1f)
        {
            transform.parent.localScale += Vector3.one * 0.01f;
            yield return null;
        }
        
    }

    public void OnExit()
    {
        StartCoroutine(IESmaller());

    }

    IEnumerator IESmaller()
    {
        while (transform.parent.localScale.x >= 1f)
        {
            transform.parent.localScale -= Vector3.one * 0.01f;
            yield return null;
        }
       
    }
}
