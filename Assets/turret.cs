using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour
{
    public GameObject target = null;
    public float fireRate = 10;
    public float detection_radius = 20;
    public float hp = 5;
    public float rotation_speed = 1;
    public Transform rcOrigin;

    public float bulletspan = 3;
    public float bulletspeed = 60;
    public GameObject bulletPrefab;
    public AudioSource audioshoot;
    public float bulletcd = 1;

    public GameObject explosion;

    private float timeavl;

    void Start()
    {
        timeavl = Time.time + bulletcd;
    }

	void Update ()
	{
        Debug.DrawLine(transform.position, transform.position + transform.right * 20, Color.cyan);
        if (target == null)
	    {
	        Quaternion finalRot = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), rotation_speed * Time.deltaTime);
	        transform.rotation = Quaternion.Euler(finalRot.eulerAngles.x, finalRot.eulerAngles.y, 0);

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
	                    var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position,transform.up);
	                    targetRotation.x = 0;
                        Quaternion finalRot = Quaternion.Slerp(transform.rotation, targetRotation, rotation_speed * Time.deltaTime);
	                    transform.rotation = Quaternion.Euler(finalRot.eulerAngles.x, finalRot.eulerAngles.y , 0);
	                    if ((Time.time >= timeavl))
	                    {
	                        Shoot();
                        }
	                }
                }
	            else
	            {
	                target = null;
	            }
	        }
	    }
	}

    void OnCollisionEnter(Collision collision)
    {
        Bulette b = collision.gameObject.GetComponent<Bulette>();
        if (b != null)
        {
            hp -= 1;
        }

        MissileBehaviour m = collision.gameObject.GetComponent<MissileBehaviour>();
        if (m != null)
        {
            hp -= m.dmg;
        }

        if (hp <= 0)
        {
            //Turret dies
            GameObject.Instantiate(explosion, transform.position,transform.rotation);

            Destroy(gameObject.transform.parent.gameObject); //destroys parent
            
        }
    }

    public void Shoot()
    {
        timeavl = Time.time + bulletcd;
        audioshoot.Stop();
        audioshoot.Play();

        GameObject newbull = Instantiate(bulletPrefab);
        newbull.transform.position = rcOrigin.position;
        newbull.transform.rotation = transform.rotation;
        newbull.transform.Rotate(90, 0, 0);
        newbull.GetComponent<Rigidbody>().velocity = newbull.transform.up * bulletspeed;
        Destroy(newbull, bulletspan);
    }
}
