using UnityEngine;
using System.Collections;

public class AIBehaviour3 : MonoBehaviour
{
    [SerializeField]
    float speed = 3F;
    [SerializeField]
    float arrival_slow_radius = 20F;
    [SerializeField]
    float rotation_speed = 2F;
    [SerializeField]
    float min_distance = 5F;
    [SerializeField]
    float safe_distance = 50F;
    [SerializeField]
    float w1_time_target_change = 3F;
    [SerializeField]
    float w1_max_random = 20F;
    [SerializeField]
    float w1_min_random = 15F;
    [SerializeField]
    float max_velocity = 15F;
    [SerializeField]
    State self_state;

    private int predict_ahead = 30;

    private Vector3 w1_random_pos;
    private float self_time = 0F;
    private Transform self_transfo;
    private Rigidbody self_rb;
    public Transform target_transform;
    private Vector3 target_velocity;
    private Vector3 target_pos = Vector3.zero;

    //delete this
    public bool ok = true;


    void Start()
    {
        target_velocity = Vector3.zero;
        self_transfo = GetComponent<Transform>();
        self_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 pos = self_transfo.position;
        if (ok)
        {
            self_transfo.position += Seek(target_transform.position);
        }
        else
        {
            self_transfo.position += Pursuit();
        }
        
    }

    Vector3 Seek(Vector3 target_pos)
    {
        Vector3 direction = target_pos - self_transfo.position;
        self_transfo.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotation_speed * Time.deltaTime);
        return direction.normalized * speed * Time.deltaTime;
    }

    Vector3 Flee(Vector3 target_pos)
    {
        Vector3 direction = -target_pos + self_transfo.position;

        if (direction.magnitude < safe_distance)
        {
            self_transfo.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotation_speed * Time.deltaTime);
            return direction.normalized * speed * Time.deltaTime;
        }
        else
        {
            return Vector3.zero;
        }
    }

    Vector3 Arrival()
    {
        Vector3 direction = target_transform.position - self_transfo.position;
        self_transfo.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotation_speed * Time.deltaTime);

        if (direction.magnitude > arrival_slow_radius)
        {
            return direction.normalized * Time.deltaTime * speed;
        }
        else
        {
            float new_speed = ((direction.magnitude - min_distance) / (arrival_slow_radius - min_distance)) * speed;

            if (new_speed < 0)
            {
                new_speed = 0;
            }

            return direction.normalized * Time.deltaTime * new_speed;
        }
    }

    Vector3 Wander1()
    {
        self_time += Time.deltaTime;
        if (self_time >= w1_time_target_change)
        {
            w1_random_pos = self_transfo.position + Random.insideUnitSphere.normalized * Random.Range(w1_min_random, w1_max_random);
            self_time = 0F;
        }
        return Seek(w1_random_pos);
    }

    Vector3 Wander()
    {
        //https://gamedev.stackexchange.com/questions/106737/wander-steering-behaviour-in-3d
        return Vector3.zero;
    }


    //Recqlculate velocity target
    Vector3 Pursuit()
    {
        target_velocity = target_pos - target_transform.position;
        predict_ahead = (int)((target_transform.position - self_transfo.position).magnitude / max_velocity);
        Vector3 future_target_pos = target_transform.position + target_velocity * predict_ahead;
        target_pos = target_transform.position;
        return Seek(future_target_pos);
    }

    Vector3 Evade()
    {
        target_velocity = target_pos - target_transform.position;
        predict_ahead = (int)((target_transform.position - self_transfo.position).magnitude / max_velocity);
        Vector3 future_target_pos = target_transform.position + target_velocity * predict_ahead;
        target_pos = target_transform.position;
        return Flee(future_target_pos);
    }
}
