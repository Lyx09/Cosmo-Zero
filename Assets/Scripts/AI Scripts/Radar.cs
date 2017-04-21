using UnityEngine;
using System.Collections;

public class Radar : MonoBehaviour
{
    [SerializeField]
    private Transform radar_center;
    [SerializeField]
    private float radius_radar = 20F;
    [SerializeField]
    private GameObject blip;

    private Transform blips;

    void Start () {
	
	}
	
	void Update ()
	{
	    Collider[] insideRadar = Physics.OverlapSphere(radar_center.position,radius_radar);
	    foreach (Collider collider1 in insideRadar)
	    {
	        Instantiate(blip, blips);
	    }

	}
}
