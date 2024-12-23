using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    public Transform[] GetWaypoints() => waypoints;

}
