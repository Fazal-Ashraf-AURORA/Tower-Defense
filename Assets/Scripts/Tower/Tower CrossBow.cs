using UnityEngine;

public class TowerCrossBow : Tower
{
    private CrossbowVisuals visuals;

    [Header("Crossbow Details")]
    [SerializeField] private Transform firePoint;
    [SerializeField]private int damage;

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

            visuals.PlayAttackVFX(firePoint.position, hitInfo.point);
            visuals.PlayReloadVFX(attackCooldown);

            IDamagable damagable = hitInfo.transform.GetComponent<IDamagable>();

            if (damagable != null)
            {
                damagable.TakeDamage(damage);
            }
        }
    }
}
