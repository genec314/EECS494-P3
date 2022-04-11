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
    [SerializeField] bool canSplit = false;

    Transform tf;
    Rigidbody orig;
    Subscription<LevelStartEvent> start_subscription;

    Material material;

    // Start is called before the first frame update
    void Awake()
    {
        tf = this.GetComponent<Transform>();
        orig = this.GetComponent<Rigidbody>();
        start_subscription = EventBus.Subscribe<LevelStartEvent>(OnLevelStart);
        material = this.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (needtoDie && isSplit)
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
            else if (canSplit && orig.velocity.magnitude > 0.5f)
            {
                isSplit = true;
                canSplit = false;
                Vector3 vel = orig.velocity;
                secondBall1 = Instantiate(secondBall);
                secondBall1.GetComponent<MeshRenderer>().material = material;
                secondBall1.transform.position = transform.position;
                // StartCoroutine(ChangeToBallLayer(secondBall1));
                Rigidbody rb = secondBall1.GetComponent<Rigidbody>();
                //rb = GetComponent<Rigidbody>();
                rb.velocity = vel.magnitude * (tf.forward.normalized - tf.right.normalized);
                
                //transform.position = transform.position + new Vector3(3, 0, 0);
                orig.velocity = vel.magnitude * (tf.forward.normalized + tf.right.normalized);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.CompareTag("Ball") && needtoDie)
        {
            Destroy(secondBall1);
            // GetComponent<Rigidbody>().velocity = Vector3.zero;
            // this.enabled = false;
            isSplit = false;
            needtoDie = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.CompareTag("Ball") && other.transform.parent.gameObject.layer == 9 && other.transform.parent.gameObject.layer != 7)
        {
            other.transform.parent.gameObject.layer = 8;
        }
    }

    /* IEnumerator ChangeToBallLayer(GameObject ball2)
    {
        yield return new WaitForSeconds(1f);

        if (ball2.layer != 7)
        {
            ball2.layer = 8;
        }
    } */

    private void OnLevelStart(LevelStartEvent e)
    {
        canSplit = e.level.canBallSplit;

        if (secondBall1 != null)
        {
            isSplit = false;
            needtoDie = false;

            Destroy(secondBall1);
        }
    }
}
