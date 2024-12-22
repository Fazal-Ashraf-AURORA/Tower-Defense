using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private EnemyCreator enemyCreator;

    [SerializeField] private Transform towerHead;
    private Transform enemy;

    [Header("Attack Details")]
    [SerializeField] private float attackRange = 3.0f;  // attack range of the tower
    [SerializeField] private float attackCooldown = 2.0f;//attack cooldown(interval between each bullet)
    private float lastAttackTime;

    [Header("Bullet Details")]
    [SerializeField] private GameObject bulletPrefab; // bullet prefab reference
    [SerializeField] private float bulletSpeed = 3.0f; // speed of the bullet

    private void Awake()
    {
        enemyCreator = FindAnyObjectByType<EnemyCreator>();
    }

    private void Update()
    {
        if (enemy == null)
        {
          enemy = FindClosestEnemy();
          return;
        }
        
        if (Vector3.Distance(enemy.position, towerHead.position) < attackRange)
        {
          towerHead.LookAt(enemy);

          if(ReadyToAttack())
          {
            CreateBullet();
          }
        }
        
    }

    Transform FindClosestEnemy()
    {
        float closestDistance = float.MaxValue;
        Transform closestEnemy = null;

        foreach (Transform enemy in enemyCreator.EnemyList())
        {
            float distance = Vector3.Distance(enemy.position, transform.position);

            if (distance < closestDistance && distance <= attackRange )
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if( closestEnemy != null )
        {
            enemyCreator.EnemyList().Remove(closestEnemy);
        }

        return closestEnemy;
    }


    void FindRandomEnemy()
    {
        if(enemyCreator.EnemyList().Count <= 0)
        {
          return;
        }
        int randomEnemyIndex = Random.Range(0, enemyCreator.EnemyList().Count);
        enemy = enemyCreator.EnemyList()[randomEnemyIndex];
        enemyCreator.EnemyList().RemoveAt(randomEnemyIndex);
    }

    private bool ReadyToAttack()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            return true;
        }
        return false;
    }

    private void CreateBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, towerHead.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody>().velocity = (enemy.position - towerHead.position).normalized * bulletSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
