using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeWorldData : MonoBehaviour
{

    static HomeWorldData instance;

    bool[] activeLanes;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        instance = this;
        DontDestroyOnLoad(this);

        activeLanes = new bool[3]{ false, false, false};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool[] GetActiveLanes()
    {
        return activeLanes;
    }
}
