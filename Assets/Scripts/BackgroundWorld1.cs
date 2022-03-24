using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundWorld1 : MonoBehaviour
{

    public GameObject bolt1;
    public GameObject directional_light;
    Light light;
    LightningBolt bolt;
    LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        light = directional_light.GetComponent<Light>();
        bolt = this.GetComponent<LightningBolt>();
        lr = this.GetComponent<LineRenderer>();
        lr.widthMultiplier = 2.5f;
        //StartCoroutine(SpawnBolts());
        StartCoroutine(LightningEffect());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LightningEffect()
    {
        while (true)
        {
            float downtime = Random.Range(0.5f, 3f);
            yield return new WaitForSeconds(downtime);
            float intensity = Random.Range(3f, 15f);
            lr.widthMultiplier = intensity / 5;
            float duration = Random.Range(intensity / 5, 3);
            float dist = 30f;

            float x = Random.Range(-dist, dist);
            float z = Random.Range(-dist, dist);
            Vector3 startPos = new Vector3(x, Random.Range(20, 50), z);
            Debug.Log(startPos);
            Vector3 endPos = new Vector3(startPos.x, 0, startPos.z);

            bolt.Trigger(startPos, endPos);

            light.intensity = intensity;
            float elapsed = 0.0f;
            while(elapsed < duration)
            {
                light.intensity = Mathf.Lerp(intensity, 1, elapsed / duration);
                Color c = Color.white;
                c.a = 1 - (elapsed / duration);
                lr.material.SetColor("_Color", c);
                elapsed += Time.deltaTime;
                yield return null;
            }
            lr.widthMultiplier = 0;
        }
    }
    IEnumerator SpawnBolts()
    {
        Vector3 loc = GetRandomLocation();
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject.Instantiate(bolt1, loc, Quaternion.identity);
            loc = GetRandomLocation();
        }
    }

    Vector3 GetRandomLocation()
    {
        Vector3 loc = new Vector3();
        loc.x = Random.Range(-100, 100);
        loc.y = Random.Range(20, 100);
        loc.z = Random.Range(-100, 100);

        return loc;
    }

}
