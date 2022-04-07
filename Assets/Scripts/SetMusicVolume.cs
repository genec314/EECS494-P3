using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetMusicVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    public Text text;
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVol", 0.5f);
        text.text = (int)(PlayerPrefs.GetFloat("MusicVol", 0.5f) * 100) + "%";
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVol", sliderValue);
        text.text = (int)(PlayerPrefs.GetFloat("MusicVol", 0.5f) * 100) + "%";
    }
}
