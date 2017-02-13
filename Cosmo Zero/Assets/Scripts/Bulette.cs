using UnityEngine;
using System.Collections;

public class Bulette : MonoBehaviour {

    private float creation;
    private Rigidbody rb;
    private GameObject sender;
    // Use this for initialization
    public void SetSender(GameObject go)
    {
        sender = go;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        creation = Time.time;
        //rb.AddForce(transform.up * 50);
    }

    void Update()
    {
        if (Time.time > creation + 2.5F)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(transform.up * 20);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            BadTest enemysc = other.GetComponent<BadTest>();
            if (enemysc.life <= 1)
            {
                State shooter = sender.GetComponent<State>();
                shooter.Kill();
            }
            enemysc.Hurt(1);
            Object.Destroy(gameObject);
        }
    }
}
