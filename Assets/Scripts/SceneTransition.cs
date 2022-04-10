using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{

    static SceneTransition ins;

    Image black;
    // Start is called before the first frame update
    void Start()
    {
        if(ins != null)
        {
            Destroy(this.gameObject);
        }
        ins = this;
        DontDestroyOnLoad(this);

        black = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeOut(float duration, int level)
    {
        float elapsed = 0;
        while(elapsed < duration)
        {
            Color c = black.color;
            c.a = elapsed / duration;
            black.color = c;
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        EventBus.Publish(new LoadWorldEvent(level));
    }
}
