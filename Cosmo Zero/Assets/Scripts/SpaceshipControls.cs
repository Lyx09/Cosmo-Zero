using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public class SpaceshipControls : MonoBehaviour
{
    private Rigidbody rb;

    public float maxSpeed = 100;
    public float minSpeed = -100;

    float dragLongi = 1;
    float dragLat= 1;
    float dragPerp = 1;

    public float RotationSpeed = 1;

    public float acceleration = 1;
    public float deceleration = 1;

    float forwardBackward = 0;
    float leftRight = 0;
    float upDown = 0;


    void Start ()
	{
	    rb = GetComponent<Rigidbody>();

	}
	
	void FixedUpdate ()
	{
        //TODO set project control settings
        //CANDO Different speed for the 3 axes

        float magnitudeLongi = Vector3.Magnitude(Vector3.Project(rb.velocity, transform.forward));
        float magnitudePerp = Vector3.Magnitude(Vector3.Project(rb.velocity, transform.up));
        float magnitudeLat = Vector3.Magnitude(Vector3.Project(rb.velocity, transform.right));
	    float magnitudeRoll = Vector3.Magnitude(rb.angularVelocity);

        // LONGITUDINAL
        if (Input.GetButton("Longitudinal") )
	    {
            if (magnitudeLongi < 10F)
	        {
                rb.AddForce(transform.forward * Input.GetAxis("Longitudinal") * 12);
            }
	        else if (magnitudeLongi >= 10F && magnitudeLongi <= 20F)
            {
                rb.AddForce(transform.forward * Input.GetAxis("Longitudinal") * 22);
            }
            else
            {
                rb.AddForce(transform.forward * Input.GetAxis("Longitudinal") * 35);
            }
            Debug.Log("magnitudeLongi" + "\"" + magnitudeLongi + "\"");
        }
        //else here


        // PERPENDICULAR 
        if (Input.GetButton("Perpendicular"))
        {
            if (magnitudePerp < 10F)
            {
                rb.AddForce(transform.up * Input.GetAxis("Perpendicular") * 12);
            }
            else if (magnitudePerp >= 10F && magnitudePerp <= 20F)
            {
                rb.AddForce(transform.up * Input.GetAxis("Perpendicular") * 22);
            }
            else
            {
                rb.AddForce(transform.up * Input.GetAxis("Perpendicular") * 35);
            }
            Debug.Log("magnitudePerp" + "\"" + magnitudePerp + "\"");
        }

        // LATERAL
        if (Input.GetButton("Lateral"))
        {
            if (magnitudeLat < 10F)
            {
                rb.AddForce(transform.right * Input.GetAxis("Lateral") * 12);
            }
            else if (magnitudeLat >= 10F && magnitudeLat <= 20F)
            {
                rb.AddForce(transform.right * Input.GetAxis("Lateral") * 22);
            }
            else
            {
                rb.AddForce(transform.right * Input.GetAxis("Lateral") * 35);
            }
        }

        // ROLL 
        //TODO Change values of steps
        if (Input.GetButton("Roll"))
        {
            if (magnitudeRoll < 10F)
            {
                rb.AddTorque(transform.forward * Input.GetAxis("Roll") * 0.50F);
            }

            /*
             * else if (magnitudeRoll >= 10F && magnitudeRoll <= 20F)
            {
                rb.AddTorque(transform.forward * Input.GetAxis("Roll") * 5);
            }
            else
            {
                rb.AddTorque(transform.forward * Input.GetAxis("roll") * 10);
            }
            */
        }

        // Display
        Debug.Log("LONG : " + magnitudeLongi);
        Debug.Log("PERP : " + magnitudePerp);
        Debug.Log("LATE : " + magnitudeLat);
        Debug.Log("Roll : " + magnitudeRoll);
        Debug.Log("--------------------------");



        // http://answers.unity3d.com/questions/233850/rigidbody-making-drag-affect-only-horizontal-speed.html

        //https://docs.unity3d.com/ScriptReference/Input-mousePosition.html
        //https://docs.unity3d.com/ScriptReference/Cursor.SetCursor.html
        //rotate cursor

        //For double tapping: http://answers.unity3d.com/questions/340593/how-do-i-make-a-double-tap-system-for-dashing.html


        //For launching missiles https://docs.unity3d.com/ScriptReference/Camera.ScreenToWorldPoint.html

        //Debug.Log("Velocity magnitude " + "\"" + rb.velocity.magnitude + "\"" );

        //Debug.Log("Colinearity " + "\"" + Vector3.Dot(rb.velocity, transform.forward) + "\"");
    }
}
