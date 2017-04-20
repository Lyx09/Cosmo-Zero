using UnityEngine;
using System.Collections;

public class MissileBehaviour : MonoBehaviour
{
    public Transform target;
    public GameObject sender;
    public Vector3 speed;
    private Rigidbody rb;
    public int dmg;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Rigidbody>().transform.LookAt(target);
    }
    void FixedUpdate()
    {
        rb.velocity = rb.transform.forward * 50;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            BadTest enemysc = other.gameObject.GetComponent<BadTest>();
            if (enemysc.life <= dmg)
            {
                State shooter = sender.GetComponent<State>();
                shooter.Kill(enemysc.xp);
            }
            enemysc.Hurt(dmg);
        }
    }
}
