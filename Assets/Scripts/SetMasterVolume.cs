using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SetMasterVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    public TextMeshProUGUI text;
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MasterVol", 0.5f);
        text.text = (int)(PlayerPrefs.GetFloat("MasterVol", 0.5f) * 100) + "%";
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVol", sliderValue);
        text.text = (int)(PlayerPrefs.GetFloat("MasterVol", 0.5f) * 100) + "%";
    }
}
