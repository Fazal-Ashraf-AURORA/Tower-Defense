using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPortal : MonoBehaviour
{
    [Header("Spawn Details")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] private float spawnCooldown;
    private float spawnTimer;

    public List<GameObject> enemiesToCreate;

    private void Update()
    {
        //spawnTimer -= Time.deltaTime;

        //if (spawnTimer <= 0 && enemiesToCreate.Count > 0)
        //{
        //    CreateEnemy();
        //    spawnTimer = spawnCooldown;
        //}
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

    public List<GameObject> GetEnemyList() => enemiesToCreate;
}
