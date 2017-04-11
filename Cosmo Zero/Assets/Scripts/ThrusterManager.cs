using UnityEngine;
using System.Collections;
using System.Resources;

public class ThrusterManager : MonoBehaviour
{
    private GameObject MainEngine;
    private ParticleSystem ParticleEngine;
    private Light LightEngine;

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

    void Start()
    {
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
                    MainEngine = child.gameObject;
                    break;
            }
        }

        //ParticleEngine = MainEngine.transform.Find("Particle System");
        //LightEngine = MainEngine.transform.Find("Spotlight");
    }

    // Update is called once per frame
    void Update()
    {
        
        //AnimationCurve curve = new AnimationCurve();
        //curve.AddKey(1.0f, 0.0f);
        //curve.AddKey(0.0f, playerrb.velocity/200f);
        //ParticleEngine.sizeOverLifetime(curve);


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
            //Nothing here Engine already working on it
        }
        else if (Input.GetAxis("Longitudinal") < -threshold)
        {
            DLF = true;
            ULF = true;
            URF = true;
            DLF = true;
        }


        // ---------- LATERAL ----------
        if (Input.GetAxis("Lateral") > threshold)
        {
            ULF = true;
            ULB = true;
            DLB = true;
            DLB = true;
        }
        else if (Input.GetAxis("Lateral") < -threshold)
        {
            URF = true;
            URB = true;
            DRB = true;
            DRF = true;
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

        if (RollDL && RollDR)
        {
            RollDR = false;
            RollDR = false;
            RollUL = false;
            RollUR = false;
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
