﻿using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{

    private Rigidbody rb;
    public GameObject bullet;
    public float cd;
    public int damage;
    private float timeavl; //Moment at which next shoot will be available;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        timeavl = Time.time + cd;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if ((Input.GetKeyDown(KeyCode.P)) & Time.time >= timeavl)
        {
            timeavl = Time.time + cd;
            Shoot();
        }
	}
    void Shoot()
    {
        GameObject test = Instantiate(bullet);
        Vector3 place = new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z);
        test.transform.position = place;
        test.transform.rotation = rb.transform.rotation;
        Vector3 rot = new Vector3(90, 0, 0);
        test.transform.Rotate(rot);
        Bulette bull = test.GetComponent<Bulette>();
        bull.SetSender(gameObject);
    }
}
