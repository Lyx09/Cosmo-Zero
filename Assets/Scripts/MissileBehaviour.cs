using UnityEngine;
using System.Collections;

public class MissileBehaviour : MonoBehaviour
{
    public Transform target;
    public GameObject sender;
    public Vector3 speed;
    private Rigidbody rb;
    public float dmg;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        if (target.GetComponent<State>() != null)
        {
            target.GetComponent<State>().enemy = gameObject;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }
    void FixedUpdate()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < 10)
            {
                transform.LookAt(target.transform);
                transform.Translate(Vector3.forward * Time.fixedDeltaTime * 20);
            }
            else
            {
                transform.Translate(Vector3.forward * Time.fixedDeltaTime * 20);
                var rotation = Quaternion.LookRotation(target.position - transform.position);
                rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, 5));
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            EnemyScript enemysc = other.gameObject.GetComponent<EnemyScript>();
            if (enemysc.life <= dmg)
            {
                State shooter = sender.GetComponent<State>();
                shooter.Kill(enemysc.xpvalue);
            }
            enemysc.Hurt(dmg);
        }
    }
}
