using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{

    public float time = 3;
    public float init = 0;
    public AudioSource As;

	void Start ()
	{
        As.Play();
	    init = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if (init < time)
	    {
	        init += Time.deltaTime;
	    }
	    else
	    {
	        Destroy(gameObject);
	    }
	}
}
