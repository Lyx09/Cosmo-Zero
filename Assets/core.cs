using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class core : MonoBehaviour
{
    public State state;
    public Text CoreLife;
    public GameObject explosion;

	// Update is called once per frame
	void Update ()
	{
	    CoreLife.text = "Core energy: " + ((state.life / 500) * 100).ToString() + "%";

	    if (state.life <= 0)
	    {
	        GameObject.Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
	    }
	}

    void OnCollisionEnter(Collision collision)
    {
        state.Hurt(5);
    }
}
