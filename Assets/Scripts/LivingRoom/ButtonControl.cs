using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    private Color SelectColor = new Color(1, 0, 1, 1);
    private Color NormalColor = new Color(1, 1, 1, 1);
    private Image myImage;
    public Image OtherImage;
    private Controller controller;

    private void Awake()
    {
        myImage = GetComponent<Image>();
        //controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>();
        //controller.Show(30, controller.Big_middle_canvs, "Big middle canvs", "Big Middle panel/User log in");
    }

    public void ChangeColor()
    {
        myImage.color = SelectColor;
        OtherImage.color = NormalColor;
    }
}
