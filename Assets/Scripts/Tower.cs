using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform towerHead;
    private Transform enemy;

    public List<Transform> enemyList;

    [Header("Attack Details")]
    public float attackRange = 3.0f;  // attack range of the tower
    public float attackCooldown = 2.0f;//attack cooldown(interval between each bullet)
    private float lastAttackTime;

    [Header("Bullet Details")]
    public GameObject bulletPrefab; // bullet prefab reference
    public float bulletSpeed = 3.0f; // speed of the bullet

    private void Update()
    {
        if (enemy == null)
        {
          FindRandomEnemy();
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


    void FindRandomEnemy()
    {
        if(enemyList.Count <= 0)
        {
          return;
        }
        int randomEnemyIndex = Random.Range(0, enemyList.Count);
        enemy = enemyList[randomEnemyIndex];
        enemyList.RemoveAt(randomEnemyIndex);
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
