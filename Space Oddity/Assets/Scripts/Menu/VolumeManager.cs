using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider slider;
    public float SliderValue;
    public Image ImageMute;
    public Image ImageUnmute;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute();
    }

    public void ChangeSlider(float valor)
    {
        SliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", SliderValue);
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute();
    }

    public void RevisarSiEstoyMute()
    {
        if (SliderValue == 0)
        {
            ImageMute.enabled = true;
            ImageUnmute.enabled = false;
        }
        else
        {
            ImageMute.enabled = false;
            ImageUnmute.enabled = true;
        }
    }
}
