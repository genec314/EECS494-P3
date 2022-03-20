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
    bool is_moving = false;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        throw_sub = EventBus.Subscribe<BallThrownEvent>(OnBallThrown);
        rest_sub = EventBus.Subscribe<BallAtRestEvent>(OnBallAtRest);
    }

    void OnBallThrown(BallThrownEvent e)
    {
        is_moving = true;
        audiosource.enabled = true;
        StartCoroutine(PlayRollSound());
    }

    void OnBallAtRest(BallAtRestEvent e)
    {
        is_moving = false;
        audiosource.enabled = false;
        audiosource.loop = false;
        AudioSource.PlayClipAtPoint(ready, Camera.main.transform.position);
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Pin") && is_moving)
        {
            AudioSource.PlayClipAtPoint(pin_knockdown_sound, transform.position, 0.25f);
        }
    }
}
