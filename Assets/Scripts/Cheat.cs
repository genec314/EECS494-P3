using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            GameObject.Find("GameControl").GetComponent<GameControl>().unlock_all_levels = true;
            Debug.Log("Cheat activated");
        }
    }
}
