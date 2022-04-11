using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-50f, 40f, -100f);
        StartCoroutine(Lerpy3());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator Lerpy2()
    {
        float duration = 25f;
        float elapsed = 0f;

        while(elapsed < duration)
        {
            Vector3 pos = transform.position;
            pos.y += 15f * Time.deltaTime;
            transform.position = pos;
            elapsed += Time.deltaTime;
            Debug.Log(elapsed);
            yield return null;
        }
    }

    IEnumerator Lerpy3()
    {
        float duration = 25f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 pos = transform.position;
            pos.z += 18f * Time.deltaTime;
            transform.position = pos;
            elapsed += Time.deltaTime;
            Debug.Log(elapsed);
            yield return null;
        }
    }
}
