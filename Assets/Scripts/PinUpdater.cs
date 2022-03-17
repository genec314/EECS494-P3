using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PinUpdater : MonoBehaviour
{
    public int startingPins = 10;

    TextMeshProUGUI text;

    Subscription<PinKnockedOverEvent> pin_knock_sub;

    // Start is called before the first frame update
    void Awake()
    {
        text = this.GetComponent<TextMeshProUGUI>();
        text.text = "Pins Remaning: " + startingPins;
        pin_knock_sub = EventBus.Subscribe<PinKnockedOverEvent>(OnPinKnock);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPinKnock(PinKnockedOverEvent e)
    {
        text.text = "Pins Remaning: " + --startingPins;

        if (startingPins == 0)
        {
            EventBus.Publish(new LoadNextLevelEvent());
        }
    }
}
