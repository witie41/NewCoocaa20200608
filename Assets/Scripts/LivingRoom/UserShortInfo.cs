using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserShortInfo : MonoBehaviour
{
    public string Name
    {
        set
        {
            if(value==null)
               NameConp.text = "未知";
            else
               NameConp.text = value;
        }
    }


    public string Photo
    {
        set
        {
            StartCoroutine(DataClassInterface.IEGetSprite(value,(Sprite sprite,GameObject gtb, string nothing) => { PhotoConp.sprite = sprite; },null));
        }
     }

    public int Contribution
    {
        set
        {
            ContributionConp.text = value.ToString();
        }
    }

    public int UserId;


    public Text NameConp;
    public Text ContributionConp;
    public Image PhotoConp;

}
