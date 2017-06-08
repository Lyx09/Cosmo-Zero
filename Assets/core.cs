using UnityEngine;
using System.Collections;

public class core : MonoBehaviour
{
    public State state;
	
	// Update is called once per frame
	void Update () {
	    if (state.life <= 0)
	    {
	        Destroy(gameObject);
	    }
	}

    void OnCollisionEnter(Collision collision)
    {
        state.Hurt(5);
    }
}
