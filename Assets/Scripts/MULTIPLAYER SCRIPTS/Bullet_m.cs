using UnityEngine;
using System.Collections;

public class Bullet_m : MonoBehaviour
{
    private GameObject sender;

    public void SetSender(GameObject go)
    {
        sender = go;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy")) //OR PLAYER
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
