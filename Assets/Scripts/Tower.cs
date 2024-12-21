using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform towerHead;
    public Transform enemy;

    public float attackRange = 3.0f;


    private void Update()
    {
        if(Vector3.Distance(enemy.position, towerHead.position) < attackRange)
        {
            towerHead.LookAt(enemy);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
