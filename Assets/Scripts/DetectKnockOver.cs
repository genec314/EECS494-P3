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
    Subscription<ResetPinsEvent> reset_pin_sub;

    Vector3 startPos;
    Quaternion startRot;
    Color[] startColors;
    GameControl gc;
    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();
        pin_renderers = GetComponentsInChildren<MeshRenderer>();

        ready_sub = EventBus.Subscribe<BallReadyEvent>(FadeOutWhenReady);
        reset_pin_sub = EventBus.Subscribe<ResetPinsEvent>(_OnResetPins);
        startPos = transform.localPosition;
        startRot = transform.localRotation;
        startColors = new Color[pin_renderers.Length];
        for(int i = 0; i < pin_renderers.Length; i++)
        {
            startColors[i] = pin_renderers[i].material.color;
        }

        gc = GameObject.Find("GameControl").GetComponent<GameControl>();
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
        if (knockedOver && !gc.InTutorial() && GetComponent<MeshCollider>().enabled == true)
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

    void _OnResetPins(ResetPinsEvent e)
    {
        StopAllCoroutines();
        GetComponent<MeshCollider>().enabled = true;
        transform.localPosition = startPos;
        transform.localRotation = startRot;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        for(int i = 0; i < pin_renderers.Length; i++)
        {
            pin_renderers[i].material.color = startColors[i];
        }

        
    }
}
