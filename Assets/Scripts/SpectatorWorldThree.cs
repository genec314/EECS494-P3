using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorWorldThree : MonoBehaviour
{
    Rigidbody rb;

    float small_force = 0f;
    float big_force = 250f;

    bool jumping = false;

    float startY;
    // Start is called before the first frame update
    void Start()
    {
        string path = "Materials/Balls/";
        Object[] mats = Resources.LoadAll(path);
        GetComponent<MeshRenderer>().material = (Material)mats[Random.Range(0, mats.Length)];

        small_force = Random.Range(0f, 100f);
        big_force = Random.Range(200f, 350f);
        rb = GetComponent<Rigidbody>();

        startY = transform.position.y;
        StartCoroutine(JumpRepeat());
    }


    IEnumerator JumpRepeat()
    {
        while (true)
        {
            small_force = Random.Range(100, 300f);
            Jump(small_force);
            float duration = Random.Range(2f, 5f);
            yield return new WaitForSeconds(duration);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.other.transform.parent.name);
    }
    void Jump(float force)
    {
        rb.AddForce(new Vector3(0, force, 0));
    }
}
