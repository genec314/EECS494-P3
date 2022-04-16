using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    private int num_pins = 0;

    Subscription<BallBoughtEvent> ball_bought_sub;
    Subscription<GainPinsEvent> gain_pins_sub;
    // Start is called before the first frame update
    void Start()
    {
        ball_bought_sub = EventBus.Subscribe<BallBoughtEvent>(_OnBallBought);
        gain_pins_sub = EventBus.Subscribe<GainPinsEvent>(_OnGainPins);
    }

    void _OnGainPins(GainPinsEvent e)
    {
        AddPins(e.num);
    }

    void _OnBallBought(BallBoughtEvent e)
    {
        AddPins(-e.cost);
    }

    public int GetPins()
    {
        return num_pins;
    }

    public void AddPins(int num)
    {
        num_pins += num;
    }

    public void SetPins(int num)
    {
        num_pins = num;
    }
}
