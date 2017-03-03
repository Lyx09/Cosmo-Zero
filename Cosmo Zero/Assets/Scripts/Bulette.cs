using UnityEngine;
using System.Collections;

public class Bulette : MonoBehaviour
{
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
        if (Time.time > creation + 1F)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = speed;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            BadTest enemysc = other.gameObject.GetComponent<BadTest>();
            if (enemysc.life <= 1)
            {
                State shooter = sender.GetComponent<State>();
                shooter.Kill(enemysc.xp);
            }
            enemysc.Hurt(1);
        }
    }
}
