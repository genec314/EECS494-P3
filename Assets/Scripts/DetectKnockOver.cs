using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectKnockOver : MonoBehaviour
{
    public int pin_id;
    public float fadeSpeed = 1f;
    public AudioClip knockdown_sound;
    Transform tf;
    bool knockedOver = false;
    MeshRenderer[] pin_renderers;
    Subscription<BallReadyEvent> ready_sub;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();
        pin_renderers = GetComponentsInChildren<MeshRenderer>();
        ready_sub = EventBus.Subscribe<BallReadyEvent>(FadeOutWhenReady);
    }

    // Update is called once per frame
    void Update()
    {
        if (!knockedOver && tf.up.y < 0.5f)
        {
            knockedOver = true;
            PinKnockedOverEvent knock = new PinKnockedOverEvent(pin_id);
            EventBus.Publish(knock);
            AudioSource.PlayClipAtPoint(knockdown_sound, transform.position, 0.25f);
        }
    }

    void FadeOutWhenReady(BallReadyEvent e)
    {
        if (knockedOver && GetComponent<MeshCollider>().enabled == true)
        {
            for (int i = 0; i < pin_renderers.Length; i++)
            {
                StartCoroutine(FadeOut(pin_renderers[i]));
            }
        }
    }

    IEnumerator FadeOut(MeshRenderer renderer)
    {
        while (renderer.material.color.a > 0)
        {
            Color objectColor = renderer.material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            renderer.material.color = objectColor;
            yield return null;
        }
        GetComponent<MeshCollider>().enabled = false;
    }
}
