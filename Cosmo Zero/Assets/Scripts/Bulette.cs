using UnityEngine;
using System.Collections;

public class Bulette : MonoBehaviour {

    private float creation;
    private Rigidbody rb;
    private GameObject sender;
    public Vector3 speed;
    // Use this for initialization
    public void SetSender(GameObject go)
    {
        sender = go;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        creation = Time.time;
        rb.AddForce(transform.up * 50);
    }

    void Update()
    {
        if (Time.time > creation + 1.25F)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            BadTest enemysc = other.GetComponent<BadTest>();
            if (enemysc.life <= 1)
            {
                State shooter = sender.GetComponent<State>();
                shooter.Kill();
            }
            enemysc.Hurt(1);
        }
    }
}
