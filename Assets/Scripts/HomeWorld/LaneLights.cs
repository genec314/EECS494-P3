using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneLights : MonoBehaviour
{
    [SerializeField]
    public GameObject[] lights;

    [SerializeField]
    public GameObject[] pictures;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnLights(int index)
    {
        lights[index].gameObject.SetActive(true);
        pictures[index].gameObject.SetActive(true);

    }

    public void TurnOffLights(int index)
    {
        lights[index].gameObject.SetActive(false);
        pictures[index].gameObject.SetActive(false);

    }
}
