using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour
{
    public GameObject target = null;
    public float fireRate = 10;
    public float detection_radius = 20;
    public float hp = 5;
    public float rotation_speed = 1;
    public Shooting shooting;
    public Transform rcOrigin;

    void Start()
    {
        shooting = gameObject.GetComponent<Shooting>();
    }

	void Update ()
	{
	    if (target == null)
	    {
	        Collider[] detected = Physics.OverlapSphere(gameObject.transform.position, detection_radius);
	        foreach (Collider collider in detected)
	        {
	            if (collider.tag == "Player")
	            {
	                target = collider.gameObject;
	            }
	        }
        }
	    else
	    {
	        if ((target.transform.position - gameObject.transform.position).magnitude > detection_radius)
	        {
	            target = null;
	        }
	        else
	        {
                Ray ray = new Ray(rcOrigin.position,target.transform.position - rcOrigin.position);
                Debug.DrawLine(rcOrigin.position, target.transform.position);
                RaycastHit hit;
	            if (Physics.Raycast(ray, out hit, detection_radius))
	            {
                    Debug.DrawLine(ray.origin, hit.point,Color.cyan);
	                if (hit.collider.tag == "Player")
	                {
	                    transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(target.transform.position), rotation_speed * Time.deltaTime);
                        //shooting.Shoot();
	                }
                }
	        }
	    }
	}

    void OnCollisionEnter()
    {
        
    }
}
