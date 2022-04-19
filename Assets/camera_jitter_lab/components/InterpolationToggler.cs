using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterpolationToggler : MonoBehaviour
{

    public string game_object_name;

    Rigidbody rb;

    // Use this for initialization
    void Start ()
    {
        GameObject g = GameObject.Find (game_object_name);
        if (g == null) {
            Debug.LogError ("No gameobject with name \"" + game_object_name + "\" exists in the scene.");
            return;
        }

        rb = g.GetComponent <Rigidbody> ();
        GetComponent <Button> ().onClick.AddListener (OnClick);
    }

    void OnClick ()
    {
        if (rb == null)
            return;

        if (rb.interpolation == RigidbodyInterpolation.None)
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        else
            rb.interpolation = RigidbodyInterpolation.None;
    }
	
    // Update is called once per frame
    void Update ()
    {
		
    }
}
