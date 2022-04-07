using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetSoundEffectsVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    public Text text;
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("SoundEffectsVol", 0.5f);
        text.text = (int)(PlayerPrefs.GetFloat("SoundEFfectsVol", 0.5f) * 100) + "%";
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("SoundEffectsVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SoundEffectsVol", sliderValue);
        text.text = (int)(PlayerPrefs.GetFloat("SoundEffectsVol", 0.5f) * 100) + "%";
    }
}
