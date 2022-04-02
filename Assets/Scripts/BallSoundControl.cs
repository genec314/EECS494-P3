using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSoundControl : MonoBehaviour
{
    public AudioClip ready;
    public AudioClip roll_intro;
    public AudioClip roll_loop;
    public AudioClip pin_knockdown_sound;

    AudioSource audiosource;

    Subscription<BallThrownEvent> throw_sub;
    Subscription<BallAtRestEvent> rest_sub;
    Subscription<BallReadyEvent> ready_sub;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        throw_sub = EventBus.Subscribe<BallThrownEvent>(OnBallThrown);
        rest_sub = EventBus.Subscribe<BallAtRestEvent>(OnBallAtRest);
        ready_sub = EventBus.Subscribe<BallReadyEvent>(OnBallReady);
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

    void OnBallReady(BallReadyEvent e)
    {
        AudioSource.PlayClipAtPoint(ready, Camera.main.transform.position, 0.5f);
    }

    IEnumerator PlayRollSound()
    {
        audiosource.clip = roll_intro;
        audiosource.PlayOneShot(audiosource.clip, 0.5f);
        yield return new WaitForSeconds(roll_intro.length);
        audiosource.clip = roll_loop;
        audiosource.loop = true;
        audiosource.PlayOneShot(audiosource.clip, 0.5f);
    }
}
