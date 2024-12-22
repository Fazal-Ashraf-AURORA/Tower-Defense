using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] private GameObject enemyprefab;
    [SerializeField] private int enemiesToSpawn = 4;


   private List<Transform> enemyList = new List<Transform>();


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

   public List<Transform> EnemyList() => enemyList;
}