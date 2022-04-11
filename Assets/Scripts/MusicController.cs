using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public AudioClip title_clip;
    public AudioClip world_clip;
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
                audioSource.volume = 0.25f;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.clip != world_clip)
            {
                audioSource.clip = world_clip;
                audioSource.volume = 0.5f;
                audioSource.Play();
            }
        }
    }
}
