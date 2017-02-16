using UnityEngine;
using System.Collections;

public class BadTest : State {

	// Use this for initialization
	void Start ()
    {
        life = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (life <= 0)
            gameObject.SetActive(false);
	}
}
