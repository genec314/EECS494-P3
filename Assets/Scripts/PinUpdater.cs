using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PinUpdater : MonoBehaviour
{
    public int startingPins = 10;

    public Sprite upPin;
    public Sprite downPin;

    public List<Image> pins;
    Subscription<PinKnockedOverEvent> pin_knock_sub;

    // Start is called before the first frame update
    void Awake()
    {
        pin_knock_sub = EventBus.Subscribe<PinKnockedOverEvent>(OnPinKnock);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPinKnock(PinKnockedOverEvent e)
    {
        pins[e.id].sprite = downPin;

        if (startingPins == 0)
        {
            EventBus.Publish(new LoadNextLevelEvent());
            for(int i = 0; i < pins.Count; i++)
            {
                pins[e.id].sprite = upPin;
            }
        }
    }
}
