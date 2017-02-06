using UnityEngine;
using System.Collections;

public class Bulette : MonoBehaviour {

    private float creation;
    private Rigidbody rb;
    private GameObject sender;
    // Use this for initialization
    public void SetSender(GameObject go)
    {
        sender = go;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        creation = Time.time;
        //rb.AddForce(transform.up * 50);
    }

    void Update()
    {
        if (Time.time > creation + 5.0F)
        {
            Object.Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(transform.up * 20);
    }

    void OnTriggerEnter(Collider other)
    {
        //BadTest enemysc = other.GetComponent<BadTest>();
        //enemysc.Hurt(1);
        other.gameObject.SetActive(false);
    }
}
