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
        
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 35);
        var rotation = Quaternion.LookRotation(target.position - transform.position);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, 5));
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
                Debug.Log("ok");
            }
            enemysc.Hurt(dmg);
        }
    }
}
