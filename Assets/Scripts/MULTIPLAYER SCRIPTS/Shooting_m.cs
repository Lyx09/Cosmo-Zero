using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Shooting_m : NetworkBehaviour
{
    public AudioSource audioshoot;
    
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 57.0f;
    public float bulletcd = 0.1f;
    public float bulletDamage = 1;
    private float timeavl; //Moment at which next shoot will be available;
    public float bulletLifeSpan = 2.0f;

    public GameObject missilePrefab;
    public Transform missileSpawn;
    public static Vector3 tarV3;
    public float missilecd = 5.0f;
    public float missileDamage = 7;
    public float missileavl;
    public float missileLifeSpan = 4.5f;
    
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        timeavl = Time.time + bulletcd;
        missileavl = Time.time + missilecd;
    }
    
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetButton("Fire1") && (Time.time >= timeavl))
        {
            timeavl = Time.time + bulletcd;
            audioshoot.Stop();
            audioshoot.Play();
            CmdShoot();
        }

        if (Input.GetMouseButtonDown(1) && (Time.time >= missileavl) && GetComponent<Lock_m>().target != null)
        {
            tarV3 = GetComponent<Lock_m>().targetV3;
            missileavl = Time.time + missilecd;
            CmdShootMissile();
        }
    }

    [Command]
    public void CmdShoot()
    {
        var bullet = (GameObject)Instantiate(bulletPrefab,bulletSpawn.position,bulletSpawn.rotation);
        bullet.GetComponent<Bullet_m>().Initialize(gameObject, bulletDamage);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, bulletLifeSpan);
    }

    [Command]
    void CmdShootMissile()
    {
        var missile = (GameObject)Instantiate(missilePrefab, missileSpawn.position, missileSpawn.rotation);
        
        missile.GetComponent<MissileBehaviour_m>().Initialize(gameObject,missileDamage);

        NetworkServer.Spawn(missile);
        Destroy(missile, missileLifeSpan);
    }
}