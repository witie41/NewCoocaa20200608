using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    public Transform VolumeSlider;
    public Slider slider;
    private float CurrentVolume;
    
    public void OnClick()
    {
        VolumeSlider.gameObject.SetActive(!VolumeSlider.gameObject.activeSelf);
    }
}
