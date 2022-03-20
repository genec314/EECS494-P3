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
    Subscription<NewHoleEvent> new_hole_subscription;

    // Start is called before the first frame update
    void Awake()
    {
        pin_knock_sub = EventBus.Subscribe<PinKnockedOverEvent>(OnPinKnock);
        new_hole_subscription = EventBus.Subscribe<NewHoleEvent>(NewHole);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPinKnock(PinKnockedOverEvent e)
    {
        pins[e.id].sprite = downPin;
    }

    private void NewHole(NewHoleEvent e)
    {
        for (int i = 0; i < pins.Count; ++i)
        {
            pins[i].sprite = upPin;
        }
    }
}
