using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform currentEnemy;

    [Header("Tower Setup")]
    [SerializeField] private Transform towerHead;
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        if(currentEnemy == null) return;
        RotateTowardsEnemy();
    }

    private void RotateTowardsEnemy()
    {
        Vector3 directionToEnemy = currentEnemy.position - towerHead.position;

        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);

        Vector3 rotation = Quaternion.Lerp(towerHead.rotation, lookRotation, rotationSpeed).eulerAngles;

        towerHead.rotation = Quaternion.Euler(rotation);
    }
}
