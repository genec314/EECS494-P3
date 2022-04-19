using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButtons : MonoBehaviour
{
    public GameObject credits_menu;

    public void CreditsToTitle()
    {
        if (credits_menu != null) credits_menu.SetActive(false);
    }

    public void CreditsFromTitle()
    {
        if (credits_menu != null) credits_menu.SetActive(true);
    }
}
