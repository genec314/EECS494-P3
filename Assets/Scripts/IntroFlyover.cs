using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroFlyover : MonoBehaviour
{
    public Animator transition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlyoverWorlds());
    }

    IEnumerator FlyoverWorlds()
    {
        while (true)
        {
            transform.position = new Vector3(-30, 30, -85);
            transform.localEulerAngles = new Vector3(51, 45, 0);
            StartCoroutine(LerpWorldOne());
            yield return new WaitForSeconds(14f);
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(1.25f);
            transition.SetTrigger("End");
            transform.position = new Vector3(1024, 7, -20);
            transform.localEulerAngles = new Vector3(45, 0, 0);
            StartCoroutine(LerpWorldTwo());
            yield return new WaitForSeconds(14f);
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(1.25f);
            transition.SetTrigger("End");
        }
    }

    IEnumerator LerpWorldOne()
    {
        float duration = 15f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 pos = transform.position;
            pos.z += 5f * Time.deltaTime;
            pos.x += 5f * Time.deltaTime;
            transform.position = pos;
            elapsed += Time.deltaTime;
            // Debug.Log(elapsed);
            yield return null;
        }
    }

    IEnumerator LerpWorldTwo()
    {
        float duration = 15f;
        float elapsed = 0f;

        while(elapsed < duration)
        {
            Vector3 pos = transform.position;
            pos.y += 6f * Time.deltaTime;
            transform.position = pos;
            elapsed += Time.deltaTime;
            // Debug.Log(elapsed);
            yield return null;
        }
    }
}
