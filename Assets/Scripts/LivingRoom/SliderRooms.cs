using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderRooms : MonoBehaviour
{
    private float SliderLongth;
    private float speed=10;
    private RectTransform rectT;
    private float MaxLength;

    private void Awake() {
        SliderLongth=GetComponentInParent<ScrollRect>().GetComponent<RectTransform>().rect.width;
        rectT=GetComponent<RectTransform>();
    }
    public void OnLeftButtonControl()
    {
        StartCoroutine(IELeftSlide(SliderLongth));
    } 
    
    private IEnumerator IELeftSlide(float value)
    {
        while(value>0)
        {
            rectT.anchoredPosition=rectT.anchoredPosition-Vector2.left*speed;
            value-=speed;
            yield return null;
        }
        yield break;
    }
        public void OnRightButtonControl()
    {
        StartCoroutine(IERightSlide(SliderLongth));
    } 
    
    private IEnumerator IERightSlide(float value)
    {
        while(value>0)
        {
            rectT.anchoredPosition=rectT.anchoredPosition-Vector2.right*speed;
            value-=speed;
            yield return null;
        }
        yield break;
    }
}
