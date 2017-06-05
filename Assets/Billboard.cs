using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{

    public Camera playerCam;

	void Update () {
        transform.LookAt(playerCam.transform);
	    transform.localRotation = Quaternion.identity;
	}
}
