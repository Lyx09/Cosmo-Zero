using UnityEngine;
using System.Collections;

public class Skills : MonoBehaviour {

    private Rigidbody rb;
    [SerializeField] private SpaceshipControls spaceShipControls;

    //For doubleTap function
    public static bool dashUnlock = false;

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
    public static bool timeControl = false; //Should not be used in multiplayer
    [SerializeField] private float timeFactor = 0.5F;
    [SerializeField] private float maxTimeUseTime = 5F;
    [SerializeField] private float maxTimeCoolDown = 10F;
    private float timeUseLeft = 0F;
    private float timeCoolDown = 0F;

    //Shield
    public static bool shieldUnlock;
    private float block = 150;
    private float shieldcd = 75.0f; //Cooldown between two uses of the shield
    private float shieldavl; //Time at which the shield will be available
    private float shieldspan = 15.0f; //Time the shield lasts
    private float shieldend; //Time at which the shield will end

    //Stealth
    public static bool stealthUnlock;
    public GameObject stealth;
    private float stealthcd = 75.0f;
    private float stealthavl;
    private float stealthspan = 15.0f;
    private float stealthend;

    //Missile
    public static bool missileUnlock;
    public GameObject Missile;
    public static Transform target;
    public float missilecd = 5.0f;
    public int missiledmg = 7;
    private float missileavl;
    public float missilespan = 4.5f;

    //Lure
    public static bool lureUnlock;
    public GameObject lure;
    public float lurecd = 15.0f;
    private float lureavl;
    public float lurespan = 2.0f;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        shieldavl = Time.time;
        stealthavl = Time.time;
        missileavl = Time.time + missilecd;
        lureavl = Time.time;
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
                if (Input.GetKeyDown(KeyCode.A))
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
                if (Input.GetKeyDown(KeyCode.E))
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
                if (Input.GetKeyDown(KeyCode.Z))
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
                if (Input.GetKeyDown(KeyCode.S))
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
                if (Input.GetKeyDown(KeyCode.Q))
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
                if (Input.GetKeyDown(KeyCode.D))
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
                if (Input.GetKeyDown(KeyCode.Space))
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
                if (Input.GetKeyDown(KeyCode.LeftControl))
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
        if (shieldUnlock)
        {
            if (Time.time >= shieldavl)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    shieldavl = Time.time + shieldcd;
                    shieldend = Time.time + shieldspan;
                    GetComponent<State>().shield.SetActive(true);
                    GetComponent<State>().shieldblock = block;
                }
            }
            if (Time.time >= shieldend)
            {
                GetComponent<State>().shield.SetActive(false);
                GetComponent<State>().shieldblock = 0;
            }
        }
        if (stealthUnlock)
        {
            if (Time.time >+ stealthavl)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    stealthavl = Time.time + stealthavl;
                    stealthend = Time.time + stealthspan;
                    GetComponent<MeshRenderer>().enabled = false;
                    stealth.SetActive(true);
                }
            }
            if (Time.time >= stealthend)
            {
                GetComponent<MeshRenderer>().enabled = true;
                stealth.SetActive(false);
            }
        }
        if (missileUnlock)
        {
            if (Input.GetMouseButtonDown(1) && (Time.time >= missileavl) && GetComponent<Lock>().target != null)
            {
                target = GetComponent<Lock>().target;
                missileavl = Time.time + missilecd;
                SendMissile();
            }
        }
        if (lureUnlock)
        {
            if (Input.GetKeyDown(KeyCode.L) && Time.time >= lureavl)
            {
                lureavl = Time.time + lurecd;
                GameObject mylure = Instantiate(lure);
                mylure.transform.position = transform.position;
                mylure.transform.rotation = transform.rotation;
                mylure.GetComponent<Rigidbody>().velocity = - transform.forward * 30;
                Destroy(mylure, lurespan);
            }
        }
    }
    void SendMissile()
    {
        GameObject mymissile = Instantiate(Missile);
        mymissile.transform.position = gameObject.transform.position + gameObject.transform.forward;
        mymissile.transform.rotation = gameObject.transform.rotation;
        MissileBehaviour mb = mymissile.GetComponent<MissileBehaviour>();
        mb.target = target;
        mb.sender = gameObject;
        mb.dmg = missiledmg;
        Destroy(mymissile, missilespan);
    }
}
