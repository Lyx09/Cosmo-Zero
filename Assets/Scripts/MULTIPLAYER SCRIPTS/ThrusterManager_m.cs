using UnityEngine;
using System.Collections;
using System.Resources;
using System.Security;

public class ThrusterManager_m : MonoBehaviour
{
    //TODO: Add flare to main engine

    //Main engine
    private GameObject mainEngine;
    private ParticleSystem particleEngine;
    private LensFlare lensFlare;
    private Light lightEngine;

    [Tooltip("Please indicate the maximum speed the ship can reach. Used for main engine")]
    public float maxSpeed = 100f; // float topVelocity = ((addedForce.magnitude / rigidbody.drag) - Time.fixedDeltaTime * addedForce.magnitude) / rigidbody.mass;

    private Rigidbody playerrb;
    //Forward
    private GameObject ThrusterDLF;
    private GameObject ThrusterULF;
    private GameObject ThrusterURF;
    private GameObject ThrusterDRF;

    //Backward
    private GameObject ThrusterDLB;
    private GameObject ThrusterULB;
    private GameObject ThrusterURB;
    private GameObject ThrusterDRB;

    //Roll
    private GameObject ThrusterRollUR;
    private GameObject ThrusterRollUL;
    private GameObject ThrusterRollDR;
    private GameObject ThrusterRollDL;

    //BOOLS
    private bool URF = true;
    private bool URB = true;
    private bool ULF = true;
    private bool ULB = true;
    private bool DRF = true;
    private bool DRB = true;
    private bool DLF = true;
    private bool DLB = true;
    private bool RollDR = true;
    private bool RollDL = true;
    private bool RollUL = true;
    private bool RollUR = true;

    [Tooltip("Time needed for the ship to deactivate the thrusters, should be between 0 and 1")]
    [Range(0,1)]
    public float threshold = 0.1f;

    [SerializeField] private Transform cameraTransform;
    public CameraScript_m cameraScript;
    private Vector3 originalCamPos;

    void Start()
    {
        originalCamPos = cameraTransform.localPosition;
        playerrb = GetComponent<Rigidbody>();

        foreach (Transform child in transform)
        {
            switch (child.tag)
            {
                case "ThrusterDLF":
                    ThrusterDLF = child.gameObject;
                    //Debug.Log("OK DLF");
                    break;
                case "ThrusterULF":
                    ThrusterULF = child.gameObject;
                    //Debug.Log("OK ULF");
                    break;
                case "ThrusterURF":
                    ThrusterURF = child.gameObject;
                    //Debug.Log("OK URF");
                    break;
                case "ThrusterDRF":
                    ThrusterDRF = child.gameObject;
                    //Debug.Log("OK DRF");
                    break;
                case "ThrusterDLB":
                    //Debug.Log("OK DLB");
                    ThrusterDLB = child.gameObject;
                    break;
                case "ThrusterULB":
                    ThrusterULB = child.gameObject;
                    //Debug.Log("OK ULB");
                    break;
                case "ThrusterURB":
                    ThrusterURB = child.gameObject;
                    //Debug.Log("OK URB");
                    break;
                case "ThrusterDRB":
                    ThrusterDRB = child.gameObject;
                    //Debug.Log("OK DRB");
                    break;
                case "ThrusterRollUR":
                    ThrusterRollUR = child.gameObject;
                    //Debug.Log("OK RollUR");
                    break;
                case "ThrusterRollUL":
                    ThrusterRollUL = child.gameObject;
                    //Debug.Log("OK RollUL");
                    break;
                case "ThrusterRollDL":
                    ThrusterRollDL = child.gameObject;
                    //Debug.Log("OK RollDL");
                    break;
                case "ThrusterRollDR":
                    ThrusterRollDR = child.gameObject;
                    //Debug.Log("OK RollDR");
                    break;
                case "Engine":
                    mainEngine = child.gameObject;
                    //Debug.Log("OK Main Engine");
                    break;
            }
        }

        particleEngine =  mainEngine.GetComponent<ParticleSystem>();
        lightEngine = mainEngine.GetComponent<Light>();
        lensFlare = mainEngine.GetComponent<LensFlare>();
    }
    

