using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    [Header("Enemy Prefabs")]
    [SerializeField] GameObject enemyBasicPrefab;
    [SerializeField] private float spawnCooldown;
    private float spawnTimer;

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if(spawnTimer <= 0)
        {
            CreateEnemy();
            spawnTimer = spawnCooldown;
        }
    }


    private void CreateEnemy()
    {
        GameObject enemy = Instantiate(enemyBasicPrefab, spawnPoint.position, Quaternion.identity);
    }

}
