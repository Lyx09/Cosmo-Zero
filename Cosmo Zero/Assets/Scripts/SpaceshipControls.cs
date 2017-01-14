using UnityEngine;
using System.Collections;

public class SpaceshipControls : MonoBehaviour
{
    private Rigidbody rb;

    public float maxSpeed = 100;
    public float minSpeed = -100;

    public float RotationSpeed = 100;

    public float acceleration = 1;
    public float deceleration = 1;
    public float thrust = 0;
    



	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    if ()
	    {
	        
	    }

	    float forwardBackward = Input.;


	    if ( && thrust > 0)
	    {
	        thrust -= deceleration;
	    }
        else if ( && thrust < 0)
        {
            thrust += deceleration;
        }
        Debug.Log("thrust: " + thrust);
	}
}
