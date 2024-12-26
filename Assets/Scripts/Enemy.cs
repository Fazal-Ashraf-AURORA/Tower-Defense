using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    private NavMeshAgent agent;

    public int healthPoints = 4;

    [Header("Movement")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float turnSpeed = 10;
    private int waypointIndex;

    private float totalDistance;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.avoidancePriority = Mathf.RoundToInt(agent.speed * 10);
    }

    private void Start()
    {
        waypoints = FindAnyObjectByType<WaypointManager>().GetWaypoints();
        CollectTotalDistance();
    }
    private void Update()
    {
        Facetarget(agent.steeringTarget);

        //check is the agent is close to the current waypoint target point
        if (agent.remainingDistance < 0.5f)
        {
            //set destination to next waypoint
            agent.SetDestination(GetNextWaypoint());
        }
    }

    public float DistanceToFinishLine() => totalDistance + agent.remainingDistance;

    private void CollectTotalDistance()
    {
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            float distance = Vector3.Distance(waypoints[i].position, waypoints[i + 1].position);
            totalDistance += distance;
        }
    }

    private void Facetarget(Vector3 newTarget)
    {
        //calculate the direction from current position to newtarget
        Vector3 directionToTarget = newTarget - transform.position;
        directionToTarget.y = 0;//Ignore vertical component

        //Create a rotation that points the forward vector up the calculated direction
        Quaternion newRotation = Quaternion.LookRotation(directionToTarget);

        //Smoothly rotates from current rotation to new rotation with defined turnSpeed// Time.deltaTime makes it framerate independent
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, turnSpeed * Time.deltaTime);
    }

    private Vector3 GetNextWaypoint()
    {
        //check if the waypoint index is beyond the last waypoint
        if (waypointIndex >= waypoints.Length)
        {
            //if true, return the agent's current position, affectively stopping it
            //uncomment the line below to loop the waypoints
            //waypointIndex = 0;
            return transform.position;
        }

        //get the current target point form the waypoints array
        Vector3 targetWaypoint = waypoints[waypointIndex].position;

        //if this is not the first waypoint, calculate the distance from the previous waypoint
        if (waypointIndex > 0)
        {
            float distance = Vector3.Distance(waypoints[waypointIndex].position, waypoints[waypointIndex - 1].position);

            //subtract this distance from the total distance
            totalDistance -= distance;
        }

        //increament the waypoint index to move to the next waypoint on the next call
        waypointIndex++;

        //return the current target point
        return targetWaypoint;
    }

    public void TakeDamage(int damage)
    {
        healthPoints = healthPoints - damage;

        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
