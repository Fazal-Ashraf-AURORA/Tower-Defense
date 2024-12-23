using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform currentEnemy;

    [Header("Tower Setup")]
    [SerializeField] private Transform towerHead;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask whatIsEnemy;


    private void Start()
    {
        
    }


    private void Update()
    {
        if(currentEnemy == null)
        {
            currentEnemy = FindRandomEnemyWithInRange();
            return;
        }

        if(Vector3.Distance(currentEnemy.position, towerHead.position) > attackRange)
        {
            currentEnemy = null;
        }

        RotateTowardsEnemy();
    }
    private Transform FindRandomEnemyWithInRange()
    {
        List<Transform> possibleTargets = new List<Transform>();
        Collider[] enemiesAround = Physics.OverlapSphere(transform.position, attackRange, whatIsEnemy);

        foreach (var enemy in enemiesAround)
        {
            possibleTargets.Add(enemy.transform);
        }

        int randomIndex = Random.Range(0, possibleTargets.Count);

        if(possibleTargets.Count <= 0)
        {
            return null;
        }

        return possibleTargets[randomIndex];
    }

    private void RotateTowardsEnemy()
    {
        if (currentEnemy == null)
            { return; }

        Vector3 directionToEnemy = currentEnemy.position - towerHead.position;

        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);

        Vector3 rotation = Quaternion.Lerp(towerHead.rotation, lookRotation, rotationSpeed).eulerAngles;

        towerHead.rotation = Quaternion.Euler(rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
