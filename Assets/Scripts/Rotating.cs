using UnityEngine;
using System.Collections;

public class Rotating : MonoBehaviour
{

    [SerializeField] private Vector3 rotation;
	
	// Update is called once per frame
	void Update ()
	{
	    transform.Rotate(rotation.x * Time.deltaTime, rotation.y * Time.deltaTime, rotation.z * Time.deltaTime);
	}
}
