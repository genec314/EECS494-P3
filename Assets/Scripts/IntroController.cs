using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public CanvasGroup intro1;
    public CanvasGroup intro2;
    public CanvasGroup intro3;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        yield return new WaitForSeconds(0.5f);
        audioSource.Play();
        StartCoroutine(Fade(intro1, 1f, 1f));
        yield return new WaitForSeconds(3f);
        StartCoroutine(Fade(intro2, 1f, 1f));
        yield return new WaitForSeconds(3f);
        StartCoroutine(Fade(intro3, 1f, 1f));
        yield return new WaitForSeconds(3f);
        audioSource.Stop();
        yield return new WaitForSeconds(3f);
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
