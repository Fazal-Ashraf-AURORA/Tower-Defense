using UnityEngine;

public class TowerCrossBow : Tower
{
    private CrossbowVisuals visuals;

    [Header("Crossbow Details")]
    [SerializeField] private Transform firePoint;

    protected override void Awake()
    {
        base.Awake();

        visuals = GetComponent<CrossbowVisuals>();
    }

    protected override void Attack()
    {
        Vector3 directionToEnemy = DirectionToEnemyFrom(firePoint);

        if (Physics.Raycast(firePoint.position, directionToEnemy, out RaycastHit hitInfo, Mathf.Infinity))
        {
            towerHead.forward = directionToEnemy;

            Debug.Log(hitInfo.collider.gameObject.name + "was attacked");
            Debug.DrawLine(firePoint.position, hitInfo.point);

            visuals.PlayAttackVFX(firePoint.position, hitInfo.point);
            visuals.PlayReloadVFX(attackCooldown);
        }
    }
}
