using UnityEngine;
using System.Collections;

public class race_m : MonoBehaviour
{
    public int count = 0;

	// Use this for initialization
	void Start ()
	{
	    count = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        count += 1;
        Destroy(other.gameObject);
    }
}
