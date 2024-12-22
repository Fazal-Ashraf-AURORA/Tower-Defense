using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private Transform[] waypoint;
    [SerializeField] private float turnSpeed = 10;
    private int waypointIndex;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.avoidancePriority = Mathf.RoundToInt(agent.speed * 10);
    }

    private void Update()
    {
        Facetarget(agent.steeringTarget);

        //check is the agent is close to the current waypoint target point
        if(agent.remainingDistance  < 0.2f)
        {
            //set destination to next waypoint
            agent.SetDestination(GetNextWaypoint());
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
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, turnSpeed* Time.deltaTime);
    }

    private Vector3 GetNextWaypoint()
    {
        if (waypointIndex >= waypoint.Length)
        {
            return transform.position;
        }

        Vector3 targetWaypoint = waypoint[waypointIndex].position;
        waypointIndex++;

        return targetWaypoint;
    }
}
