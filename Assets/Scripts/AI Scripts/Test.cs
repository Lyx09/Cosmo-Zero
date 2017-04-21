using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class Test : MonoBehaviour
{

    public Transform target;

    Vector3 acceleration;
    Vector3 velocty;
    float speed = 5.0f;
    private Rigidbody rb;

    GameObject[] boids;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boids = GameObject.FindGameObjectsWithTag("Enemy");

        //velocty = (target.position - transform.position).normalized * speed;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 r1 = Rule_1();
        Vector3 r2 = Rule_2();
        Vector3 r3 = Rule_3();

        acceleration = r1 + r2 + r3;
        velocty += 2 * acceleration * Time.deltaTime;

        if (velocty.magnitude > speed)
            velocty = velocty.normalized * speed;
        rb.velocity = velocty;

        Quaternion desiredRotation = Quaternion.LookRotation(velocty);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 3);
    }


    Vector3 Rule_1()
    {

        Vector3 distance = target.position - transform.position;

        if (distance.magnitude < 3)
            return distance.normalized * -12;
        else
            return distance.normalized * 2;
    }

    Vector3 Rule_2()
    {

        if (!Physics.Raycast(transform.position, transform.forward, 2.0f))
        {
            return -transform.up;
        }

        return Vector3.zero;
    }

    Vector3 Rule_3()
    {

        Vector3 c = Vector3.zero;

        foreach (GameObject g in boids)
        {
            if (g.transform.position != transform.position)
            {
                if ((g.transform.position - transform.position).magnitude < 1.0f)
                {
                    c -= (g.transform.position - transform.position);
                }
            }

        }

        return c * 3.0f;

    }
}