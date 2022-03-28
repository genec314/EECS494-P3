using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSplit : MonoBehaviour
{
    bool isSplit = false;
    public GameObject secondBall;
    GameObject secondBall1;
    [SerializeField] float joinSpeed = 5;
    bool needtoDie = false;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (needtoDie)
        {
            secondBall1.GetComponent<Rigidbody>().velocity = (transform.position - secondBall1.transform.position).normalized * joinSpeed;
        }

        if (Input.GetKeyDown(KeyCode.F) && !needtoDie)
        {
            if (isSplit)
            {
                needtoDie = true;
                secondBall1.GetComponent<Rigidbody>().useGravity = false;
                secondBall1.gameObject.layer = 7;
                //Bring them back together
                //GetComponent<Rigidbody>().velocity = (secondBall1.transform.position - transform.position) * joinSpeed ;
                secondBall1.GetComponent<Rigidbody>().velocity = (transform.position - secondBall1.transform.position).normalized * joinSpeed;
                
            }
            else
            {
                isSplit = true;
                Vector3 vel = GetComponent<Rigidbody>().velocity;
                secondBall1 = Instantiate(secondBall);
                secondBall1.transform.position = transform.position;
                Rigidbody rb = secondBall1.GetComponent<Rigidbody>();
                //rb = GetComponent<Rigidbody>();
                rb.velocity = vel - new Vector3(5, 0, 0);
                
                //transform.position = transform.position + new Vector3(3, 0, 0);
                GetComponent<Rigidbody>().velocity = vel + new Vector3(5, 0, 0);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.CompareTag("Ball") && needtoDie)
        {
            Destroy(secondBall1);
            // GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.enabled = false;
        }
    }
}
