using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] waypoint;
    private int waypointIndex;

    private NavMeshAgent agent;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
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
        transform.forward = newTarget - transform.position;
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
