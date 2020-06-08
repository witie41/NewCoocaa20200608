using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelSettings : MonoBehaviour
{
    private Color SelectColor=new Color(0.44f,0.65f,1f,1f);
    private Color NormalColor=new Color(0.8f,0.8f,0.8f,1f);
    private Image ButtonImage;
    private bool buttonState = false;
    private bool ButtonState
    {
        get
        {
            return buttonState;
        }
        set
        {
            buttonState = value;
            if(buttonState)
            {
                ButtonImage.color = SelectColor;
            }
            else
            {
                ButtonImage.color = NormalColor;
            }
        }
    }

    private void Start()
    {
        ButtonImage = GetComponent<Image>();
    }

    public void OnButtonClick()
    {
        ButtonState = !ButtonState;
    }
}
