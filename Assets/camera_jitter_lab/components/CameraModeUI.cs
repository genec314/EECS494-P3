using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraModeUI : MonoBehaviour
{
    static CameraModeUI _instance;

    Text camera_mode_text;

    // Use this for initialization
    void Start ()
    {
        _instance = this;
        camera_mode_text = GetComponent <Text> ();		
    }

    public static void SetText (string t)
    {
        _instance.camera_mode_text.text = t;
    }
}
