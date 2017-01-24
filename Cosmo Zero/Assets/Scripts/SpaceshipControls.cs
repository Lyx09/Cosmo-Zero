using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine.EventSystems;

public class SpaceshipControls : MonoBehaviour
{

    //TODO: Warning HIGH MASS MAKE SPACESHIP IMPOSSIBLE TO CONTROL !

    private Rigidbody rb;

    //Keayboard controls
    public float maxSpeed = 200;
    public float RollSpeed = 0.30F;
    public float acceleration = 2.5F;
    public float deceleration = 2.5F;

    private float timeLong = 0;
    private float timeLat = 0;
    private float timePerp = 0;

    //Mouse Controls
    public float pitchRate = 1.5F;
    public float yawRate = 1.5F;

    //For doubleTap function
    private int buttonCountA = 0;
    private int buttonCountE = 0;
    private int buttonCountZ = 0;
    private int buttonCountS = 0;
    private int buttonCountQ = 0;
    private int buttonCountD = 0;
    private int buttonCountSpace = 0;
    private int buttonCountLCtrl = 0;

    private float coolDownDash = 5.0F;
    private float TapCooldown = 0;
    private float dashIntensity = 10000;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = rb.mass * deceleration;
    }


    void Update()
    {
        //################################################################################
        //#--------------------------------DASH MANAGER---------------------------------#
        //################################################################################

        //TODO Make TapCooldown FOR EACH KEY PRESSED !

        Debug.Log("Dash Cool Down:" + coolDownDash );
        if (Input.anyKeyDown )
        {

            // A KEY
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (coolDownDash <= 0 && buttonCountA == 1 && TapCooldown > 0)
                {
                    rb.AddTorque(transform.forward * dashIntensity);
                    coolDownDash = 5.0F;
                    buttonCountA = 0;
                }
                else
                {
                    if (buttonCountA < 1)
                    { buttonCountA += 1; }
                    TapCooldown = 0.2F;
                }
            }
            
            // E KEY
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (coolDownDash <= 0 && buttonCountE == 1 && TapCooldown > 0)
                {
                    rb.AddTorque(-transform.forward * dashIntensity); 
                    coolDownDash = 5.0F;
                    buttonCountE = 0;
                }
                else
                {
                    if (buttonCountE < 1)
                    { buttonCountE += 1; }
                    TapCooldown = 0.2F;
                }
            }

            // Z KEY
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (coolDownDash <= 0 && buttonCountZ == 1 && TapCooldown > 0)
                {
                    rb.AddForce(transform.forward * dashIntensity);
                    coolDownDash = 5.0F;
                    buttonCountZ = 0;
                }
                else
                {
                    if (buttonCountZ < 1)
                    {buttonCountZ += 1;}
                    TapCooldown = 0.2F;
                }
            }

            // S KEY
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (coolDownDash <= 0 && buttonCountS == 1 && TapCooldown > 0)
                {
                    rb.AddForce(-transform.forward * dashIntensity);
                    coolDownDash = 5.0F;
                    buttonCountS = 0;
                }
                else
                {

                    if (buttonCountS < 1)
                    { buttonCountS += 1; }
                    TapCooldown = 0.2F;
                }
            }

            // Q KEY
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (coolDownDash <= 0 && buttonCountQ == 1 && TapCooldown > 0)
                {
                    rb.AddForce(-transform.right * dashIntensity);
                    coolDownDash = 5.0F;
                    buttonCountQ = 0;
                }
                else
                {
                    if (buttonCountQ < 1)
                    { buttonCountQ += 1; }
                    TapCooldown = 0.2F;
                }
            }

            // D KEY
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (coolDownDash <= 0 && buttonCountD == 1 && TapCooldown > 0)
                {
                    rb.AddForce(transform.right * dashIntensity);
                    coolDownDash = 5.0F;
                    buttonCountD = 0;
                }
                else
                {
                    if (buttonCountD < 1)
                    { buttonCountD += 1; }
                    TapCooldown = 0.2F;
                }
            }

            // SPACE
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (coolDownDash <= 0 && buttonCountSpace == 1 && TapCooldown > 0)
                {
                    rb.AddForce(transform.up * dashIntensity);
                    coolDownDash = 5.0F;
                    buttonCountSpace = 0;
                }
                else
                {
                    if (buttonCountSpace < 1)
                    { buttonCountSpace += 1; }
                    TapCooldown = 0.2F;
                }
            }

            // LEFT CONTROL
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (coolDownDash <= 0 && buttonCountLCtrl == 1 && TapCooldown > 0)
                {
                    rb.AddForce(-transform.up * dashIntensity);
                    coolDownDash = 5.0F;
                    buttonCountLCtrl = 0;
                }
                else
                {
                    if (buttonCountLCtrl < 1)
                    { buttonCountLCtrl += 1; }
                    TapCooldown = 0.2F;
                }
            }
        }

        //UPDATING TIMERS
        if (TapCooldown <= 0)
        {
            buttonCountZ = 0;
            buttonCountA = 0;
            buttonCountE = 0;
            buttonCountZ = 0;
            buttonCountS = 0;
            buttonCountQ = 0;
            buttonCountD = 0;
            buttonCountSpace = 0;
            buttonCountLCtrl = 0;
        }
        else
        {
            TapCooldown -= Time.deltaTime;
        }

        if (coolDownDash > 0)
        {
            coolDownDash -= Time.deltaTime;
        }

    }



    void FixedUpdate()
    {


        //TODO set project control settings
        //CANDO Different speed for the 3 axes

        //################################################################################
        //#------------------------------KEYBOARD INPUTS---------------------------------#
        //################################################################################

        // ---------- LONGITUDINAL ----------
        if (Input.GetButton("Longitudinal"))
        {
            if (timeLong > maxSpeed)
            { timeLong = maxSpeed; }
            else
            { timeLong += acceleration; }
        }
        else
        {
            if (timeLong > 0)
            { timeLong -= 10; }
            else
            { timeLong = 0; }
        }
        rb.AddForce(transform.forward * Input.GetAxis("Longitudinal") * timeLong);

        // ---------- LATERAL ----------
        if (Input.GetButton("Lateral"))
        {
            if (timeLat > maxSpeed)
            { timeLat = maxSpeed; }
            else
            { timeLat += acceleration; }
        }
        else
        {
            if (timeLat > 0)
            { timeLat -= 10; }
            else
            { timeLat = 0; }
        }
        rb.AddForce(transform.right * Input.GetAxis("Lateral") * timeLat);

        // ---------- PERPENDICULAR ----------
        if (Input.GetButton("Perpendicular"))
        {
            if (timePerp > maxSpeed)
            { timePerp = maxSpeed; }
            else
            { timePerp += acceleration; }
        }
        else
        {
            if (timePerp > 0)
            { timePerp -= 10; }
            else
            { timePerp = 0; }
        }
        rb.AddForce(transform.up * Input.GetAxis("Perpendicular") * timePerp);

        // ---------- ROLL ---------- 
        //TODO Change values of steps
        if (Input.GetButton("Roll"))
        {
            rb.AddTorque(transform.forward * Input.GetAxis("Roll") * RollSpeed);
        }

        //DISPLAY
        /*
        Debug.Log("PERP" + timePerp);
        Debug.Log("LAT" + timeLat);
        Debug.Log("LONG" + timeLong);
        */

        // http://answers.unity3d.com/questions/233850/rigidbody-making-drag-affect-only-horizontal-speed.html


        //################################################################################
        //#--------------------------------MOUSE INPUTS----------------------------------#
        //################################################################################

        float deadZoneRadius = Screen.width / 32.0F; //relative to screen width

        float disToCenX = Input.mousePosition[0] - Screen.width / 2F;
        float disToCenY = Input.mousePosition[1] - Screen.height / 2F;
        Vector3 disToCen = new Vector3(disToCenX, disToCenY, 0);

        if (Vector3.Magnitude(disToCen) > deadZoneRadius)
        {
            float rotateOnXAxis = -disToCenY * 2F / Screen.height;
            float rotateOnYAxis = disToCenX * 2F / Screen.width;
            transform.Rotate(rotateOnXAxis * pitchRate, rotateOnYAxis * yawRate, 0);
        }

        //Display
        /*
        Debug.Log("PITCH:" + disToCenX * 2F / Screen.width);
        Debug.Log("YAW:" + disToCenY * 2F / Screen.height);
        */


        //https://docs.unity3d.com/ScriptReference/Input-mousePosition.html
        //https://docs.unity3d.com/ScriptReference/Cursor.SetCursor.html
        //rotate cursor

        //For double tapping: http://answers.unity3d.com/questions/340593/how-do-i-make-a-double-tap-system-for-dashing.html
        //For launching missiles https://docs.unity3d.com/ScriptReference/Camera.ScreenToWorldPoint.html
    }
}
