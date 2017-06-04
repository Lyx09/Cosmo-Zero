using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class SpaceshipControls_m : NetworkBehaviour
{
    //https://gist.github.com/LotteMakesStuff/b8853a16de3e680dc1c326fe6f5ebd7e
    //TODO: Add speed level (Quick acceleration / slow deceleration ?)

    private Rigidbody rb;

    //ESC
    public bool isInMenu = false;

    //Keyboard controls
    public float maxSpeed = 200;
    public float RollSpeed = 0.5F;
    public float acceleration = 2.5F;
    public float deceleration = 2.0F;

    private float timeLong = 0;
    private float timeLat = 0;
    private float timePerp = 0;

    //Mouse Controls
    public float pitchRate = 50F;
    public float yawRate = 100F;

    public float deadZone = 64;
    public bool deadZoneActive = true;

    public bool blockRotation = false;
    public bool blockMovement = false;

    public float rotateOnXAxis;
    public float rotateOnYAxis;

    //Network
    public bool isCurrentPlayer;

    void Start()
    {
        if (!isLocalPlayer)
        {
            isCurrentPlayer = false;
            return;
        }
        else
        {
            rb = GetComponent<Rigidbody>();
            rb.drag = deceleration;
            rb.angularDrag = deceleration;
            isCurrentPlayer = true;
        }
    }

    public override void OnStartLocalPlayer()
    {
        //Change player name etc..
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //TODO set project control settings
        //CANDO Different speed for the 3 axes

        //################################################################################
        //#------------------------------KEYBOARD INPUTS---------------------------------#
        //################################################################################

        blockMovement = isInMenu;
        blockRotation = isInMenu;

        if (!blockMovement)
        {


            // ---------- LONGITUDINAL ----------
            if (Input.GetButton("Longitudinal"))
            {
                if (timeLong > maxSpeed)
                {
                    timeLong = maxSpeed;
                }
                else
                {
                    timeLong += acceleration;
                }
            }
            else
            {
                if (timeLong > 0)
                {
                    timeLong -= 10;
                }
                else
                {
                    timeLong = 0;
                }
            }
            rb.AddForce(transform.forward*Input.GetAxis("Longitudinal")*timeLong*rb.mass);

            // ---------- LATERAL ----------
            if (Input.GetButton("Lateral"))
            {
                if (timeLat > maxSpeed)
                {
                    timeLat = maxSpeed;
                }
                else
                {
                    timeLat += acceleration;
                }
            }
            else
            {
                if (timeLat > 0)
                {
                    timeLat -= 10;
                }
                else
                {
                    timeLat = 0;
                }
            }
            rb.AddForce(transform.right*Input.GetAxis("Lateral")*timeLat*rb.mass);

            // ---------- PERPENDICULAR ----------
            if (Input.GetButton("Perpendicular"))
            {
                if (timePerp > maxSpeed)
                {
                    timePerp = maxSpeed;
                }
                else
                {
                    timePerp += acceleration;
                }
            }
            else
            {
                if (timePerp > 0)
                {
                    timePerp -= 10;
                }
                else
                {
                    timePerp = 0;
                }
            }
            rb.AddForce(transform.up*Input.GetAxis("Perpendicular")*timePerp*rb.mass);

            // ---------- ROLL ---------- 
            //TODO Change values of steps
            //TODO Add a maximum torque
            if (Input.GetButton("Roll"))
            {
                rb.AddTorque(transform.forward*Input.GetAxis("Roll")*RollSpeed*rb.mass);
            }

            //DISPLAY
            /*
            Debug.Log("PERP" + timePerp);
            Debug.Log("LAT" + timeLat);
            Debug.Log("LONG" + timeLong);
            */

            // http://answers.unity3d.com/questions/233850/rigidbody-making-drag-affect-only-horizontal-speed.html
        }

        //################################################################################
        //#--------------------------------MOUSE INPUTS----------------------------------#
        //################################################################################

        if (!blockRotation)
        {
            float disToCenX = Input.mousePosition[0] - Screen.width/2F;
            float disToCenY = Input.mousePosition[1] - Screen.height/2F;
            Vector3 disToCen = new Vector3(disToCenX, disToCenY, 0);

            if (deadZoneActive)
            {
                float deadZoneRadius = Screen.width/deadZone; //relative to screen width
                if (Vector3.Magnitude(disToCen) > deadZoneRadius)
                {
                    rotateOnXAxis = (-disToCenY*2F - deadZoneRadius)/Screen.height;
                    rotateOnYAxis = (disToCenX*2F - deadZoneRadius)/Screen.width;
                    
                    rb.AddTorque(transform.up * rotateOnYAxis * yawRate );
                    rb.AddTorque(transform.right * rotateOnXAxis * pitchRate);
                   
                    //Display
                    /*
                    Debug.Log("PITCH:" + rotateOnXAxis);
                    Debug.Log("YAW:" + rotateOnYAxis);
                    */
                }
            }
            else
            {
                float rotateOnXAxis = (-disToCenY*2F)/Screen.height;
                float rotateOnYAxis = (disToCenX*2F)/Screen.width;

                rb.AddTorque(transform.up * rotateOnYAxis * yawRate);
                rb.AddTorque(transform.right * rotateOnXAxis * pitchRate);
            }

        }



        //https://docs.unity3d.com/ScriptReference/Input-mousePosition.html
        //https://docs.unity3d.com/ScriptReference/Cursor.SetCursor.html
        //rotate cursor

        //For double tapping: http://answers.unity3d.com/questions/340593/how-do-i-make-a-double-tap-system-for-dashing.html
        //For launching missiles https://docs.unity3d.com/ScriptReference/Camera.ScreenToWorldPoint.html
    }
    
}
