using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public CanvasGroup intro1;
    public CanvasGroup intro2;
    public CanvasGroup intro3;

    // Start is called before the first frame update
    void Start()
    {
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
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(Fade(intro1, 1f, 1f));
        yield return new WaitForSeconds(3f);
        StartCoroutine(Fade(intro2, 1f, 1f));
        yield return new WaitForSeconds(5f);
        StartCoroutine(Fade(intro3, 1f, 1f));
        yield return new WaitForSeconds(5f);
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
