using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingOn : MonoBehaviour
{

    [SerializeField]
    GameObject proFab;
    // Start is called before the first frame update
    void Start()
    {
        proFab = Resources.Load<GameObject>("Middle canvs/Help Canvas");
        if(proFab == null)
        {
            throw new Exception("没找到主页");
        }
        Instantiate(proFab);
        
    }

}
