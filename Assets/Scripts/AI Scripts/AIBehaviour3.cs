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
    State slef_state;

    private int predict_ahead = 30;

    private Vector3 w1_random_pos;
    private float self_time = 0F;
    private Transform self_transfo;
    private Rigidbody self_rb;
    public Transform target_transform;
    private Vector3 target_velocity;
    private Vector3 target_pos = Vector3.zero;

    void Start()
    {
        target_velocity = Vector3.zero;
        self_transfo = GetComponent<Transform>();
        self_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 pos = self_transfo.position;
        Pursuit();
        //Debug.DrawLine(transform.position,transform.position+self_rb.velocity);
    }

    void Seek(Vector3 target_pos)
    {
        //attack if close enough

        Vector3 direction = target_pos - self_transfo.position;
        self_transfo.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotation_speed * Time.deltaTime);

        self_transfo.position += direction.normalized * speed * Time.deltaTime;
    }

    void Flee(Vector3 target_pos)
    {
        Vector3 direction = -target_pos + self_transfo.position;

        if (direction.magnitude < safe_distance)
        {
            self_transfo.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotation_speed * Time.deltaTime);
            self_transfo.position += direction.normalized * speed * Time.deltaTime;
        }
    }

    void Arrival()
    {
        Vector3 direction = target_transform.position - self_transfo.position;
        self_transfo.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotation_speed * Time.deltaTime);

        if (direction.magnitude > arrival_slow_radius)
        {
            self_transfo.position += direction.normalized * Time.deltaTime * speed;
        }
        else
        {
            float new_speed = ((direction.magnitude - min_distance) / (arrival_slow_radius - min_distance)) * speed;

            if (new_speed < 0)
            {
                new_speed = 0;
            }

            self_transfo.position += direction.normalized * Time.deltaTime * new_speed;
        }
    }

    void Wander1()
    {
        self_time += Time.deltaTime;
        if (self_time >= w1_time_target_change)
        {
            w1_random_pos = self_transfo.position + Random.insideUnitSphere.normalized * Random.Range(w1_min_random, w1_max_random);
            self_time = 0F;
        }
        else
        {
            Seek(w1_random_pos);
        }
    }

    void Wander2()
    {
        //https://gamedev.stackexchange.com/questions/106737/wander-steering-behaviour-in-3d
    }

    void Pursuit()
    {
        target_velocity = target_pos - target_transform.position;
        predict_ahead = (int)((target_transform.position - self_transfo.position).magnitude / max_velocity);
        Vector3 future_target_pos = target_transform.position + target_velocity * predict_ahead;
        Seek(future_target_pos);
        target_pos = target_transform.position;
    }

    void Evade()
    {
        target_velocity = target_pos - target_transform.position;
        predict_ahead = (int)((target_transform.position - self_transfo.position).magnitude / max_velocity);
        Vector3 future_target_pos = target_transform.position + target_velocity * predict_ahead;
        Flee(future_target_pos);
        target_pos = target_transform.position;
    }
}
