using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IVRCommon.Keyboard;
public class UserControl : MonoBehaviour
{ 
    private Transform UserPanel;
    private Transform loginPanel;

    private void Awake()
    {
        UserPanel = transform.GetComponent<Button_Panel>().Patner.GetChild(0);
        loginPanel= transform.GetComponent<Button_Panel>().Patner.GetChild(1);
        Controller.Handle_panel[30]=loginPanel.gameObject;
        loginPanel.gameObject.transform.GetChild(0).GetChild(0).GetComponent<IVRInputField>().onValueChanged.AddListener(Get_Mobile_Email);
        loginPanel.gameObject.transform.GetChild(3).GetChild(0).GetComponent<IVRInputField>().onValueChanged.AddListener(Get_Mobile_Email);
        loginPanel.gameObject.transform.GetChild(0).GetChild(1).GetComponent<IVRInputField>().onValueChanged.AddListener(Get_passord);
        loginPanel.gameObject.transform.GetChild(3).GetChild(1).GetComponent<IVRInputField>().onValueChanged.AddListener(Get_passord);
        loginPanel.gameObject.transform.GetChild(4).GetChild(2).GetComponent<IVRInputField>().onValueChanged.AddListener(GetImageCode);
        loginPanel.gameObject.transform.GetChild(0).GetChild(4).GetComponent<IVRInputField>().onValueChanged.AddListener(GetSMSCode);

    }

    public void OnClick()
    {
        if(AllData.userId==-1)
        {
            loginPanel.gameObject.SetActive(true);
            UserPanel.gameObject.SetActive(false);
        }
        else
        {
            loginPanel.gameObject.SetActive(false);
            UserPanel.gameObject.SetActive(true);
        }
    }
   public void Get_Mobile_Email(string value)
    {
        Controller.mobile_number_email = value;
    }
    public void Get_passord(string value)
    {
        Controller.user_password = value;
    }

    public void GetSMSCode(string value)
    {
        Controller.SMSCode = value;
    }
    public void GetImageCode(string value)
    {
        Controller.ImageCode = value;
    }
}
