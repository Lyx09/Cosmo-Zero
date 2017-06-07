using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class Bullet_m : MonoBehaviour
{
    private GameObject sender;
    public float dmg = 1337;

    public void Initialize(GameObject go, float bulletDamage)
    {
        sender = go;
        dmg = bulletDamage;
    }

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var state = hit.GetComponent<State_m>();
        if ( state != null)
        {
            if (dmg != 0)
            {
                if (state.currentHealth - dmg <= 0)
                {
                    sender.GetComponent<State_m>().kills++;
                }
                state.TakeDamage(dmg);
                
                Debug.Log(sender.name + " hit " + collision.gameObject.name + " dealing him " + dmg + " damage");
                dmg = 0;
            }
        }
        Destroy(gameObject);
    }
}
