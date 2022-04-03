using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{

    public GameObject UI;

    private int num_pins = 0;

    Subscription<BallBoughtEvent> ball_bought_sub;
    Subscription<PinKnockedOverEvent> pin_knock_sub;
    // Start is called before the first frame update
    void Start()
    {
        ball_bought_sub = EventBus.Subscribe<BallBoughtEvent>(_OnBallBought);
        pin_knock_sub = EventBus.Subscribe<PinKnockedOverEvent>(_OnPinKnocked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _OnPinKnocked(PinKnockedOverEvent e)
    {
        AddPins(1);
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
        updateUI();
    }

    public void SetPins(int num)
    {
        num_pins = num;
        updateUI();
    }

    void updateUI()
    {
        UI.GetComponentInChildren<TextMeshProUGUI>().text = num_pins.ToString();
    }
}
