using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType { Basic, Fast, None}
public class Enemy : MonoBehaviour, IDamagable
{
    private NavMeshAgent agent;

    [SerializeField] private EnemyType enemyType;
    [SerializeField] private Transform centerPoint;

    public int healthPoints = 4;

    [Header("Movement")]
    [SerializeField] private List<Transform> myWaypoints;
    [SerializeField] private float turnSpeed = 10;
    private int nextWaypointIndex;
    private int currentWaypointIndex;

    private float totalDistance;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.avoidancePriority = Mathf.RoundToInt(agent.speed * 10);
    }

    public void SetupEnemy(List<Waypoint> newWaypoints)
    {
        myWaypoints = new List<Transform>(); 

        foreach (var waypoint in newWaypoints)
        {
            myWaypoints.Add(waypoint.transform);
        }

        CollectTotalDistance();
    }

    private void Update()
    {
        Facetarget(agent.steeringTarget);

        //check is the agent is close to the current waypoint target point
        if (ShouldChangeWaypoint())
        {
            //set destination to next waypoint
            agent.SetDestination(GetNextWaypoint());
        }
    }

    private bool ShouldChangeWaypoint()
    {
        if(nextWaypointIndex >= myWaypoints.Count)
            return false; 

        if(agent.remainingDistance < 0.5f)
            return true;

        Vector3 currentWaypoint = myWaypoints[currentWaypointIndex].position;
        Vector3 nextWaypoint = myWaypoints[nextWaypointIndex].position;

        float distanceToNextWaypoint = Vector3.Distance(transform.position, nextWaypoint);
        float distanceBetweenPoints = Vector3.Distance(currentWaypoint, nextWaypoint);

        return distanceBetweenPoints < distanceToNextWaypoint;
    }

    public float DistanceToFinishLine() => totalDistance + agent.remainingDistance;

    private void CollectTotalDistance()
    {
        for (int i = 0; i < myWaypoints.Count - 1; i++)
        {
            float distance = Vector3.Distance(myWaypoints[i].position, myWaypoints[i + 1].position);
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
        if (nextWaypointIndex >= myWaypoints.Count)
        {
            //if true, return the agent's current position, affectively stopping it
            //uncomment the line below to loop the waypoints
            //waypointIndex = 0;
            return transform.position;
        }

        //get the current target point form the waypoints array
        Vector3 targetWaypoint = myWaypoints[nextWaypointIndex].position;

        //if this is not the first waypoint, calculate the distance from the previous waypoint
        if (nextWaypointIndex > 0)
        {
            float distance = Vector3.Distance(myWaypoints[nextWaypointIndex].position, myWaypoints[nextWaypointIndex - 1].position);

            //subtract this distance from the total distance
            totalDistance -= distance;
        }

        //increament the waypoint index to move to the next waypoint on the next call
        nextWaypointIndex++;
        currentWaypointIndex = nextWaypointIndex - 1; //Assign current waypoint index

        //return the current target point
        return targetWaypoint;
    }

    public Vector3 CenterPoint() => centerPoint.position;

    public EnemyType GetEnemyType() => enemyType;

    public void TakeDamage(int damage)
    {
        healthPoints = healthPoints - damage;

        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
