using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Panel : MonoBehaviour
{
    public Transform Patner;
    private float Speed=0.05f;


    public void OnClick()
    {
        GetComponentInParent<PanelsControl>().CurrentPanel = Patner;
    }

    IEnumerator IESmaller(Transform target)
    {
        while(target.localScale.x> 0)
        {
            target.localScale -= Vector3.one * Speed;
            if(target.localScale.x<= Speed)
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
        while (target.localScale.x <1)
        {
            target.localScale += Vector3.one * Speed;
            yield return null;
        }
        yield break;
    }


}
