using UnityEngine;
using System.Collections;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] float max_velocity = 3F;
    [SerializeField] float min_velocity = 0.5F;
    [SerializeField] float acceleration = 0.1F;
    [SerializeField] float rotation_speed = 2F;
    [SerializeField] float min_distance = 5F;
    [SerializeField] float max_distance = 5F;
    private Transform self_transfo;
    private Rigidbody self_rb;
    public Transform target;
    
	void Start ()
	{
	    self_transfo = GetComponent<Transform>();
	    self_rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
	    Flee();
	}

    void Seek()
    {
        //attack if close enough

        Vector3 direction = target.position - self_transfo.position;
        self_transfo.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotation_speed*Time.deltaTime);
        
        if (direction.magnitude > min_distance)
        {
            float move_force_by_distance = direction.magnitude * acceleration;
            if (move_force_by_distance > max_velocity)
            {
                move_force_by_distance = max_velocity;
            }
            else if (move_force_by_distance < min_velocity)
            {
                move_force_by_distance = min_velocity;
            }

            self_transfo.position += direction.normalized * move_force_by_distance * Time.deltaTime;
        }
    }

    void Flee()
    {
        Vector3 direction = - target.position + self_transfo.position;
        self_transfo.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotation_speed * Time.deltaTime);

        if (direction.magnitude > max_distance)
        {
            float move_force_by_distance = - direction.magnitude * acceleration + max_velocity;
            Debug.Log(move_force_by_distance);
            if (move_force_by_distance > max_velocity)
            {
                move_force_by_distance = min_velocity;
            }
            else if (move_force_by_distance < min_velocity)
            {
                move_force_by_distance = max_velocity;
            }

            self_transfo.position += direction.normalized * move_force_by_distance * Time.deltaTime;
        }
    }
}
