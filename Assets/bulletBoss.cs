using UnityEngine;
using System.Collections;

public class bulletBoss : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<State>().Hurt(10);
            Destroy(gameObject);
        }
    }
}
