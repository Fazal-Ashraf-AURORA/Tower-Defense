using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Enemy currentEnemy;

    [SerializeField] protected float attackCooldown = 1.0f;
    protected float lastTimeAttacked;

    [Header("Tower Setup")]
    [SerializeField] protected EnemyType enemyPriorityType = EnemyType.None;
    [SerializeField] protected Transform towerHead;
    [SerializeField] protected float rotationSpeed = 10.0f;
    private bool canRotate;

    [SerializeField] protected float attackRange = 2.5f;
    [SerializeField] protected LayerMask whatIsEnemy;

    protected virtual void Awake()
    {
        EnableRotation(true);
    }

    protected virtual void Update()
    {
        if (currentEnemy == null)
        {
            currentEnemy = FindEnemyWithInRange();
            return;
        }

        if (CanAttack())
            Attack();

        if (Vector3.Distance(currentEnemy.CenterPoint(), towerHead.position) > attackRange)
        {
            currentEnemy = null;
        }

        RotateTowardsEnemy();
    }

    protected virtual void Attack()
    {
        Debug.Log("AttackedEnemy at" + Time.time);
    }

    protected bool CanAttack()
    {
        if (currentEnemy == null)
            return false;

        if (Time.time > lastTimeAttacked + attackCooldown)
        {
            lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }

    protected Enemy FindEnemyWithInRange()
    {
        List<Enemy> priorityTargets = new List<Enemy>();
        List<Enemy> possibleTargets = new List<Enemy>();
        Collider[] enemiesAround = Physics.OverlapSphere(transform.position, attackRange, whatIsEnemy);

        foreach (var enemy in enemiesAround)
        {
            Enemy newEnemy = enemy.GetComponent<Enemy>();
            EnemyType newEnemyType = newEnemy.GetEnemyType();

            if(newEnemyType == enemyPriorityType)
                priorityTargets.Add(newEnemy);
            else
            possibleTargets.Add(newEnemy);
        }

        if(priorityTargets.Count > 0)
            return GetMostAdvancedEnemy(priorityTargets);

        if(possibleTargets .Count > 0)
            return GetMostAdvancedEnemy(possibleTargets);

        return null;
    }

    private Enemy GetMostAdvancedEnemy(List<Enemy> targets)
    {
        Enemy mostAdvancedEnemy = null;
        float minRemainingDistance = float.MaxValue;

        foreach (Enemy enemy in targets)
        {
            float remainingDistance = enemy.DistanceToFinishLine();

            if (remainingDistance < minRemainingDistance)
            {
                minRemainingDistance = remainingDistance;
                mostAdvancedEnemy = enemy;
            }
        }
        return mostAdvancedEnemy;
    }

    public void EnableRotation(bool enable)
    {
        canRotate = enable;
    }

    protected virtual void RotateTowardsEnemy()
    {
        if (canRotate == false) return;

        if (currentEnemy == null)
        { return; }

        Vector3 directionToEnemy = DirectionToEnemyFrom(towerHead);

        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);

        Vector3 rotation = Quaternion.Lerp(towerHead.rotation, lookRotation, rotationSpeed).eulerAngles;

        towerHead.rotation = Quaternion.Euler(rotation);
    }

    protected Vector3 DirectionToEnemyFrom(Transform startPoint)
    {
        return (currentEnemy.CenterPoint() - startPoint.position).normalized;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
