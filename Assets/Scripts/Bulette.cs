using UnityEngine;
using System.Collections;

public class Bulette : MonoBehaviour
{
    private GameObject sender;

    // Use this for initialization
    public void SetSender(GameObject go)
    {
        sender = go;
    }

    void Start()
    {
    }

    void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyScript enemysc = other.gameObject.GetComponent<EnemyScript>();
            if (enemysc == null)
                return;
            if (enemysc.life <= 1)
            {
                State shooter = sender.GetComponent<State>();
                shooter.Kill(enemysc.xpvalue);
            }
            enemysc.Hurt(1);
            Destroy(gameObject);
        }
    }
}
