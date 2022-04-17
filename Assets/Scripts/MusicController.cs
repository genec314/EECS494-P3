using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public AudioClip title_clip;
    public AudioClip world_1_clip;
    public AudioClip world_2_clip;
    public AudioClip world_3_clip;
    static MusicController instance;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        string scene_name = SceneManager.GetActiveScene().name;
        if (scene_name == "TitleScreen" || scene_name == "HomeWorld")
        {
            if (audioSource.clip != title_clip)
            {
                audioSource.clip = title_clip;
                audioSource.Play();
            }
        }
        else if (scene_name == "WorldOne" || scene_name == "WorldOneSelect")
        {
            if (audioSource.clip != world_1_clip)
            {
                audioSource.clip = world_1_clip;
                audioSource.Play();
            }
        }
        else if (scene_name == "WorldTwo" || scene_name == "WorldTwoSelect")
        {
            if (audioSource.clip != world_2_clip)
            {
                audioSource.clip = world_2_clip;
                audioSource.Play();
            }
        }
        else if (scene_name == "WorldThree")
        {
            if (audioSource.clip != world_3_clip)
            {
                audioSource.clip = world_3_clip;
                audioSource.Play();
            }
        }
        else
        {
            // Intro, Complete
            audioSource.clip = null;
            audioSource.Stop();
        }
    }
}
