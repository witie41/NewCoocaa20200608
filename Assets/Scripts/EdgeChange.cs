using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EdgeChange : MonoBehaviour
{
    public Text UserName;
    public Text Value;
    public Image Photo;
    public int UserId;

    public Image edge;
    public Sprite blue;
    public Sprite yellow;

    public GameObject Player;
    public void Start()
    {
        if(edge==null)
            edge = GetComponent<Image>();
        Exit();
    }
    public void Enter()
    {
        if (yellow == null)
            edge.color = Color.blue;
        else
            edge.sprite = yellow;
    }
    public void Exit()
    {
        if (blue == null)
            edge.color = Color.white;
        else
            edge.sprite = blue;
    }
    public void Click()
    {
        transform.parent.GetComponent<ChangeModel>().CurrentPlayer = Player;
        transform.GetChild(0).GetComponent<Text>().text=GetComponentInChildren<Text>().text;
    }

    public void Init(string Name,int value,Sprite photo,int id)
    {
        UserName.text = Name;
        Value.text = value.ToString();
        Photo.sprite = photo;
        UserId = id;
    }
}
