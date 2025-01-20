using System.Collections.Generic;
using UnityEngine;

public class EnemyPortal : MonoBehaviour
{
    [SerializeField] private float spawnCooldown;
    private float spawnTimer;

    [Space]

    [SerializeField] private List<Waypoint> waypointList;

    private List<GameObject> enemiesToCreate = new List<GameObject>();

    private void Awake()
    {
        CollectWaypoints();
    }

    private void Update()
    {
        if(CanMakeNewEnemies())
            CreateEnemy();
    }

    private bool CanMakeNewEnemies()
    {
        spawnTimer -= Time.deltaTime;

        if(spawnTimer <= 0 && enemiesToCreate.Count > 0)
        {
            spawnTimer = spawnCooldown;
            return true;
        }
        return false;
    }

    private void CreateEnemy()
    {
        GameObject randomEnemy = GetRandomEnemy();
        GameObject enemy = Instantiate(randomEnemy, transform.position, Quaternion.identity);

        Enemy enemyScript = enemy.GetComponent<Enemy>();

        enemyScript.SetupEnemy(waypointList);
    }

    private GameObject GetRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemiesToCreate.Count);
        GameObject choosenEnemy = enemiesToCreate[randomIndex];

        enemiesToCreate.Remove(choosenEnemy);

        return choosenEnemy;
    }

    public void AddEnemy(GameObject enemyToAdd) => enemiesToCreate.Add(enemyToAdd);

    [ContextMenu("Collect Waypoints")]
    private void CollectWaypoints()
    {
        waypointList = new List<Waypoint>();

        foreach (Transform child in transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if (waypoint != null)
                waypointList.Add(waypoint);
        }
    }
}
