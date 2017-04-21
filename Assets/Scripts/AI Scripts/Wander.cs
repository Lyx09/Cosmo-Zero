using UnityEngine;

public class Wander : MonoBehaviour
{
    public float CircleRadius = 1;
    public float TurnChance = 0.05f;
    public float MaxRadius = 5;

    public float Mass = 15;
    public float MaxSpeed = 3;
    public float MaxForce = 15;

    private Vector3 velocity;
    private Vector3 wanderForce;
    private Vector3 target;

    private void Start()
    {
        velocity = Random.onUnitSphere;
        wanderForce = GetRandomWanderForce();
    }

    private void Update()
    {
        var desiredVelocity = GetWanderForce();
        desiredVelocity = desiredVelocity.normalized * MaxSpeed;

        var steeringForce = desiredVelocity - velocity;
        steeringForce = Vector3.ClampMagnitude(steeringForce, MaxForce);
        steeringForce /= Mass;

        velocity = Vector3.ClampMagnitude(velocity + steeringForce, MaxSpeed);
        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity.normalized;

        Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
        Debug.DrawRay(transform.position, desiredVelocity.normalized * 2, Color.magenta);
    }

    private Vector3 GetWanderForce()
    {
        if (transform.position.magnitude > MaxRadius)
        {
            var directionToCenter = (target - transform.position).normalized;
            wanderForce = velocity.normalized + directionToCenter;
        }
        else if (Random.value < TurnChance)
        {
            wanderForce = GetRandomWanderForce();
        }

        return wanderForce;
    }

    private Vector3 GetRandomWanderForce()
    {
        var circleCenter = velocity.normalized;
        var randomPoint = Random.insideUnitCircle;

        var displacement = new Vector3(randomPoint.x, randomPoint.y) * CircleRadius;
        displacement = Quaternion.LookRotation(velocity) * displacement;

        var wanderForce = circleCenter + displacement;
        return wanderForce;
    }
}