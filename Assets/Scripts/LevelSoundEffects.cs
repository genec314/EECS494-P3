using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSoundEffects : MonoBehaviour
{
    public AudioClip level_complete_sound;
    public AudioClip level_fail_sound;
    public AudioClip world_complete_sound;
    public AudioClip select_level_sound;
    static LevelSoundEffects instance;

    AudioSource audioSource;

    Subscription<LevelCompleteEvent> complete_sub;
    Subscription<LevelFailedEvent> fail_sub;
    Subscription<WorldCompleteEvent> world_sub;

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
        complete_sub = EventBus.Subscribe<LevelCompleteEvent>(OnWorldComplete);
        fail_sub = EventBus.Subscribe<LevelFailedEvent>(OnLevelFailed);
        world_sub = EventBus.Subscribe<WorldCompleteEvent>(OnWorldComplete);
    }

    void Update()
    {
        if (Camera.main != null) transform.position = Camera.main.transform.position;
    }

    void OnWorldComplete(LevelCompleteEvent e)
    {
        audioSource.clip = level_complete_sound;
        audioSource.Play();
    }

    void OnLevelFailed(LevelFailedEvent e)
    {
        audioSource.clip = level_fail_sound;
        audioSource.Play();
    }

    void OnWorldComplete(WorldCompleteEvent e)
    {
        audioSource.clip = world_complete_sound;
        audioSource.Play();
    }
}
