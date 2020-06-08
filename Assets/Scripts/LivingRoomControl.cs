using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class LivingRoomControl : MonoBehaviour

   
{
   
    public static LivingRoomControl m_control;
    public int roomCount;//直播间的总数量；
    public RectTransform myContent;//直播内容显示区域；
    public GridLayoutGroup myGridLayoutGroup;
    public RectTransform myView;
    private static Dictionary<int, MyLivingRoom> theRoom = new Dictionary<int, MyLivingRoom>();
    // Start is called before the first frame update
    void Start()
    {
        m_control = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