    void Update()
    {
        Vector3 projected = Vector3.Project(playerrb.velocity, transform.forward);
        float relativeSpeed = projected.magnitude/maxSpeed; //current speed compared to maxSpeed
        //Debug.Log(Vector3.Angle(projected, transform.forward));
        
        if (relativeSpeed > 3 ) //prevents from being too bright
        {
            relativeSpeed = 3;
        }
        //Debug.Log(relativeSpeed);

        if (Vector3.Angle(projected, transform.forward) < 95) //Only works when going forward
        {
            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(1.0f, 0.0f);
            curve.AddKey(0.0f, relativeSpeed);//max speed

            var sol = particleEngine.sizeOverLifetime;
            sol.size = new ParticleSystem.MinMaxCurve(1.5f, curve);

            lightEngine.range = relativeSpeed * 10;
            lensFlare.brightness = relativeSpeed / 2.2F;
        }

        if (relativeSpeed > 0.15)
        {
            if (relativeSpeed > 0.3)
            {
                relativeSpeed = 0.3F;
            }
            Vector3 zero = Vector3.zero;
            cameraTransform.localPosition = Vector3.SmoothDamp(cameraTransform.localPosition, cameraScript.currentPos + Random.insideUnitSphere * (relativeSpeed - 0.15F) * 0.2F , ref zero , 0.03F);
        }

        ULF = false;
        ULB = false;
        URF = false;
        URB = false;
        DLF = false;
        DLB = false;
        DRF = false;
        DRB = false;
        RollDR = false;
        RollDL = false;
        RollUL = false;
        RollUR = false;

        // ---------- LONGITUDINAL ----------
        if (Input.GetAxis("Longitudinal") > threshold)
        {
            //Main Engine wrking on it
        }
        else if (Input.GetAxis("Longitudinal") < -threshold)
        {
            DLB = true;
            ULB = true;
            URB = true;
            DRB = true;
        }
        else
        {
            DLB = false;
            ULB = false;
            URB = false;
            DRB = false;
        }
        


        // ---------- LATERAL ----------
        if (Input.GetAxis("Lateral") > threshold)
        {
            ULF = true;
            ULB = true;
            DLB = true;
            DLF = true;
        }
        else if (Input.GetAxis("Lateral") < -threshold)
        {
            URF = true;
            URB = true;
            DRB = true;
            DRF = true;
        }
        else
        {
            URF = false;
            URB = false;
            DRB = false;
            DRF = false;
            ULF = false;
            ULB = false;
            DLB = false;
            DLF = false;
        }

        
        // ---------- PERPENDICULAR ----------
        if (Input.GetAxis("Perpendicular") > threshold)
        {
            DLF = true;
            DLB = true;
            DRB = true;
            DRF = true;

            RollDL = true;
            RollDR = true;
        }
        else if (Input.GetAxis("Perpendicular") < -threshold)
        {
            ULF = true;
            ULB = true;
            URB = true;
            URF = true;

            RollUL = true;
            RollUR = true;
        }
        else
        {
            DLF = false;
            DLB = false;
            DRB = false;
            DRF = false;

            RollDL = false;
            RollDR = false;

            ULF = false;
            ULB = false;
            URB = false;
            URF = false;

            RollUL = false;
            RollUR = false;
        }


        // ---------- ROLL ---------- 

        if (Input.GetAxis("Roll") > threshold)
        {
            RollDR = true;
            RollUL = true;
        }
        else if (Input.GetAxis("Roll") < -threshold)
        {
            RollDL = true;
            RollUR = true;
        }
        else
        {
            RollDL = false;
            RollUR = false;
            RollDR = false;
            RollUL = false;
        }


        //CHECKS
        if (ULF && DRB)
        {
            ULF = false;
            DRB = false;
        }

        if (URF && DLB)
        {
            URF = false;
            DLB = false;
        }

        if (ULB && DRF)
        {
            ULB = false;
            DRF = false;
        }

        if (URB && DLF)
        {
            URB = false;
            DLF = false;
        }

        ThrusterULB.SetActive(ULB);
        ThrusterDLF.SetActive(DLF);
        ThrusterDLB.SetActive(DLB);
        ThrusterDRB.SetActive(DRB);
        ThrusterDRF.SetActive(DRF);
        ThrusterULF.SetActive(ULF);
        ThrusterURF.SetActive(URF);
        ThrusterURB.SetActive(URB);
        ThrusterDRB.SetActive(DRB);
        ThrusterRollDL.SetActive(RollDL);
        ThrusterRollDR.SetActive(RollDR);
        ThrusterRollUL.SetActive(RollUL);
        ThrusterRollUR.SetActive(RollUR);
      
    }
}
