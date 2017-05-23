using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Shooting_m : NetworkBehaviour
{

    private Rigidbody rb;
    public float cd;
    public int damage;
    private float timeavl; //Moment at which next shoot will be available;
    public GameObject Missile;
    public static Transform target;
    public float missilecd;
    public int missiledmg;
    public float missileavl;
    private AudioSource audioshoot;


    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 20F;
    public float lifeTime = 1F;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        timeavl = Time.time + cd;
        missileavl = Time.time + missilecd;
        audioshoot = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetButton("Fire1") && (Time.time >= timeavl))
        {
            timeavl = Time.time + cd;
            audioshoot.Stop();
            audioshoot.Play();
            CmdShoot();
        }
        if (Input.GetMouseButtonDown(1) && (Time.time >= missileavl) && GetComponent<Lock_m>().target != null)
        {
            target = GetComponent<Lock>().target;
            missileavl = Time.time + missilecd;
            SendMissile();
        }
	}

    [Command]
    public void CmdShoot()
    {
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position , bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed + rb.velocity;
        bullet.GetComponent<Bullet_m>().SetSender(gameObject);


        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bulletPrefab);

        Destroy(bullet, lifeTime);
    }
    void SendMissile()
    {
        GameObject mymissile = Instantiate(Missile);
        mymissile.transform.position = gameObject.transform.position + gameObject.transform.forward;
        mymissile.transform.LookAt(target);
        MissileBehaviour mb = mymissile.GetComponent<MissileBehaviour>();
        Rigidbody msrb = mymissile.GetComponent<Rigidbody>();
        mb.target = target;
        mb.sender = gameObject;
        Vector3 missilespeed = new Vector3();
        missilespeed = msrb.transform.forward * 50;
        msrb.velocity = missilespeed;
        mb.speed = missilespeed;
        mb.dmg = missiledmg;
    }
}
