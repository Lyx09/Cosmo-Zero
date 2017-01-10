using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player1Controls : MonoBehaviour
{

    private int hp;
    private float thrust;
    public float acceleration;
    public float mass;
    public string playerName;
    public string shipName;
    public float friction; // for deceleration ?
    public float maxSpeed;
    public int fp; //Fire power
    public float rotationSpeed;
    private Rigidbody rb;
    public Text UIhp;
    public Text UIthrustGauge;

    // Use this for initialization
    void Start ()
    {
        rotationSpeed = 1.0F;
        rb = GetComponent<Rigidbody>();
        acceleration = 10;
        maxSpeed = 1000;
        hp = 1000;
        fp = 100;
        UIhp.text = "";
        UIthrustGauge.text = "";

    }

    void FixedUpdate()
    {
        float verticalMovement = Input.GetAxis("Vertical");
        thrust += verticalMovement * acceleration;

        // TODO: Add boost
        // TODO: decelerating force

        //BOOST
        if (thrust >= maxSpeed)
        {
            thrust = 1000;
        }
        else if (thrust <= 0)
        {
            thrust = 0;
        }
        Debug.Log("Speed:" + thrust);

        //ROTATION
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0,  rotationSpeed);
        }   
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, 0,  -rotationSpeed);
        }

        //TODO Mouse control
        /*
        Vector3 mouseMovement = (Input.mousePosition - (new Vector3(Screen.width, Screen.height, 0) / 2.0f)) * 1;
        transform.Rotate(new Vector3(-mouseMovement.y, mouseMovement.x, -mouseMovement.x) * 0.025f);
        transform.Translate(Vector3.forward * currrentSpeed);
        */

        Vector3 forwardMovement = new Vector3(0F,0F,thrust/50);
        rb.AddForce(forwardMovement);


    }
    // Update is called once per frame
    void Update () {
	


	}
}
