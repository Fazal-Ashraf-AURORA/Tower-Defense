using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveDetails
{
    public int basicEnemy;
    public int fastEnemy;
}

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private WaveDetails currentWave;

    [Header("Spawn Details")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] private float spawnCooldown;
    private float spawnTimer;

    private List<GameObject> enemiesToCreate;

    [Header("Enemy Prefabs")]
    [SerializeField] GameObject enemyBasicPrefab;
    [SerializeField] GameObject enemyFastPrefab;

    private void Start()
    {
        enemiesToCreate = NewEnemyWave();
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0 && enemiesToCreate.Count > 0)
        {
            CreateEnemy();
            spawnTimer = spawnCooldown;
        }
    }


    private void CreateEnemy()
    {
        GameObject randomEnemy = GetRandomEnemy();
        GameObject enemy = Instantiate(randomEnemy, spawnPoint.position, Quaternion.identity);
    }

    private GameObject GetRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemiesToCreate.Count);
        GameObject choosenEnemy = enemiesToCreate[randomIndex];

        enemiesToCreate.Remove(choosenEnemy);

        return choosenEnemy;
    }

    private List<GameObject> NewEnemyWave()
    {
        List<GameObject> newEnemyList = new List<GameObject>();

        for (int i = 0; i < currentWave.basicEnemy; i++)
        {
            newEnemyList.Add(enemyBasicPrefab);
        }

        for (int i = 0; i < currentWave.fastEnemy; i++)
        {
            newEnemyList.Add(enemyFastPrefab);
        }

        return newEnemyList;
    }

}
