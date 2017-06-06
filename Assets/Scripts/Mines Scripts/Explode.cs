using UnityEngine;
using System.Collections;
using System;

public class Explode : MonoBehaviour {


    public float radius = 20f;
    public float minespeed = 0.01f;
    public float dammages = 150f;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update ()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position , radius);
        
        float mindist = radius;
        if (hitColliders.Length != 0)
        {
            foreach(Collider c in hitColliders)
            {
                if (c.tag == "Player" || c.tag == "Enemy")
                {
                    Transform closest_target = null;
                    if (mindist > (c.transform.position - transform.position).magnitude)
                    {
                        mindist = (c.transform.position - transform.position).magnitude;
                        closest_target = c.transform;
                    }
                    Vector3 direction = (closest_target.position - transform.position).normalized * Mathf.Abs((closest_target.position - transform.position).magnitude - radius) * minespeed;
                    rb.AddForce(direction);
                }                
            }            
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        Destroy(gameObject);
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            State state = collision.gameObject.GetComponent<State>();
            state.Hurt(dammages);
        }
    }
}
