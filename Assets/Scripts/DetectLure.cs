using UnityEngine;
using System.Collections;

public class DetectLure : MonoBehaviour
{
    public GameObject missile;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lure"))
        {
            missile.GetComponent<MissileBehaviour>().target = this.transform;
        }
    }
}
