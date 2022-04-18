using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSpectators : MonoBehaviour
{

    public GameObject spec_prefab;

    int num = 360;
    // Start is called before the first frame update
    void Start()
    {
        float angle;
        for(int i = 0; i < num; i++)
        {
            angle = i;
            float x = 0 + 150 * Mathf.Cos(angle);
            float z = 0 + 200 * Mathf.Sin(angle);
            float y = Random.Range(-8, 3);
            Vector3 pos = new Vector3(x, y, z);
            GameObject.Instantiate(spec_prefab, pos, Quaternion.identity);
            y = Random.Range(y + 3, 12);
            pos.y = y;
            GameObject.Instantiate(spec_prefab, pos, Quaternion.identity);
            y = Random.Range(y + 3, 24);
            pos.y = y;
            GameObject.Instantiate(spec_prefab, pos, Quaternion.identity);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
