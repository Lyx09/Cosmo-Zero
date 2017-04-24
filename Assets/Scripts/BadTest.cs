using UnityEngine;
using System.Collections;

public class BadTest : State
{
    public int xpvalue;
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (life <= 0)
            Destroy(gameObject);
	}
}
