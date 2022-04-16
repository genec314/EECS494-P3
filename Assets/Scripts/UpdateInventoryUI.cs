using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateInventoryUI : MonoBehaviour
{
    PlayerInventory inventory;
    Subscription<BallBoughtEvent> ball_bought_sub;
    Subscription<GainPinsEvent> gain_pins_sub;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("GameControl").GetComponent<PlayerInventory>();
        ball_bought_sub = EventBus.Subscribe<BallBoughtEvent>(_OnBallBought);
        gain_pins_sub = EventBus.Subscribe<GainPinsEvent>(_OnGainPins);
    }

    void _OnGainPins(GainPinsEvent e)
    {
        UpdateUI();
    }

    void _OnBallBought(BallBoughtEvent e)
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = inventory.GetPins().ToString();
    }
}
