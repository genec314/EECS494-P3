using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public CanvasGroup exit;
    public CanvasGroup intro1;
    public FadeInTextLetterByLetter intro1_text;
    public CanvasGroup intro2;
    public CanvasGroup intro2_world1;
    public CanvasGroup intro2_world2;
    public CanvasGroup intro2_world3;
    public FadeInTextLetterByLetter intro2_text;
    public CanvasGroup intro3;
    public FadeInTextLetterByLetter intro3_text;
    public Animator anim;
    public AudioSource world1_source;
    public AudioSource world2_source;
    public AudioSource world3_source;
    AudioSource audioSource;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cam = Camera.main;
        StartCoroutine(ShowIntro());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EventBus.Publish<LoadWorldEvent>(new LoadWorldEvent(0));
        }
    }

    IEnumerator ShowIntro()
    {
        cam.transform.position = new Vector3(0f, 1.2f, -10f);
        yield return new WaitForSeconds(1f);
        
        audioSource.Play();
        StartCoroutine(Fade(exit, 1f, 1f));
        intro1.alpha = 1;
        intro1_text.Fade();
        yield return new WaitForSeconds(6f);

        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        intro1.alpha = 0;
        exit.alpha = 0;
        cam.transform.position = new Vector3(100f, 1.2f, -10f);
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("End");
        yield return new WaitForSeconds(1f);

        audioSource.Play();
        intro2.alpha = 1;
        intro2_text.Fade();
        yield return new WaitForSeconds(1.5f);
        world1_source.Play();
        StartCoroutine(Fade(intro2_world1, 1f, 0.5f));
        yield return new WaitForSeconds(1f);
        world2_source.Play();
        StartCoroutine(Fade(intro2_world2, 1f, 0.5f));
        yield return new WaitForSeconds(1f);
        world3_source.Play();
        StartCoroutine(Fade(intro2_world3, 1f, 0.5f));
        yield return new WaitForSeconds(8f);

        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        cam.transform.position = new Vector3(200f, 1.2f, -10f);
        intro2.alpha = 0;
        intro2_world1.alpha = 0;
        intro2_world2.alpha = 0;
        intro2_world3.alpha = 0;
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("End");
        yield return new WaitForSeconds(1f);

        audioSource.Play();
        intro3.alpha = 1;
        intro3_text.Fade();
        yield return new WaitForSeconds(6f);

        EventBus.Publish<LoadWorldEvent>(new LoadWorldEvent(0));
    }

    IEnumerator Fade(CanvasGroup canvasGroup, float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetValue;
    }
}
