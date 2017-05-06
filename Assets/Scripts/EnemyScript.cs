using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public float life;
    public int xpvalue;
	// Use this for initialization
	void Start ()
    {
        if (life <= 0)
            Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (life <= 0)
            Destroy(gameObject);
    }
    public void Hurt(float power) //A appeler quand le vaisseau est touché
    {
        life -= power;
    }
}
