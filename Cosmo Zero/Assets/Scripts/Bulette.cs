using UnityEngine;
using System.Collections;

public class Bulette : MonoBehaviour {

    private float creation;
    private Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        creation = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(transform.up * 20);
    }
}
