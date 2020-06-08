using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportButton : MonoBehaviour
{
    public int id;
    public string Name;
    public int seq;
    private Color SelectColor = new Color(0.44f, 0.65f, 1f, 1f);
    private Color NormalColor = new Color(0.8f, 0.8f, 0.8f, 1f);
    private Image ButtonImage;
    private bool buttonState = false;
    public bool ButtonState
    {
        get
        {
            return buttonState;
        }
        set
        {
            buttonState = value;
            if (buttonState)
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

    public void Init(int id, string name, int seq)
    {
        this.id = id;
        this.Name = name;
        this.seq = seq;
        GetComponentInChildren<Text>().text = name;
    }

    public void OnClick()
    {
        GetComponentInParent<ReportCnotrol>().CurrentButton = this;
    }
}
