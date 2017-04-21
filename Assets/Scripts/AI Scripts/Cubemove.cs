using UnityEngine;
using System.Collections;

public class Cubemove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.position += Vector3.right * 0.1F;
	}
}
