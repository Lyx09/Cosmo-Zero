using UnityEngine;
using System.Collections;
using System.IO;

public class AIBehaviourTest : MonoBehaviour
{
    public float max_velocity = 10F;
    public float max_speed = 10F;
    public float max_force = 10F;
    public float rotation_speed = 1.5F;
    public float arrival_slow_radius = 20F;
    public float min_distance = 10F;
    public Transform target;
    private Vector3 velocity;
    private Rigidbody rb;

	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
	{
	    Vector3 position = transform.position;

	    //Actions
        Vector3 steering = Seek(target.position);
	    steering = Vector3.ClampMagnitude(steering, max_force) / rb.mass;
        velocity = Vector3.ClampMagnitude((velocity + steering), max_speed);
        Debug.Log((velocity * Time.deltaTime).magnitude);
        transform.position += velocity * Time.deltaTime;

        velocity = transform.position - position;
	}

    Vector3 Seek(Vector3 target_position)
    {
        Vector3 desired_velocity = (target_position - transform.position).normalized * max_velocity;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desired_velocity), rotation_speed * Time.deltaTime);
        return (desired_velocity - velocity);
    }

    Vector3 Flee(Vector3 target_position)
    {
        Vector3 desired_velocity = -(target_position - transform.position).normalized * max_velocity;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desired_velocity), rotation_speed * Time.deltaTime);
        return (desired_velocity - velocity);
    }

    Vector3 Arrival(Vector3 target_position)
    {
        Vector3 desired_velocity = (target_position - transform.position).normalized * max_velocity;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desired_velocity), rotation_speed * Time.deltaTime);

        if (desired_velocity.magnitude > arrival_slow_radius)
        {
            return (desired_velocity - velocity);
        }
        else
        {
            Vector3 steering = (desired_velocity - velocity) * ((desired_velocity.magnitude - min_distance) / (arrival_slow_radius - min_distance));
            
            if (steering.magnitude < 0)
            {
                steering = Vector3.zero;
            }
            
            return (steering - velocity);
        }
    }

}
