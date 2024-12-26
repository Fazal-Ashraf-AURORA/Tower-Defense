using UnityEngine;

public class TowerCrossBow : Tower
{
    private CrossbowVisuals visuals;

    [Header("Crossbow Details")]
    [SerializeField] private Transform firePoint;
    [SerializeField]private int damage;

    // Define the layer mask
    int layerMask ; 

    protected override void Awake()
    {
        base.Awake();

        visuals = GetComponent<CrossbowVisuals>();
        layerMask = LayerMask.GetMask(whatIsEnemy.ToString());// Replace "EnemyLayer" with the name of your desired layer
    }

    protected override void Attack()
    {
        Vector3 directionToEnemy = DirectionToEnemyFrom(firePoint);

        if (Physics.Raycast(firePoint.position, directionToEnemy, out RaycastHit hitInfo, Mathf.Infinity/*, layerMask*/))
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
