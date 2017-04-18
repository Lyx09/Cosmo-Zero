using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    //private Rigidbody ship;

    //[SerializeField]
    //private float maxMovement = 1F; //not used -> must be used to check camMov
    public float cameraVelocity = 3F;
    public float cameraRollVelocity = 20F;
    public Transform cameraMainPos;
    public Transform cameraRearPos;

    private SpaceshipControls Scontrols;

    //These 3 values must be consistent with the Gravity and Sensitivity of the 3 axis input (Perp, Lat, Long)
    public float inputFactX = 1F;
    public float inputFactY = 0.1F;
    public float inputFactZ = 1F;
    public float inputFactRoll = 1F;

    public float inputMaxX = 1F;
    public float inputMaxY = 0.5F;
    public float inputMaxZ = 1F;
    public float inputMaxRoll = 1F;

    public float inputMinX = -1F;
    public float inputMinY = -0.5F;
    public float inputMinZ = -1F;
    public float inputMinRoll = -1F;

    public bool invertCamMovX = true;
    public bool invertCamMovY = true;
    public bool invertCamMovZ = true;
    public bool invertCamMovRoll = false;

    public float smoothTime = 0.1F; //Should be below 1 

    public float camPitchRate = 2F;
    public float camYawRate = 2F;

    public Vector3 currentPos;
    private bool cameraIsRear = false;
    //private float previousVelo = 0;
    //public float maxShipVelo = 68F;

    void Start ()
	{
	    //ship = GetComponentInParent<Rigidbody>();
	    Scontrols = GetComponentInParent<SpaceshipControls>();
    }
	

	void Update ()
	{
        if (Input.GetButton("Camera unlock"))
        {
            Scontrols.blockRotation = true;

            float disToCenX = Input.mousePosition[0] - Screen.width / 2F;
            float disToCenY = Input.mousePosition[1] - Screen.height / 2F;
            float rotateOnXAxis = (-disToCenY * 2F) / Screen.height;
            float rotateOnYAxis = (disToCenX * 2F) / Screen.width;
            transform.Rotate(-rotateOnXAxis * camPitchRate, -rotateOnYAxis * camYawRate, 0);
        }
        else if (Input.GetButton("Camera rear"))
        {
            Scontrols.blockRotation = true; //could be ommited
            transform.localPosition = cameraRearPos.localPosition ;
            transform.localRotation = Quaternion.Euler(0,180,0);
            cameraIsRear = true;
        }
	    else
        {
            if (cameraIsRear)
            {
                transform.localPosition = currentPos;
                cameraIsRear = false;
            }
            Scontrols.blockRotation = false;
            currentPos = transform.localPosition;

            float inputX = Input.GetAxis("Lateral")*inputFactX;
	        float inputY = Input.GetAxis("Perpendicular")*inputFactY;
	        float inputZ = Input.GetAxis("Longitudinal")*inputFactZ;
	        float inputRoll = Input.GetAxis("Roll")*inputFactRoll;


	        // Min/Max Values
	        //X
	        if (inputX > inputMaxX)
	        {
	            inputX = inputMaxX;
	        }
	        else if (inputX < inputMinX)
	        {
	            inputX = inputMinX;
	        }

	        //Y
	        if (inputY > inputMaxY)
	        {
	            inputY = inputMaxY;
	        }
	        else if (inputY < inputMinY)
	        {
	            inputY = inputMinY;
	        }

	        //Z
	        if (inputZ > inputMaxZ)
	        {
	            inputZ = inputMaxZ;
	        }
	        else if (inputZ < inputMinZ)
	        {
	            inputZ = inputMinZ;
	        }

	        //Roll
	        if (inputRoll > inputMaxRoll)
	        {
	            inputRoll = inputMaxRoll;
	        }
	        else if (inputRoll < inputMinRoll)
	        {
	            inputRoll = inputMinRoll;
	        }

	        //Inversion
	        if (invertCamMovX)
	        {
	            inputX = -inputX;
	        }

	        if (invertCamMovY)
	        {
	            inputY = -inputY;
	        }

	        if (invertCamMovZ)
	        {
	            inputZ = -inputZ;
	        }

	        if (invertCamMovRoll)
	        {
	            inputRoll += 180;
	        }


	        Vector3 camMov = new Vector3(inputX, inputY, inputZ)*cameraVelocity;
	        Vector3 velocity = Vector3.zero;
	        float zVelocity = 0.0F;

	        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, camMov + cameraMainPos.localPosition, ref velocity, smoothTime);

	        float zAngle = Mathf.SmoothDampAngle(transform.localRotation.eulerAngles.z, inputRoll*cameraRollVelocity + cameraMainPos.localRotation.eulerAngles.z, ref zVelocity, smoothTime);
            
	        transform.localRotation = Quaternion.Euler(Scontrols.rotateOnXAxis*1.5F, Scontrols.rotateOnYAxis*1.5F, zAngle);


	        //Some tests below

	        /*
    
            //float shipVeloMagni = ship.velocity.magnitude;
            //Debug.Log(shipVeloMagni);
            if (shipVeloMagni > maxShipVelo && previousVelo < shipVeloMagni+50) //put max velocity without dash
            {
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, (camMov + cameraMainPos.localPosition), ref velocity, smoothTime/ 1000);
                previousVelo = shipVeloMagni;
            }
            else
            {
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, (camMov + cameraMainPos.localPosition), ref velocity, smoothTime);
                previousVelo = shipVeloMagni;
            }
            */


	        /*
            Vector3 movement = camMov + cameraMainPos.localPosition;
    
            transform.localPosition = (movement);
            if (transform.localPosition.magnitude - cameraMainPos.localPosition.magnitude > maxMovement)
            {
                transform.localPosition = (movement).normalized * maxMovement;
            }
            */
	    }

	}
}
