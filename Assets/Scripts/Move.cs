using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{

    [SerializeField] private Vector3 move;
    [SerializeField] private float speed;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.position += transform.forward * move.x * speed;
        transform.position += transform.right * move.y * speed;
        transform.position += transform.up * move.z * speed;
    }
}
