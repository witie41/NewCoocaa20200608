using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterEnter : MonoBehaviour
{
    public void OnEnterVideo()
    {
        (Controller.panelComeback.Peek() as GameObject).GetComponentInChildren<VideoManager>().Id = int.Parse(name);
    }

    public void OnEnterRecommendVideo()
    {      
       // (Controller.panelComeback.Peek() as GameObject).GetComponentInChildren<VideoManager>().RecommendId = int.Parse(name);
  
    }

    public  void OnEnterLivingRoom()
    {
        (Controller.panelComeback.Peek() as GameObject).GetComponentInChildren<MsgManager>().CurrentId = int.Parse(name);
    }
}
