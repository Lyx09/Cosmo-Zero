using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class Bullet_m : MonoBehaviour
{
    private GameObject sender;
    public float dmg = 999999;

    public void Initialize(GameObject go, float bulletDamage)
    {
        sender = go;
        dmg = bulletDamage;
        Debug.Log(1);
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
            enemysc.Hurt(dmg);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<State_m>().Hurt(dmg);
            Debug.Log(dmg);
            Debug.Log(sender.GetComponent<State_m>().life);
            Debug.Log(other.gameObject.GetComponent<State_m>().life);
            Debug.Log(sender.name + " hit " + other.gameObject.name + "dealing him "  + dmg + "damage");
        }
    }
}
