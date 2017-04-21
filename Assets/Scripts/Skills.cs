using UnityEngine;
using System.Collections;

public class Skills : MonoBehaviour {

    private Rigidbody rb;
    [SerializeField] private SpaceshipControls spaceShipControls;

    //For doubleTap function
    public bool dashUnlock = true;

    private int buttonCountA = 0;
    private int buttonCountE = 0;
    private int buttonCountZ = 0;
    private int buttonCountS = 0;
    private int buttonCountQ = 0;
    private int buttonCountD = 0;
    private int buttonCountSpace = 0;
    private int buttonCountLCtrl = 0;

    [SerializeField] private float maxCoolDownDash = 0.0F;
    private float coolDownDash = 0.0F;
    private float TapCooldown = 0;
    public float dashIntensity = 5000;

    // For TimeControl
    public bool timeControl = true; //Should not be used in multiplayer
    [SerializeField] private float timeFactor = 0.5F;
    [SerializeField] private float maxTimeUseTime = 5F;
    [SerializeField] private float maxTimeCoolDown = 30F;
    private float timeUseLeft = 0F;
    private float timeCoolDown = 0F;

    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        //################################################################################
        //#--------------------------------DASH MANAGER---------------------------------#
        //################################################################################
        
        //DISPLAY
        //Debug.Log("Dash Cool Down:" + coolDownDash );
        if (!spaceShipControls.blockMovement)
        {


            if (Input.anyKeyDown && dashUnlock)
            {
                // A KEY
                if (Input.GetAxis("Roll") > 0)
                {
                    if (coolDownDash <= 0 && buttonCountA == 1 && TapCooldown > 0)
                    {
                        rb.AddTorque(transform.forward * dashIntensity * rb.mass);
                        coolDownDash = maxCoolDownDash;
                        buttonCountA = 0;
                    }
                    else
                    {
                        if (buttonCountA < 1)
                        {
                            buttonCountA += 1;
                        }
                        TapCooldown = 0.2F;
                    }
                }

                // E KEY
                if (Input.GetAxis("Roll") < 0)
                {
                    if (coolDownDash <= 0 && buttonCountE == 1 && TapCooldown > 0)
                    {
                        rb.AddTorque(-transform.forward * dashIntensity * rb.mass);
                        coolDownDash = maxCoolDownDash;
                        buttonCountE = 0;
                    }
                    else
                    {
                        if (buttonCountE < 1)
                        {
                            buttonCountE += 1;
                        }
                        TapCooldown = 0.2F;
                    }
                }

                // Z KEY
                if (Input.GetAxis("Longitudinal") > 0)
                {
                    if (coolDownDash <= 0 && buttonCountZ == 1 && TapCooldown > 0)
                    {
                        rb.AddForce(transform.forward * dashIntensity * rb.mass);
                        coolDownDash = maxCoolDownDash;
                        buttonCountZ = 0;
                    }
                    else
                    {
                        if (buttonCountZ < 1)
                        {
                            buttonCountZ += 1;
                        }
                        TapCooldown = 0.2F;
                    }
                }

                // S KEY
                if (Input.GetAxis("Longitudinal") < 0)
                {
                    if (coolDownDash <= 0 && buttonCountS == 1 && TapCooldown > 0)
                    {
                        rb.AddForce(-transform.forward * dashIntensity * rb.mass);
                        coolDownDash = maxCoolDownDash;
                        buttonCountS = 0;
                    }
                    else
                    {

                        if (buttonCountS < 1)
                        {
                            buttonCountS += 1;
                        }
                        TapCooldown = 0.2F;
                    }
                }

                // Q KEY
                if (Input.GetAxis("Lateral") < 0)
                {
                    if (coolDownDash <= 0 && buttonCountQ == 1 && TapCooldown > 0)
                    {
                        rb.AddForce(-transform.right * dashIntensity * rb.mass);
                        coolDownDash = maxCoolDownDash;
                        buttonCountQ = 0;
                    }
                    else
                    {
                        if (buttonCountQ < 1)
                        {
                            buttonCountQ += 1;
                        }
                        TapCooldown = 0.2F;
                    }
                }

                // D KEY
                if (Input.GetAxis("Lateral") > 0)
                {
                    if (coolDownDash <= 0 && buttonCountD == 1 && TapCooldown > 0)
                    {
                        rb.AddForce(transform.right * dashIntensity * rb.mass);
                        coolDownDash = maxCoolDownDash;
                        buttonCountD = 0;
                    }
                    else
                    {
                        if (buttonCountD < 1)
                        {
                            buttonCountD += 1;
                        }
                        TapCooldown = 0.2F;
                    }
                }

                // SPACE
                if (Input.GetAxis("Perpendicular") > 0)
                {
                    if (coolDownDash <= 0 && buttonCountSpace == 1 && TapCooldown > 0)
                    {
                        rb.AddForce(transform.up * dashIntensity * rb.mass);
                        coolDownDash = maxCoolDownDash;
                        buttonCountSpace = 0;
                    }
                    else
                    {
                        if (buttonCountSpace < 1)
                        {
                            buttonCountSpace += 1;
                        }
                        TapCooldown = 0.2F;
                    }
                }

                // LEFT CONTROL
                if (Input.GetAxis("Perpendicular") < 0)
                {
                    if (coolDownDash <= 0 && buttonCountLCtrl == 1 && TapCooldown > 0)
                    {
                        rb.AddForce(-transform.up * dashIntensity * rb.mass);
                        coolDownDash = maxCoolDownDash;
                        buttonCountLCtrl = 0;
                    }
                    else
                    {
                        if (buttonCountLCtrl < 1)
                        {
                            buttonCountLCtrl += 1;
                        }
                        TapCooldown = 0.2F;
                    }
                }
            }
        }

	    if (timeControl)
	    {
	        if (Input.GetButton("Time Control") && timeCoolDown <= 0F)
	        {
	            Time.timeScale = timeFactor;
	            timeUseLeft = maxTimeUseTime;
	            timeCoolDown = maxTimeCoolDown + maxTimeUseTime;
	        }

	        if (timeUseLeft <= 0)
	        {
	            Time.timeScale = 1f;
	        }
	    }

        //UPDATING TIMERS

        //dash
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

        //Time Control
	    if (timeUseLeft > 0)
	    {
	        timeUseLeft -= Time.deltaTime * (1/Time.timeScale);
	    }

	    if (timeCoolDown > 0)
	    {
	        timeCoolDown -= Time.deltaTime *(1/ Time.timeScale);
	    }

        //Back
        /*
         * 
        private Vector3[] Pos;


        Pos = new Vector3[60];
        for (int i = 0; i < Pos.Length; i++)
        {
            Pos[i] = transform.position;
        }


        for (int i = 0; i < Pos.Length - 1; i++)
        {
            Pos[i] = Pos[i + 1];
        }
        Pos[Pos.Length - 1] = transform.position;



    */
    }
}
