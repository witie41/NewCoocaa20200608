using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsControl : MonoBehaviour
{
    public Transform RoomInfoPanel;
    private Transform currentPanel;
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
            else
            {   
                currentPanel=RoomInfoPanel;
                StartCoroutine(IEBigger(currentPanel));
            }
        }
        get
        {
            return currentPanel;
        }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        CurrentPanel = RoomInfoPanel;
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
