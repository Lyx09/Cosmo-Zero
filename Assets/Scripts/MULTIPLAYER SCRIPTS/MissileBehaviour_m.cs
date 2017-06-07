using UnityEngine;
using System.Collections;

public class MissileBehaviour_m : MonoBehaviour
{
    public Vector3 targetV3;
    public GameObject sender;
    public Vector3 speed;
    private Rigidbody rb;
    public float dmg;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
    void FixedUpdate()
    {
        Debug.Log(sender.GetComponent<Lock_m>().targetV3);
        transform.Translate(Vector3.forward * Time.deltaTime * 35);
        var rotation = Quaternion.LookRotation(sender.GetComponent<Lock_m>().targetV3 - transform.position);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, 5));
    }

    public void Initialize(GameObject gameObject,float missileDamage)
    {
        sender = gameObject;
        dmg = missileDamage;
    }
       

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var state = hit.GetComponent<State_m>();
        if (state != null)
        {
            if (dmg != 0)
            {
                if (state.currentHealth - dmg <= 0)
                {
                    sender.GetComponent<State_m>().kills++;
                }
                state.TakeDamage(dmg);
                Debug.Log(sender.name + " launched a missile towards " + collision.gameObject.name + " dealing him " + dmg + " damage");
                dmg = 0;
            }
        }
        Destroy(gameObject);
    }
}
