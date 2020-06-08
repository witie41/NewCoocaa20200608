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
        if (GetComponentInParent<PanelsControl>().CurrentPanel == VolumeSlider)
            Mute();
        GetComponentInParent<PanelsControl>().CurrentPanel = VolumeSlider;
    }


    public void Mute()
    {
        if (slider.value != 0)
        {
            CurrentVolume = slider.value;
            slider.value = 0;
        }
        else
        {
            slider.value = CurrentVolume;
        }
    }
}
