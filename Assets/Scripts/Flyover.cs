using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(13.4899998f, 2.52999997f, 11.9399996f);
        transform.rotation = Quaternion.Euler(7f, 150f, 0f);
        //transform.position = new Vector3(18.7800007f, 3.06999993f, -15.1000004f);
        //transform.rotation = Quaternion.Euler(20f, 0, 0);
        //transform.position = new Vector3(150, 28.5300007f, 75);
        //transform.rotation = Quaternion.Euler(40, -125, 0);
        //StartCoroutine(Lerpy1());

        //transform.position = new Vector3(15.6f, 5f, 31.1f);
        //transform.rotation = Quaternion.Euler(20f, 180f, 0);
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    IEnumerator Lerpy1()
    {
        float duration = 25f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 pos = transform.position;
            pos.x -= 15f * Time.deltaTime;
            pos.z -= 15f * Time.deltaTime;
            transform.position = pos;
            elapsed += Time.deltaTime;
            Debug.Log(elapsed);
            yield return null;
        }
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
