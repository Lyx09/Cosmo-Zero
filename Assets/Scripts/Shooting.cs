using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public AudioSource audioshoot;
    private Rigidbody rb;
    public GameObject bullet;
    public float bulletspeed = 57.0f;
    public float cd = 0.1f;
    public int damage = 1;
    private float timeavl; //Moment at which next shoot will be available;
    public float bulletspan = 2.0f;

    public GameObject Missile;
    public static Transform target;
    public float missilecd = 5.0f;
    public int missiledmg = 7;
    public float missileavl;
    public float missilespan = 4.5f;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        timeavl = Time.time + cd;
        missileavl = Time.time + missilecd;
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
        if (Input.GetMouseButtonDown(1) && (Time.time >= missileavl) && GetComponent<Lock>().target != null)
        {
            target = GetComponent<Lock>().target;
            missileavl = Time.time + missilecd;
            SendMissile();
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
    void SendMissile()
    {
        GameObject mymissile = Instantiate(Missile);
        mymissile.transform.position = gameObject.transform.position + gameObject.transform.forward;
        mymissile.transform.rotation = gameObject.transform.rotation;
        MissileBehaviour mb = mymissile.GetComponent<MissileBehaviour>();
        mb.target = target;
        mb.sender = gameObject;
        mb.dmg = missiledmg;
        Destroy(mymissile, missilespan);
    }
}
