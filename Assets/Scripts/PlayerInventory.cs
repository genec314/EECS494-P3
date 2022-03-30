using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    private int num_pins = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
