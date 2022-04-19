using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeltatimeToggler : MonoBehaviour
{
    FollowTarget2 ft;

    // Use this for initialization
    void Start ()
    {
        ft = Camera.main.GetComponent <FollowTarget2> ();
        GetComponent <Button> ().onClick.AddListener (OnClick);
    }

    void OnClick ()
    {
        ft.use_deltatime = !ft.use_deltatime;
    }
}
