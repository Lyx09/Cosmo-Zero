using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public AudioSource audioshoot;
    private Rigidbody rb;
    public GameObject bullet;
    public float bulletspeed = 57.0f;
    public static float cd = 0.6f;
    public static int damage = 1;
    private float timeavl; //Moment at which next shoot will be available;
    public float bulletspan = 2.0f;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        timeavl = Time.time + cd;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Fire1") && (Time.time >= timeavl))
        {
            timeavl = Time.time + cd;
            audioshoot.Stop();
            audioshoot.Play();
            Shoot();
        }
	}
    public void Shoot()
    {
        GameObject newbull = Instantiate(bullet);
        newbull.transform.position = gameObject.transform.position + gameObject.transform.forward;
        newbull.transform.rotation = rb.transform.rotation;
        newbull.transform.Rotate(90, 0, 0);
        newbull.GetComponent<Bulette>().SetSender(gameObject);
        newbull.GetComponent<Rigidbody>().velocity = newbull.transform.up * bulletspeed;
        Destroy(newbull, bulletspan);
    }
}
