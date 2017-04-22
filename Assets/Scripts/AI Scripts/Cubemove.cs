using UnityEngine;
using System.Collections;

public class Cubemove : MonoBehaviour
{
    private float tme = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    //transform.position += Vector3.right * 0.1F + Vector3.forward * 0.05F;
	    transform.position += Vector3.right * Mathf.Sin(tme) * 0.2F + Vector3.forward*Mathf.Cos(tme)*0.2F;
	    tme += Time.deltaTime * 0.5F;
	}
}
