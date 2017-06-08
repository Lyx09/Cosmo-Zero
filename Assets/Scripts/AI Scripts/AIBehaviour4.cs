using UnityEngine;
using System.Collections;

public class AIBehaviour4 : MonoBehaviour //Make it an interface ?
{
    //https://gamedevelopment.tutsplus.com/series/understanding-steering-behaviors--gamedev-12732
    [SerializeField]
    float speed = 3F;
    [SerializeField] private float max_speed = 5F;
    [SerializeField]
    private float self_size = 5F;
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
    [SerializeField]
    Transform target2_transform;

    private Vector3 w1_random_pos;
    private Vector3 target_prevpos = Vector3.zero;
    private float self_time = 0F;
    private Transform self_transfo;
    public Transform target_transform;
    private  Vector3 self_prevpos = Vector3.zero;
    public Transform sphereCast_origin; //Make it automatic Find child with name
    private KeyCode variable;
    private GameObject target;
    private Vector3 steering;

    public float range = 50;

    void Start()
    {
        target = null;
        self_transfo = GetComponent<Transform>();
        self_state = GetComponent<State>();
        self_shooting = GetComponent<Shooting>();
    }

    void OnCollisionEnter(Collision collision) //TODO
    {
        self_state.life -= (int)collision.relativeVelocity.magnitude;
        Debug.Log(collision.relativeVelocity.magnitude);
    }


    void Update()
    {
        if (target == null)
        {
            Collider[] colli = Physics.OverlapSphere(transform.position, 50);
            foreach (Collider collider1 in colli)
            {
                if (collider1.gameObject.tag == "Player")
                {
                    target = collider1.gameObject;
                }
            }
        }
        else
        {
            target_transform = target.transform;
            Seek(target_transform.position);
            Avoid(target_transform.position);
            self_transfo.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(steering), rotation_speed * Time.deltaTime);
            self_transfo.position += self_transfo.forward * 0.35f;
            //self_transfo.position += Vector3.ClampMagnitude(steering, max_speed); //make mass matter
            steering = Vector3.zero;
            if ((target.transform.position - transform.position).magnitude >= 50)
            {
                target = null;
            }
        }
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

    void Pursuit(Vector3 target_pos, float max_velocity_target = 0.2F)
    {
        steering += doPursuit(target_pos, max_velocity_target);
    }

    void Evade(Vector3 target_pos, float max_velocity_target = 0.2F)
    {
        steering += doEvade(target_pos, max_velocity_target);
    }

    void Avoid(Vector3 target_pos, float max_see_ahead = 10F)
    {
        steering += TestAvoid(target_pos);
    }

    void Avoid2(Vector3 target_pos)
    {
        steering += doAvoid2(target_pos);
    }

    //########################################################################

    Vector3 doSeek(Vector3 target_pos , float slowingRadius = 20F, float min_distance = 5F)
    {
        Vector3 direction = target_pos - self_transfo.position;
        //self_transfo.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotation_speed * Time.deltaTime);
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
        Vector3 target_velocity = target_pos - target_prevpos;
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
        Debug.DrawLine(self_transfo.position, self_transfo.position + self_transfo.forward * max_see_ahead, Color.cyan);
        if (Physics.SphereCast(sphereCast_origin.position, self_size / 2F, self_transfo.forward, out hit, max_see_ahead) && hit.transform.name != target_transform.name && hit.transform.name != self_transfo.name)
        {
            avoidance = hit.normal;
            avoidance *= hit.distance * 0.01F; //Change 0.1F
            self_transfo.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(avoidance), rotation_speed * Time.deltaTime);
            Debug.Log("###" + hit.transform.name);
        }
        
        return avoidance;
    }

    Vector3 doAvoid2(Vector3 target_pos)
    {
        RaycastHit hit;
        Vector3 avoidance = Vector3.zero;
        float dynamic_length = 10F; //(self_prevpos - self_transfo.position).magnitude / speed;

        Debug.Log("DYN LENGTH" + dynamic_length);

        if (Physics.SphereCast(sphereCast_origin.position, self_size / 2F, self_transfo.forward, out hit, dynamic_length ) && hit.transform.name != target_transform.name && hit.transform.name != self_transfo.name)
        {
            avoidance = (transform.forward * dynamic_length - hit.transform.position).normalized; // hit.point - hit.transform.position;
            Debug.DrawLine(hit.transform.position, transform.position + transform.forward * dynamic_length, Color.blue);
            Debug.Log("###" + hit.transform.name);
        }
        Debug.Log("AVOIDANCE" + avoidance.magnitude);

        self_prevpos = self_transfo.position;
        return avoidance;
    }

    Vector3 TestAvoid (Vector3 target_pos)
    {
        Vector3 ahead = self_transfo.position + GetComponent<Rigidbody>().velocity * 20.0f;
        Vector3 ahead2 = ahead * 0.5f;
        Vector3 avoidance = Vector3.zero;
        RaycastHit hit;
        if (Physics.SphereCast(sphereCast_origin.position, self_size / 2F, self_transfo.forward, out hit, 20.0f) && hit.transform.name != target_transform.name && hit.transform.name != self_transfo.name)
        {
            if (hit.distance <= ahead.magnitude || hit.distance <= ahead2.magnitude)
            {
                avoidance.x = ahead.x - hit.transform.position.x;
                avoidance.y = ahead.y - hit.transform.position.y;
                avoidance.z = ahead.z - hit.transform.position.z;
                if (avoidance.magnitude > 20.0f)
                {
                    return avoidance.normalized * Time.deltaTime * speed;
                }
                float new_speed = ((avoidance.magnitude - 5.0f) / (10.0f - 5.0f)) * speed;

                if (new_speed < 0) //Also clamp below ? Mathf.Clamp
                {
                    new_speed = 0;
                }

                return avoidance.normalized * Time.deltaTime * new_speed;
            }
        }
        return avoidance;
    }

    //Function rotation ?
}
