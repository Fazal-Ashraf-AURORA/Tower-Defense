using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public GameObject enemyprefab;
    public int enemiesToSpawn = 4;


    public List<Transform> enemyList;


    private void Start()
    {
        CreateEnemiesAtRandomPosition();
    }

    private void CreateEnemiesAtRandomPosition()
    {

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            float randomX = Random.Range(-4, 4);
            float randomZ = Random.Range(-4, 4);

            Vector3 spawnPosition = new Vector3(randomX, 0.25f, randomZ);
            GameObject enemy = Instantiate(enemyprefab, spawnPosition, Quaternion.identity);

            enemyList.Add(enemy.transform);
        }
    }
}