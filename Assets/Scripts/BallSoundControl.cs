using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSoundControl : MonoBehaviour
{
    public AudioClip roll_intro;
    public AudioClip roll_loop;
    bool paused = false;

    AudioSource audiosource;

    Subscription<BallThrownEvent> throw_sub;
    Subscription<BallAtRestEvent> rest_sub;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        throw_sub = EventBus.Subscribe<BallThrownEvent>(OnBallThrown);
        rest_sub = EventBus.Subscribe<BallAtRestEvent>(OnBallAtRest);
    }

    void Update()
    {
        if (Time.timeScale == 0 && !paused)
        {
            paused = true;
            audiosource.Pause();
        }
        else if (Time.timeScale == 1 && paused)
        {
            paused = false;
            audiosource.UnPause();
        }
    }

    void OnBallThrown(BallThrownEvent e)
    {
        audiosource.enabled = true;
        StartCoroutine(PlayRollSound());
    }

    void OnBallAtRest(BallAtRestEvent e)
    {
        audiosource.enabled = false;
        audiosource.loop = false;
    }

    IEnumerator PlayRollSound()
    {
        audiosource.clip = roll_intro;
        audiosource.Play();
        yield return new WaitForSeconds(roll_intro.length);
        audiosource.clip = roll_loop;
        audiosource.loop = true;
        audiosource.Play();
    }
}
