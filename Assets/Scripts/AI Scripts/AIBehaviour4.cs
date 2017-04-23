using UnityEngine;
using System.Collections;

public class AIBehaviour4 : MonoBehaviour //Make it an interface ?
{
    [SerializeField]
    float speed = 3F;
    [SerializeField] private float max_speed = 5F;
    [SerializeField]
    private float self_size = 3F;
    [SerializeField]
    private float attack_range = 10F;
    [SerializeField]
    private float safe_range = 10F;
    [SerializeField]
    float rotation_speed = 2F;
    [SerializeField]
    State self_state;
    [SerializeField]
    Shooting self_shooting;

    private Vector3 w1_random_pos;
    private Vector3 target_prevpos = Vector3.zero;
    private float self_time = 0F;
    private Transform self_transfo;
    public Transform target_transform;

    private Vector3 steering;

    void Start()
    {
        self_transfo = GetComponent<Transform>();
        self_state = GetComponent<State>();
        self_shooting = GetComponent<Shooting>();
    }

    void OnCollisionEnter(Collision collision)
    {
        self_state.life -= (int)collision.relativeVelocity.magnitude;
        Debug.Log(collision.relativeVelocity.magnitude);
    }


    void Update()
    {
        // APPLY FORCES HERE

        if (target_transform == null)
        {
            Wander1();
            //Check if ennemy is visible
        }
        else if (self_state.life <= self_state.maxlife/4)
        {
            if ((target_transform.position - self_transfo.position).magnitude > safe_range)
            {
                Wander1();
            }
            else
            {
                Evade(target_transform.position);
            }
        }
        else
        {
            Pursuit(target_transform.position);
            if ((target_transform.position - self_transfo.position).magnitude < attack_range)
            {
                self_shooting.Shoot();
            }
        }

        Avoid(target_transform.position);
        self_transfo.position += Vector3.ClampMagnitude(steering,max_speed); //make mass matter
        steering = Vector3.zero;
    }

    //####################### BEHAVIORS ##############################

    void Seek(Vector3 target_pos, float slowingRadius = 20F)
    {
        steering += doSeek(target_pos , slowingRadius);
    }

    void Flee(Vector3 target_pos, float safe_distance = 50F)
    {
        steering += doFlee(target_pos, safe_distance);
    }

    void Wander1(float w1_time_target_change = 3F, float w1_min_random = 15F, float w1_max_random = 10F)
    {
        steering += doWander1( w1_time_target_change,  w1_min_random ,  w1_max_random);
    }

    void Wander() //To add
    {
        steering += doWander();
    }

    void Pursuit(Vector3 target_pos, float max_velocity_target = 1F)
    {
        steering += doPursuit(target_pos, max_velocity_target);
    }

    void Evade(Vector3 target_pos, float max_velocity_target = 0.2F)
    {
        steering += doEvade(target_pos, max_velocity_target);
    }

    void Avoid(Vector3 target_pos, float max_see_ahead = 10F)
    {
        steering += doAvoid(target_pos, max_see_ahead);
    }

    //########################################################################

    Vector3 doSeek(Vector3 target_pos , float slowingRadius = 20F, float min_distance = 5F)
    {
        Vector3 direction = target_pos - self_transfo.position;
        self_transfo.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotation_speed * Time.deltaTime);
        if (direction.magnitude > slowingRadius)
        {
            return direction.normalized * Time.deltaTime * speed;
        }
        else
        {
            float new_speed = ((direction.magnitude - min_distance) / (slowingRadius - min_distance)) * speed;

            if (new_speed < 0) //Also clamp below ? Mathf.Clamp
            {
                new_speed = 0;
            }

            return direction.normalized * Time.deltaTime * new_speed;
        }
    }

    Vector3 doFlee(Vector3 target_pos, float safe_distance = 50F)
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

    Vector3 doWander1(float w1_time_target_change = 3F, float w1_min_random = 15F, float w1_max_random = 20F)
    {
        self_time += Time.deltaTime;
        if (self_time >= w1_time_target_change)
        {
            w1_random_pos = self_transfo.position + Random.insideUnitSphere.normalized * Random.Range(w1_min_random, w1_max_random);
            self_time = 0F;
        }
        return doSeek(w1_random_pos);
    }

    Vector3 doWander()
    {
        //https://gamedev.stackexchange.com/questions/106737/wander-steering-behaviour-in-3d
        return Vector3.zero;
    }
    
    Vector3 doPursuit(Vector3 target_pos, float max_velocity_target = 0.2F)
    {
        Vector3 target_velocity = target_pos - target_prevpos;
        float predict_ahead = (target_pos - self_transfo.position).magnitude / max_velocity_target;
        Vector3 future_target_pos = target_pos + target_velocity * predict_ahead;
        target_prevpos = target_pos;

        //Debug.Log(target_velocity.magnitude);
        //Debug.Log(predict_ahead);
        Debug.DrawLine(transform.position, future_target_pos);

        return doSeek(future_target_pos);
    }

    Vector3 doEvade(Vector3 target_pos, float max_velocity_target = 0.2F)
    {
        Vector3 target_velocity = target_prevpos - target_pos;
        float predict_ahead = (target_pos - self_transfo.position).magnitude / max_velocity_target;
        Vector3 future_target_pos = target_pos + target_velocity * predict_ahead;
        target_prevpos = target_pos;

        //Debug.Log(target_velocity.magnitude);
        //Debug.Log(predict_ahead);
        //Debug.DrawLine(transform.position, future_target_pos);

        return doFlee(future_target_pos);
    }

    Vector3 doAvoid(Vector3 target_pos, float max_see_ahead = 10F)
    {
        //Vector3 ahead = self_transfo.position + self_transfo.forward * max_see_ahead;
        RaycastHit hit;
        Vector3 avoidance = Vector3.zero;
        if (Physics.SphereCast(self_transfo.position, self_size / 2F, self_transfo.forward, out hit, max_see_ahead) && hit.transform.name != target_transform.name)
        {
            avoidance = hit.normal;
            Debug.DrawLine(hit.point,hit.point + hit.normal * 3);
            avoidance *= hit.distance * 0.01F; //Change 0.1F
            Debug.Log(avoidance.magnitude);
            Debug.DrawLine(self_transfo.position, self_transfo.position + avoidance);
            self_transfo.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(avoidance), rotation_speed * Time.deltaTime);
        }
        
        return avoidance;
    }
}
