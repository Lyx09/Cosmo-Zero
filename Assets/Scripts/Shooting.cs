using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{

    private Rigidbody rb;
    public GameObject bullet;
    public float cd;
    public int damage;
    private float timeavl; //Moment at which next shoot will be available;
    private AudioSource audioshoot;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        timeavl = Time.time + cd;
        audioshoot = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Fire1") & (Time.time >= timeavl))
        {
            timeavl = Time.time + cd;
            audioshoot.Stop();
            audioshoot.Play();
            Shoot();
        }
	}
    void Shoot()
    {
        GameObject test = Instantiate(bullet);
        test.transform.position = gameObject.transform.position + gameObject.transform.forward;
        test.transform.rotation = rb.transform.rotation;
        test.transform.Rotate(90, 0, 0);
        /*float deadZoneRadius = Screen.width / 25.0F; //relative to screen width
        float disToCenX = Input.mousePosition[0] - Screen.width / 2F;
        float disToCenY = Input.mousePosition[1] - Screen.height / 2F;
        Vector3 disToCen = new Vector3(disToCenX, disToCenY, 0);
        float rotateOnXAxis = -disToCenY * 2F / Screen.height;
        float rotateOnYAxis = disToCenX * 2F / Screen.width;
        test.transform.Rotate(rb.transform.rotation.x * rotateOnXAxis * 1000, rb.transform.rotation.y * rotateOnYAxis * 1000, 0);*/
        Rigidbody rb2 = test.GetComponent<Rigidbody>();
        Bulette bull = test.GetComponent<Bulette>();
        Vector3 speed = new Vector3();
        speed = /*rb.velocity * 2 +*/ transform.forward * 170;
        bull.speed = speed;
        rb2.velocity = speed;
        bull.SetSender(gameObject);
    }
}
